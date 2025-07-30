using CrystalDecisions.CrystalReports.Engine;
using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_PrintInvoice : Form
    {
        private string maHD;

        public frm_PrintInvoice(string maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
        }

        public string _maHD
        {
            get { return maHD; }
            set { maHD = value; }
        }

        private void frm_PrintInvoice_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maHD))
            {
                MessageBox.Show("Không tìm thấy mã hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            try
            {
                // Khởi tạo báo cáo
                Report.rp_PrintInvoice report = new Report.rp_PrintInvoice();
                if (report == null)
                {
                    MessageBox.Show("Không thể khởi tạo báo cáo rp_PrintInvoice.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Truyền tham số maHD vào báo cáo
                report.SetParameterValue("mahd", maHD);

                // Không cần gán DataTable vì báo cáo sử dụng command
                // Chỉ dựa vào tham số mahd để lấy dữ liệu từ CSDL

                // Hiển thị báo cáo
                rpInvoice.ReportSource = report;
                rpInvoice.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}