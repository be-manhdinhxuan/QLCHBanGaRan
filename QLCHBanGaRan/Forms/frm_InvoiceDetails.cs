using System;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_InvoiceDetails : Form
    {
        public frm_InvoiceDetails()
        {
            InitializeComponent();
        }

        public string numReceipt { get; set; } // Add this property to resolve the error

        private void frm_InvoiceDetails_Load(object sender, EventArgs e)
        {
            // Tạm thời không có logic báo cáo
            // Bạn có thể thêm placeholder hoặc thông báo nếu cần
            // MessageBox.Show("Chức năng chi tiết hóa đơn đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Đóng form khi nhấn nút Close
            this.Close();
        }
    }
}