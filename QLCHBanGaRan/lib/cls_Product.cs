using System;
using System.Data;
using System.Data.SqlClient;

namespace QLCHBanGaRan.lib
{
    class cls_Product
    {
        // Hiển thị danh sách món ăn
        public static DataTable _showDoAn()
        {
            string query = "SELECT da.MaMon, da.TenMon, da.MaNCC, ncc.TenNCC, da.GiaTien, da.GiamGia, da.SoLuong, da.SoLuongDaBan, da.IsDeleted " +
                           "FROM DoAn da " +
                           "LEFT JOIN NhaCungCap ncc ON da.MaNCC = ncc.MaNCC " +
                           "WHERE da.IsDeleted = 0";
            return cls_DatabaseManager.TableRead(query, null);
        }

        // Hiển thị danh sách đồ uống
        public static DataTable _showDoUong()
        {
            string query = "SELECT du.MaDoUong, du.TenDoUong, du.MaNCC, ncc.TenNCC, du.GiaTien, du.GiamGia, du.SoLuong, du.IsDeleted " +
                           "FROM DoUong du " +
                           "LEFT JOIN NhaCungCap ncc ON du.MaNCC = ncc.MaNCC " +
                           "WHERE du.IsDeleted = 0";
            return cls_DatabaseManager.TableRead(query, null);
        }

        // Hiển thị danh sách nhà cung cấp
        public static DataTable _showNCC()
        {
            string query = "SELECT MaNCC, TenNCC FROM NhaCungCap WHERE IsDeleted = 0";
            return cls_DatabaseManager.TableRead(query);
        }

