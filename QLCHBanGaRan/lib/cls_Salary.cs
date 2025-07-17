using System;
using System.Data;
using System.Data.SqlClient;

namespace QLCHBanGaRan.lib
{
    class cls_Salary
    {
        // Lấy danh sách chức danh
        public static DataTable GetChucDanh()
        {
            string query = "SELECT MaChucDanh, TenChucDanh, LuongCoBan, PhuCap FROM ChucDanh";
            return cls_DatabaseManager.TableRead(query);
        }

        // Thêm chức danh
        public static bool InsertChucDanh(string maChucDanh, string tenChucDanh, decimal luongCoBan, decimal phuCap)
        {
            try
            {
                string query = "INSERT INTO ChucDanh (MaChucDanh, TenChucDanh, LuongCoBan, PhuCap) VALUES (@MaChucDanh, @TenChucDanh, @LuongCoBan, @PhuCap)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaChucDanh", SqlDbType.NVarChar, 10) { Value = maChucDanh },
                    new SqlParameter("@TenChucDanh", SqlDbType.NVarChar, 100) { Value = tenChucDanh },
                    new SqlParameter("@LuongCoBan", SqlDbType.Decimal) { Value = luongCoBan },
                    new SqlParameter("@PhuCap", SqlDbType.Decimal) { Value = phuCap }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertChucDanh Error - " + ex.Message);
                return false;
            }
        }

