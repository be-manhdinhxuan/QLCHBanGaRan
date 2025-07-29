using QLCHBanGaRan.Forms;
using QLCHBanGaRan.lib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLCHBanGaRan
{
    public partial class frm_Login : Form
    {
        // false = ẨN; true = HIỆN
        private bool _showPassword = false;

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

            // Anchor để icon bám mép phải khi form giãn
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            picEyeToggle.Anchor = AnchorStyles.Top | AnchorStyles.Right;

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

            // Nếu muốn toggle bằng 1 lần click:
            // picEyeToggle.Click += picEyeToggle_Click;

            btnClose.Click += btnClose_Click;

            // KHÔNG gọi BeginInvoke/refresh ở đây.
            // Đợi form hiển thị xong mới set trạng thái ban đầu.
            this.Shown += (s, e) =>
            {
                _showPassword = false;      // mặc định ẨN
                UpdateMaskAndIcon();        // chỉ set mask + icon
            };

            // Focus ban đầu
            txtTenDangNhap.Focus();
        }

        // ================== Core helpers ==================

        /// <summary>
        /// Cập nhật trạng thái hiển thị mật khẩu và icon.
        /// Không dùng BeginInvoke ở đây.
        /// </summary>
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
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
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
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
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

        // Nếu muốn toggle theo từng lần click, bỏ MouseDown/Up ở trên và dùng handler này:
        //private void picEyeToggle_Click(object sender, EventArgs e)
        //{
        //    _showPassword = !_showPassword;
        //    UpdateMaskAndIcon();
        //    FocusPasswordAtEnd();
        //}

        // ================== Đóng & Đăng nhập ==================

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
            string username = txtTenDangNhap.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ username và password!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maND = cls_EmployeeManagement.CheckLogin(username, password);

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
                MessageBox.Show("Username hoặc password không đúng!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
