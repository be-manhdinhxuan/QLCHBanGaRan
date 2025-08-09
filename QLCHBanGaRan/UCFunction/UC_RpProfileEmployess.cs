using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_RpProfileEmployess : UserControl
    {
        public UC_RpProfileEmployess()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Quay lại trang trước đó
            if (Forms.frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_Report"))
            {
                Forms.frm_Main.Instance.pnlContainer.Controls["UC_Report"].BringToFront();
            }
            else
            {
                MessageBox.Show("Không tìm thấy trang báo cáo để quay lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            // Hiển thị báo cáo nhân viên   
        }
    }
}
