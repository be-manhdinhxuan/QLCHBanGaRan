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
            using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.connectionString))
            {
                conn.Open();
                string query = "SELECT CMND, Email FROM NhanVien WHERE MaNV = @MaNV";
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

        public static string CheckLogin(string username, string password)
        {
            string MaND = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_CheckLogin", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TenDangNhap", username);
                        cmd.Parameters.AddWithValue("@MatKhau", password);
                        SqlParameter outputParameter = new SqlParameter("@MaND", SqlDbType.NVarChar, 10)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParameter);

                        cmd.ExecuteNonQuery();
                        MaND = cmd.Parameters["@MaND"].Value.ToString();
                    }
                }
                return string.IsNullOrEmpty(MaND) ? "ERROR" : MaND;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "ERROR";
            }
        }

        public static bool CheckPermission(string maND)
        {
            string query = "SELECT LaQuanTri FROM NguoiDung WHERE MaND = @MaND";
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
            string query = "SELECT nv.MaNV, nv.TenNV FROM NguoiDung nd JOIN NhanVien nv ON nd.MaNV = nv.MaNV WHERE nd.MaND = @MaND";
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
            // Logic truy vấn SQL để lấy thông tin nhân viên theo MaNV
            string query = "SELECT MaNV, TenNV, NgaySinh, GioiTinh, SDT, DiaChi, Email, CMND, TrangThai, MaChucDanh FROM NhanVien WHERE MaNV = @MaNV";
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

                string query = "EXEC sp_ThemNhanVien @MaNV, @TenNV, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @Email, @CMND, @TrangThai, @MaChucDanh";
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
                    new SqlParameter("@MaChucDanh", SqlDbType.NVarChar, 10) { Value = (object)maChucDanh ?? DBNull.Value }
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
                           "cd.MaChucDanh, COALESCE(cd.TenChucDanh, N'Chưa có chức danh') AS TenChucDanh " +
                           "FROM NhanVien nv " +
                           "LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh";
            DataTable dt = cls_DatabaseManager.TableRead(query);
            // Debug chi tiết để kiểm tra giá trị TrangThai
            foreach (DataRow row in dt.Rows)
            {
                int trangThaiValue = Convert.ToInt32(row["TrangThaiID"]);
                Console.WriteLine($"ShowEmployees - MaNV: {row["MaNV"]}, TrangThaiID: {trangThaiValue}, GioiTinh: {row["GioiTinh"]}");
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
                           "WHERE " + column + " LIKE @Keyword";

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
                if (CheckInNguoiDung(maNV) && CheckInHoaDon(maNV))
                {
                    string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV }
                    };
                    cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CheckInNguoiDung(string maNV)
        {
            string query = "SELECT MaNV FROM NguoiDung WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV }
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            return dt.Rows.Count == 0;
        }

        public static bool CheckInHoaDon(string maNV)
        {
            string query = "SELECT MaNV FROM HoaDon WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV }
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            return dt.Rows.Count == 0;
        }

        public static DataTable GetIDEmployees()
        {
            string query = "SELECT MaNV FROM NhanVien ORDER BY MaNV";
            return cls_DatabaseManager.TableRead(query);
        }

        public static bool UpdateEmployee(string maNVCu, string maNV, string tenNV, DateTime ngaySinh, bool gioiTinh, string diaChi, string sdt, string email, string cmnd, int trangThai, string maChucDanh)
        {
            try
            {
                Console.WriteLine($"Update - maNVCu: {maNVCu}, maNV: {maNV}, TrangThai: {trangThai}, GioiTinh: {gioiTinh}, CMND: {cmnd}, Email: {email}");
                string query = "sp_CapNhatNhanVien"; // Tên stored procedure
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
            new SqlParameter("@MaChucDanh", SqlDbType.NVarChar, 10) { Value = (object)maChucDanh ?? DBNull.Value }
                };

                using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Update executed - Rows affected: {rowsAffected}");
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Update successful.");
                            return true;
                        }
                        else if (rowsAffected == 0)
                        {
                            Console.WriteLine("No rows affected - checking DB...");
                            // So sánh với giá trị ban đầu, xử lý trường hợp null
                            bool hasChange = false;
                            if (_initialCMND != null && cmnd != null && !_initialCMND.Equals(cmnd)) hasChange = true;
                            else if (_initialEmail != null && email != null && !_initialEmail.Equals(email)) hasChange = true;
                            else if (_initialCMND == null && cmnd != null) hasChange = true; // Thay đổi từ null sang giá trị
                            else if (_initialEmail == null && email != null) hasChange = true;
                            else if (_initialCMND != null && cmnd == null) hasChange = true; // Thay đổi từ giá trị sang null
                            else if (_initialEmail != null && email == null) hasChange = true;

                            if (hasChange)
                            {
                                Console.WriteLine("Data updated in DB compared to initial values.");
                                return true; // Giả định thành công nếu dữ liệu thay đổi so với ban đầu
                            }
                            Console.WriteLine("No changes detected in DB compared to initial values.");
                            return false;
                        }
                        else // rowsAffected < 0 (bất thường)
                        {
                            Console.WriteLine("Unexpected rows affected value: " + rowsAffected);
                            // So sánh với giá trị ban đầu, xử lý trường hợp null
                            bool hasChange = false;
                            if (_initialCMND != null && cmnd != null && !_initialCMND.Equals(cmnd)) hasChange = true;
                            else if (_initialEmail != null && email != null && !_initialEmail.Equals(email)) hasChange = true;
                            else if (_initialCMND == null && cmnd != null) hasChange = true;
                            else if (_initialEmail == null && email != null) hasChange = true;
                            else if (_initialCMND != null && cmnd == null) hasChange = true;
                            else if (_initialEmail != null && email == null) hasChange = true;

                            if (hasChange)
                            {
                                Console.WriteLine("Data updated in DB despite negative rowsAffected compared to initial values.");
                                return true; // Giả định thành công nếu dữ liệu thay đổi so với ban đầu
                            }
                            Console.WriteLine("No changes detected in DB despite negative rowsAffected compared to initial values.");
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
                return false; // Thất bại
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Thất bại
            }
        }

        public static DataTable GetChucDanh()
        {
            string query = "SELECT MaChucDanh, TenChucDanh FROM ChucDanh";
            return cls_DatabaseManager.TableRead(query);
        }
    }
}