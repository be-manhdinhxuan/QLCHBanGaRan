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
    public partial class frm_RpProductInventory : Form
    {
        public frm_RpProductInventory()
        {
            InitializeComponent();
        }

        private void frm_RpProductInventory_Load(object sender, EventArgs e)
        {
            // Tạo danh sách loại sản phẩm thủ công
            cmbLoaiSP.Items.Add("Tất cả");
            cmbLoaiSP.Items.Add("Đồ ăn");
            cmbLoaiSP.Items.Add("Đồ uống");
            cmbLoaiSP.SelectedIndex = 0;

            // Load báo cáo ban đầu
            LoadReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Forms.frm_Main.Instance.Controls["frm_Report"].BringToFront();
        }

        private void cmbLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                Report.rp_ProductInventory r = new Report.rp_ProductInventory();
                string loaiSanPham = cmbLoaiSP.SelectedItem.ToString();

                // Truyền tham số TenSP vào báo cáo (hiển thị loại sản phẩm)
                r.SetParameterValue("TenSP", loaiSanPham);

                // Lọc báo cáo dựa trên LoaiSanPham
                if (loaiSanPham != "Tất cả")
                {
                    r.RecordSelectionFormula = "{Command.LoaiSanPham} = '" + loaiSanPham + "'";
                }

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
