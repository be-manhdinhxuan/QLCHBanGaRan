using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCHBanGaRan.Forms;
using QLCHBanGaRan.UCFunction;

namespace QLCHBanGaRan.UCSystems
{
    public partial class UC_Report : UserControl
    {
        public UC_Report()
        {
            InitializeComponent();
        }

        private void btnRpHoSoNV_Click(object sender, EventArgs e)
        {

            if (!Forms.frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_RpProfileAllEmployess"))
            {
                UC_RpProfileAllEmployess profileAllEmployess = new UC_RpProfileAllEmployess();
                profileAllEmployess.Dock = DockStyle.Fill;
                Forms.frm_Main.Instance.pnlContainer.Controls.Add(profileAllEmployess);
            }
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_RpProfileAllEmployess"].BringToFront();

        }

        private void btnRpChamCong_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa
            /*
            if (!Forms.frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_RpProfileEmployess"))
            {
                UC_RpProfileEmployess profileEmployess = new UC_RpProfileEmployess();
                profileEmployess.Dock = DockStyle.Fill;
                Forms.frm_Main.Instance.pnlContainer.Controls.Add(profileEmployess);
            }
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_RpProfileEmployess"].BringToFront();
            */
        }

        private void btnRpSalary_Click(object sender, EventArgs e)
        {
            
            if (!Forms.frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_RpSalary"))
            {
                UC_RpSalary rpSalary = new UC_RpSalary();
                rpSalary.Dock = DockStyle.Fill;
                Forms.frm_Main.Instance.pnlContainer.Controls.Add(rpSalary);
            }
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_RpSalary"].BringToFront();
            
        }

        private void btnRpSanPham_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa
            
            if (!Forms.frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_RpProduct"))
            {
                UC_RpProduct rpProduct = new UC_RpProduct();
                rpProduct.Dock = DockStyle.Fill;
                Forms.frm_Main.Instance.pnlContainer.Controls.Add(rpProduct);
            }
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_RpProduct"].BringToFront();
            
        }

        private void btnRpSPDaBan_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa
            /*
            if (!Forms.frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_RpProductSold"))
            {
                UC_RpProductSold rpProductSold = new UC_RpProductSold();
                rpProductSold.Dock = DockStyle.Fill;
                Forms.frm_Main.Instance.pnlContainer.Controls.Add(rpProductSold);
            }
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_RpProductSold"].BringToFront();
            */
        }

        private void btnRpDoanhThu_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa
            /*
            if (!Forms.frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_RpRevenue"))
            {
                UC_RpRevenue rpRevenue = new UC_RpRevenue();
                rpRevenue.Dock = DockStyle.Fill;
                Forms.frm_Main.Instance.pnlContainer.Controls.Add(rpRevenue);
            }
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_RpRevenue"].BringToFront();
            */
        }

        private void btnRpHoaDon_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa
            /*
            if (!Forms.frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_InvoiceDetails"))
            {
                UC_InvoiceDetails invoiceDetails = new UC_InvoiceDetails();
                invoiceDetails.Dock = DockStyle.Fill;
                Forms.frm_Main.Instance.pnlContainer.Controls.Add(invoiceDetails);
            }
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_InvoiceDetails"].BringToFront();
            */
        }

        private void btnRpSPTonKho_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa
            /*
            if (!Forms.frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_RpProductInventory"))
            {
                UC_RpProductInventory productInventory = new UC_RpProductInventory();
                productInventory.Dock = DockStyle.Fill;
                Forms.frm_Main.Instance.pnlContainer.Controls.Add(productInventory);
            }
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_RpProductInventory"].BringToFront();
            */
        }
    }
}