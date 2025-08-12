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
    public partial class frm_Personnel : Form
    {
        public frm_Personnel()
        {
            InitializeComponent();
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.Controls.ContainsKey("UC_PersonnelManager"))
            {
                UC_PersonnelManager personnelManager = new UC_PersonnelManager();
                personnelManager.Dock = DockStyle.Fill;
                frm_Main.Instance.Controls.Add(personnelManager);
            }
            frm_Main.Instance.Controls["UC_PersonnelManager"].BringToFront();
        }

        private void btnHoSoNhanVien_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.Controls.ContainsKey("UC_ProfilePersonnelManager"))
            {
                UC_ProfilePersonnelManager profilePersonnelManager = new UC_ProfilePersonnelManager();
                profilePersonnelManager.Dock = DockStyle.Fill;
                frm_Main.Instance.Controls.Add(profilePersonnelManager);
            }
            frm_Main.Instance.Controls["UC_ProfilePersonnelManager"].BringToFront();
        }


    }
}