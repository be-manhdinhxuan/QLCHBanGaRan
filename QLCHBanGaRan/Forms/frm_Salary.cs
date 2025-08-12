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
    public partial class frm_Salary : Form
    {
        public frm_Salary()
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
            if (!frm_Main.Instance.Controls.ContainsKey("UC_SalaryManager"))
            {
                UC_SalaryManager salaryManager = new UC_SalaryManager();
                salaryManager.Dock = DockStyle.Fill;
                frm_Main.Instance.Controls.Add(salaryManager);
            }
            frm_Main.Instance.Controls["UC_SalaryManager"].BringToFront();
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

            if (!frm_Main.Instance.Controls.ContainsKey(controlName))
            {
                UC_TimeSheetEmployee timeSheetEmployee = new UC_TimeSheetEmployee(maND); // Truyền maND
                timeSheetEmployee.Name = controlName;
                timeSheetEmployee.Dock = DockStyle.Fill;
                frm_Main.Instance.Controls.Add(timeSheetEmployee);
            }

            frm_Main.Instance.Controls[controlName].BringToFront();
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

            if (!frm_Main.Instance.Controls.ContainsKey(controlName))
            {
                UC_TimeSheetManager timeSheetManager = new UC_TimeSheetManager(); // Truyền maND
                timeSheetManager.Name = controlName;
                timeSheetManager.Dock = DockStyle.Fill;
                frm_Main.Instance.Controls.Add(timeSheetManager);
            }

            frm_Main.Instance.Controls[controlName].BringToFront();
        }
    }
}