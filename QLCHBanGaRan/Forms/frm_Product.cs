using QLCHBanGaRan.UCFunction;
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
            if (!frm_Main.Instance.Controls.ContainsKey("UC_FoodManager"))
            {
                UC_FoodManager foodManager = new UC_FoodManager();
                foodManager.Dock = DockStyle.Fill;
                frm_Main.Instance.Controls.Add(foodManager);
            }
            frm_Main.Instance.Controls["UC_FoodManager"].BringToFront();
        }

        private void btnQuanLyNuocUong_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.Controls.ContainsKey("UC_DrinkManager"))
            {
                UC_DrinkManager drinkManager = new UC_DrinkManager();
                drinkManager.Dock = DockStyle.Fill;
                frm_Main.Instance.Controls.Add(drinkManager);
            }
            frm_Main.Instance.Controls["UC_DrinkManager"].BringToFront();
        }


        private void btnQuanLyNCC_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.Controls.ContainsKey("UC_NCCManager"))
            {
                UC_NCCManager nCCManager = new UC_NCCManager();
                nCCManager.Dock = DockStyle.Fill;
                frm_Main.Instance.Controls.Add(nCCManager);
            }
            frm_Main.Instance.Controls["UC_NCCManager"].BringToFront();
        }

    }
}