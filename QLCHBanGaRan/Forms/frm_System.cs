
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
            if (frm_Main.Instance != null)
            {
                bool userManagerFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_UserManager)
                    {
                        child.BringToFront();
                        child.Activate();
                        userManagerFound = true;
                        break;
                    }
                }

                if (!userManagerFound)
                {
                    frm_UserManager userManager = new frm_UserManager();
                    userManager.MdiParent = frm_Main.Instance;
                    userManager.Text = "Quản lý người dùng";
                    userManager.WindowState = FormWindowState.Normal;
                    userManager.Size = new Size(1000, 750);
                    userManager.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(userManager, "Quản lý người dùng");

                    userManager.Show();
                    userManager.Activate();
                }
            }
        }

    }
}
