
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
    public partial class frm_Category : Form
    {
        public frm_Category()
        {
            InitializeComponent();
        }

        private void btnManageDeletedProducts_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool deletedProductsFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_DeletedProducts)
                    {
                        child.BringToFront();
                        child.Activate();
                        deletedProductsFound = true;
                        break;
                    }
                }

                if (!deletedProductsFound)
                {
                    frm_DeletedProducts deletedProducts = new frm_DeletedProducts();
                    deletedProducts.MdiParent = frm_Main.Instance;
                    deletedProducts.Text = "Quản lý sản phẩm đã xóa";
                    deletedProducts.WindowState = FormWindowState.Normal;
                    deletedProducts.Size = new Size(1000, 750);
                    deletedProducts.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(deletedProducts, "Quản lý sản phẩm đã xóa");

                    deletedProducts.Show();
                    deletedProducts.Activate();
                }
            }
        }

        private void btnManageDeletedSuppliers_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool deletedSuppliersFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_DeletedSuppliers)
                    {
                        child.BringToFront();
                        child.Activate();
                        deletedSuppliersFound = true;
                        break;
                    }
                }

                if (!deletedSuppliersFound)
                {
                    frm_DeletedSuppliers deletedSuppliers = new frm_DeletedSuppliers();
                    deletedSuppliers.MdiParent = frm_Main.Instance;
                    deletedSuppliers.Text = "Quản lý nhà cung cấp đã xóa";
                    deletedSuppliers.WindowState = FormWindowState.Normal;
                    deletedSuppliers.Size = new Size(1000, 750);
                    deletedSuppliers.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(deletedSuppliers, "Quản lý nhà cung cấp đã xóa");

                    deletedSuppliers.Show();
                    deletedSuppliers.Activate();
                }
            }
        }

        private void btnManageDeletedEmployees_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool deletedEmployeesFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_DeletedEmployees)
                    {
                        child.BringToFront();
                        child.Activate();
                        deletedEmployeesFound = true;
                        break;
                    }
                }

                if (!deletedEmployeesFound)
                {
                    frm_DeletedEmployees deletedEmployees = new frm_DeletedEmployees();
                    deletedEmployees.MdiParent = frm_Main.Instance;
                    deletedEmployees.Text = "Quản lý nhân viên đã xóa";
                    deletedEmployees.WindowState = FormWindowState.Normal;
                    deletedEmployees.Size = new Size(1000, 750);
                    deletedEmployees.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(deletedEmployees, "Quản lý nhân viên đã xóa");

                    deletedEmployees.Show();
                    deletedEmployees.Activate();
                }
            }
        }

        private void btnManageDeletedPositions_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool deletedPositionsFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_DeletedPositions)
                    {
                        child.BringToFront();
                        child.Activate();
                        deletedPositionsFound = true;
                        break;
                    }
                }

                if (!deletedPositionsFound)
                {
                    frm_DeletedPositions deletedPositions = new frm_DeletedPositions();
                    deletedPositions.MdiParent = frm_Main.Instance;
                    deletedPositions.Text = "Quản lý chức danh đã xóa";
                    deletedPositions.WindowState = FormWindowState.Normal;
                    deletedPositions.Size = new Size(1000, 750);
                    deletedPositions.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(deletedPositions, "Quản lý chức danh đã xóa");

                    deletedPositions.Show();
                    deletedPositions.Activate();
                }
            }
        }

        private void btnManageDeletedInvoices_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool deletedInvoicesFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_DeletedInvoices)
                    {
                        child.BringToFront();
                        child.Activate();
                        deletedInvoicesFound = true;
                        break;
                    }
                }

                if (!deletedInvoicesFound)
                {
                    frm_DeletedInvoices deletedInvoices = new frm_DeletedInvoices();
                    deletedInvoices.MdiParent = frm_Main.Instance;
                    deletedInvoices.Text = "Quản lý hóa đơn đã xóa";
                    deletedInvoices.WindowState = FormWindowState.Normal;
                    deletedInvoices.Size = new Size(1000, 750);
                    deletedInvoices.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(deletedInvoices, "Quản lý hóa đơn đã xóa");

                    deletedInvoices.Show();
                    deletedInvoices.Activate();
                }
            }
        }
    }
}