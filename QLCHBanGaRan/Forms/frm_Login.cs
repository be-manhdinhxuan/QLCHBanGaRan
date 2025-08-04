using QLCHBanGaRan.Forms;
using QLCHBanGaRan.lib;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLCHBanGaRan
{
    public partial class frm_Login : Form
    {
        // false = ẨN; true = HIỆN
        private bool _showPassword = false;
        private bool isLoginInProgress = false; // Cờ để ngăn chặn gọi lặp
        private bool isMessageShown = false;    // Cờ để ngăn thông báo lặp

        public frm_Login()
        {
            InitializeComponent();

            // ====== Tùy chỉnh ban đầu ======
            txtTenDangNhap.Text = "";
            txtPassword.Text = "";

            // Không dùng HintText của MaterialTextbox (dùng label mờ)
            txtTenDangNhap.HintText = "";
            txtPassword.HintText = "";

            // Label gợi ý (placeholder)
            lblHintUsername.ForeColor = Color.FromArgb(128, 64, 64, 64);
            lblHintUsername.Enabled = false;
            lblHintPassword.ForeColor = Color.FromArgb(128, 64, 64, 64);
            lblHintPassword.Enabled = false;

            // Icon mắt: cảm giác nút bấm + không bắt Tab
            picEyeToggle.TabStop = false;
            picEyeToggle.Cursor = Cursors.Hand;
            picEyeToggle.BringToFront();

            // ====== Gán sự kiện ======
            txtTenDangNhap.Enter += txtTenDangNhap_Enter;
            txtTenDangNhap.Leave += txtTenDangNhap_Leave;
            txtTenDangNhap.KeyPress += txtTenDangNhap_KeyPress;
            //txtTenDangNhap.KeyDown += txtTenDangNhap_KeyDown;

            txtPassword.Enter += txtPassword_Enter;
            txtPassword.Leave += txtPassword_Leave;
            txtPassword.KeyPress += txtPassword_KeyPress;
            //txtPassword.KeyDown += txtPassword_KeyDown;

            // “Nhấn-giữ để xem” (ổn định hơn Click)
            picEyeToggle.MouseDown += picEyeToggle_MouseDown;
            picEyeToggle.MouseUp += picEyeToggle_MouseUp;

            btnClose.Click += btnClose_Click;

            this.Shown += (s, e) =>
            {
                _showPassword = false;      // mặc định ẨN
                UpdateMaskAndIcon();        // chỉ set mask + icon
            };

            // Focus ban đầu
            txtTenDangNhap.Focus();
            this.AcceptButton = btnLogin;

        }

        private void UpdateMaskAndIcon()
        {
            // MaterialTextbox: isPassword == true => ẨN
            txtPassword.isPassword = !_showPassword;

            // Đổi icon theo trạng thái
            picEyeToggle.Image = _showPassword
                ? Properties.Resources.eye_open   // đang HIỆN
                : Properties.Resources.eye;       // đang ẨN

            // Ép vẽ lại ngay
            txtPassword.Invalidate();
            txtPassword.Update();
            txtPassword.Refresh();
        }

        /// <summary>
        /// Trả focus về ô mật khẩu và đưa caret về cuối (an toàn sau khi handle đã tạo).
        /// </summary>
        private void FocusPasswordAtEnd()
        {
            txtPassword.Focus();

            // Chỉ BeginInvoke khi handle đã tạo để tránh InvalidOperationException
            if (this.IsHandleCreated && txtPassword.IsHandleCreated)
            {
                this.BeginInvoke((Action)(() =>
                {
                    try { SendKeys.Send("{END}"); } catch { /* ignore */ }
                }));
            }
        }

        // ================== Username handlers ==================

        private void txtTenDangNhap_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
                txtTenDangNhap.ForeColor = Color.Black;
        }

        private void txtTenDangNhap_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                lblHintUsername.Visible = true;
                txtTenDangNhap.Text = "";
                txtTenDangNhap.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void txtTenDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lblHintUsername.Visible)
            {
                lblHintUsername.Visible = false;
                txtTenDangNhap.Text = "";
            }
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !isLoginInProgress)
            {
                e.Handled = true;
                e.SuppressKeyPress = true; // Ngăn chặn event bubbling
                btnLogin.PerformClick();
            }
        }

        // ================== Password handlers ==================

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
                txtPassword.ForeColor = Color.Black;

            // Không set cứng isPassword; giữ theo _showPassword
            UpdateMaskAndIcon();
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblHintPassword.Visible = true;
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.FromArgb(64, 64, 64);
            }
            UpdateMaskAndIcon();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lblHintPassword.Visible)
            {
                lblHintPassword.Visible = false;
                txtPassword.Text = "";
            }
            UpdateMaskAndIcon();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !isLoginInProgress)
            {
                e.Handled = true;
                e.SuppressKeyPress = true; // Ngăn chặn event bubbling
                btnLogin.PerformClick();
            }
        }

        // ================== Toggle hiển thị/ẩn mật khẩu ==================

        // Nhấn-giữ để xem
        private void picEyeToggle_MouseDown(object sender, MouseEventArgs e)
        {
            _showPassword = true;   // HIỆN
            UpdateMaskAndIcon();
            FocusPasswordAtEnd();
        }

        private void picEyeToggle_MouseUp(object sender, MouseEventArgs e)
        {
            _showPassword = false;  // ẨN
            UpdateMaskAndIcon();
            FocusPasswordAtEnd();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn muốn thoát chương trình?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Ngăn chặn gọi lặp và vô hiệu hóa tạm thời
            if (!isLoginInProgress)
            {
                isLoginInProgress = true;
                isMessageShown = false; // Đặt lại cờ thông báo
                btnLogin.Enabled = false; // Vô hiệu hóa nút trong quá trình xử lý

                // Thêm delay nhỏ để tránh gọi lặp
                System.Threading.Thread.Sleep(50);

                try
                {
                    string username = txtTenDangNhap.Text; // Không trim để giữ nguyên ký tự
                    string password = txtPassword.Text;    // Không trim để giữ nguyên ký tự

                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ username và password!", "Lỗi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Mã hóa mật khẩu trước khi kiểm tra
                    string encryptedPassword = cls_Encryption.Encrypt(password);
                    string maND = cls_EmployeeManagement.CheckLogin(username, encryptedPassword);

                    if (maND != "ERROR")
                    {
                        MessageBox.Show("Đăng nhập thành công!", "Thành công",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        frm_Main mainForm = new frm_Main(maND);
                        mainForm.Show();
                    }
                    else
                    {
                        // Trường hợp maND trả về "ERROR" (không có exception)
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    if (!isMessageShown) // Chỉ hiển thị thông báo nếu chưa hiển thị
                    {
                        isMessageShown = true;
                        if (ex.Number == 50002) // Tài khoản bị vô hiệu hóa
                        {
                            MessageBox.Show("Tài khoản của bạn đã bị vô hiệu hóa.", "Lỗi",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (ex.Number == 50001) // Sai username hoặc password
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Lỗi đăng nhập: " + ex.Message, "Lỗi",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Chỉ xử lý các exception không phải SqlException
                    if (!isMessageShown && !(ex is SqlException))
                    {
                        isMessageShown = true;
                        MessageBox.Show("Lỗi đăng nhập: " + ex.Message, "Lỗi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    isLoginInProgress = false;
                    btnLogin.Enabled = true; // Kích hoạt lại nút sau khi hoàn tất
                }
            }
        }
    }
}