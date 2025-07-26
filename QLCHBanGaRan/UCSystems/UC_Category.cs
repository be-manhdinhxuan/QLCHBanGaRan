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
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_DeletedSuppliers"))
            {
                UC_DeletedSuppliers deletedSuppliers = new UC_DeletedSuppliers();
                deletedSuppliers.Dock = DockStyle.Fill;
                deletedSuppliers.Name = "UC_DeletedSuppliers"; // Gán tên rõ ràng
                frm_Main.Instance.pnlContainer.Controls.Add(deletedSuppliers);
            }

            Control uc = frm_Main.Instance.pnlContainer.Controls["UC_DeletedSuppliers"];
            uc.Visible = true; // Đảm bảo hiển thị
            uc.BringToFront();
        }

        private void btnManageDeletedEmployees_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_DeletedEmployees"))
            {
                UC_DeletedEmployees deletedEmployees = new UC_DeletedEmployees();
                deletedEmployees.Dock = DockStyle.Fill;
                deletedEmployees.Name = "UC_DeletedEmployees"; // Gán tên rõ ràng
                frm_Main.Instance.pnlContainer.Controls.Add(deletedEmployees);
            }

            Control uc = frm_Main.Instance.pnlContainer.Controls["UC_DeletedEmployees"];
            uc.Visible = true; // Đảm bảo hiển thị
            uc.BringToFront();
        }

        private void btnManageDeletedPositions_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_DeletedPositions"))
            {
                UC_DeletedPositions deletedPositions = new UC_DeletedPositions();
                deletedPositions.Dock = DockStyle.Fill;
                deletedPositions.Name = "UC_DeletedPositions"; // Gán tên rõ ràng
                frm_Main.Instance.pnlContainer.Controls.Add(deletedPositions);
            }

            Control uc = frm_Main.Instance.pnlContainer.Controls["UC_DeletedPositions"];
            uc.Visible = true; // Đảm bảo hiển thị
            uc.BringToFront();
        }

        private void btnManageDeletedInvoices_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_DeletedInvoices"))
            {
                UC_DeletedInvoices deletedInvoices = new UC_DeletedInvoices();
                deletedInvoices.Dock = DockStyle.Fill;
                deletedInvoices.Name = "UC_DeletedInvoices"; // Gán tên rõ ràng
                frm_Main.Instance.pnlContainer.Controls.Add(deletedInvoices);
            }

            Control uc = frm_Main.Instance.pnlContainer.Controls["UC_DeletedInvoices"];
            uc.Visible = true; // Đảm bảo hiển thị
            uc.BringToFront();
        }


    }
}