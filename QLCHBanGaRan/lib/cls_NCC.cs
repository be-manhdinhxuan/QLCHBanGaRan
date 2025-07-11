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
            string query = "SELECT MaNCC, TenNCC FROM NhaCungCap";
            return cls_DatabaseManager.TableRead(query);
        }

        public static bool _addNCC(string tenNCC)
        {
            try
            {
                string maNCC = GenerateMaNCC();
                string query = "INSERT INTO NhaCungCap (MaNCC, TenNCC) VALUES (@MaNCC, @TenNCC)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNCC", maNCC),
                    new SqlParameter("@TenNCC", tenNCC)
                };
                int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }

        public static bool _updateNCC(string maNCC, string tenNCC)
        {
            try
            {
                string query = "UPDATE NhaCungCap SET TenNCC = @TenNCC WHERE MaNCC = @MaNCC";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNCC", maNCC),
                    new SqlParameter("@TenNCC", tenNCC)
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
                string query = "EXEC sp_DelNCC @MaNCC";
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaNCC", maNCC) };
                using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Trả về true nếu có dòng bị ảnh hưởng
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
            string query = "SELECT COUNT(*) FROM DoAn WHERE MaNCC = @MaNCC UNION SELECT COUNT(*) FROM DoUong WHERE MaNCC = @MaNCC";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNCC", maNCC)
            };
            return cls_DatabaseManager.TableRead(query, parameters);
        }

        private static string GenerateMaNCC()
        {
            string query = "SELECT MAX(CAST(SUBSTRING(MaNCC, 4, 3) AS INT)) FROM NhaCungCap WHERE MaNCC LIKE 'NCC[0-9][0-9][0-9]'";
            object result = cls_DatabaseManager.ExecuteScalar(query);
            int maxNumber = result == DBNull.Value ? 0 : Convert.ToInt32(result);
            return $"NCC{(maxNumber + 1).ToString("D3")}";
        }
    }
}