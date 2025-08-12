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
    public partial class frm_RpRevenue : Form
    {
        public frm_RpRevenue()
        {
            InitializeComponent();
        }

        private void frm_RpRevenue_Load(object sender, EventArgs e)
        {
            cmbFilter.DisplayMember = "Text";
            cmbFilter.ValueMember = "Value";

            var items = new[] {
                new { Text = "Theo ngày", Value = "1" },
                new { Text = "Theo tháng", Value = "2" }
            };
            cmbFilter.DataSource = items;

            // Gắn sự kiện thay đổi
            cmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;
            dtpThang.ValueChanged += DtpThang_ValueChanged;

            // Load dữ liệu ban đầu
            LoadReport();
        }

        private void CmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void DtpThang_ValueChanged(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            if (cmbFilter.SelectedValue == null) return;

            if (Convert.ToInt32(cmbFilter.SelectedValue) == 1)
            {
                Report.rp_RevenueDay rd = new Report.rp_RevenueDay();
                rd.SetParameterValue("year", dtpThang.Value.Year.ToString());
                rpProfile.ReportSource = rd;
            }
            else if (Convert.ToInt32(cmbFilter.SelectedValue) == 2)
            {
                Report.rp_RevenueMonth rm = new Report.rp_RevenueMonth();
                rm.SetParameterValue("year", dtpThang.Value.Year.ToString());
                rpProfile.ReportSource = rm;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (frm_Main.Instance != null)
            {
                foreach (Form child in frm_Main.Instance.MdiChildren)
                {
                    if (child is frm_Report)
                    {
                        child.BringToFront();
                        child.Activate();
                        break;
                    }
                }
            }
        }
    }
}