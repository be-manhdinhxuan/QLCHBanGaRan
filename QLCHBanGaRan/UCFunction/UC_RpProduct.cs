using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_RpProduct : UserControl
    {
        public UC_RpProduct()
        {
            InitializeComponent();
        }

        private void UC_RpProduct_Load(object sender, EventArgs e)
        {
            // Tạo DataTable thủ công cho loại sản phẩm
            DataTable dt = new DataTable();
            dt.Columns.Add("LoaiSPID", typeof(string)); // Sử dụng string để tránh lỗi kiểu dữ liệu
            dt.Columns.Add("TenLoaiSP", typeof(string));

            // Thêm dữ liệu thủ công
            DataRow drAll = dt.NewRow();
            drAll["LoaiSPID"] = "-1";
            drAll["TenLoaiSP"] = "Tất cả";
            dt.Rows.Add(drAll);

            DataRow drDoAn = dt.NewRow();
            drDoAn["LoaiSPID"] = "1";
            drDoAn["TenLoaiSP"] = "Đồ ăn";
            dt.Rows.Add(drDoAn);

            DataRow drDoUong = dt.NewRow();
            drDoUong["LoaiSPID"] = "2";
            drDoUong["TenLoaiSP"] = "Đồ uống";
            dt.Rows.Add(drDoUong);

            cmbLoaiSP.ValueMember = "LoaiSPID";
            cmbLoaiSP.DisplayMember = "TenLoaiSP";
            cmbLoaiSP.DataSource = dt;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Forms.frm_Main.Instance.Controls["frm_Report"].BringToFront();
        }

        private void cmbLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            Report.rp_Product r = new Report.rp_Product();
            string selectedValue = cmbLoaiSP.SelectedValue?.ToString() ?? "-1";
            switch (selectedValue)
            {
                case "-1": // Tất cả
                    r.RecordSelectionFormula = ""; // Không có điều kiện
                    break;
                case "1": // Đồ ăn
                    r.RecordSelectionFormula = "{Command.LoaiSanPham} = \"Đồ ăn\""; 
                    break;
                case "2": // Đồ uống
                    r.RecordSelectionFormula = "{Command.LoaiSanPham} = \"Đồ uống\"";
                    break;
            }
            r.SetParameterValue("LoaiSPID", cmbLoaiSP.Text);
            rpProfile.ReportSource = r;
            
        }
    }
}