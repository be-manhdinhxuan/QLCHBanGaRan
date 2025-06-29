using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCSystems
{
    public partial class UC_Order : UserControl
    {
        private string idEmployess;
        private string tenNV;
        private string nhanVienID;
        private string maHD;
        private DataTable tbOrder = new DataTable();

        public UC_Order()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            // Không cần khởi tạo SqlConnection trực tiếp, sử dụng cls_DatabaseManager
        }

        private DataTable LoadProducts(string searchText)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT MaMon AS MaSP, TenMon AS TenSP, GiaTien, SoLuong, GiamGia FROM DoAn WHERE TenMon LIKE @SearchText + '%'" +
                               "UNION SELECT MaDoUong AS MaSP, TenDoUong AS TenSP, GiaTien, SoLuong, GiamGia FROM DoUong WHERE TenDoUong LIKE @SearchText + '%'";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SearchText", SqlDbType.NVarChar) { Value = searchText }
                };
                dt = cls_DatabaseManager.TableRead(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                dtSearch.DataSource = LoadProducts(txtSearch.Text);
                dtSearch.Columns["MaSP"].Width = 70;
                dtSearch.Columns["TenSP"].Width = 150;
                dtSearch.Columns["GiaTien"].Width = 100;
                dtSearch.Columns["SoLuong"].Width = 80;
                dtSearch.Columns["GiamGia"].Width = 100;
            }
        }

        private int _productSelect(string nameProduct, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["TenSP"].ToString() == nameProduct)
                {
                    return i;
                }
            }
            return -1;
        }

        private decimal _sumPrice(DataTable dt, string columnName)
        {
            return dt.AsEnumerable().Sum(row => Convert.ToDecimal(row[columnName]));
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (dtSearch.SelectedRows.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maSP = dtSearch.SelectedRows[0].Cells["MaSP"].Value.ToString();
            string tenSP = dtSearch.SelectedRows[0].Cells["TenSP"].Value.ToString();
            decimal giaTien = Convert.ToDecimal(dtSearch.SelectedRows[0].Cells["GiaTien"].Value);
            int soLuongTon = Convert.ToInt32(dtSearch.SelectedRows[0].Cells["SoLuong"].Value);
            int giamGia = Convert.ToInt32(dtSearch.SelectedRows[0].Cells["GiamGia"].Value);

            if (soLuongTon <= 0)
            {
                MessageBox.Show("Món bạn chọn đã hết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow dtr = tbOrder.NewRow();
            int temp = _productSelect(tenSP, tbOrder);

            if (temp != -1)
            {
                int currentSoLuong = Convert.ToInt32(tbOrder.Rows[temp]["SoLuong"]);
                if (currentSoLuong < soLuongTon)
                {
                    tbOrder.Rows[temp]["SoLuong"] = currentSoLuong + 1;
                    tbOrder.Rows[temp]["GiaTien"] = Convert.ToDecimal(tbOrder.Rows[temp]["GiaTien"]) + giaTien;
                    tbOrder.Rows[temp]["GiamGia"] = Convert.ToDecimal(tbOrder.Rows[temp]["GiamGia"]) + (giaTien * giamGia / 100);
                }
                else
                {
                    MessageBox.Show("Số lượng tồn không đủ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                dtr["MaSP"] = maSP;
                dtr["TenSP"] = tenSP;
                dtr["SoLuong"] = 1;
                dtr["GiaTien"] = giaTien;
                dtr["GiamGia"] = giaTien * giamGia / 100;
                tbOrder.Rows.Add(dtr);
            }

            dtChoose.DataSource = tbOrder.Copy();
            dtChoose.Columns["MaSP"].Width = 70;
            dtChoose.Columns["TenSP"].Width = 150;
            dtChoose.Columns["GiaTien"].Width = 100;
            dtChoose.Columns["SoLuong"].Width = 80;
            dtChoose.Columns["GiamGia"].Width = 100;

            UpdatePaymentInfo();
        }

        private void UpdatePaymentInfo()
        {
            decimal tongTien = _sumPrice(tbOrder, "GiaTien");
            decimal giamGiaTong = _sumPrice(tbOrder, "GiamGia");
            decimal tongCong = tongTien - giamGiaTong;

            txtMoney.Text = tongTien.ToString("N0");
            txtDiscount.Text = giamGiaTong.ToString("N0");
            lblTotalMoney.Text = tongCong.ToString("N0") + " VND";
        }

        private void UC_Order_Load(object sender, EventArgs e)
        {
            idEmployess = Forms.frm_Main.NguoiDungID; // Giả định frm_Main tồn tại
            // Thay cls_Employess bằng cls_EmployeeManagement
            string[] employeeInfo = cls_EmployeeManagement.GetEmployeeInfo(idEmployess);
            tenNV = employeeInfo[1];
            nhanVienID = employeeInfo[0];
            txtSearch.Focus();
            txtEmployess.Text = tenNV;
            txtEmployess.ReadOnly = true;

            tbOrder.Columns.Add("MaSP", typeof(string));
            tbOrder.Columns.Add("TenSP", typeof(string));
            tbOrder.Columns.Add("SoLuong", typeof(int));
            tbOrder.Columns.Add("GiaTien", typeof(decimal));
            tbOrder.Columns.Add("GiamGia", typeof(decimal));

            btnDone.Enabled = false;
            btnPrintInvoice.Enabled = false;
            txtMoney.ReadOnly = true;
            txtDiscount.ReadOnly = true;
            txtReturnPayment.ReadOnly = true;

            // Tự động giãn cột DataGridView
            dtSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtChoose.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void UC_Order_Resize(object sender, EventArgs e)
        {
            // Không cần set lại Width cho từng cột nữa, DataGridView sẽ tự động giãn đều
        }

        private void btnDelProduct_Click(object sender, EventArgs e)
        {
            if (dtChoose.SelectedRows.Count > 0)
            {
                int index = dtChoose.SelectedRows[0].Index;
                int soLuong = Convert.ToInt32(tbOrder.Rows[index]["SoLuong"]);
                if (soLuong > 1)
                {
                    tbOrder.Rows[index]["SoLuong"] = soLuong - 1;
                    tbOrder.Rows[index]["GiaTien"] = Convert.ToDecimal(tbOrder.Rows[index]["GiaTien"]) - Convert.ToDecimal(dtSearch.SelectedRows[0].Cells["GiaTien"].Value);
                    tbOrder.Rows[index]["GiamGia"] = Convert.ToDecimal(tbOrder.Rows[index]["GiamGia"]) - (Convert.ToDecimal(dtSearch.SelectedRows[0].Cells["GiaTien"].Value) * Convert.ToInt32(dtSearch.SelectedRows[0].Cells["GiamGia"].Value) / 100);
                }
                else
                {
                    tbOrder.Rows.RemoveAt(index);
                }

                dtChoose.DataSource = tbOrder.Copy();
                UpdatePaymentInfo();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order hoàn tất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearOrder();
            btnDone.Enabled = false;
            btnPrintInvoice.Enabled = false;
            btnCancel.Enabled = true;
            txtSearch.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Hủy đơn hàng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ClearOrder();
                btnPrintInvoice.Enabled = false;
                btnDone.Enabled = false;
                btnCancel.Enabled = true;
                txtSearch.Focus();
            }
        }

        private string _GenMaHD()
        {
            try
            {
                string query = "SELECT TOP 1 MaHD FROM HoaDon ORDER BY MaHD DESC";
                object lastMaHD = cls_DatabaseManager.ExecuteScalar(query);
                if (lastMaHD == null || string.IsNullOrEmpty(lastMaHD.ToString()))
                    return "HD001";

                int number = int.Parse(lastMaHD.ToString().Substring(2)) + 1;
                return number < 10 ? $"HD00{number}" : number < 100 ? $"HD0{number}" : $"HD{number}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo mã hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "HD001";
            }
        }

        private void btnSaveDB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || tbOrder.Rows.Count == 0 || string.IsNullOrEmpty(txtReceive.Text))
            {
                MessageBox.Show("Vui lòng kiểm tra thông tin khách hàng, danh sách món ăn và tiền khách đưa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUser.Focus();
                return;
            }

            decimal tongTien = _sumPrice(tbOrder, "GiaTien");
            decimal giamGiaTong = _sumPrice(tbOrder, "GiamGia");
            decimal tongCong = tongTien - giamGiaTong;
            decimal receive = decimal.Parse(txtReceive.Text.Replace(",", ""));

            if (receive < tongCong)
            {
                MessageBox.Show("Tiền khách đưa không đủ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            maHD = _GenMaHD();
            DataTable chiTietHoaDon = new DataTable();
            chiTietHoaDon.Columns.Add("MaChiTietHD", typeof(string));
            chiTietHoaDon.Columns.Add("MaMon", typeof(string));
            chiTietHoaDon.Columns.Add("MaDoUong", typeof(string));
            chiTietHoaDon.Columns.Add("SoLuongMua", typeof(int));

            int ctIndex = 1;
            foreach (DataRow row in tbOrder.Rows)
            {
                string maChiTietHD = $"CTHD{ctIndex:D3}";
                string maSP = row["MaSP"].ToString();
                chiTietHoaDon.Rows.Add(maChiTietHD, maSP.StartsWith("M") ? maSP : (object)DBNull.Value, maSP.StartsWith("DU") ? maSP : (object)DBNull.Value, Convert.ToInt32(row["SoLuong"]));
                ctIndex++;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_LapHoaDon", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaHD", maHD);
                    cmd.Parameters.AddWithValue("@MaNV", nhanVienID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayLapHD", DateTime.Now);
                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ChiTietHoaDon", chiTietHoaDon);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "ChiTietHoaDonType";

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu đơn hàng vào CSDL!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnPrintInvoice.Enabled = true;
                    btnDone.Enabled = true;
                    btnCancel.Enabled = false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa phần liên quan đến frm_PrintInvoice
            /*
            decimal tongTien = _sumPrice(tbOrder, "GiaTien");
            decimal giamGiaTong = _sumPrice(tbOrder, "GiamGia");
            decimal tongCong = tongTien - giamGiaTong;
            decimal receive = decimal.Parse(txtReceive.Text.Replace(",", ""));
            decimal refund = receive - tongCong;

            using (Forms.frm_PrintInvoice printInvoice = new Forms.frm_PrintInvoice())
            {
                printInvoice._numInvoice = maHD;
                printInvoice._money = tongTien.ToString("N0");
                printInvoice._refund = refund.ToString("N0");
                printInvoice._receive = receive.ToString("N0");
                printInvoice._discount = giamGiaTong.ToString("N0");
                printInvoice._total = tongCong.ToString("N0");
                printInvoice.ShowDialog();
            }
            */
            MessageBox.Show("Chức năng in hóa đơn đang được vô hiệu hóa tạm thời.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReceive.Text))
            {
                txtReturnPayment.Text = "0";
                return;
            }

            if (!decimal.TryParse(txtReceive.Text.Replace(",", ""), out decimal receive))
            {
                MessageBox.Show("Vui lòng chỉ nhập số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReceive.Clear();
                return;
            }

            decimal tongTien = _sumPrice(tbOrder, "GiaTien");
            decimal giamGiaTong = _sumPrice(tbOrder, "GiamGia");
            decimal tongCong = tongTien - giamGiaTong;
            txtReturnPayment.Text = (receive - tongCong).ToString("N0");
        }

        private void ClearOrder()
        {
            tbOrder.Clear();
            dtChoose.DataSource = tbOrder.Copy();
            txtUser.Text = "";
            txtSearch.Text = "";
            txtMoney.Text = "";
            txtReceive.Text = "";
            txtReturnPayment.Text = "";
            txtDiscount.Text = "";
            lblTotalMoney.Text = "0 VND";
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = System.Drawing.Color.Black; // Khôi phục màu text thông thường
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm...";
                txtSearch.ForeColor = System.Drawing.Color.Gray; // Đặt lại màu mờ
            }
        }
    }
}