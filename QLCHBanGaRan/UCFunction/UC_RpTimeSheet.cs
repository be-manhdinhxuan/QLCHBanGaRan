using QLCHBanGaRan.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_RpTimeSheet : UserControl
    {
        public UC_RpTimeSheet()
        {
            InitializeComponent();
            LoadNhanVienComboBox(); // Gọi hàm để nạp dữ liệu vào combobox nhân viên
        }

        private void UC_RpTimeSheet_Load(object sender, EventArgs e)
        {
            btnHienThi_Click(sender, e);
            // Thiết lập định dạng ngày tháng cho DateTimePicker
            dtpThang.Format = DateTimePickerFormat.Custom;
            dtpThang.CustomFormat = "MM/yyyy"; // Hiển thị tháng và năm
            dtpThang.Value = DateTime.Now; // Mặc định là tháng hiện tại
        }

        private void LoadNhanVienComboBox()
        {
            string query = "SELECT MaNV, TenNV FROM NhanVien WHERE IsDeleted = 0 ORDER BY TenNV";
            DataTable dtNhanVien = cls_DatabaseManager.TableRead(query);
            cmbFilterNV.DropDownStyle = ComboBoxStyle.DropDownList; // Đặt kiểu dropdown list
            cmbFilterNV.Items.Clear(); // Xóa các mục cũ trong combobox
            cmbFilterNV.DataSource = dtNhanVien;
            cmbFilterNV.DisplayMember = "TenNV"; // Hiển thị tên nhân viên
            cmbFilterNV.ValueMember = "MaNV";    // Giá trị thực sự là mã nhân viên
            cmbFilterNV.SelectedIndex = -1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Quay lại trang trước đó
            if (Forms.frm_Main.Instance.Controls.ContainsKey("frm_Report"))
            {
                Forms.frm_Main.Instance.Controls["frm_Report"].BringToFront();
            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            Report.rp_TimeSheet report = new Report.rp_TimeSheet();
            string thang = dtpThang.Value.ToString("yyyyMM"); // Định dạng yyyyMM (ví dụ: "202508")
            report.SetParameterValue("Thang", thang); // Truyền tham số tháng dưới dạng yyyyMM
            report.SetParameterValue("MaNV", cmbFilterNV.SelectedValue ?? DBNull.Value); // Truyền mã nhân viên đã chọn, nếu không có thì truyền DBNull
            rpProfile.ReportSource = report; // Gán nguồn dữ liệu cho CrystalReportViewer
            rpProfile.Refresh(); // Làm mới CrystalReportViewer để hiển thị dữ liệu
        }
    }
}
