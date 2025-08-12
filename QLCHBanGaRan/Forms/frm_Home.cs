using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_Home : Form
    {
        public frm_Home()
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

            this.Resize += frm_Home_Resize;
            this.Load += frm_Home_Load;
        }

        private void frm_Home_Resize(object sender, EventArgs e)
        {
            panelTitle.Width = this.Width;
            labelTitle.Width = this.Width;

            // panelBanner và pictureBoxBanner sẽ tự động điều chỉnh nhờ Dock = Fill
        }

        private void frm_Home_Load(object sender, EventArgs e)
        {
            // Đảm bảo kích thước được set đúng ngay khi load
            frm_Home_Resize(sender, e);
        }
    }
}