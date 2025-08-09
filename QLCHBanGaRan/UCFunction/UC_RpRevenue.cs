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
    public partial class UC_RpRevenue : UserControl
    {
        public UC_RpRevenue()
        {
            InitializeComponent();
        }
        private void UC_RpRevenue_Load(object sender, EventArgs e)
        {
            cmbFilter.DisplayMember = "Text";
            cmbFilter.ValueMember = "Value";

            var items = new[] {
                new { Text = "Theo ngày", Value = "1" },
                new { Text = "Theo tháng", Value = "2" }
            };
            cmbFilter.DataSource = items;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Report"].BringToFront();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbFilter.SelectedValue) == 1)
            {
                Report.rp_RevenueDay rd = new Report.rp_RevenueDay();
                rd.SetParameterValue("year", dtpThang.Value.Year.ToString());
                rpProfile.ReportSource = rd;
                return;
            }
            if (Convert.ToInt32(cmbFilter.SelectedValue) == 2)
            {
                Report.rp_RevenueMonth rm = new Report.rp_RevenueMonth();
                rm.SetParameterValue("year", dtpThang.Value.Year.ToString());
                rpProfile.ReportSource = rm;
                return;
            }
        }
    }
}
