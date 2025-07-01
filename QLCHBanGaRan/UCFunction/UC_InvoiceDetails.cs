using QLCHBanGaRan.lib;
using System;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_InvoiceDetails : UserControl
    {
        public UC_InvoiceDetails()
        {
            InitializeComponent();
            LoadInvoiceList();
        }

        private void LoadInvoiceList()
        {
            try
            {
                dtList.DataSource = cls_Report._searchInvoice(""); // Tải toàn bộ danh sách
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Report"].BringToFront();
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchInvoices();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            SearchInvoices();
        }

        private void SearchInvoices()
        {
            try
            {
                dtList.DataSource = cls_Report._searchInvoice(txtTimKiem.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtList.Columns[e.ColumnIndex].Name == "TongTien" && e.Value != null && e.Value != DBNull.Value)
            {
                e.Value = string.Format("{0:N0}", e.Value);
                e.FormattingApplied = true;
            }
            else if (dtList.Columns[e.ColumnIndex].Name == "NgayLapHD" && e.Value != null && e.Value != DBNull.Value)
            {
                e.Value = ((DateTime)e.Value).ToString("dd/MM/yyyy HH:mm");
                e.FormattingApplied = true;
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dtList.SelectedRows.Count > 0)
            {
                string maHD = dtList.SelectedRows[0].Cells["MaHD"].Value.ToString();
                using (Forms.frm_InvoiceDetails invoiceDetails = new Forms.frm_InvoiceDetails())
                {
                    invoiceDetails.numReceipt = maHD;
                    invoiceDetails.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}