using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QLCHBanGaRan.lib
{
    class cls_NCC
    {
        public static DataTable _showDetailNCC()
        {
            string query = "SELECT MaNCC, TenNCC, DiaChi, SDT, IsDeleted FROM NhaCungCap WHERE IsDeleted = 0";
            return cls_DatabaseManager.TableRead(query);
        }

        public static DataTable _searchNCC(string filter, string keyword)
        {
            string query = "";
            if (filter == "Tên NCC")
                query = "SELECT MaNCC, TenNCC, DiaChi, SDT, IsDeleted FROM NhaCungCap WHERE IsDeleted = 0 AND TenNCC LIKE @Keyword";
            else if (filter == "Địa chỉ")
                query = "SELECT MaNCC, TenNCC, DiaChi, SDT, IsDeleted FROM NhaCungCap WHERE IsDeleted = 0 AND DiaChi LIKE @Keyword";
            else if (filter == "SĐT")
                query = "SELECT MaNCC, TenNCC, DiaChi, SDT, IsDeleted FROM NhaCungCap WHERE IsDeleted = 0 AND SDT LIKE @Keyword";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", $"%{keyword}%")
            };
            return cls_DatabaseManager.TableRead(query, parameters);
        }

        public static bool _addNCC(string maNCC, string tenNCC, string diaChi, string sdt)
        {
            try
            {
                string query = "INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, SDT, IsDeleted) VALUES (@MaNCC, @TenNCC, @DiaChi, @SDT, 0)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNCC", maNCC),
                    new SqlParameter("@TenNCC", tenNCC),
                    new SqlParameter("@DiaChi", diaChi),
                    new SqlParameter("@SDT", sdt)
                };
                int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }

        public static bool _updateNCC(string maNCC, string tenNCC, string diaChi, string sdt)
        {
            try
            {
                string query = "UPDATE NhaCungCap SET TenNCC = @TenNCC, DiaChi = @DiaChi, SDT = @SDT WHERE MaNCC = @MaNCC";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNCC", maNCC),
                    new SqlParameter("@TenNCC", tenNCC),
                    new SqlParameter("@DiaChi", diaChi),
                    new SqlParameter("@SDT", sdt)
                };
                int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }

        public static bool _delNCC(string maNCC)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_DelNCC", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                    SqlParameter returnParam = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnParam.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnParam);
                    cmd.ExecuteNonQuery();
                    return (int)returnParam.Value == 1; // Trả về true nếu stored procedure thành công (return 1)
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL khi xóa nhà cung cấp {maNCC}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định khi xóa nhà cung cấp {maNCC}: {ex.Message}");
                return false;
            }
        }

        public static DataTable _checkNCC(string maNCC)
        {
            string query = "SELECT COUNT(*) FROM DoAn WHERE MaNCC = @MaNCC AND IsDeleted = 0 UNION SELECT COUNT(*) FROM DoUong WHERE MaNCC = @MaNCC AND IsDeleted = 0";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNCC", maNCC)
            };
            return cls_DatabaseManager.TableRead(query, parameters);
        }

        public static string GenerateMaNCC()
        {
            string query = "SELECT MAX(CAST(SUBSTRING(MaNCC, 4, 3) AS INT)) FROM NhaCungCap WHERE MaNCC LIKE 'NCC[0-9][0-9][0-9]'";
            object result = cls_DatabaseManager.ExecuteScalar(query);
            int maxNumber = result == DBNull.Value ? 0 : Convert.ToInt32(result);
            return $"NCC{(maxNumber + 1).ToString("D3")}";
        }
    }
}