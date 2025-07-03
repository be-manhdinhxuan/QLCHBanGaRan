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
                // Tải báo cáo
                ReportDocument report = new ReportDocument();
                report.Load(@"D:\source\repos\BTL\QLCHBanGaRan\InvoiceReport.rpt");

                // Lấy dữ liệu từ CSDL
                string queryHoaDon = "SELECT MaHD, MaNV, NgayLapHD, TongTien FROM HoaDon WHERE MaHD = @MaHD";
                SqlParameter[] parametersHoaDon = new SqlParameter[] { new SqlParameter("@MaHD", maHD) };
                DataTable dtHoaDon = cls_DatabaseManager.TableRead(queryHoaDon, parametersHoaDon);

                // Kiểm tra dữ liệu
                if (dtHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn với mã: " + maHD, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Gán dữ liệu vào Dataset
                InvoiceDataset dataset = new InvoiceDataset();
                if (dtHoaDon.Rows.Count > 0)
                {
                    DataRow row = dtHoaDon.Rows[0];
                    decimal tongTien = row["TongTien"] != DBNull.Value ? row.Field<decimal>("TongTien") : 0;
                    dataset.HoaDon.AddHoaDonRow(
                        row.Field<string>("MaHD"),
                        row.Field<string>("MaNV"),
                        row.Field<DateTime>("NgayLapHD"),
                        tongTien
                    );
                }

                // Gán Dataset vào báo cáo
                report.SetDataSource(dataset);

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