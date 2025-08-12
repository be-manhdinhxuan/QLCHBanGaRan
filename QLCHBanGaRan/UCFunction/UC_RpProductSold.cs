using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_RpProductSold : UserControl
    {
        public UC_RpProductSold()
        {
            InitializeComponent();
            // Đảm bảo dtpThang có định dạng MM/yyyy
            dtpThang.CustomFormat = "MM/yyyy";
            dtpThang.Format = DateTimePickerFormat.Custom;
            // Gán sự kiện ValueChanged cho dtpThang
            dtpThang.ValueChanged += dtpThang_ValueChanged;
            // Thiết lập giá trị mặc định cho dtpThang
            dtpThang.Value = DateTime.Now;
        }

        private void UC_RpProductSold_Load(object sender, EventArgs e)
        {
            // Load báo cáo khi form load lần đầu
            LoadReport();
        }

        private void dtpThang_ValueChanged(object sender, EventArgs e)
        {
            // Load báo cáo khi thay đổi tháng/năm
            LoadReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Quay lại màn hình chính
            Forms.frm_Main.Instance.Controls["frm_Report"].BringToFront();
        }

        private void LoadReport()
        {
            try
            {
                // Tạo báo cáo mới
                Report.rp_ProductSold r = new Report.rp_ProductSold();

                // Lấy giá trị tháng/năm từ dtpThang dưới dạng MM/yyyy cho ThangThongKe
                string selectedMonthYear = dtpThang.Value.ToString("MM/yyyy");
                r.SetParameterValue("ThangThongKe", selectedMonthYear);

                // Lọc dữ liệu dựa trên tháng và năm từ dtpThang
                int selectedMonth = dtpThang.Value.Month;
                int selectedYear = dtpThang.Value.Year;
                string selectionFormula = "{Command.Nam} = " + selectedYear + " AND {Command.Thang} = " + selectedMonth;
                r.RecordSelectionFormula = selectionFormula;

                // Gán báo cáo vào CrystalReportViewer
                rpProfile.ReportSource = r;
                rpProfile.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}