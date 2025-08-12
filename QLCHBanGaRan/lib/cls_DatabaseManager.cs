using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

namespace QLCHBanGaRan.lib
{
    public class cls_DatabaseManager
    {
        private static string _connectionString;

        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    // Bỏ qua khi đang ở Design mode (VD: kéo thả UC vào Form)
                    if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    {
                        return "Server=.;Database=FakeDB;Trusted_Connection=True;";
                    }

                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.env");
                    if (!File.Exists(path))
                    {
                        throw new FileNotFoundException($"Không tìm thấy file cấu hình: {path}");
                    }

                    _connectionString = File.ReadAllText(path).Trim();
                }
                return _connectionString;
            }
        }

        public static void Connect()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
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
            using (SqlConnection conn = new SqlConnection(ConnectionString))
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

        public static DataTable TableReadStoredProc(string storedProcName, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(storedProcName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
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
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (query.Trim().ToLower().StartsWith("sp_") ||
                        (!query.ToLower().Contains("select") &&
                         !query.ToLower().Contains("insert") &&
                         !query.ToLower().Contains("update") &&
                         !query.ToLower().Contains("delete")))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        cmd.CommandType = CommandType.Text;
                    }

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"ExecuteNonQuery - Rows affected: {rowsAffected}, CommandType: {cmd.CommandType}, Query: {query}");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"ExecuteNonQuery Error - Number: {ex.Number}, Message: {ex.Message}, Procedure: {query}, Parameters: {string.Join(", ", parameters?.Select(p => $"{p.ParameterName}={p.Value}") ?? new string[] { "None" })}");
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"General ExecuteNonQuery Error: {ex.Message}, Procedure: {query}");
                        throw;
                    }
                }
            }
            return rowsAffected;
        }

        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object result = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
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

        public static bool CheckDuplicate(string table, string column, string value, string condition = "")
        {
            string query = $"SELECT COUNT(*) FROM {table} WHERE {column} = @Value {condition}";
            SqlParameter[] parameters = { new SqlParameter("@Value", value) };
            object result = ExecuteScalar(query, parameters);
            return Convert.ToInt32(result) > 0;
        }

        public static string GenerateRandomMaND(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
