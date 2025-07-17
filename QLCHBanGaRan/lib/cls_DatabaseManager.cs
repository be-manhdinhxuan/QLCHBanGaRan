using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QLCHBanGaRan.lib
{
    public class cls_DatabaseManager
    {
        public static readonly string connectionString = File.ReadAllText("config.env").Trim();

        public static void Connect()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi kết nối SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        public static DataTable TableRead(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text; // Thay bằng CommandType.StoredProcedure nếu query là tên stored procedure
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    try
                    {
                        rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"ExecuteNonQuery - Rows affected: {rowsAffected}");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"ExecuteNonQuery Error - Number: {ex.Number}, Message: {ex.Message}");
                        throw; // Ném lại ngoại lệ để xử lý ở cấp cao hơn
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"General ExecuteNonQuery Error: {ex.Message}");
                        throw;
                    }
                }
            }
            return rowsAffected;
        }

        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object result = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        result = cmd.ExecuteScalar();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi thực thi truy vấn Scalar: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return result;
        }

        // Thêm phương thức kiểm tra trùng lặp
        public static bool CheckDuplicate(string table, string column, string value, string condition = "")
        {
            string query = $"SELECT COUNT(*) FROM {table} WHERE {column} = @Value {condition}";
            SqlParameter[] parameters = { new SqlParameter("@Value", value) };
            object result = ExecuteScalar(query, parameters);
            return Convert.ToInt32(result) > 0;
        }

        // Thêm phương thức tạo mã ngẫu nhiên
        public static string GenerateRandomMaND(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}