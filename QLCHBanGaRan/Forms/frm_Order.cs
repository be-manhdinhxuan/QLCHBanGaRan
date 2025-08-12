using QLCHBanGaRan.lib;
using QLCHBanGaRan.UCSystems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_Order : Form
    {
        private string idEmployess;
        private string tenNV;
        private string nhanVienID;
        private string maHD;
        private DataTable tbOrder;

        public frm_Order()
        {
            InitializeComponent();
            if (!this.DesignMode) // Chỉ khởi tạo logic khi không phải Design Time
            {
                InitializeOrderTable(); // Khởi tạo DataTable
                InitializeControls(); // Khởi tạo các control
                this.Load += frm_Order_Load; // Gán sự kiện Load
            }
        }

        private void InitializeControls()
        {
            // Khởi tạo placeholder cho txtSearch
            txtSearch.Text = "Tìm kiếm...";
            txtSearch.ForeColor = System.Drawing.Color.Gray;

            // Đăng ký event handlers cho txtSearch
            txtSearch.Enter += txtSearch_Enter;
            txtSearch.Leave += txtSearch_Leave;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyPress += txtSearch_KeyPress;

            // Khởi tạo giá trị mặc định cho cmbFilter
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("Tên SP");
            cmbFilter.Items.Add("Giá tiền");
            cmbFilter.Items.Add("Giảm giá");
            cmbFilter.SelectedIndex = 0; // Chọn "Tên SP" làm giá trị mặc định

            // Đăng ký event handler cho cmbFilter
            cmbFilter.SelectedIndexChanged += cmbFilter_SelectedIndexChanged;

            // Đăng ký event handlers cho các button
            btnAddProduct.Click += btnAddProduct_Click;
            btnDelProduct.Click += btnDelProduct_Click;
            btnDone.Click += btnDone_Click;
            btnCancel.Click += btnCancel_Click;
            btnSaveDB.Click += btnSaveDB_Click;
            btnPrintInvoice.Click += btnPrintInvoice_Click;

            // Đăng ký event handler cho txtReceive
            txtReceive.TextChanged += txtReceive_TextChanged;
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

        private DataTable LoadProducts(string searchText, string filterType = "Tên SP")
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "";
                string whereClauseDoAn = "";
                string whereClauseDoUong = "";

                // Xây dựng điều kiện tìm kiếm dựa trên filterType
                switch (filterType)
                {
                    case "Tên SP":
                        whereClauseDoAn = "TenMon LIKE '%' + @SearchText + '%'";
                        whereClauseDoUong = "TenDoUong LIKE '%' + @SearchText + '%'";
                        break;
                    case "Giá tiền":
                        whereClauseDoAn = "CAST(GiaTien AS NVARCHAR) LIKE '%' + @SearchText + '%'";
                        whereClauseDoUong = "CAST(GiaTien AS NVARCHAR) LIKE '%' + @SearchText + '%'";
                        break;
                    case "Giảm giá":
                        whereClauseDoAn = "CAST(GiamGia AS NVARCHAR) LIKE '%' + @SearchText + '%'";
                        whereClauseDoUong = "CAST(GiamGia AS NVARCHAR) LIKE '%' + @SearchText + '%'";
                        break;
                    default:
                        whereClauseDoAn = "TenMon LIKE '%' + @SearchText + '%'";
                        whereClauseDoUong = "TenDoUong LIKE '%' + @SearchText + '%'";
                        break;
                }

                query = $"SELECT MaMon AS MaSP, TenMon AS TenSP, GiaTien, SoLuong, GiamGia FROM DoAn WHERE IsDeleted = 0 AND {whereClauseDoAn}" +
                        $" UNION SELECT MaDoUong AS MaSP, TenDoUong AS TenSP, GiaTien, SoLuong, GiamGia FROM DoUong WHERE IsDeleted = 0 AND {whereClauseDoUong}";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SearchText", SqlDbType.NVarChar) { Value = searchText }
                };
                dt = cls_DatabaseManager.TableRead(query, parameters);
                if (dt.Columns.Contains("GiamGia"))
                    dt.Columns["GiamGia"].ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        // Thêm phương thức làm mới danh sách sản phẩm
        public void RefreshProductList()
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
            {
                string filterType = cmbFilter.SelectedItem?.ToString() ?? "Tên SP";
                dtSearch.DataSource = LoadProducts("", filterType); // Tải lại toàn bộ sản phẩm chưa xóa
                ConfigureDataGridViewColumns(dtSearch); // Cấu hình cột
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtSearch.Text == "Tìm kiếm...")
                {
                    return;
                }

                string filterType = cmbFilter.SelectedItem?.ToString() ?? "Tên SP";
                dtSearch.DataSource = LoadProducts(txtSearch.Text, filterType);
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
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
            {
                try
                {
                    if (dtSearch.SelectedRows.Count == 0)
                    {
                        MessageBox.Show("Bạn chưa chọn món! Vui lòng click vào một hàng trong danh sách sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var selectedRow = dtSearch.SelectedRows[0];

                    if (selectedRow.Cells["MaSP"].Value == null ||
                        selectedRow.Cells["TenSP"].Value == null ||
                        selectedRow.Cells["GiaTien"].Value == null ||
                        selectedRow.Cells["SoLuong"].Value == null ||
                        selectedRow.Cells["GiamGia"].Value == null)
                    {
                        MessageBox.Show("Dữ liệu sản phẩm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string maSP = selectedRow.Cells["MaSP"].Value.ToString();
                    string tenSP = selectedRow.Cells["TenSP"].Value.ToString();
                    decimal giaTien = Convert.ToDecimal(selectedRow.Cells["GiaTien"].Value);
                    int soLuongTon = Convert.ToInt32(selectedRow.Cells["SoLuong"].Value);
                    decimal giamGiaPhanTram = Convert.ToDecimal(selectedRow.Cells["GiamGia"].Value);

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
                        dtr["GiamGia"] = (giaTien * giamGiaPhanTram / 100);
                        tbOrder.Rows.Add(dtr);
                    }

                    dtChoose.DataSource = tbOrder;
                    ConfigureDataGridViewColumns(dtChoose);
                    dtChoose.Refresh();

                    UpdatePaymentInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfigureDataGridViewColumns(DataGridView dataGridView)
        {
            if (dataGridView.Columns.Contains("MaSP"))
            {
                dataGridView.Columns["MaSP"].HeaderText = "Mã SP";
                dataGridView.Columns["MaSP"].Width = 70;
                dataGridView.Columns["MaSP"].ReadOnly = true;
            }
            if (dataGridView.Columns.Contains("TenSP"))
            {
                dataGridView.Columns["TenSP"].HeaderText = "Tên SP";
                dataGridView.Columns["TenSP"].Width = 150;
                dataGridView.Columns["TenSP"].ReadOnly = true;
            }
            if (dataGridView.Columns.Contains("GiaTien"))
            {
                dataGridView.Columns["GiaTien"].HeaderText = "Giá Tiền";
                dataGridView.Columns["GiaTien"].Width = 100;
                dataGridView.Columns["GiaTien"].ReadOnly = true;
            }
            if (dataGridView.Columns.Contains("SoLuong"))
            {
                dataGridView.Columns["SoLuong"].HeaderText = "Số Lượng";
                dataGridView.Columns["SoLuong"].Width = 80;
                dataGridView.Columns["SoLuong"].ReadOnly = true;
            }
            if (dataGridView.Columns.Contains("GiamGia"))
            {
                dataGridView.Columns["GiamGia"].HeaderText = "Giảm Giá %";
                dataGridView.Columns["GiamGia"].Width = 100;
                dataGridView.Columns["GiamGia"].ReadOnly = true;
            }
            dataGridView.ReadOnly = true;
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void UpdatePaymentInfo()
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
            {
                decimal tongTien = _sumPrice(tbOrder, "GiaTien");
                decimal giamGiaTong = _sumPrice(tbOrder, "GiamGia");
                decimal tongCong = tongTien - giamGiaTong;

                txtMoney.Text = tongTien.ToString("N0");
                txtDiscount.Text = giamGiaTong.ToString("N0");
                lblTotalMoney.Text = tongCong.ToString("N0") + " VND";
            }
        }

        private void frm_Order_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
            {
                idEmployess = Forms.frm_Main.NguoiDungID;
                string[] employeeInfo = cls_EmployeeManagement.GetEmployeeInfo(idEmployess);
                tenNV = employeeInfo[1];
                nhanVienID = employeeInfo[0];
                txtEmployess.Text = tenNV;
                txtEmployess.ReadOnly = true;

                btnDone.Enabled = false;
                btnPrintInvoice.Enabled = false;
                txtMoney.ReadOnly = true;
                txtDiscount.ReadOnly = true;
                txtReturnPayment.ReadOnly = true;
                btnSaveDB.Enabled = true;

                dtSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtChoose.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dtSearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtSearch.MultiSelect = false;
                dtSearch.AllowUserToAddRows = false;
                dtSearch.AllowUserToDeleteRows = false;
                dtSearch.ReadOnly = true;

                dtChoose.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtChoose.MultiSelect = false;
                dtChoose.AllowUserToAddRows = false;
                dtChoose.AllowUserToDeleteRows = false;
                dtChoose.ReadOnly = true;

                RefreshProductList(); // Tải dữ liệu ban đầu
                dtChoose.DataSource = tbOrder;
                ConfigureDataGridViewColumns(dtSearch);
                ConfigureDataGridViewColumns(dtChoose);
            }
        }

        private void btnDelProduct_Click(object sender, EventArgs e)
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
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
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
            {
                MessageBox.Show("Order hoàn tất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearOrder();
                btnDone.Enabled = false;
                btnPrintInvoice.Enabled = false;
                btnCancel.Enabled = true;
                btnSaveDB.Enabled = true;

                RefreshProductList();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
            {
                DialogResult result = MessageBox.Show("Hủy đơn hàng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ClearOrder();
                    btnPrintInvoice.Enabled = false;
                    btnDone.Enabled = false;
                    btnCancel.Enabled = true;
                    btnSaveDB.Enabled = true;
                }
            }
        }

        private string _GenMaHD()
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
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
            return "HD001"; // Giá trị mặc định trong Design Time
        }

        private void btnSaveDB_Click(object sender, EventArgs e)
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
            {
                btnSaveDB.Enabled = false;

                if (string.IsNullOrEmpty(txtUser.Text) || tbOrder.Rows.Count == 0 || string.IsNullOrEmpty(txtReceive.Text))
                {
                    MessageBox.Show("Vui lòng kiểm tra thông tin khách hàng, danh sách món ăn và tiền khách đưa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUser.Focus();
                    btnSaveDB.Enabled = true;
                    return;
                }

                decimal tongTien = _sumPrice(tbOrder, "GiaTien");
                decimal giamGiaTong = _sumPrice(tbOrder, "GiamGia");
                decimal tongCong = tongTien - giamGiaTong;
                decimal receive = decimal.Parse(txtReceive.Text.Replace(",", ""));

                if (receive < tongCong)
                {
                    MessageBox.Show("Tiền khách đưa không đủ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSaveDB.Enabled = true;
                    return;
                }

                maHD = _GenMaHD();
                DataTable chiTietHoaDon = new DataTable();
                chiTietHoaDon.Columns.Add("MaChiTietHD", typeof(string));
                chiTietHoaDon.Columns.Add("MaMon", typeof(string));
                chiTietHoaDon.Columns.Add("MaDoUong", typeof(string));
                chiTietHoaDon.Columns.Add("SoLuongMua", typeof(int));

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
                    btnSaveDB.Enabled = true;
                    return;
                }

                int ctIndex = int.Parse(maxMaChiTietHD.Substring(4)) + 1;
                foreach (DataRow row in tbOrder.Rows)
                {
                    string maChiTietHD = $"CTHD{ctIndex:D3}";
                    string maSP = row["MaSP"].ToString();
                    chiTietHoaDon.Rows.Add(maChiTietHD, maSP.StartsWith("M") ? maSP : (object)DBNull.Value, maSP.StartsWith("DU") ? maSP : (object)DBNull.Value, Convert.ToInt32(row["SoLuong"]));
                    ctIndex++;
                }

                try
                {
                    using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("sp_LapHoaDon", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaHD", maHD);
                        cmd.Parameters.AddWithValue("@MaNV", nhanVienID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgayLapHD", DateTime.Now);
                        cmd.Parameters.AddWithValue("@TenKhachHang", txtUser.Text);
                        cmd.Parameters.AddWithValue("@TienKhachDua", receive);
                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ChiTietHoaDon", chiTietHoaDon);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "ChiTietHoaDonType";

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã lưu đơn hàng vào CSDL!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnPrintInvoice.Enabled = true;
                        btnDone.Enabled = true;
                        btnCancel.Enabled = false;

                        RefreshProductList();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSaveDB.Enabled = true;
                    return;
                }

                btnSaveDB.Enabled = false;
            }
        }

        private void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
            {
                if (string.IsNullOrEmpty(maHD))
                {
                    MessageBox.Show("Vui lòng lưu hóa đơn trước khi in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (frm_PrintInvoice printInvoice = new frm_PrintInvoice(maHD))
                {
                    printInvoice._maHD = maHD;
                    printInvoice.ShowDialog();
                }
            }
        }

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
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
        }

        private void ClearOrder()
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
            {
                tbOrder.Clear();
                dtChoose.DataSource = tbOrder;
                ConfigureDataGridViewColumns(dtChoose);
                txtUser.Text = "";
                txtSearch.Text = "Tìm kiếm...";
                txtSearch.ForeColor = System.Drawing.Color.Gray;
                txtMoney.Text = "";
                txtReceive.Text = "";
                txtReturnPayment.Text = "";
                txtDiscount.Text = "";
                lblTotalMoney.Text = "0 VND";
            }
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
            {
                if (txtSearch.Text == "Tìm kiếm...")
                {
                    return;
                }

                string filterType = cmbFilter.SelectedItem?.ToString() ?? "Tên SP";
                dtSearch.DataSource = LoadProducts(txtSearch.Text, filterType);
                ConfigureDataGridViewColumns(dtSearch);
            }
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.DesignMode) // Chỉ thực thi khi không phải Design Time
            {
                string searchText = txtSearch.Text == "Tìm kiếm..." ? "" : txtSearch.Text;
                string filterType = cmbFilter.SelectedItem?.ToString() ?? "Tên SP";
                dtSearch.DataSource = LoadProducts(searchText, filterType);
                ConfigureDataGridViewColumns(dtSearch);
            }
        }
    }
}