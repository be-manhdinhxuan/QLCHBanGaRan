using System;
using System.Windows.Forms;
using QLCHBanGaRan.UCFunction;
using QLCHBanGaRan.Forms;

namespace QLCHBanGaRan.UCSystems
{
    public partial class UC_Product : UserControl
    {
        public UC_Product()
        {
            InitializeComponent();
        }

        private void btnQuanLyMonAn_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_FoodManager"))
            {
                UC_FoodManager foodManager = new UC_FoodManager();
                foodManager.Dock = DockStyle.Fill;
                frm_Main.Instance.pnlContainer.Controls.Add(foodManager);
            }
            frm_Main.Instance.pnlContainer.Controls["UC_FoodManager"].BringToFront();
        }

        private void btnQuanLyNuocUong_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_DrinkManager"))
            {
                UC_DrinkManager drinkManager = new UC_DrinkManager();
                drinkManager.Dock = DockStyle.Fill;
                frm_Main.Instance.pnlContainer.Controls.Add(drinkManager);
            }
            frm_Main.Instance.pnlContainer.Controls["UC_DrinkManager"].BringToFront();
        }

        private void btnQuanLySP_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_ProductType"))
            {
                UC_ProductType productType = new UC_ProductType();
                productType.Dock = DockStyle.Fill;
                frm_Main.Instance.pnlContainer.Controls.Add(productType);
            }
            frm_Main.Instance.pnlContainer.Controls["UC_ProductType"].BringToFront();
        }

        private void btnQuanLyNCC_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_NCCManager"))
            {
                UC_NCCManager nCCManager = new UC_NCCManager();
                nCCManager.Dock = DockStyle.Fill;
                frm_Main.Instance.pnlContainer.Controls.Add(nCCManager);
            }
            frm_Main.Instance.pnlContainer.Controls["UC_NCCManager"].BringToFront();
        }
    }
}