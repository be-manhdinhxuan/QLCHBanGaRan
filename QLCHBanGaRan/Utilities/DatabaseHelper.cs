using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace QLCHBanGaRan.Utilities
{
    public class DatabaseHelper
    {
        private static SqlConnection conn;

        static DatabaseHelper()
        {
            // Khởi tạo kết nối từ config.env khi class được tải
            if (File.Exists("config.env"))
            {
                conn = new SqlConnection(File.ReadAllText("config.env").Trim());
            }
            else
            {
                MessageBox.Show("Lỗi: File cấu hình 'config.env' không tồn tại.", "Lỗi Kết Nối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Connect()
        {
            try
            {
                if (conn == null)
                {
                    throw new Exception("Kết nối không được khởi tạo. Vui lòng kiểm tra config.env.");
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi Kết Nối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Disconnect()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi Kết Nối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm đọc dữ liệu từ bảng
        public static DataTable TableRead(string query)
        {
            Connect();
            DataTable dt = new DataTable();
            try
            {
                using (SqlDataAdapter dbA = new SqlDataAdapter(query, conn))
                {
                    dbA.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: Không thể đọc dữ liệu. {ex.Message}", "Lỗi Kết Nối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Disconnect();
            }
            return dt;
        }

        // Hàm thêm, xóa, sửa
        public static void AED(string query)
        {
            Connect();
            try
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: Không thể thực thi truy vấn. {ex.Message}", "Lỗi Kết Nối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Disconnect();
            }
        }
    }
}