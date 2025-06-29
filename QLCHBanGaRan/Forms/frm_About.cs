using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_About : Form
    {
        public frm_About()
        {
            InitializeComponent();
            InitializeDynamicContent();
        }

        private void InitializeDynamicContent()
        {
            // Cập nhật thông tin động
            label3.Text = $"Phiên bản: 1.0.0 (29/06/2025)";
            textBox1.Text = $"Hệ thống Quản lý Cửa hàng bán Gà Rán được phát triển nhằm hỗ trợ quản lý hiệu quả các " +
                $"hoạt động của cửa hàng Chicken Bông. Phiên bản hiện tại được cập nhật vào ngày 29/06/2025 " +
                $"với nhiều cải tiến về giao diện và tính năng.";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Đóng form khi nhấn nút Close
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Mở liên kết Facebook khi nhấn vào label5
            string url = "https://www.facebook.com/XuanManh.Coder";
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở liên kết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Đảm bảo form được hiển thị ở trung tâm màn hình cha (nếu có)
            if (this.Owner != null)
            {
                this.Location = new Point(
                    this.Owner.Location.X + (this.Owner.Width - this.Width) / 2,
                    this.Owner.Location.Y + (this.Owner.Height - this.Height) / 2);
            }
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
    }
}