        // Xóa món ăn
        public static bool _delDoAn(string maMon)
        {
            try
            {
                string query = "DELETE FROM DoAn WHERE MaMon = @MaMon";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaMon", SqlDbType.NVarChar, 10) { Value = maMon }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Xóa đồ uống
        public static bool _delDoUong(string maDoUong)
        {
            try
            {
                string query = "DELETE FROM DoUong WHERE MaDoUong = @MaDoUong";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaDoUong", SqlDbType.NVarChar, 10) { Value = maDoUong }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Kiểm tra món ăn trong ChiTietHoaDon
        public static DataTable _checkDoAn(string maMon)
        {
            string query = "SELECT * FROM ChiTietHoaDon WHERE MaMon = @MaMon";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaMon", SqlDbType.NVarChar, 10) { Value = maMon }
            };
            return cls_DatabaseManager.TableRead(query, parameters);
        }

        // Kiểm tra đồ uống trong ChiTietHoaDon
        public static DataTable _checkDoUong(string maDoUong)
        {
            string query = "SELECT * FROM ChiTietHoaDon WHERE MaDoUong = @MaDoUong";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDoUong", SqlDbType.NVarChar, 10) { Value = maDoUong }
            };
            return cls_DatabaseManager.TableRead(query, parameters);
        }

        // Cập nhật thông tin món ăn
        public static bool _updateDoAn(string maMon, string tenMon, string maNCC, decimal giaTien, int giamGia, int soLuong)
        {
            try
            {
                string query = "UPDATE DoAn SET TenMon = @TenMon, MaNCC = @MaNCC, GiaTien = @GiaTien, GiamGia = @GiamGia, SoLuong = @SoLuong WHERE MaMon = @MaMon";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenMon", SqlDbType.NVarChar, 100) { Value = tenMon },
                    new SqlParameter("@MaNCC", SqlDbType.NVarChar, 10) { Value = maNCC },
                    new SqlParameter("@GiaTien", SqlDbType.Decimal) { Value = giaTien },
                    new SqlParameter("@GiamGia", SqlDbType.Int) { Value = giamGia },
                    new SqlParameter("@SoLuong", SqlDbType.Int) { Value = soLuong },
                    new SqlParameter("@MaMon", SqlDbType.NVarChar, 10) { Value = maMon }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Cập nhật thông tin đồ uống
        public static bool _updateDoUong(string maDoUong, string tenDoUong, string maNCC, decimal giaTien, int giamGia, int soLuong)
        {
            try
            {
                string query = "UPDATE DoUong SET TenDoUong = @TenDoUong, MaNCC = @MaNCC, GiaTien = @GiaTien, GiamGia = @GiamGia, SoLuong = @SoLuong WHERE MaDoUong = @MaDoUong";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDoUong", SqlDbType.NVarChar, 100) { Value = tenDoUong },
                    new SqlParameter("@MaNCC", SqlDbType.NVarChar, 10) { Value = maNCC },
                    new SqlParameter("@GiaTien", SqlDbType.Decimal) { Value = giaTien },
                    new SqlParameter("@GiamGia", SqlDbType.Int) { Value = giamGia },
                    new SqlParameter("@SoLuong", SqlDbType.Int) { Value = soLuong },
                    new SqlParameter("@MaDoUong", SqlDbType.NVarChar, 10) { Value = maDoUong }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Tìm kiếm món ăn
        public static DataTable _searchDoAn(string tenMon)
        {
            string query = "SELECT da.MaMon, da.TenMon, da.MaNCC, ncc.TenNCC, da.GiaTien, da.GiamGia, da.SoLuong " +
                           "FROM DoAn da " +
                           "JOIN NhaCungCap ncc ON da.MaNCC = ncc.MaNCC " +
                           "WHERE TenMon LIKE @TenMon1 OR TenMon LIKE @TenMon2 OR TenMon LIKE @TenMon3";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenMon1", SqlDbType.NVarChar, 100) { Value = tenMon + "%" },
                new SqlParameter("@TenMon2", SqlDbType.NVarChar, 100) { Value = "%" + tenMon + "%" },
                new SqlParameter("@TenMon3", SqlDbType.NVarChar, 100) { Value = "%" + tenMon }
            };
            return cls_DatabaseManager.TableRead(query, parameters);
        }

        // Tìm kiếm đồ uống
        public static DataTable _searchDoUong(string tenDoUong)
        {
            string query = "SELECT du.MaDoUong, du.TenDoUong, du.MaNCC, ncc.TenNCC, du.GiaTien, du.GiamGia, du.SoLuong " +
                           "FROM DoUong du " +
                           "JOIN NhaCungCap ncc ON du.MaNCC = ncc.MaNCC " +
                           "WHERE TenDoUong LIKE @TenDoUong1 OR TenDoUong LIKE @TenDoUong2 OR TenDoUong LIKE @TenDoUong3";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDoUong1", SqlDbType.NVarChar, 100) { Value = tenDoUong + "%" },
                new SqlParameter("@TenDoUong2", SqlDbType.NVarChar, 100) { Value = "%" + tenDoUong + "%" },
                new SqlParameter("@TenDoUong3", SqlDbType.NVarChar, 100) { Value = "%" + tenDoUong }
            };
            return cls_DatabaseManager.TableRead(query, parameters);
        }

        // Thêm món ăn
        public static bool _addDoAn(string maMon, string tenMon, string maNCC, decimal giaTien, int giamGia, int soLuong)
        {
            try
            {
                string query = "INSERT INTO DoAn (MaMon, TenMon, MaNCC, GiaTien, GiamGia, SoLuong, SoLuongDaBan) VALUES (@MaMon, @TenMon, @MaNCC, @GiaTien, @GiamGia, @SoLuong, 0)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaMon", SqlDbType.NVarChar, 10) { Value = maMon },
                    new SqlParameter("@TenMon", SqlDbType.NVarChar, 100) { Value = tenMon },
                    new SqlParameter("@MaNCC", SqlDbType.NVarChar, 10) { Value = maNCC },
                    new SqlParameter("@GiaTien", SqlDbType.Decimal) { Value = giaTien },
                    new SqlParameter("@GiamGia", SqlDbType.Int) { Value = giamGia },
                    new SqlParameter("@SoLuong", SqlDbType.Int) { Value = soLuong }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Thêm đồ uống
        public static bool _addDoUong(string maDoUong, string tenDoUong, string maNCC, decimal giaTien, int giamGia, int soLuong)
        {
            try
            {
                string query = "INSERT INTO DoUong (MaDoUong, TenDoUong, MaNCC, GiaTien, GiamGia, SoLuong, SoLuongDaBan) VALUES (@MaDoUong, @TenDoUong, @MaNCC, @GiaTien, @GiamGia, @SoLuong, 0)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaDoUong", SqlDbType.NVarChar, 10) { Value = maDoUong },
                    new SqlParameter("@TenDoUong", SqlDbType.NVarChar, 100) { Value = tenDoUong },
                    new SqlParameter("@MaNCC", SqlDbType.NVarChar, 10) { Value = maNCC },
                    new SqlParameter("@GiaTien", SqlDbType.Decimal) { Value = giaTien },
                    new SqlParameter("@GiamGia", SqlDbType.Int) { Value = giamGia },
                    new SqlParameter("@SoLuong", SqlDbType.Int) { Value = soLuong }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Lấy danh sách mã món ăn
        public static DataTable _getIDDoAn()
        {
            string query = "SELECT MaMon FROM DoAn";
            return cls_DatabaseManager.TableRead(query);
        }

        // Lấy danh sách mã đồ uống
        public static DataTable _getIDDoUong()
        {
            string query = "SELECT MaDoUong FROM DoUong";
            return cls_DatabaseManager.TableRead(query);
        }

        // Hiển thị danh sách loại sản phẩm
        public static DataTable _showProductType()
        {
            string query = "SELECT LoaiSPID, TenLoaiSP FROM LoaiSanPham";
            return cls_DatabaseManager.TableRead(query);
        }

        // Thêm loại sản phẩm
        public static bool _addProductType(string tenLoaiSP)
        {
            try
            {
                string query = "INSERT INTO LoaiSanPham (TenLoaiSP) VALUES (@TenLoaiSP)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenLoaiSP", SqlDbType.NVarChar, 100) { Value = tenLoaiSP }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Cập nhật loại sản phẩm
        public static bool _updateProductType(int loaiSPID, string tenLoaiSP)
        {
            try
            {
                string query = "UPDATE LoaiSanPham SET TenLoaiSP = @TenLoaiSP WHERE LoaiSPID = @LoaiSPID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenLoaiSP", SqlDbType.NVarChar, 100) { Value = tenLoaiSP },
                    new SqlParameter("@LoaiSPID", SqlDbType.Int) { Value = loaiSPID }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Xóa loại sản phẩm
        public static bool _delProductType(int loaiSPID)
        {
            try
            {
                string query = "DELETE FROM LoaiSanPham WHERE LoaiSPID = @LoaiSPID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@LoaiSPID", SqlDbType.Int) { Value = loaiSPID }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Cập nhật mã món ăn (MaMon) trong bảng DoAn
        public static bool _updateDoAnMaSanPham(string oldMaSanPham, string newMaSanPham)
        {
            try
            {
                string query = "UPDATE DoAn SET MaMon = @NewMaSanPham WHERE MaMon = @OldMaSanPham";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@OldMaSanPham", SqlDbType.NVarChar, 10) { Value = oldMaSanPham },
                    new SqlParameter("@NewMaSanPham", SqlDbType.NVarChar, 10) { Value = newMaSanPham }
                };
                cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in _updateDoAnMaSanPham: " + ex.Message);
                return false;
            }
        }
    }
}