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

        public static bool AddEmployee(string maNV, string tenNV, DateTime ngaySinh, bool gioiTinh, string diaChi, string sdt, string email, string cmnd, int trangThai, string maChucDanh)
        {
            try
            {
                // Kiểm tra CMND trước khi gửi đến stored procedure
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
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    string errorMessage = "Dữ liệu đã tồn tại: ";
                    if (ex.Message.Contains("IX_NhanVien_Email")) errorMessage += "Email đã được sử dụng.";
                    else if (ex.Message.Contains("IX_NhanVien_SDT")) errorMessage += "Số điện thoại đã được sử dụng.";
                    else if (ex.Message.Contains("IX_NhanVien_CMND")) errorMessage += "Số CMND đã được sử dụng.";
                    else errorMessage += "Dữ liệu bị trùng (kiểm tra Email, SDT, hoặc CMND).";
                    MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Number == 50006) // Lỗi từ stored procedure về độ dài CMND
                {
                    MessageBox.Show("Số CMND phải có đúng 9 số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable ShowEmployees()
        {
            string query = "SELECT nv.MaNV, nv.TenNV, nv.NgaySinh, " +
                           "CASE nv.GioiTinh WHEN 0 THEN N'Nam' WHEN 1 THEN N'Nữ' END AS GioiTinh, " +
                           "nv.DiaChi, nv.SDT, nv.Email, nv.CMND, nv.TrangThai, " +
                           "cd.MaChucDanh, COALESCE(cd.TenChucDanh, N'Chưa có chức danh') AS TenChucDanh " +
                           "FROM NhanVien nv " +
                           "LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh";
            DataTable dt = cls_DatabaseManager.TableRead(query);
            // Debug: Kiểm tra dữ liệu
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"MaNV: {row["MaNV"]}, MaChucDanh: {row["MaChucDanh"]}, TenChucDanh: {row["TenChucDanh"]}, GioiTinh: {row["GioiTinh"]}");
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
                Console.WriteLine($"Update - maNVCu: {maNVCu}, maNV: {maNV}, GioiTinh: {gioiTinh}");
                string query = "EXEC sp_CapNhatNhanVien @MaNVCu, @MaNV, @TenNV, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @Email, @CMND, @TrangThai, @MaChucDanh";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@MaNVCu", SqlDbType.NVarChar, 10) { Value = maNVCu },
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
                MessageBox.Show("Lỗi khi cập nhật nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static DataTable GetChucDanh()
        {
            string query = "SELECT MaChucDanh, TenChucDanh FROM ChucDanh";
            return cls_DatabaseManager.TableRead(query);
        }
    }
}