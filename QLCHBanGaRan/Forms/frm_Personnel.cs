
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
    public partial class frm_Personnel : Form
    {
        public frm_Personnel()
        {
            InitializeComponent();
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool personnelManagerFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_PersonnelManager)
                    {
                        child.BringToFront();
                        child.Activate();
                        personnelManagerFound = true;
                        break;
                    }
                }

                if (!personnelManagerFound)
                {
                    frm_PersonnelManager personnelManager = new frm_PersonnelManager();
                    personnelManager.MdiParent = frm_Main.Instance;
                    personnelManager.Text = "Quản lý nhân viên";
                    personnelManager.WindowState = FormWindowState.Normal;
                    personnelManager.Size = new Size(1000, 750);
                    personnelManager.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(personnelManager, "Quản lý nhân viên");

                    personnelManager.Show();
                    personnelManager.Activate();
                }
            }
        }

        private void btnHoSoNhanVien_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                bool profilePersonnelManagerFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_ProfilePersonnelManager)
                    {
                        child.BringToFront();
                        child.Activate();
                        profilePersonnelManagerFound = true;
                        break;
                    }
                }

                if (!profilePersonnelManagerFound)
                {
                    frm_ProfilePersonnelManager profilePersonnelManager = new frm_ProfilePersonnelManager();
                    profilePersonnelManager.MdiParent = frm_Main.Instance;
                    profilePersonnelManager.Text = "Hồ sơ nhân viên";
                    profilePersonnelManager.WindowState = FormWindowState.Normal;
                    profilePersonnelManager.Size = new Size(1000, 750);
                    profilePersonnelManager.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(profilePersonnelManager, "Hồ sơ nhân viên");

                    profilePersonnelManager.Show();
                    profilePersonnelManager.Activate();
                }
            }
        }
    }
}