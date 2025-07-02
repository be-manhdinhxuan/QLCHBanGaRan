using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QLCHBanGaRan.lib;

namespace QLCHBanGaRan.lib
{
    class cls_EmployeeTitleManagement
    {
        // Lấy danh sách nhân viên - chức danh
        public static DataTable GetEmployeeTitles()
        {
            string query = "SELECT nvcd.MaNV, nv.TenNV, cd.TenChucDanh " +
                           "FROM NhanVienChucDanh nvcd " +
                           "JOIN NhanVien nv ON nvcd.MaNV = nv.MaNV " +
                           "JOIN ChucDanh cd ON nvcd.MaChucDanh = cd.MaChucDanh";
            return cls_DatabaseManager.TableRead(query);
        }

        // Lấy danh sách chức danh
        public static DataTable GetTitles()
        {
            string query = "SELECT MaChucDanh, TenChucDanh FROM ChucDanh";
            return cls_DatabaseManager.TableRead(query);
        }

        // Thêm nhân viên - chức danh
        public static bool InsertEmployeeTitle(string maNV, int maChucDanh)
        {
            try
            {
                string query = "INSERT INTO NhanVienChucDanh (MaNV, MaChucDanh) VALUES (@MaNV, @MaChucDanh)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV },
                    new SqlParameter("@MaChucDanh", SqlDbType.Int) { Value = maChucDanh }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Cập nhật nhân viên - chức danh
        public static bool UpdateEmployeeTitle(string maNV, int maChucDanh)
        {
            try
            {
                string query = "UPDATE NhanVienChucDanh SET MaChucDanh = @MaChucDanh WHERE MaNV = @MaNV";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV },
                    new SqlParameter("@MaChucDanh", SqlDbType.Int) { Value = maChucDanh }
                };
                int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Xóa nhân viên - chức danh
        public static bool DeleteEmployeeTitle(string maNV)
        {
            try
            {
                string query = "DELETE FROM NhanVienChucDanh WHERE MaNV = @MaNV";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV }
                };
                int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}