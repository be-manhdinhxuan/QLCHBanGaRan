using System;
using System.Windows.Forms;
using QLCHBanGaRan.Forms;
using QLCHBanGaRan.UCFunction;

namespace QLCHBanGaRan.UCSystems
{
    public partial class UC_Category : UserControl
    {
        public UC_Category()
        {
            InitializeComponent();
        }

        private void btnManageDeletedProducts_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_DeletedProducts"))
            {
                UC_DeletedProducts deletedProducts = new UC_DeletedProducts();
                deletedProducts.Dock = DockStyle.Fill;
                frm_Main.Instance.pnlContainer.Controls.Add(deletedProducts);
            }
            frm_Main.Instance.pnlContainer.Controls["UC_DeletedProducts"].BringToFront();
        }

        private void btnManageDeletedSuppliers_Click(object sender, EventArgs e)
        {
            
        }

        private void btnManageDeletedEmployees_Click(object sender, EventArgs e)
        {
            
        }

        private void btnManageDeletedPositions_Click(object sender, EventArgs e)
        {
            
        }

        private void btnManageDeletedInvoices_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}