        // Cập nhật chức danh
        public static bool UpdateChucDanh(string tenChucDanh, decimal luongCoBan, decimal phuCap, string maChucDanh)
        {
            try
            {
                string query = "UPDATE ChucDanh SET TenChucDanh = @TenChucDanh, LuongCoBan = @LuongCoBan, PhuCap = @PhuCap WHERE MaChucDanh = @MaChucDanh";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenChucDanh", SqlDbType.NVarChar, 100) { Value = tenChucDanh },
                    new SqlParameter("@LuongCoBan", SqlDbType.Decimal) { Value = luongCoBan },
                    new SqlParameter("@PhuCap", SqlDbType.Decimal) { Value = phuCap },
                    new SqlParameter("@MaChucDanh", SqlDbType.NVarChar, 10) { Value = maChucDanh }
                };
                int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateChucDanh Error - " + ex.Message);
                return false;
            }
        }

        // Kiểm tra xem chức danh có được sử dụng trong NhanVien không
        public static bool CheckChucDanh(string maChucDanh)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM NhanVien WHERE MaChucDanh = @MaChucDanh";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaChucDanh", SqlDbType.NVarChar, 10) { Value = maChucDanh }
                };
                DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
                return Convert.ToInt32(dt.Rows[0][0]) == 0; // Trả về true nếu không có bản ghi
            }
            catch (Exception ex)
            {
                Console.WriteLine("CheckChucDanh Error - " + ex.Message);
                return false; // Giả định lỗi thì không cho xóa
            }
        }

        // Lấy danh sách nhân viên liên quan đến chức danh để hiển thị
        public static DataTable GetNhanVienByMaChucDanh(string maChucDanh)
        {
            try
            {
                string query = "SELECT MaNV, TenNV FROM NhanVien WHERE MaChucDanh = @MaChucDanh";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaChucDanh", SqlDbType.NVarChar, 10) { Value = maChucDanh }
                };
                return cls_DatabaseManager.TableRead(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetNhanVienByMaChucDanh Error - " + ex.Message);
                return null;
            }
        }

        // Xóa chức danh (bao gồm xóa cascade nếu được yêu cầu)
        public static bool DeleteChucDanh(string maChucDanh, bool deleteRelatedNhanVien = false)
        {
            try
            {
                if (deleteRelatedNhanVien)
                {
                    // Xóa các bản ghi trong NhanVien liên quan trước
                    string deleteNhanVienQuery = "DELETE FROM NhanVien WHERE MaChucDanh = @MaChucDanh";
                    SqlParameter[] nhanVienParams = new SqlParameter[]
                    {
                        new SqlParameter("@MaChucDanh", SqlDbType.NVarChar, 10) { Value = maChucDanh }
                    };
                    cls_DatabaseManager.ExecuteNonQuery(deleteNhanVienQuery, nhanVienParams);
                }

                // Xóa bản ghi trong ChucDanh
                string query = "DELETE FROM ChucDanh WHERE MaChucDanh = @MaChucDanh";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaChucDanh", SqlDbType.NVarChar, 10) { Value = maChucDanh }
                };
                int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteChucDanh Error - " + ex.Message);
                return false;
            }
        }

        // Lấy danh sách chấm công theo tháng và bộ phận
        public static DataTable GetChamCong(string thang, int boPhanID)
        {
            try
            {
                string query = @"SELECT a.BoPhanID, a.NhanVienID, b.MaNV, b.TenNV, c.ChamCongID, 
                                c.NgayCongChuan, c.NgayDiLam, c.NgayNghi, c.NgayTinhLuong, c.GhiChu, c.TrangThai, c.Thang
                                FROM tbl_NhanVienBoPhan a
                                INNER JOIN tbl_NhanVien b ON a.NhanVienID = b.NhanVienID
                                LEFT JOIN tbl_ChamCong c ON c.BoPhanID = a.BoPhanID AND c.NhanVienID = a.NhanVienID AND c.Thang = @Thang
                                WHERE a.BoPhanID = @BoPhanID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Thang", SqlDbType.NVarChar, 10) { Value = thang },
                    new SqlParameter("@BoPhanID", SqlDbType.Int) { Value = boPhanID }
                };
                return cls_DatabaseManager.TableRead(query, parameters);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Thêm chấm công
        public static bool InsertChamCong(string thang, int ngayCongChuan, int boPhanID, int nhanVienID, int ngayDiLam, int ngayNghi, int ngayTinhLuong, string ghiChu)
        {
            try
            {
                string query = "INSERT INTO tbl_ChamCong (Thang, NgayCongChuan, BoPhanID, NhanVienID, NgayDiLam, NgayNghi, NgayTinhLuong, GhiChu, TrangThai) " +
                               "VALUES (@Thang, @NgayCongChuan, @BoPhanID, @NhanVienID, @NgayDiLam, @NgayNghi, @NgayTinhLuong, @GhiChu, 1)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Thang", SqlDbType.NVarChar, 10) { Value = thang },
                    new SqlParameter("@NgayCongChuan", SqlDbType.Int) { Value = ngayCongChuan },
                    new SqlParameter("@BoPhanID", SqlDbType.Int) { Value = boPhanID },
                    new SqlParameter("@NhanVienID", SqlDbType.Int) { Value = nhanVienID },
                    new SqlParameter("@NgayDiLam", SqlDbType.Int) { Value = ngayDiLam },
                    new SqlParameter("@NgayNghi", SqlDbType.Int) { Value = ngayNghi },
                    new SqlParameter("@NgayTinhLuong", SqlDbType.Int) { Value = ngayTinhLuong },
                    new SqlParameter("@GhiChu", SqlDbType.NVarChar, 255) { Value = (object)ghiChu ?? DBNull.Value }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Cập nhật chấm công
        public static bool UpdateChamCong(string thang, int ngayCongChuan, int boPhanID, int nhanVienID, int ngayDiLam, int ngayNghi, int ngayTinhLuong, string ghiChu, string chamCongID)
        {
            try
            {
                string query = "UPDATE tbl_ChamCong SET Thang = @Thang, NgayCongChuan = @NgayCongChuan, BoPhanID = @BoPhanID, NhanVienID = @NhanVienID, " +
                               "NgayDiLam = @NgayDiLam, NgayNghi = @NgayNghi, NgayTinhLuong = @NgayTinhLuong, GhiChu = @GhiChu WHERE ChamCongID = @ChamCongID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Thang", SqlDbType.NVarChar, 10) { Value = thang },
                    new SqlParameter("@NgayCongChuan", SqlDbType.Int) { Value = ngayCongChuan },
                    new SqlParameter("@BoPhanID", SqlDbType.Int) { Value = boPhanID },
                    new SqlParameter("@NhanVienID", SqlDbType.Int) { Value = nhanVienID },
                    new SqlParameter("@NgayDiLam", SqlDbType.Int) { Value = ngayDiLam },
                    new SqlParameter("@NgayNghi", SqlDbType.Int) { Value = ngayNghi },
                    new SqlParameter("@NgayTinhLuong", SqlDbType.Int) { Value = ngayTinhLuong },
                    new SqlParameter("@GhiChu", SqlDbType.NVarChar, 255) { Value = (object)ghiChu ?? DBNull.Value },
                    new SqlParameter("@ChamCongID", SqlDbType.NVarChar, 10) { Value = chamCongID }
                };
                int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Cập nhật trạng thái chấm công
        public static bool UpdateChamCongStatus(string chamCongID)
        {
            try
            {
                string query = "UPDATE tbl_ChamCong SET TrangThai = 2 WHERE ChamCongID = @ChamCongID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ChamCongID", SqlDbType.NVarChar, 10) { Value = chamCongID }
                };
                int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Thêm chi tiết bảng kê lương
        public static bool InsertChiTietLuong(string thangKeLuong, int nhanVienID, int ngayCongChuan, int ngayTinhLuong)
        {
            try
            {
                string query = "INSERT INTO tbl_ChiTietBanKeLuong (ThangKeLuong, NhanVienID, NgayCongChuan, NgayTinhLuong, TrangThai) " +
                               "VALUES (@ThangKeLuong, @NhanVienID, @NgayCongChuan, @NgayTinhLuong, 1)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ThangKeLuong", SqlDbType.NVarChar, 10) { Value = thangKeLuong },
                    new SqlParameter("@NhanVienID", SqlDbType.Int) { Value = nhanVienID },
                    new SqlParameter("@NgayCongChuan", SqlDbType.Int) { Value = ngayCongChuan },
                    new SqlParameter("@NgayTinhLuong", SqlDbType.Int) { Value = ngayTinhLuong }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Lấy chi tiết bảng kê lương theo tháng
        public static DataTable GetChiTietBanKeLuong(string thang)
        {
            try
            {
                string query = @"SELECT d.ChiTietBanKeLuongID, a.NhanVienID, a.MaNV, a.TenNV, d.NgayCongChuan, d.NgayTinhLuong, c.LuongCoBan, 
                                c.PhuCap * c.LuongCoBan / 100 AS [PhuCap],
                                c.LuongCoBan * (CAST(d.NgayTinhLuong AS FLOAT) / CAST(d.NgayCongChuan AS FLOAT)) AS [TongLuong],
                                c.LuongCoBan * (CAST(d.NgayTinhLuong AS FLOAT) / CAST(d.NgayCongChuan AS FLOAT)) + c.PhuCap * c.LuongCoBan / 100 AS [ThucLinh], 
                                d.TrangThai
                                FROM tbl_NhanVien a
                                INNER JOIN ChucDanh c ON c.MaChucDanh = a.MaChucDanh
                                LEFT JOIN tbl_ChiTietBanKeLuong d ON d.NhanVienID = a.NhanVienID AND d.ThangKeLuong = @Thang";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Thang", SqlDbType.NVarChar, 10) { Value = thang }
                };
                return cls_DatabaseManager.TableRead(query, parameters);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Cập nhật chi tiết bảng kê lương
        public static bool UpdateChiTietBanKeLuong(decimal tienLuongCung, decimal phuCap, decimal tongLuong, decimal thucLinh, string chiTietBanKeLuongID)
        {
            try
            {
                string query = "UPDATE tbl_ChiTietBanKeLuong SET TienLuongCung = @TienLuongCung, PhuCap = @PhuCap, TongLuong = @TongLuong, ThucLinh = @ThucLinh WHERE ChiTietBanKeLuongID = @ChiTietBanKeLuongID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TienLuongCung", SqlDbType.Decimal) { Value = tienLuongCung },
                    new SqlParameter("@PhuCap", SqlDbType.Decimal) { Value = phuCap },
                    new SqlParameter("@TongLuong", SqlDbType.Decimal) { Value = tongLuong },
                    new SqlParameter("@ThucLinh", SqlDbType.Decimal) { Value = thucLinh },
                    new SqlParameter("@ChiTietBanKeLuongID", SqlDbType.NVarChar, 10) { Value = chiTietBanKeLuongID }
                };
                int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Cập nhật trạng thái bảng kê lương
        public static bool UpdateTrangThaiBanKeLuong(string chiTietBanKeLuongID)
        {
            try
            {
                string query = "UPDATE tbl_ChiTietBanKeLuong SET TrangThai = 2 WHERE ChiTietBanKeLuongID = @ChiTietBanKeLuongID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ChiTietBanKeLuongID", SqlDbType.NVarChar, 10) { Value = chiTietBanKeLuongID }
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