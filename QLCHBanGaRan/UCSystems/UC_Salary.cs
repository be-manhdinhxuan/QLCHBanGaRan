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
            bool isAdmin = QLCHBanGaRan.lib.cls_EmployeeManagement.CheckPermission(QLCHBanGaRan.Forms.frm_Main.NguoiDungID);
            if (!isAdmin)
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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

        private void UC_Salary_Load(object sender, EventArgs e)
        {
            bool isAdmin = QLCHBanGaRan.lib.cls_EmployeeManagement.CheckPermission(QLCHBanGaRan.Forms.frm_Main.NguoiDungID);
            btnQuanLyChucDanh.Visible = isAdmin;
        }

        private void btnThongKeChamCong_Click(object sender, EventArgs e)
        {
            string controlName = "UC_TimeSheetManager";
            bool isAdmin = QLCHBanGaRan.lib.cls_EmployeeManagement.CheckPermission(QLCHBanGaRan.Forms.frm_Main.NguoiDungID);
            if (!isAdmin)
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!frm_Main.Instance.pnlContainer.Controls.ContainsKey(controlName))
            {
                UC_TimeSheetManager timeSheetManager = new UC_TimeSheetManager(); // Truyền maND
                timeSheetManager.Name = controlName;
                timeSheetManager.Dock = DockStyle.Fill;
                frm_Main.Instance.pnlContainer.Controls.Add(timeSheetManager);
            }

            frm_Main.Instance.pnlContainer.Controls[controlName].BringToFront();
        }
    }
}