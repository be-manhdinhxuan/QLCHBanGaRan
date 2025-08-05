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
            string query = "SELECT nv.MaNV AS MaNhanVien, nv.TenNV AS TenNhanVien, CONVERT(VARCHAR, nv.NgaySinh, 103) AS NgaySinh, nv.GioiTinh, nv.DiaChi, nv.SDT, nv.Email, nv.CMND, " +
                           "nv.MaChucDanh, " +
                           "CASE WHEN nv.IsDeletedCD = 1 THEN NULL ELSE cd.TenChucDanh END AS ChucDanh " +
                           "FROM NhanVien nv " +
                           "LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh " +
                           "WHERE nv.IsDeleted = 0";
            return cls_DatabaseManager.TableRead(query);
        }

        // Lấy danh sách chức danh
        public static DataTable GetTitles()
        {
            string query = "SELECT MaChucDanh, TenChucDanh FROM ChucDanh";
            return cls_DatabaseManager.TableRead(query);
        }

        // Thêm nhân viên - chức danh (cập nhật MaChucDanh trong NhanVien)
        public static bool InsertEmployeeTitle(string maNV, int maChucDanh)
        {
            try
            {
                string query = "UPDATE NhanVien SET MaChucDanh = @MaChucDanh WHERE MaNV = @MaNV";
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

        // Cập nhật nhân viên - chức danh (cập nhật MaChucDanh trong NhanVien)
        public static bool UpdateEmployeeTitle(string maNV, int maChucDanh)
        {
            try
            {
                string query = "UPDATE NhanVien SET MaChucDanh = @MaChucDanh WHERE MaNV = @MaNV";
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

        // Xóa nhân viên - chức danh (đặt MaChucDanh về NULL hoặc giá trị mặc định)
        public static bool DeleteEmployeeTitle(string maNV)
        {
            try
            {
                string query = "UPDATE NhanVien SET MaChucDanh = NULL WHERE MaNV = @MaNV";
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