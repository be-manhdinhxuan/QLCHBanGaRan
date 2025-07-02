using System;
using System.Windows.Forms;
using QLCHBanGaRan.Forms;
using QLCHBanGaRan.UCFunction;

namespace QLCHBanGaRan.UCSystems
{
    public partial class UC_Salary : UserControl
    {
        public UC_Salary()
        {
            InitializeComponent();
        }

        private void btnQuanLyChucDanh_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_SalaryManager"))
            {
                UC_SalaryManager salaryManager = new UC_SalaryManager();
                salaryManager.Dock = DockStyle.Fill;
                frm_Main.Instance.pnlContainer.Controls.Add(salaryManager);
            }
            frm_Main.Instance.pnlContainer.Controls["UC_SalaryManager"].BringToFront();
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_TimeSheetManager"))
            {
                UC_TimeSheetManager timeSheetManager = new UC_TimeSheetManager();
                timeSheetManager.Dock = DockStyle.Fill;
                frm_Main.Instance.pnlContainer.Controls.Add(timeSheetManager);
            }
            frm_Main.Instance.pnlContainer.Controls["UC_TimeSheetManager"].BringToFront();
        }
    }
}