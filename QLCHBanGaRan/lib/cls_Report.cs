using System;
using System.Data;
using System.Data.SqlClient;

namespace QLCHBanGaRan.lib
{
    class cls_Report
    {
        public static DataTable _getProfileEmployees(int nhanvienID)
        {
            string query = @"SELECT a.TenNV, a.NgaySinh, a.CMND, a.DiaChi, c.TenChucDanh, c.LuongCoBan AS TienLuong
                            FROM NhanVien a
                            INNER JOIN ChucDanh c ON a.MaChucDanh = c.MaChucDanh
                            WHERE a.MaNV = @MaNV";
            return cls_DatabaseManager.TableRead(query, new SqlParameter[] { new SqlParameter("@MaNV", nhanvienID) });
        }

        public static DataTable _getNhanVien()
        {
            string query = "SELECT MaNV AS NhanVienID, TenNV FROM NhanVien";
            return cls_DatabaseManager.TableRead(query);
        }

        public static DataTable _getLoaiSP()
        {
            // Giả định không có bảng LoaiSP trong CSDL hiện tại, có thể thay bằng DoAn hoặc DoUong
            string query = "SELECT MaMon AS LoaiSPID, TenMon AS TenLoaiSP FROM DoAn UNION SELECT MaDoUong AS LoaiSPID, TenDoUong AS TenLoaiSP FROM DoUong";
            return cls_DatabaseManager.TableRead(query);
        }

        public static DataTable _searchInvoice(string tenNV)
        {
            string query = @"SELECT a.MaHD, COALESCE(a.TenKhachHang, N'Không có') AS TenKhach, b.TenNV, a.NgayLapHD, a.TongTien
                    FROM HoaDon a
                    INNER JOIN NhanVien b ON a.MaNV = b.MaNV
                    WHERE b.TenNV LIKE @Keyword
                    AND a.IsDeleted = 0"; // Giả định có cột IsDeleted để lọc hóa đơn chưa xóa
            return cls_DatabaseManager.TableRead(query, new SqlParameter[] { new SqlParameter("@Keyword", $"%{tenNV}%") });
        }
    }
}