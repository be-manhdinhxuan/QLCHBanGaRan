using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCSystems
{
    public partial class UC_Home : UserControl
    {
        public UC_Home()
        {
            InitializeComponent();

            // Khởi tạo MaterialSkin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Red800, // Gần với (228, 0, 42)
                Primary.Red900,
                Primary.Red500,
                Accent.Red200,
                TextShade.WHITE
            );

            // Tùy chỉnh thêm nếu cần
            this.BackColor = Color.White; // Đảm bảo background đồng nhất
            this.ForeColor = materialSkinManager.ColorScheme.PrimaryColor; // Đồng bộ màu chữ

            // Đảm bảo panelTitle và pictureBoxBanner tự điều chỉnh khi UC_Home thay đổi kích thước
            this.Resize += UC_Home_Resize;
            this.Load += UC_Home_Load;
        }

        private void UC_Home_Resize(object sender, EventArgs e)
        {
            // Cập nhật chiều rộng panelTitle và labelTitle theo UC_Home
            panelTitle.Width = this.Width;
            labelTitle.Width = this.Width;

            // panelBanner và pictureBoxBanner sẽ tự động điều chỉnh nhờ Dock = Fill
        }

        private void UC_Home_Load(object sender, EventArgs e)
        {
            // Đảm bảo kích thước được set đúng ngay khi load
            UC_Home_Resize(sender, e);
        }
    }
}