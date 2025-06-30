using System;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_ChangePassword : Form
    {
        public frm_ChangePassword()
        {
            InitializeComponent();
        }

        private void frm_ChangePassword_Load(object sender, EventArgs e)
        {
            // Khởi tạo form (nếu cần)
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Đóng form khi nhấn nút Close
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các TextBox
            string oldPassword = txtMatKhauCu.Text.Trim();
            string newPassword = txtMatKhauMoi.Text.Trim();
            string confirmPassword = txtNhapLai.Text.Trim();

            // Kiểm tra các trường rỗng
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra mật khẩu cũ (giả định kiểm tra với cơ sở dữ liệu hoặc tài khoản hiện tại)
            if (!VerifyOldPassword(oldPassword)) // Phương thức giả định
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra mật khẩu mới và nhập lại
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu mới và nhập lại không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thay đổi mật khẩu (giả định cập nhật vào cơ sở dữ liệu)
            if (ChangePassword(newPassword)) // Phương thức giả định
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức giả định để kiểm tra mật khẩu cũ (thay bằng logic thực tế)
        private bool VerifyOldPassword(string oldPassword)
        {
            // Thay bằng logic kiểm tra với cơ sở dữ liệu hoặc tài khoản hiện tại
            // Ví dụ: Kiểm tra với mật khẩu lưu trong session hoặc database
            string currentPassword = "oldpassword123"; // Giả định
            return oldPassword == currentPassword;
        }

        // Phương thức giả định để thay đổi mật khẩu (thay bằng logic thực tế)
        private bool ChangePassword(string newPassword)
        {
            // Thay bằng logic cập nhật mật khẩu vào cơ sở dữ liệu
            // Ví dụ: Gọi API hoặc truy vấn SQL
            return true; // Giả định thành công
        }
    }
}