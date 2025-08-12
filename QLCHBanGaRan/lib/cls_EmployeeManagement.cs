using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QLCHBanGaRan.lib
{
    class cls_EmployeeManagement
    {
        // Biến tĩnh để lưu giá trị ban đầu khi chọn nhân viên
        private static string _initialCMND = null;
        private static string _initialEmail = null;

        public static void SetInitialValues(string maNV)
        {
            using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.ConnectionString))
            {
                conn.Open();
                string query = "SELECT CMND, Email FROM NhanVien WHERE MaNV = @MaNV AND IsDeleted = 0";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _initialCMND = reader["CMND"]?.ToString();
                            _initialEmail = reader["Email"]?.ToString();
                        }
                    }
                }
            }
        }

        public static string CheckLogin(string username, string encryptedPassword)
        {
            string MaND = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT MaND 
                FROM NguoiDung 
                WHERE TenDangNhap = @TenDangNhap COLLATE Latin1_General_CS_AS 
                AND MatKhau = @MatKhau 
                AND (IsDeleted = 0 OR IsDeleted IS NULL)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", username);
                        cmd.Parameters.AddWithValue("@MatKhau", encryptedPassword);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MaND = reader["MaND"].ToString();
                            }
                        }
                    }
                }
                return string.IsNullOrEmpty(MaND) ? "ERROR" : MaND;
            }
            catch (SqlException ex)
            {
                // Ném lại exception để xử lý ở tầng trên
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool CheckPermission(string maND)
        {
            string query = "SELECT LaQuanTri FROM NguoiDung WHERE MaND = @MaND AND IsDeleted = 0";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaND", SqlDbType.NVarChar, 10) { Value = maND }
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            return dt.Rows.Count > 0 && Convert.ToBoolean(dt.Rows[0]["LaQuanTri"]);
        }

        public static string[] GetEmployeeInfo(string maND)
        {
            string[] info = new string[2];
            string query = "SELECT nv.MaNV, nv.TenNV FROM NguoiDung nd JOIN NhanVien nv ON nd.MaNV = nv.MaNV WHERE nd.MaND = @MaND AND nd.IsDeleted = 0 AND nv.IsDeleted = 0";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaND", SqlDbType.NVarChar, 10) { Value = maND }
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            if (dt.Rows.Count > 0)
            {
                info[0] = dt.Rows[0]["MaNV"].ToString();
                info[1] = dt.Rows[0]["TenNV"].ToString();
            }
            return info;
        }

        public static DataTable GetEmployeeByMaNV(string maNV)
        {
            string query = "SELECT MaNV, TenNV, NgaySinh, GioiTinh, SDT, DiaChi, Email, CMND, TrangThai, MaChucDanh, IsDeleted FROM NhanVien WHERE MaNV = @MaNV AND IsDeleted = 0";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV }
            };
            return cls_DatabaseManager.TableRead(query, parameters);
        }

        public static bool AddEmployee(string maNV, string tenNV, DateTime ngaySinh, bool gioiTinh, string diaChi, string sdt, string email, string cmnd, int trangThai, string maChucDanh)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cmnd))
                {
                    MessageBox.Show("Số CMND không được trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (cmnd.Length != 9)
                {
                    MessageBox.Show("Số CMND phải có đúng 9 số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (!Regex.IsMatch(cmnd, "^[0-9]+$"))
                {
                    MessageBox.Show("Số CMND phải là số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Kiểm tra độ tuổi trên 18
                int age = DateTime.Now.Year - ngaySinh.Year;
                if (ngaySinh > DateTime.Now.AddYears(-age)) age--;
                if (age < 18)
                {
                    MessageBox.Show("Nhân viên phải trên 18 tuổi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string query = "EXEC sp_ThemNhanVien @MaNV, @TenNV, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @Email, @CMND, @TrangThai, @MaChucDanh, @IsDeleted";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV },
                    new SqlParameter("@TenNV", SqlDbType.NVarChar, 50) { Value = tenNV },
                    new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = ngaySinh },
                    new SqlParameter("@GioiTinh", SqlDbType.Bit) { Value = gioiTinh },
                    new SqlParameter("@DiaChi", SqlDbType.NVarChar, 255) { Value = (object)diaChi ?? DBNull.Value },
                    new SqlParameter("@SDT", SqlDbType.NVarChar, 10) { Value = (object)sdt ?? DBNull.Value },
                    new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = (object)email ?? DBNull.Value },
                    new SqlParameter("@CMND", SqlDbType.NVarChar, 12) { Value = (object)cmnd ?? DBNull.Value },
                    new SqlParameter("@TrangThai", SqlDbType.Int) { Value = trangThai },
                    new SqlParameter("@MaChucDanh", SqlDbType.NVarChar, 10) { Value = (object)maChucDanh ?? DBNull.Value },
                    new SqlParameter("@IsDeleted", SqlDbType.Bit) { Value = 0 } // Mặc định IsDeleted = 0 khi thêm
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (SqlException ex)
            {
                string msg = ex.Message.ToLower();
                if (msg.Contains("unique key constraint") && msg.Contains("cmnd"))
                {
                    MessageBox.Show("Số CMND đã được sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if ((ex.Number == 2627 || ex.Number == 2601) && msg.Contains("unique"))
                {
                    string errorMessage = "Dữ liệu đã tồn tại: ";
                    if (msg.Contains("email")) errorMessage += "Email đã được sử dụng.";
                    else if (msg.Contains("sdt") || msg.Contains("dienthoai")) errorMessage += "Số điện thoại đã được sử dụng.";
                    else errorMessage += "Dữ liệu bị trùng (kiểm tra Email, SĐT, hoặc CMND).";
                    MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Number == 50006)
                {
                    MessageBox.Show("Số CMND phải có đúng 9 số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi thực thi truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToLower();
                if (msg.Contains("unique key constraint") && msg.Contains("cmnd"))
                {
                    MessageBox.Show("Số CMND đã được sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                MessageBox.Show("Lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable ShowEmployees()
        {
            string query = "SELECT nv.MaNV, nv.TenNV, nv.NgaySinh, " +
                           "CASE nv.GioiTinh WHEN 0 THEN N'Nam' WHEN 1 THEN N'Nữ' END AS GioiTinh, " +
                           "nv.DiaChi, nv.SDT, nv.Email, nv.CMND, nv.TrangThai AS TrangThaiID, " +
                           "nv.IsDeleted, " + // Thêm cột IsDeleted
                           "nv.MaChucDanh, " +
                           "CASE WHEN nv.IsDeletedCD = 1 THEN N'Chưa có chức danh' " +
                           "ELSE COALESCE(cd.TenChucDanh, N'Chưa có chức danh') END AS TenChucDanh " +
                           "FROM NhanVien nv " +
                           "LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh " +
                           "WHERE nv.IsDeleted = 0"; // Chỉ lấy nhân viên chưa xóa

            DataTable dt = cls_DatabaseManager.TableRead(query);

            foreach (DataRow row in dt.Rows)
            {
                int trangThaiValue = Convert.ToInt32(row["TrangThaiID"]);
                //Console.WriteLine($"ShowEmployees - MaNV: {row["MaNV"]}, TrangThaiID: {trangThaiValue}, GioiTinh: {row["GioiTinh"]}, IsDeleted: {row["IsDeleted"]}, IsDeletedCD: {row["IsDeletedCD"]}, TenChucDanh: {row["TenChucDanh"]}");
            }

            return dt;
        }

        public static DataTable SearchEmployees(string column, string keyword)
        {
            string query = "SELECT nv.MaNV, nv.TenNV, nv.NgaySinh, " +
                           "CASE nv.GioiTinh WHEN 0 THEN N'Nam' WHEN 1 THEN N'Nữ' END AS GioiTinh, " +
                           "nv.DiaChi, nv.SDT, nv.Email, nv.CMND, nv.TrangThai AS TrangThaiID, " +
                           "cd.MaChucDanh, COALESCE(cd.TenChucDanh, N'Chưa có chức danh') AS TenChucDanh " +
                           "FROM NhanVien nv " +
                           "LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh " +
                           "WHERE nv.IsDeleted = 0 AND " + column + " LIKE @Keyword";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", SqlDbType.NVarChar, 100) { Value = "%" + keyword + "%" }
            };

            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                int trangThaiValue = Convert.ToInt32(row["TrangThaiID"]);
                Console.WriteLine($"SearchEmployees - MaNV: {row["MaNV"]}, TrangThaiID: {trangThaiValue}, GioiTinh: {row["GioiTinh"]}");
            }
            return dt;
        }

        public static bool DeleteEmployee(string maNV)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        SqlParameter outputParam = new SqlParameter("@TotalRowsAffected", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);
                        Console.WriteLine($"Executing sp_DeleteEmployee for MaNV: {maNV}");
                        cmd.ExecuteNonQuery(); // Không cần gán rowsAffected
                        int totalRows = (int)outputParam.Value;
                        Console.WriteLine($"Delete executed - Total rows affected: {totalRows}");
                        return totalRows >= 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa nhân viên: {ex.Message} - StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        public static bool CheckInNguoiDung(string maNV)
        {
            string query = "SELECT COUNT(*) FROM NguoiDung WHERE MaNV = @MaNV AND (IsDeleted = 0 OR IsDeleted IS NULL)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV }
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            int count = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
            Console.WriteLine($"CheckInNguoiDung - MaNV: {maNV}, Count: {count}");
            return count > 0; // Trả về true nếu có bản ghi liên quan chưa xóa hoặc NULL
        }

        public static bool CheckInHoaDon(string maNV)
        {
            string query = "SELECT COUNT(*) FROM HoaDon WHERE MaNV = @MaNV AND (IsDeleted = 0 OR IsDeleted IS NULL)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV }
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            int count = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
            Console.WriteLine($"CheckInHoaDon - MaNV: {maNV}, Count: {count}");
            return count > 0; // Trả về true nếu có bản ghi liên quan chưa xóa hoặc NULL
        }

        public static DataTable GetIDEmployees()
        {
            string query = "SELECT MaNV FROM NhanVien WHERE IsDeleted = 0 ORDER BY MaNV";
            return cls_DatabaseManager.TableRead(query);
        }

        public static bool UpdateEmployee(string maNVCu, string maNV, string tenNV, DateTime ngaySinh, bool gioiTinh, string diaChi, string sdt, string email, string cmnd, int trangThai, string maChucDanh)
        {
            try
            {
                // Kiểm tra độ tuổi trên 18
                int age = DateTime.Now.Year - ngaySinh.Year;
                if (ngaySinh > DateTime.Now.AddYears(-age)) age--;
                if (age < 18)
                {
                    MessageBox.Show("Nhân viên phải trên 18 tuổi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                Console.WriteLine($"Update - maNVCu: {maNVCu}, maNV: {maNV}, TrangThai: {trangThai}, GioiTinh: {gioiTinh}, CMND: {cmnd}, Email: {email}");

                string query = "sp_CapNhatNhanVien";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaNVCu", SqlDbType.NVarChar, 10) { Value = maNVCu },
            new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV },
            new SqlParameter("@TenNV", SqlDbType.NVarChar, 50) { Value = (object)tenNV ?? DBNull.Value },
            new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = ngaySinh },
            new SqlParameter("@GioiTinh", SqlDbType.Bit) { Value = gioiTinh },
            new SqlParameter("@DiaChi", SqlDbType.NVarChar, 255) { Value = (object)diaChi ?? DBNull.Value },
            new SqlParameter("@SDT", SqlDbType.NVarChar, 10) { Value = (object)sdt ?? DBNull.Value },
            new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = (object)email ?? DBNull.Value },
            new SqlParameter("@CMND", SqlDbType.NVarChar, 12) { Value = (object)cmnd ?? DBNull.Value },
            new SqlParameter("@TrangThai", SqlDbType.Int) { Value = trangThai },
            new SqlParameter("@MaChucDanh", SqlDbType.NVarChar, 10) { Value = (object)maChucDanh ?? DBNull.Value },
            // Thêm output parameter
            new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output }
                };

                using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        bool success = (bool)cmd.Parameters["@Success"].Value; // Lấy giá trị từ output parameter

                        Console.WriteLine($"Update executed - Rows affected: {rowsAffected}, Success: {success}");

                        if (success)
                        {
                            Console.WriteLine("Update successful.");
                            return true;
                        }
                        else if (rowsAffected == 0)
                        {
                            Console.WriteLine("No rows affected - checking DB...");
                            bool hasChange = false;
                            if (_initialCMND != null && cmnd != null && !_initialCMND.Equals(cmnd)) hasChange = true;
                            else if (_initialEmail != null && email != null && !_initialEmail.Equals(email)) hasChange = true;
                            else if (_initialCMND == null && cmnd != null) hasChange = true;
                            else if (_initialEmail == null && email != null) hasChange = true;
                            else if (_initialCMND != null && cmnd == null) hasChange = true;
                            else if (_initialEmail != null && email == null) hasChange = true;

                            if (hasChange)
                            {
                                Console.WriteLine("Data updated in DB compared to initial values.");
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("No changes detected in DB compared to initial values.");
                                MessageBox.Show("Không có thay đổi nào được thực hiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Unexpected rows affected value: " + rowsAffected);
                            return false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error - Number: {ex.Number}, Message: {ex.Message}");
                string errorMessage = "Lỗi thực thi truy vấn: ";
                switch (ex.Number)
                {
                    case 50002: // CMND trùng
                        errorMessage = $"Số CMND '{cmnd}' đã được sử dụng bởi nhân viên khác.";
                        break;
                    case 50003: // Email trùng
                        errorMessage = $"Email '{email}' đã được sử dụng bởi nhân viên khác.";
                        break;
                    case 50004: // SDT trùng
                        errorMessage = $"Số điện thoại '{sdt}' đã được sử dụng bởi nhân viên khác.";
                        break;
                    case 50005: // Không có thay đổi
                        errorMessage = "Không có thay đổi nào để cập nhật.";
                        break;
                    case 50001: // Không tìm thấy nhân viên
                        errorMessage = ex.Message;
                        break;
                    default:
                        errorMessage += ex.Message;
                        break;
                }
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable GetChucDanh()
        {
            string query = "SELECT MaChucDanh, TenChucDanh FROM ChucDanh WHERE IsDeleted = 0";
            return cls_DatabaseManager.TableRead(query);
        }

        public static DataTable GetSalaryData(int thang, string chucDanhID = "-1")
        {
            string query = @"
        SELECT 
            nv.MaNV,
            nv.TenNV,
            cd.TenChucDanh AS ChucDanh,
            cd.LuongTheoGio,
            cd.ThuongChucDanh AS Thuong,
            nv.MaChucDanh, -- Thêm cột MaChucDanh vào SELECT
            COALESCE(SUM(CASE 
                WHEN cc.TrangThai = 2 AND cc.GioVao IS NOT NULL AND cc.GioRa IS NOT NULL 
                THEN DATEDIFF(HOUR, CAST(cc.GioVao AS TIME), CAST(cc.GioRa AS TIME))
                ELSE 0 
            END), 0) AS SoGioLam,
            (COALESCE(SUM(CASE 
                WHEN cc.TrangThai = 2 AND cc.GioVao IS NOT NULL AND cc.GioRa IS NOT NULL 
                THEN DATEDIFF(HOUR, CAST(cc.GioVao AS TIME), CAST(cc.GioRa AS TIME))
                ELSE 0 
            END), 0) * cd.LuongTheoGio) + cd.ThuongChucDanh - 
            (COALESCE(COUNT(CASE WHEN cc.TrangThai = 3 AND cc.LyDoNghi IS NULL THEN 1 END), 0) * 50000 + 
             COALESCE(COUNT(CASE WHEN cc.TrangThai = 1 THEN 1 END), 0) * 50000) AS TienLuong
        FROM NhanVien nv
        LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh AND cd.IsDeleted = 0
        LEFT JOIN ChamCongTheoNgay cc ON nv.MaNV = cc.MaNV AND cc.IsDeleted = 0 
            AND (YEAR(cc.NgayChamCong) * 100 + MONTH(cc.NgayChamCong)) = @Thang
        WHERE nv.IsDeleted = 0
        AND (nv.MaChucDanh IS NULL OR nv.MaChucDanh = @ChucDanhID OR @ChucDanhID = '-1')
        GROUP BY nv.MaNV, nv.TenNV, cd.TenChucDanh, cd.LuongTheoGio, cd.ThuongChucDanh, nv.MaChucDanh;";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Thang", SqlDbType.Int) { Value = thang },
        new SqlParameter("@ChucDanhID", SqlDbType.NVarChar, 10) { Value = chucDanhID }
            };
            return cls_DatabaseManager.TableRead(query, parameters);
        }

        public static string GenerateNewMaND()
        {
            string query = "SELECT MaND FROM NguoiDung WHERE MaND LIKE 'ND%' ORDER BY MaND DESC";
            DataTable dt = cls_DatabaseManager.TableRead(query);
            int newNumber = 1; // Giá trị mặc định nếu không có bản ghi

            if (dt.Rows.Count > 0)
            {
                string maxMaND = dt.Rows[0]["MaND"].ToString();
                string numberPart = maxMaND.Substring(2); // Lấy phần số (ví dụ: "002" từ "ND002")
                newNumber = int.Parse(numberPart) + 1; // Tăng số lên
            }

            return "ND" + newNumber.ToString("D3"); // Định dạng 3 chữ số (ND001, ND002, ...)
        }

        public static bool InsertNguoiDung(string maNV)
        {
            try
            {
                string defaultPassword = maNV; // Mật khẩu mặc định là MaNV
                Console.WriteLine($"InsertNguoiDung - MaNV: {maNV}, DefaultPassword: {defaultPassword}");
                string maND = GenerateNewMaND();
                string encryptedPassword = cls_Encryption.Encrypt(defaultPassword); // Mã hóa bằng AES

                string query = @"
            INSERT INTO NguoiDung (MaND, MaNV, TenDangNhap, MatKhau, LaQuanTri, IsDeleted) 
            VALUES (@MaND, @MaNV, @TenDangNhap, @MatKhau, 0, 0)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaND", SqlDbType.NVarChar, 10) { Value = maND },
                    new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV },
                    new SqlParameter("@TenDangNhap", SqlDbType.NVarChar, 50) { Value = maNV },
                    new SqlParameter("@MatKhau", SqlDbType.NVarChar, 256) { Value = encryptedPassword } // Cần độ dài lớn hơn do Base64
                };

                int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertNguoiDung Error - " + ex.Message);
                return false;
            }
        }

    }
}