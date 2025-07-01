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

        public static DataTable _searchInvoice(string id)
        {
            string query = @"SELECT a.MaHD, a.TenNV AS TenKhach, b.TenNV, a.NgayLapHD, a.TongTien
                            FROM HoaDon a
                            INNER JOIN NhanVien b ON a.MaNV = b.MaNV
                            WHERE a.MaHD LIKE @Keyword";
            return cls_DatabaseManager.TableRead(query, new SqlParameter[] { new SqlParameter("@Keyword", $"%{id}%") });
        }
    }
}