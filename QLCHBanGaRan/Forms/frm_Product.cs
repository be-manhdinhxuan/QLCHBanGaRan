
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
    public partial class frm_Product : Form
    {
        public frm_Product()
        {
            InitializeComponent();
        }

        private void btnQuanLyMonAn_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                // Kiểm tra xem frm_FoodManager đã mở chưa
                bool foodManagerFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_FoodManager)
                    {
                        child.BringToFront();
                        child.Activate();
                        foodManagerFound = true;
                        break;
                    }
                }

                // Nếu chưa mở, tạo mới và thêm vào MDI
                if (!foodManagerFound)
                {
                    frm_FoodManager foodManager = new frm_FoodManager();
                    foodManager.MdiParent = frm_Main.Instance; // Liên kết với frm_Main như MDI parent
                    foodManager.Text = "Quản lý món ăn"; // Đặt tiêu đề cho tab
                    foodManager.WindowState = FormWindowState.Normal;
                    foodManager.Size = new Size(1000, 750); // Kích thước mặc định
                    foodManager.StartPosition = FormStartPosition.CenterParent;

                    // Tạo tab cho form
                    frm_Main.Instance.CreateTabForForm(foodManager, "Quản lý món ăn");

                    foodManager.Show();
                    foodManager.Activate();
                }
            }
        }

        private void btnQuanLyNuocUong_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool drinkManagerFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_DrinkManager) // Giả định chuyển UC_DrinkManager thành frm_DrinkManager
                    {
                        child.BringToFront();
                        child.Activate();
                        drinkManagerFound = true;
                        break;
                    }
                }

                if (!drinkManagerFound)
                {
                    frm_DrinkManager drinkManager = new frm_DrinkManager();
                    drinkManager.MdiParent = frm_Main.Instance;
                    drinkManager.Text = "Quản lý nước uống";
                    drinkManager.WindowState = FormWindowState.Normal;
                    drinkManager.Size = new Size(1000, 750);
                    drinkManager.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(drinkManager, "Quản lý nước uống");

                    drinkManager.Show();
                    drinkManager.Activate();
                }
            }
        }

        private void btnQuanLyNCC_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool nccManagerFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_NCCManager) // Giả định chuyển UC_NCCManager thành frm_NCCManager
                    {
                        child.BringToFront();
                        child.Activate();
                        nccManagerFound = true;
                        break;
                    }
                }

                if (!nccManagerFound)
                {
                    frm_NCCManager nccManager = new frm_NCCManager();
                    nccManager.MdiParent = frm_Main.Instance;
                    nccManager.Text = "Quản lý nhà cung cấp";
                    nccManager.WindowState = FormWindowState.Normal;
                    nccManager.Size = new Size(1000, 750);
                    nccManager.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(nccManager, "Quản lý nhà cung cấp");

                    nccManager.Show();
                    nccManager.Activate();
                }
            }
        }
    }
}