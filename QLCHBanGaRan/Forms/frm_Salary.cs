
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

            if (frm_Main.Instance != null)
            {
                bool salaryManagerFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_SalaryManager)
                    {
                        child.BringToFront();
                        child.Activate();
                        salaryManagerFound = true;
                        break;
                    }
                }

                if (!salaryManagerFound)
                {
                    frm_SalaryManager salaryManager = new frm_SalaryManager();
                    salaryManager.MdiParent = frm_Main.Instance;
                    salaryManager.Text = "Quản lý chức danh";
                    salaryManager.WindowState = FormWindowState.Normal;
                    salaryManager.Size = new Size(1000, 750);
                    salaryManager.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(salaryManager, "Quản lý chức danh");

                    salaryManager.Show();
                    salaryManager.Activate();
                }
            }
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            string maND = frm_Main.Instance.CurrentMaND;
            if (string.IsNullOrEmpty(maND))
            {
                MessageBox.Show("Vui lòng đăng nhập để sử dụng chức năng chấm công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (frm_Main.Instance != null)
            {
                bool timeSheetEmployeeFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_TimeSheetEmployee)
                    {
                        child.BringToFront();
                        child.Activate();
                        timeSheetEmployeeFound = true;
                        break;
                    }
                }

                if (!timeSheetEmployeeFound)
                {
                    frm_TimeSheetEmployee timeSheetEmployee = new frm_TimeSheetEmployee(maND);
                    timeSheetEmployee.MdiParent = frm_Main.Instance;
                    timeSheetEmployee.Text = "Chấm công";
                    timeSheetEmployee.WindowState = FormWindowState.Normal;
                    timeSheetEmployee.Size = new Size(1000, 750);
                    timeSheetEmployee.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(timeSheetEmployee, "Chấm công");

                    timeSheetEmployee.Show();
                    timeSheetEmployee.Activate();
                }
            }
        }

        private void frm_Salary_Load(object sender, EventArgs e)
        {
            bool isAdmin = QLCHBanGaRan.lib.cls_EmployeeManagement.CheckPermission(QLCHBanGaRan.Forms.frm_Main.NguoiDungID);
            btnQuanLyChucDanh.Visible = isAdmin;
        }

        private void btnThongKeChamCong_Click(object sender, EventArgs e)
        {
            bool isAdmin = QLCHBanGaRan.lib.cls_EmployeeManagement.CheckPermission(QLCHBanGaRan.Forms.frm_Main.NguoiDungID);
            if (!isAdmin)
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (frm_Main.Instance != null)
            {
                bool timeSheetManagerFound = false;
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_TimeSheetManager)
                    {
                        child.BringToFront();
                        child.Activate();
                        timeSheetManagerFound = true;
                        break;
                    }
                }

                if (!timeSheetManagerFound)
                {
                    frm_TimeSheetManager timeSheetManager = new frm_TimeSheetManager();
                    timeSheetManager.MdiParent = frm_Main.Instance;
                    timeSheetManager.Text = "Thống kê chấm công";
                    timeSheetManager.WindowState = FormWindowState.Normal;
                    timeSheetManager.Size = new Size(1000, 750);
                    timeSheetManager.StartPosition = FormStartPosition.CenterParent;

                    frm_Main.Instance.CreateTabForForm(timeSheetManager, "Thống kê chấm công");

                    timeSheetManager.Show();
                    timeSheetManager.Activate();
                }
            }
        }
    }
}