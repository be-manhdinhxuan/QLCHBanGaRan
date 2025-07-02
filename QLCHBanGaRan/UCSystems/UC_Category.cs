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
    public partial class UC_Category : UserControl
    {
        public UC_Category()
        {
            InitializeComponent();
        }

        private void btnThemKhuyenMai_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa logic, sẽ thêm sau
            /*
            using (frm_AddDiscount addDiscount = new frm_AddDiscount())
            {
                addDiscount.ShowDialog();
            }
            */
        }

        private void btnSuaKhuyenMai_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa logic, sẽ thêm sau
            /*
            using (frm_EditDiscount editDiscount = new frm_EditDiscount())
            {
                editDiscount.ShowDialog();
            }
            */
        }

        private void btnXoaKhuyenMai_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa logic, sẽ thêm sau
            /*
            using (frm_DeleteDiscount deleteDiscount = new frm_DeleteDiscount())
            {
                deleteDiscount.ShowDialog();
            }
            */
        }

        private void btnXemDanhSachKhuyenMai_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa logic, sẽ thêm sau
            /*
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_DiscountList"))
            {
                UC_DiscountList discountList = new UC_DiscountList();
                discountList.Dock = DockStyle.Fill;
                frm_Main.Instance.pnlContainer.Controls.Add(discountList);
            }
            frm_Main.Instance.pnlContainer.Controls["UC_DiscountList"].BringToFront();
            */
        }
    }
}