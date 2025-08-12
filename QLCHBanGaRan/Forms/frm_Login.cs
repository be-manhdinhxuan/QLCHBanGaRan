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
        private bool _showPassword = false;
        private bool isLoginInProgress = false; // Cờ để ngăn chặn gọi lặp
        private bool isMessageShown = false;    // Cờ để ngăn thông báo lặp
        private bool isDragging = false;
        private Point lastCursorPosition;

        public frm_Login()
        {
            InitializeComponent();

            // ====== Tùy chỉnh ban đầu ======
            txtTenDangNhap.Text = "";
            txtPassword.Text = "";

            // Label gợi ý (placeholder) ban đầu
            lblHintUsername.ForeColor = Color.FromArgb(128, 64, 64, 64);
            lblHintUsername.Visible = true; // Hiển thị ban đầu khi ô trống
            lblHintPassword.ForeColor = Color.FromArgb(128, 64, 64, 64);
            lblHintPassword.Visible = true; // Hiển thị ban đầu khi ô trống

            // Icon mắt: cảm giác nút bấm + không bắt Tab
            picEyeToggle.TabStop = false;
            picEyeToggle.Cursor = Cursors.Hand;
            picEyeToggle.BringToFront();

            // ====== Gán sự kiện ======
            txtTenDangNhap.Enter += txtTenDangNhap_Enter;
            txtTenDangNhap.Leave += txtTenDangNhap_Leave;
            txtTenDangNhap.KeyPress += txtTenDangNhap_KeyPress;
            txtTenDangNhap.KeyDown += txtTenDangNhap_KeyDown;

            txtPassword.Enter += txtPassword_Enter;
            txtPassword.Leave += txtPassword_Leave;
            txtPassword.KeyPress += txtPassword_KeyPress;
            txtPassword.KeyDown += txtPassword_KeyDown;

            // “Nhấn-giữ để xem” (ổn định hơn Click)
            picEyeToggle.MouseDown += picEyeToggle_MouseDown;
            picEyeToggle.MouseUp += picEyeToggle_MouseUp;

            btnClose.Click += btnClose_Click;
            pnlRight.MouseDown += pnlRight_MouseDown;
            pnlRight.MouseMove += pnlRight_MouseMove;
            pnlRight.MouseUp += pnlRight_MouseUp;

            this.Shown += (s, e) =>
            {
                _showPassword = false;      // mặc định ẨN
                UpdateMaskAndIcon();        // Cập nhật mask và icon
                txtTenDangNhap.Focus();
            };

            this.AcceptButton = btnLogin;
        }

        private void UpdateMaskAndIcon()
        {
            // Sử dụng UseSystemPasswordChar thay vì isPassword
            txtPassword.UseSystemPasswordChar = !_showPassword;

            // Đổi icon theo trạng thái
            picEyeToggle.Image = _showPassword
                ? Properties.Resources.eye_open   // đang HIỆN
                : Properties.Resources.eye;       // đang ẨN

            // Ép vẽ lại ngay
            txtPassword.Invalidate();
            txtPassword.Update();
            txtPassword.Refresh();
        }

        private void FocusPasswordAtEnd()
        {
            txtPassword.Focus();
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
            {
                lblHintUsername.Visible = false;
                txtTenDangNhap.ForeColor = Color.Black;
            }
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
                txtTenDangNhap.ForeColor = Color.Black;
            }
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !isLoginInProgress)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                btnLogin.PerformClick();
            }
        }

        // ================== Password handlers ==================

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblHintPassword.Visible = false;
                txtPassword.ForeColor = Color.Black;
            }
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
                txtPassword.ForeColor = Color.Black;
            }
            UpdateMaskAndIcon();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !isLoginInProgress)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                btnLogin.PerformClick();
            }
        }

        // ================== Toggle hiển thị/ẩn mật khẩu ==================

        private void picEyeToggle_MouseDown(object sender, MouseEventArgs e)
        {
            _showPassword = true;
            UpdateMaskAndIcon();
            FocusPasswordAtEnd();
        }

        private void picEyeToggle_MouseUp(object sender, MouseEventArgs e)
        {
            _showPassword = false;
            UpdateMaskAndIcon();
            FocusPasswordAtEnd();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn muốn thoát chương trình?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                this.Hide(); // Ẩn frm_Login thay vì Application.Exit()
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (isLoginInProgress) return;

            isLoginInProgress = true;
            isMessageShown = false;
            btnLogin.Enabled = false;

            try
            {
                string username = txtTenDangNhap.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ username và password!", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string encryptedPassword = cls_Encryption.Encrypt(password);
                string maND = cls_EmployeeManagement.CheckLogin(username, encryptedPassword);

                if (maND != "ERROR")
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thành công",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide(); // Ẩn frm_Login thay vì Close
                    frm_Main mainForm = new frm_Main(maND); // Tạo instance mới của frm_Main
                    mainForm.FormClosed += (s, args) =>
                    {
                        if (!this.IsDisposed) // Kiểm tra frm_Login chưa bị dispose
                            this.Show(); // Hiển thị lại frm_Login khi frm_Main đóng
                    };
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                if (!isMessageShown)
                {
                    isMessageShown = true;
                    if (ex.Number == 50002)
                    {
                        MessageBox.Show("Tài khoản của bạn đã bị vô hiệu hóa.", "Lỗi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (ex.Number == 50001)
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
                btnLogin.Enabled = true;
                txtTenDangNhap.Focus();
            }
        }

        private void pnlRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPosition = e.Location;
            }
        }

        private void pnlRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = e.X - lastCursorPosition.X;
                int deltaY = e.Y - lastCursorPosition.Y;
                this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);
            }
        }

        private void pnlRight_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
    }
}