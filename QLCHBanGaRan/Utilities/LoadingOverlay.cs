using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLCHBanGaRan.Utilities
{
    public class LoadingOverlay : Form
    {
        private PictureBox pictureBoxLoading;
        private Label lblMessage;
        private Form parent;

        public LoadingOverlay(Form parent, string message)
        {
            this.parent = parent; // Lưu tham chiếu đến form cha

            // Cấu hình form overlay
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0.7; // Độ trong suốt 70%
            this.BackColor = Color.White; // Màu nền tối
            this.ShowInTaskbar = false;
            this.Owner = parent; // Gán form cha để làm mờ

            // Tạo PictureBox cho animation loading
            pictureBoxLoading = new PictureBox
            {
                Size = new Size(50, 50),
                Location = new Point(0, 0), // Ban đầu tạm thời
                Image = Properties.Resources.loading_gif // Thay bằng file GIF animation nếu có
            };
            pictureBoxLoading.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLoading.BackColor = Color.Transparent;

            // Tạo Label cho thông báo
            lblMessage = new Label
            {
                Text = message,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(250, 20),
                Location = new Point(0, 0), // Ban đầu tạm thời
                BackColor = Color.Transparent
            };

            this.Controls.Add(pictureBoxLoading);
            this.Controls.Add(lblMessage);

            // Đảm bảo overlay luôn nằm trên form cha
            this.Deactivate += (s, e) => this.BringToFront();

            // Cập nhật vị trí và kích thước sau khi form cha được hiển thị
            this.Load += (s, e) => UpdatePositionAndSize();
        }

        private void UpdatePositionAndSize()
        {
            if (parent != null)
            {
                this.Location = parent.Location; // Đặt vị trí khớp với form cha
                this.Size = parent.Size; // Đặt kích thước khớp với form cha
                pictureBoxLoading.Location = new Point((this.Width - pictureBoxLoading.Width) / 2, (this.Height - pictureBoxLoading.Height) / 2 - 30);
                lblMessage.Location = new Point((this.Width - lblMessage.Width) / 2, (this.Height + pictureBoxLoading.Height) / 2);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.BringToFront();
            UpdatePositionAndSize(); // Đảm bảo cập nhật lại khi hiển thị
        }

        // Phương thức tùy chỉnh kích thước
        public void SetSize(int width, int height)
        {
            this.Size = new Size(width, height);
            pictureBoxLoading.Location = new Point((this.Width - pictureBoxLoading.Width) / 2, (this.Height - pictureBoxLoading.Height) / 2 - 30);
            lblMessage.Location = new Point((this.Width - lblMessage.Width) / 2, (this.Height + pictureBoxLoading.Height) / 2);
        }

        // Phương thức tùy chỉnh thông báo
        public void SetMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.Location = new Point((this.Width - lblMessage.Width) / 2, (this.Height + pictureBoxLoading.Height) / 2);
        }
    }
}