using System;
using System.Data;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_RpProfileAllEmployess : UserControl
    {
        public UC_RpProfileAllEmployess()
        {
            InitializeComponent();
        }

        private void UC_RpProfileAllEmployess_Load(object sender, EventArgs e)
        {
            // Lấy danh sách chức danh
            DataTable dtTitles = QLCHBanGaRan.lib.cls_EmployeeTitleManagement.GetTitles();
            DataRow dr = dtTitles.NewRow();
            dr["MaChucDanh"] = "-1"; // Giả sử ID cho "Tất cả"
            dr["TenChucDanh"] = "Tất cả";
            dtTitles.Rows.InsertAt(dr, 0);
            cmbChucDanh.ValueMember = "MaChucDanh";
            cmbChucDanh.DisplayMember = "TenChucDanh";
            cmbChucDanh.DataSource = dtTitles;
        }

        private void cmbChucDanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            Report.rp_ProfileAllEmployess r = new Report.rp_ProfileAllEmployess();
            string chucDanhID = cmbChucDanh.SelectedValue?.ToString() ?? "-1"; // Xử lý null

            // Nếu chọn "Tất cả" (MaChucDanh = "-1"), lấy toàn bộ nhân viên
            if (chucDanhID == "-1")
            {
                DataTable dtAllEmployees = QLCHBanGaRan.lib.cls_EmployeeTitleManagement.GetEmployeeTitles();
                r.SetDataSource(dtAllEmployees); // Truyền toàn bộ dữ liệu
            }
            else
            {
                // Lọc nhân viên theo MaChucDanh
                DataTable dtEmployees = QLCHBanGaRan.lib.cls_EmployeeTitleManagement.GetEmployeeTitles();
                DataTable filteredDt = dtEmployees.Clone(); // Sao chép cấu trúc
                foreach (DataRow row in dtEmployees.Rows)
                {
                    if (row["MaChucDanh"].ToString() == chucDanhID)
                        filteredDt.ImportRow(row);
                }
                r.SetDataSource(filteredDt); // Truyền dữ liệu đã lọc
            }

            r.SetParameterValue("ChucDanhID", chucDanhID);
            r.SetParameterValue("TenChucDanh", cmbChucDanh.Text);
            rpProfile.ReportSource = r;
            rpProfile.Zoom(75);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Report"].BringToFront();
        }
    }
}