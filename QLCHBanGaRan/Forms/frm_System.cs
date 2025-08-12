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
    public partial class frm_System : Form
    {
        public frm_System()
        {
            InitializeComponent();
        }

        private void btnQuanLyNguoiDung_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.Controls.ContainsKey("UC_UserManager"))
            {
                UC_UserManager userManager = new UC_UserManager();
                userManager.Dock = DockStyle.Fill;
                frm_Main.Instance.Controls.Add(userManager);
            }
            frm_Main.Instance.Controls["UC_UserManager"].BringToFront();
        }

    }
}
