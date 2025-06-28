using MaterialSkin;
using MaterialSkin.Controls;
using QLCHBanGaRan.Forms;
using QLCHBanGaRan.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_Connect : MaterialForm
    {
        private LoadingOverlay loadingOverlay; // Sử dụng class riêng

        public frm_Connect()
        {
            InitializeComponent();

            // Khởi tạo và áp dụng theme MaterialSkin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Red800,
                Primary.Red900,
                Primary.Red500,
                Accent.Red200,
                TextShade.WHITE
            );
        }

        private void frm_Connect_Load(object sender, EventArgs e)
        {
            cmbAuthor.ValueMember = "Value";
            cmbAuthor.DisplayMember = "Text";
            var items = new[] {
                new { Text = "Windows Authentication", Value = "1" },
                new { Text = "SQL Server Authentication", Value = "2" },
            };
            cmbAuthor.DataSource = items;
            lblSelectDB.Enabled = false;
            cmbSelectDB.Enabled = false;
            btnGetDB.Enabled = false;

            txtUserName.Enabled = true;
            txtPassword.Enabled = true;
            txtServerName.Enabled = true;

            // Điền thông tin từ config.env nếu tồn tại (không kiểm tra)
            if (File.Exists("config.env"))
            {
                string connString = File.ReadAllText("config.env");
                // Phân tích và điền (tùy chọn)
                // Ví dụ: txtServerName.Text = ExtractServerName(connString);
            }
        }

        private void cmbAuthor_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbAuthor.SelectedValue) == 1)
            {
                lblPassword.Enabled = false;
                lblUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtUserName.Enabled = false;
            }
            else
            {
                lblPassword.Enabled = true;
                lblUserName.Enabled = true;
                txtPassword.Enabled = true;
                txtUserName.Enabled = true;
            }
            btnGetDB.Enabled = true;
            txtServerName.Enabled = true;
        }

        private async void btnGetDB_Click(object sender, EventArgs e)
        {
            // Đặt lại trạng thái ban đầu
            lblSelectDB.Enabled = false;
            cmbSelectDB.Enabled = false;
            cmbSelectDB.DataSource = null;
            btnConnect.Enabled = false;
            cmbAuthor.Enabled = true;

            string connString = null;

            if (cmbAuthor.SelectedIndex == 0)
            {
                connString = @"Data Source=" + txtServerName.Text.Trim() + ";Initial Catalog=master;Integrated Security=True";
            }
            else if (cmbAuthor.SelectedIndex == 1)
            {
                connString = @"Data Source=" + txtServerName.Text.Trim() + ";Initial Catalog=master;User ID=" + txtUserName.Text.Trim() + ";password=" + txtPassword.Text.Trim();
            }
            else
            {
                UpdateUI(() => MessageBox.Show("Vui lòng chọn phương thức xác thực.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                return;
            }

            await PerformDatabaseOperationAsync(() =>
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    // Debug chuỗi kết nối
                    UpdateUI(() => MessageBox.Show("Chuỗi kết nối: " + connString));
                    SqlDataAdapter dA = new SqlDataAdapter("SELECT name FROM sys.databases WHERE name NOT IN ('master', 'model', 'msdb', 'tempdb')", conn);
                    DataTable dt = new DataTable();
                    dA.Fill(dt);
                    dA.Dispose();
                    conn.Close();

                    if (dt.Rows.Count == 0)
                    {
                        throw new Exception("Không tìm thấy cơ sở dữ liệu nào. Vui lòng kiểm tra quyền truy cập.");
                    }

                    UpdateUI(() =>
                    {
                        lblSelectDB.Enabled = true;
                        cmbSelectDB.Enabled = true;
                        cmbSelectDB.DataSource = dt;
                        cmbSelectDB.DisplayMember = "name";
                        cmbSelectDB.ValueMember = "name";

                        btnGetDB.Enabled = false;
                        btnConnect.Enabled = true;
                        cmbAuthor.Enabled = false;
                        if (Convert.ToInt32(cmbAuthor.SelectedValue) != 1)
                        {
                            lblPassword.Enabled = true;
                            lblUserName.Enabled = true;
                            txtPassword.Enabled = true;
                            txtUserName.Enabled = true;
                        }
                    });
                }
            }, "Đang tải danh sách cơ sở dữ liệu...");
        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtServerName.Text))
            {
                btnGetDB.Enabled = true;
            }
            else
            {
                btnGetDB.Enabled = false;
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (cmbSelectDB.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn 1 CSDL.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connString = null;
            if (cmbAuthor.SelectedIndex == 0)
            {
                connString = @"Data Source=" + txtServerName.Text.Trim() + ";Initial Catalog=" + cmbSelectDB.SelectedValue.ToString() + ";Integrated Security=True";
            }
            else
            {
                connString = @"Data Source=" + txtServerName.Text.Trim() + ";Initial Catalog=" + cmbSelectDB.SelectedValue.ToString() + ";User ID=" + txtUserName.Text.Trim() + ";password=" + txtPassword.Text.Trim();
            }

            await PerformDatabaseOperationAsync(async () =>
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    await Task.Run(() => conn.Open());
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;

                    // Kiểm tra trước nếu stored procedure tồn tại (sử dụng CommandType.Text)
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT OBJECT_ID('dbo.TESTCONNECTION')";
                    object obj = await Task.Run(() => command.ExecuteScalar());
                    if (obj == null || obj == DBNull.Value)
                    {
                        UpdateUI(() => MessageBox.Show("Lỗi: Database không hợp lệ. Vui lòng chọn lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error));
                        return; // Thoát khỏi luồng nếu không tồn tại
                    }

                    // Thực thi stored procedure
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "TESTCONNECTION";
                    SqlDataReader dataReader = await Task.Run(() => command.ExecuteReader());
                    bool? isValid = null;

                    if (await dataReader.ReadAsync())
                    {
                        isValid = dataReader.GetBoolean(0); // Lấy giá trị BIT từ cột đầu tiên
                    }
                    dataReader.Close();
                    conn.Close();

                    // Debug chi tiết
                    UpdateUI(() => Console.WriteLine("TESTCONNECTION result: " + isValid));
                    UpdateUI(() => Console.WriteLine("isValid: " + (isValid.HasValue ? isValid.Value.ToString() : "null")));

                    if (!isValid.HasValue || !isValid.Value)
                    {
                        throw new Exception("Database không phù hợp với dự án.");
                    }

                    if (isValid.Value)
                    {
                        File.WriteAllText("config.env", connString);
                        UpdateUI(() =>
                        {
                            this.Hide();
                            new frm_Login().Show();
                            // this.Close(); // Tạm thời loại bỏ để kiểm tra
                        });
                    }
                }
            }, "Đang kết nối đến cơ sở dữ liệu...");
        }

        private async Task PerformDatabaseOperationAsync(Action operation, string loadingMessage)
        {
            loadingOverlay = new LoadingOverlay(this, loadingMessage);
            loadingOverlay.Show(); // Hiển thị overlay làm mờ form
            this.Enabled = false; // Vô hiệu hóa form chính

            try
            {
                await Task.Run(() => operation()); // Thực thi operation và chờ hoàn thành
            }
            catch (SqlException ex)
            {
                string errorMessage = "Lỗi kết nối.";
                if (ex.Number == 2) // Lỗi không tìm thấy server
                {
                    errorMessage = "Lỗi: Server không đúng.";
                }
                else if (ex.Number == 18456) // Lỗi tài khoản hoặc mật khẩu sai
                {
                    errorMessage = "Lỗi: Tài khoản hoặc mật khẩu sai.";
                }
                else if (ex.Number == 4060) // Lỗi cơ sở dữ liệu không tồn tại
                {
                    errorMessage = "Lỗi: Cơ sở dữ liệu không tồn tại.";
                }
                UpdateUI(() =>
                {
                    loadingOverlay.Close(); // Đóng overlay
                    this.Enabled = true; // Kích hoạt lại form
                    this.BringToFront(); // Đưa frm_Connect lên trên cùng
                    MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
            catch (Exception ex)
            {
                UpdateUI(() =>
                {
                    loadingOverlay.Close(); // Đóng overlay
                    this.Enabled = true; // Kích hoạt lại form
                    this.BringToFront(); // Đưa frm_Connect lên trên cùng
                    if (ex.Message.Contains("Database không phù hợp với dự án"))
                    {
                        MessageBox.Show("Lỗi: Database không hợp lệ. Vui lòng chọn lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: Kiểm tra lại thông tin. Chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
            }
            finally
            {
                if (loadingOverlay != null && !loadingOverlay.IsDisposed)
                {
                    UpdateUI(() =>
                    {
                        loadingOverlay.Close();
                        this.Enabled = true; // Đảm bảo form được kích hoạt lại
                        this.BringToFront(); // Đảm bảo frm_Connect luôn hiển thị
                    });
                }
            }
        }

        private void UpdateUI(Action action)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(action));
            }
            else
            {
                action();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}