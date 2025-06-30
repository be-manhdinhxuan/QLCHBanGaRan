using System;
using System.Windows.Forms;
//using CrystalDecisions.CrystalReports.Engine;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_PrintInvoice : Form
    {
        public frm_PrintInvoice()
        {
            InitializeComponent();
        }

        private void frm_PrintInvoice_Load(object sender, EventArgs e)
        {
            // Tải báo cáo hóa đơn (giả định bạn có file .rpt)
            try
            {
                //ReportDocument report = new ReportDocument();
                //report.Load(@"path\to\your\rp_PrintInvoice.rpt"); // Thay bằng đường dẫn thực tế đến file .rpt
                //rpInvoice.ReportSource = report;
                //rpInvoice.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Đóng form khi nhấn nút Close
            this.Close();
        }
    }
}