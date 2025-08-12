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
    public partial class frm_RpSalary : Form
    {
        public frm_RpSalary()
        {
            InitializeComponent();
        }
        private void frm_RpSalary_Load(object sender, EventArgs e)
        {
            // Đặt tháng mặc định là tháng hiện tại (07/2025)
            dtpThang.Value = new DateTime(2025, 7, 1); // Hoặc dùng DateTime.Now với năm hiện tại

            // Lấy danh sách chức danh
            DataTable dtTitles = QLCHBanGaRan.lib.cls_EmployeeManagement.GetChucDanh();
            DataRow dr = dtTitles.NewRow();
            dr["MaChucDanh"] = "-1"; // Giả sử ID cho "Tất cả"
            dr["TenChucDanh"] = "Tất cả";
            dtTitles.Rows.InsertAt(dr, 0);
            cmbChucDanh.ValueMember = "MaChucDanh";
            cmbChucDanh.DisplayMember = "TenChucDanh";
            cmbChucDanh.DataSource = dtTitles;

            // Đảm bảo chọn "Tất cả" mặc định
            cmbChucDanh.SelectedIndex = 0; // Chọn mục đầu tiên ("Tất cả")

            // Đăng ký sự kiện cho dtpThang
            this.dtpThang.ValueChanged += new System.EventHandler(this.dtpThang_ValueChanged);

            // Load báo cáo mặc định
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                Report.rp_Salary r = new Report.rp_Salary();
                string maChucDanh = cmbChucDanh.SelectedValue?.ToString() ?? "-1"; // Xử lý null

                // Lấy dữ liệu từ ChamCongTheoNgay dựa trên tháng và chức danh
                int thang = int.Parse(dtpThang.Value.ToString("yyyyMM")); // Chuyển thành YYYYMM (ví dụ: 202507)
                DataTable dtSalaryData = QLCHBanGaRan.lib.cls_EmployeeManagement.GetSalaryData(thang, maChucDanh);

                // Kiểm tra cột MaChucDanh
                if (!dtSalaryData.Columns.Contains("MaChucDanh"))
                {
                    MessageBox.Show("Cột MaChucDanh không tồn tại trong dữ liệu. Vui lòng kiểm tra query.", "Lỗi cấu trúc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataTable filteredDt = dtSalaryData.Clone(); // Sao chép cấu trúc

                if (maChucDanh == "-1")
                {
                    // Lấy toàn bộ dữ liệu cho tháng đã chọn
                    foreach (DataRow row in dtSalaryData.Rows)
                    {
                        filteredDt.ImportRow(row);
                    }
                }
                else
                {
                    // Lọc theo MaChucDanh
                    foreach (DataRow row in dtSalaryData.Rows)
                    {
                        if (row["MaChucDanh"] != DBNull.Value && row["MaChucDanh"].ToString() == maChucDanh)
                        {
                            filteredDt.ImportRow(row);
                        }
                    }
                }

                // Gán dữ liệu
                r.SetDataSource(filteredDt);

                // Đặt giá trị tham số
                r.SetParameterValue("MaChucDanh", maChucDanh);
                r.SetParameterValue("TenChucDanh", cmbChucDanh.Text);
                r.SetParameterValue("Thang", dtpThang.Value.ToString("yyyyMM")); // Đảm bảo khớp với định nghĩa
                r.SetParameterValue("ThangKe", dtpThang.Value.ToString("MM/yyyy"));

                rpProfile.ReportSource = r;
                rpProfile.Zoom(75);
                rpProfile.Refresh(); // Đảm bảo làm mới báo cáo
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message + "\nStackTrace: " + ex.StackTrace, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbChucDanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gọi phương thức load báo cáo khi thay đổi chức danh
            LoadReport();
        }

        private void dtpThang_ValueChanged(object sender, EventArgs e)
        {
            // Cập nhật báo cáo khi thay đổi tháng
            LoadReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Forms.frm_Main.Instance.Controls["frm_Report"].BringToFront();
        }
    }
}