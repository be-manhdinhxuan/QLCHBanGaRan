using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Data.SqlClient;
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

        public static bool AddEmployee(string maNV, string tenNV, DateTime ngaySinh, bool gioiTinh, string diaChi, string sdt, string email, string cmnd, int trangThai)
        {
            try
            {
                string query = "EXEC sp_ThemNhanVien @MaNV, @TenNV, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @Email, @CMND, @TrangThai";
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
                    new SqlParameter("@TrangThai", SqlDbType.Int) { Value = trangThai }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DataTable ShowEmployees()
        {
            string query = "SELECT MaNV, TenNV, NgaySinh, GioiTinh, DiaChi, SDT, Email, CMND, TrangThai FROM NhanVien";
            return cls_DatabaseManager.TableRead(query);
        }

        public static DataTable GetGender()
        {
            string query = "SELECT * FROM GioiTinh";
            return cls_DatabaseManager.TableRead(query);
        }

        public static bool DeleteEmployee(string maNV)
        {
            try
            {
                if (CheckInHoSoNhanVien(maNV) && CheckInNhanVienBoPhan(maNV) && CheckInNhanVienChucDanh(maNV) && CheckInNguoiDung(maNV) && CheckInHoaDon(maNV))
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

        public static bool CheckInHoSoNhanVien(string maNV)
        {
            string query = "SELECT MaNV FROM HoSoNhanVien WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV }
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            return dt.Rows.Count == 0;
        }

        public static bool CheckInNhanVienBoPhan(string maNV)
        {
            string query = "SELECT MaNV FROM NhanVienBoPhan WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV }
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            return dt.Rows.Count == 0;
        }

        public static bool CheckInNhanVienChucDanh(string maNV)
        {
            string query = "SELECT MaNV FROM NhanVienChucDanh WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV }
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            return dt.Rows.Count == 0;
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
            string query = "SELECT MaNV FROM NhanVien";
            return cls_DatabaseManager.TableRead(query);
        }

        public static bool UpdateEmployee(string maNVCu, string maNV, string tenNV, DateTime ngaySinh, bool gioiTinh, string diaChi, string sdt, string email, string cmnd, int trangThai)
        {
            try
            {
                string query = "EXEC sp_CapNhatNhanVien @MaNVCu, @MaNV, @TenNV, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @Email, @CMND, @TrangThai";
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
                    new SqlParameter("@TrangThai", SqlDbType.Int) { Value = trangThai }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}