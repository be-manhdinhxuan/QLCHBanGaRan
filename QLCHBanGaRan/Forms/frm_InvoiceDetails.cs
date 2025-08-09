using CrystalDecisions.CrystalReports.Engine;
using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_InvoiceDetails : Form
    {
        private string maHD;

        public frm_InvoiceDetails(string maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
        }

        public string _maHD
        {
            get { return maHD; }
            set { maHD = value; }
        }

        private void frm_InvoiceDetails_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maHD))
            {
                MessageBox.Show("Không tìm thấy mã hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            try
            {
                // Log để kiểm tra
                Console.WriteLine($"Loading invoice details for MaHD: {maHD}");

                // Khởi tạo báo cáo
                Report.rp_InvoiceDetails report = new Report.rp_InvoiceDetails();
                if (report == null)
                {
                    MessageBox.Show("Không thể khởi tạo báo cáo rp_InvoiceDetails.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Truyền tham số maHD vào báo cáo
                report.SetParameterValue("mahd", maHD);
                Console.WriteLine($"Parameter 'mahd' set to: {maHD}");

                // Hiển thị báo cáo
                rpInvoice.ReportSource = report;
                rpInvoice.Refresh();
                Console.WriteLine("Report loaded successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}