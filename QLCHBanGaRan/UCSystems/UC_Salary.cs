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
            string controlName = "UC_TimeSheetEmployee";

            // Giả định frm_Main có thuộc tính CurrentMaND để truyền vào UC_TimeSheetEmployee
            string maND = frm_Main.Instance.CurrentMaND; // Điều chỉnh nguồn maND nếu khác
            if (string.IsNullOrEmpty(maND))
            {
                MessageBox.Show("Vui lòng đăng nhập để sử dụng chức năng chấm công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey(controlName))
            {
                UC_TimeSheetEmployee timeSheetEmployee = new UC_TimeSheetEmployee(maND); // Truyền maND
                timeSheetEmployee.Name = controlName;
                timeSheetEmployee.Dock = DockStyle.Fill;
                frm_Main.Instance.pnlContainer.Controls.Add(timeSheetEmployee);
            }

            frm_Main.Instance.pnlContainer.Controls[controlName].BringToFront();
        }
    }
}