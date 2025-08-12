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
    public partial class frm_Report : Form
    {
        public frm_Report()
        {
            InitializeComponent();
        }

        private void btnRpHoSoNV_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool profileAllEmployessFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_RpProfileAllEmployess)
                    {
                        child.BringToFront();
                        child.Activate();
                        profileAllEmployessFound = true;
                        break;
                    }
                }

                if (!profileAllEmployessFound)
                {
                    frm_RpProfileAllEmployess profileAllEmployess = new frm_RpProfileAllEmployess();
                    profileAllEmployess.MdiParent = frm_Main.Instance;
                    profileAllEmployess.Text = "Báo cáo hồ sơ nhân viên";
                    profileAllEmployess.WindowState = FormWindowState.Normal;
                    profileAllEmployess.Size = new Size(1000, 750);
                    profileAllEmployess.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(profileAllEmployess, "Báo cáo hồ sơ nhân viên");

                    profileAllEmployess.Show();
                    profileAllEmployess.Activate();
                }
            }
        }

        private void btnRpChamCong_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool timeSheetFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_RpTimeSheet)
                    {
                        child.BringToFront();
                        child.Activate();
                        timeSheetFound = true;
                        break;
                    }
                }

                if (!timeSheetFound)
                {
                    frm_RpTimeSheet timeSheet = new frm_RpTimeSheet();
                    timeSheet.MdiParent = frm_Main.Instance;
                    timeSheet.Text = "Báo cáo chấm công";
                    timeSheet.WindowState = FormWindowState.Normal;
                    timeSheet.Size = new Size(1000, 750);
                    timeSheet.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(timeSheet, "Báo cáo chấm công");

                    timeSheet.Show();
                    timeSheet.Activate();
                }
            }
        }

        private void btnRpSalary_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool rpSalaryFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_RpSalary)
                    {
                        child.BringToFront();
                        child.Activate();
                        rpSalaryFound = true;
                        break;
                    }
                }

                if (!rpSalaryFound)
                {
                    frm_RpSalary rpSalary = new frm_RpSalary();
                    rpSalary.MdiParent = frm_Main.Instance;
                    rpSalary.Text = "Báo cáo lương";
                    rpSalary.WindowState = FormWindowState.Normal;
                    rpSalary.Size = new Size(1000, 750);
                    rpSalary.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(rpSalary, "Báo cáo lương");

                    rpSalary.Show();
                    rpSalary.Activate();
                }
            }
        }

        private void btnRpSanPham_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool rpProductFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_RpProduct)
                    {
                        child.BringToFront();
                        child.Activate();
                        rpProductFound = true;
                        break;
                    }
                }

                if (!rpProductFound)
                {
                    frm_RpProduct rpProduct = new frm_RpProduct();
                    rpProduct.MdiParent = frm_Main.Instance;
                    rpProduct.Text = "Báo cáo sản phẩm";
                    rpProduct.WindowState = FormWindowState.Normal;
                    rpProduct.Size = new Size(1000, 750);
                    rpProduct.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(rpProduct, "Báo cáo sản phẩm");

                    rpProduct.Show();
                    rpProduct.Activate();
                }
            }
        }

        private void btnRpSPDaBan_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool rpProductSoldFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_RpProductSold)
                    {
                        child.BringToFront();
                        child.Activate();
                        rpProductSoldFound = true;
                        break;
                    }
                }

                if (!rpProductSoldFound)
                {
                    frm_RpProductSold rpProductSold = new frm_RpProductSold();
                    rpProductSold.MdiParent = frm_Main.Instance;
                    rpProductSold.Text = "Báo cáo sản phẩm đã bán";
                    rpProductSold.WindowState = FormWindowState.Normal;
                    rpProductSold.Size = new Size(1000, 750);
                    rpProductSold.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(rpProductSold, "Báo cáo sản phẩm đã bán");

                    rpProductSold.Show();
                    rpProductSold.Activate();
                }
            }
        }

        private void btnRpDoanhThu_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool rpRevenueFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_RpRevenue)
                    {
                        child.BringToFront();
                        child.Activate();
                        rpRevenueFound = true;
                        break;
                    }
                }

                if (!rpRevenueFound)
                {
                    frm_RpRevenue rpRevenue = new frm_RpRevenue();
                    rpRevenue.MdiParent = frm_Main.Instance;
                    rpRevenue.Text = "Báo cáo doanh thu";
                    rpRevenue.WindowState = FormWindowState.Normal;
                    rpRevenue.Size = new Size(1000, 750);
                    rpRevenue.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(rpRevenue, "Báo cáo doanh thu");

                    rpRevenue.Show();
                    rpRevenue.Activate();
                }
            }
        }

        private void btnRpHoaDon_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool rpInvoiceDetailsFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_RpInvoiceDetails)
                    {
                        child.BringToFront();
                        child.Activate();
                        rpInvoiceDetailsFound = true;
                        break;
                    }
                }

                if (!rpInvoiceDetailsFound)
                {
                    frm_RpInvoiceDetails invoiceDetails = new frm_RpInvoiceDetails();
                    invoiceDetails.MdiParent = frm_Main.Instance;
                    invoiceDetails.Text = "Báo cáo hóa đơn";
                    invoiceDetails.WindowState = FormWindowState.Normal;
                    invoiceDetails.Size = new Size(1000, 750);
                    invoiceDetails.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(invoiceDetails, "Báo cáo hóa đơn");

                    invoiceDetails.Show();
                    invoiceDetails.Activate();
                }
            }
        }

        private void btnRpSPTonKho_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool rpProductInventoryFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_RpProductInventory)
                    {
                        child.BringToFront();
                        child.Activate();
                        rpProductInventoryFound = true;
                        break;
                    }
                }

                if (!rpProductInventoryFound)
                {
                    frm_RpProductInventory productInventory = new frm_RpProductInventory();
                    productInventory.MdiParent = frm_Main.Instance;
                    productInventory.Text = "Báo cáo tồn kho";
                    productInventory.WindowState = FormWindowState.Normal;
                    productInventory.Size = new Size(1000, 750);
                    productInventory.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(productInventory, "Báo cáo tồn kho");

                    productInventory.Show();
                    productInventory.Activate();
                }
            }
        }
    }
}