using CrystalDecisions.CrystalReports.Engine;
using QLCHBanGaRan.Forms;
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
        private DataTable tbOrder;

        public UC_Order()
        {
            InitializeComponent();
            InitializeDatabase();
            InitializeOrderTable(); // Khởi tạo DataTable riêng
        }

        private void InitializeDatabase()
        {
            // Không cần khởi tạo SqlConnection trực tiếp, sử dụng cls_DatabaseManager
        }

        private void InitializeOrderTable()
        {
            tbOrder = new DataTable();
            tbOrder.Columns.Add("MaSP", typeof(string));
            tbOrder.Columns.Add("TenSP", typeof(string));
            tbOrder.Columns.Add("SoLuong", typeof(int));
            tbOrder.Columns.Add("GiaTien", typeof(decimal));
            tbOrder.Columns.Add("GiamGia", typeof(decimal)).AllowDBNull = true;
            tbOrder.Columns["GiamGia"].ReadOnly = false; // Đảm bảo cột GiamGia không chỉ đọc
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
                // Đảm bảo cột GiamGia không bị khóa chỉ đọc
                if (dt.Columns.Contains("GiamGia"))
                    dt.Columns["GiamGia"].ReadOnly = false;
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
                ConfigureDataGridViewColumns(dtSearch); // Cấu hình cột cho dtSearch
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
            decimal giamGiaPhanTram = Convert.ToDecimal(dtSearch.SelectedRows[0].Cells["GiamGia"].Value); // Phần trăm giảm giá từ CSDL

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
                    // Tính lại giảm giá dựa trên phần trăm
                    decimal giamGiaMoi = (giaTien * giamGiaPhanTram / 100) * (currentSoLuong + 1);
                    tbOrder.Rows[temp]["GiamGia"] = giamGiaMoi;
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
                // Tính giảm giá ban đầu dựa trên phần trăm
                dtr["GiamGia"] = (giaTien * giamGiaPhanTram / 100);
                tbOrder.Rows.Add(dtr);
            }

            dtChoose.DataSource = tbOrder;
            ConfigureDataGridViewColumns(dtChoose); // Cấu hình cột cho dtChoose
            dtChoose.Refresh(); // Làm mới giao diện

            UpdatePaymentInfo();
        }

        private void ConfigureDataGridViewColumns(DataGridView dataGridView)
        {
            if (dataGridView.Columns.Contains("MaSP"))
            {
                dataGridView.Columns["MaSP"].HeaderText = "Mã SP";
                dataGridView.Columns["MaSP"].Width = 70;
            }
            if (dataGridView.Columns.Contains("TenSP"))
            {
                dataGridView.Columns["TenSP"].HeaderText = "Tên SP";
                dataGridView.Columns["TenSP"].Width = 150;
            }
            if (dataGridView.Columns.Contains("GiaTien"))
            {
                dataGridView.Columns["GiaTien"].HeaderText = "Giá Tiền";
                dataGridView.Columns["GiaTien"].Width = 100;
            }
            if (dataGridView.Columns.Contains("SoLuong"))
            {
                dataGridView.Columns["SoLuong"].HeaderText = "Số Lượng";
                dataGridView.Columns["SoLuong"].Width = 80;
            }
            if (dataGridView.Columns.Contains("GiamGia"))
            {
                dataGridView.Columns["GiamGia"].HeaderText = "Giảm Giá";
                dataGridView.Columns["GiamGia"].Width = 100;
                dataGridView.Columns["GiamGia"].ReadOnly = false; // Đảm bảo cột GiamGia có thể chỉnh sửa (dù bạn muốn bỏ qua)
            }
            dataGridView.ReadOnly = false; // Đảm bảo toàn bộ DataGridView không chỉ đọc
            dataGridView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2; // Cho phép chỉnh sửa khi gõ phím
        }

        private void UpdatePaymentInfo()
        {
            decimal tongTien = _sumPrice(tbOrder, "GiaTien");
            decimal giamGiaTong = _sumPrice(tbOrder, "GiamGia");
            decimal tongCong = tongTien - giamGiaTong; // Tính tổng cộng bằng cách trừ giảm giá

            txtMoney.Text = tongTien.ToString("N0"); // Hiển thị tổng tiền gốc
            txtDiscount.Text = giamGiaTong.ToString("N0"); // Hiển thị tổng giảm giá
            lblTotalMoney.Text = tongCong.ToString("N0") + " VND"; // Hiển thị tổng tiền sau khi giảm
        }

        private void UC_Order_Load(object sender, EventArgs e)
        {
            idEmployess = Forms.frm_Main.NguoiDungID; // Giả định frm_Main tồn tại
            string[] employeeInfo = cls_EmployeeManagement.GetEmployeeInfo(idEmployess);
            tenNV = employeeInfo[1];
            nhanVienID = employeeInfo[0];
            txtSearch.Focus();
            txtEmployess.Text = tenNV;
            txtEmployess.ReadOnly = true;

            InitializeOrderTable(); // Khởi tạo DataTable

            btnDone.Enabled = false;
            btnPrintInvoice.Enabled = false;
            txtMoney.ReadOnly = true;
            txtDiscount.ReadOnly = true;
            txtReturnPayment.ReadOnly = true;

            dtSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtChoose.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtSearch.DataSource = LoadProducts("");
            ConfigureDataGridViewColumns(dtSearch); // Cấu hình cột cho dtSearch
            dtChoose.DataSource = tbOrder; // Gán DataSource sau khi khởi tạo
            ConfigureDataGridViewColumns(dtChoose); // Cấu hình cột cho dtChoose

            dtChoose.CellValueChanged += dtChoose_CellValueChanged;
            dtChoose.EditingControlShowing += dtChoose_EditingControlShowing;
        }

        private void dtChoose_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dtChoose.CurrentCell.ColumnIndex == dtChoose.Columns["GiamGia"].Index && e.Control is TextBox)
            {
                TextBox tb = e.Control as TextBox;
                tb.KeyPress += (s, ev) =>
                {
                    if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar) && ev.KeyChar != '.')
                    {
                        ev.Handled = true; // Chỉ cho phép số và dấu chấm
                    }
                    // Giới hạn một dấu chấm
                    if (ev.KeyChar == '.' && tb.Text.Contains("."))
                    {
                        ev.Handled = true;
                    }
                };
            }
        }

        private void dtChoose_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dtChoose.Columns["GiamGia"].Index)
            {
                if (decimal.TryParse(dtChoose.Rows[e.RowIndex].Cells["GiamGia"].EditedFormattedValue.ToString(), out decimal newGiamGia))
                {
                    decimal giaTien = Convert.ToDecimal(dtChoose.Rows[e.RowIndex].Cells["GiaTien"].Value);
                    int soLuong = Convert.ToInt32(dtChoose.Rows[e.RowIndex].Cells["SoLuong"].Value);
                    decimal maxGiamGia = giaTien * soLuong;

                    if (newGiamGia > maxGiamGia)
                    {
                        tbOrder.Rows[e.RowIndex]["GiamGia"] = maxGiamGia;
                        MessageBox.Show("Giảm giá không được vượt quá tổng giá tiền!");
                    }
                    else
                    {
                        tbOrder.Rows[e.RowIndex]["GiamGia"] = newGiamGia;
                    }

                    dtChoose.Rows[e.RowIndex].Cells["GiamGia"].Value = tbOrder.Rows[e.RowIndex]["GiamGia"];
                    UpdatePaymentInfo();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập giá trị giảm giá hợp lệ (số)!");
                    dtChoose.Rows[e.RowIndex].Cells["GiamGia"].Value = 0;
                    tbOrder.Rows[e.RowIndex]["GiamGia"] = 0;
                }
            }
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
                    tbOrder.Rows[index]["GiamGia"] = Convert.ToDecimal(tbOrder.Rows[index]["GiamGia"]) - (Convert.ToDecimal(dtSearch.SelectedRows[0].Cells["GiaTien"].Value) * Convert.ToDecimal(dtSearch.SelectedRows[0].Cells["GiamGia"].Value) / 100);
                }
                else
                {
                    tbOrder.Rows.RemoveAt(index);
                }

                dtChoose.DataSource = tbOrder;
                ConfigureDataGridViewColumns(dtChoose);
                dtChoose.Refresh();
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

            // Làm mới dữ liệu trong dtSearch sau khi hoàn tất
            dtSearch.DataSource = LoadProducts(txtSearch.Text);
            ConfigureDataGridViewColumns(dtSearch);
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

            // Lấy mã chi tiết hóa đơn lớn nhất hiện có
            string maxMaChiTietHD = "CTHD000";
            try
            {
                string query = "SELECT ISNULL(MAX(MaChiTietHD), 'CTHD000') FROM ChiTietHoaDon";
                object result = cls_DatabaseManager.ExecuteScalar(query);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                    maxMaChiTietHD = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy mã chi tiết hóa đơn lớn nhất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Tăng dần mã chi tiết hóa đơn
            int ctIndex = int.Parse(maxMaChiTietHD.Substring(4)) + 1; // Lấy số từ "CTHDxxx" và tăng lên
            foreach (DataRow row in tbOrder.Rows)
            {
                string maChiTietHD = $"CTHD{ctIndex:D3}"; // Định dạng 3 chữ số (ví dụ: CTHD001)
                string maSP = row["MaSP"].ToString();
                chiTietHoaDon.Rows.Add(maChiTietHD, maSP.StartsWith("M") ? maSP : (object)DBNull.Value, maSP.StartsWith("DU") ? maSP : (object)DBNull.Value, Convert.ToInt32(row["SoLuong"]));
                ctIndex++; // Tăng chỉ số cho bản ghi tiếp theo
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

                    // Làm mới dữ liệu trong dtSearch sau khi lưu thành công
                    //dtSearch.DataSource = LoadProducts(txtSearch.Text);
                    //ConfigureDataGridViewColumns(dtSearch);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maHD))
            {
                MessageBox.Show("Vui lòng lưu hóa đơn trước khi in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frm_PrintInvoice printForm = new frm_PrintInvoice(maHD);
            printForm.ShowDialog();
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
            dtChoose.DataSource = tbOrder;
            ConfigureDataGridViewColumns(dtChoose); // Cấu hình lại sau khi xóa
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
                txtSearch.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm...";
                txtSearch.ForeColor = System.Drawing.Color.Gray;
            }
        }

    }
}