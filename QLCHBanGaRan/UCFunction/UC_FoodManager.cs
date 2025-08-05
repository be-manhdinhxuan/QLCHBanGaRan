using QLCHBanGaRan.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_FoodManager : UserControl
    {
        public event EventHandler ProductAdded;
        private int check = 0;

        public UC_FoodManager()
        {
            InitializeComponent();
        }

        public void btnBack_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, true, false, false, false);
            Forms.frm_Main.Instance.btnProduct_Click(null, null);
            ProductAdded?.Invoke(this, EventArgs.Empty);
        }

        private void _formatDT()
        {
            if (dtListProduct.Columns.Contains("MaSanPham"))
            {
                dtListProduct.Columns["MaSanPham"].HeaderText = "Mã món";
                dtListProduct.Columns["MaSanPham"].Width = 80;
            }

            if (dtListProduct.Columns.Contains("TenSanPham"))
            {
                dtListProduct.Columns["TenSanPham"].HeaderText = "Tên món";
                dtListProduct.Columns["TenSanPham"].Width = 200; // Đặt Width cố định
            }

            if (dtListProduct.Columns.Contains("MaNCC"))
                dtListProduct.Columns["MaNCC"].Width = 150;

            if (dtListProduct.Columns.Contains("TenNhaCungCap"))
            {
                dtListProduct.Columns["TenNhaCungCap"].HeaderText = "Nhà cung cấp";
                dtListProduct.Columns["TenNhaCungCap"].Width = 200; // Loại bỏ AutoSizeMode.Fill
            }

            if (dtListProduct.Columns.Contains("GiaTien"))
                dtListProduct.Columns["GiaTien"].Width = 100;

            if (dtListProduct.Columns.Contains("GiamGia"))
                dtListProduct.Columns["GiamGia"].Width = 80;

            if (dtListProduct.Columns.Contains("SoLuong"))
                dtListProduct.Columns["SoLuong"].Width = 80;

            if (dtListProduct.Columns.Contains("SoLuongDaBan"))
                dtListProduct.Columns["SoLuongDaBan"].Width = 100;
        }

        private void _reset()
        {
            txtMaMonAn.Text = "";
            txtTenMonAn.Text = "";
            txtGiaTien.Text = "";
            txtGiamGia.Text = "";
            txtSoLuong.Text = "";
            txtTimKiem.Text = "";
            txtTimKiem.Focus();
            errorProvider.Clear();
        }

        private IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                           .Concat(controls)
                           .Where(c => c.GetType() == type);
        }

        private string _genIdProduct()
        {
            DataTable dt = _getIDProduct();
            int temp = 1;

            if (dt.Rows.Count > 0)
            {
                int maxNumber = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string maMon = row["MaMon"].ToString();
                    if (maMon.StartsWith("M") && maMon.Length >= 4)
                    {
                        if (int.TryParse(maMon.Substring(1), out int num) && num > maxNumber)
                        {
                            maxNumber = num;
                        }
                    }
                }
                temp = maxNumber + 1;
            }

            if (temp < 10)
                return "M00" + temp;
            if (temp < 100)
                return "M0" + temp;
            return "M" + temp;
        }

        private DataTable _getIDProduct()
        {
            string query = "SELECT MaMon FROM DoAn WHERE IsDeleted = 0 ORDER BY MaMon";
            return cls_DatabaseManager.TableRead(query);
        }

        private void _sttButton(bool add, bool edit, bool delete, bool update, bool cancel, bool grpinfo)
        {
            btnThem.Enabled = add;
            btnSua.Enabled = edit;
            btnXoa.Enabled = delete;
            btnCapNhat.Enabled = update;
            btnHuyBo.Enabled = cancel;
            grpThongTin.Enabled = grpinfo;
        }

        private void UC_FoodManager_Load(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            LoadProductList();
            _formatDT();
            _sttButton(true, true, true, false, false, false);
            _reset();

            // Cấu hình ComboBox bộ lọc
            var items = new[] {
                new { Text = "Tên món", Value = "TenMon" }, // Sửa thành TenMon
                new { Text = "Giá tiền", Value = "GiaTien" },
                new { Text = "Giảm giá (%)", Value = "GiamGia" }
            };
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.Items.Clear();
            cmbFilter.DataSource = items;
            cmbFilter.DisplayMember = "Text";
            cmbFilter.ValueMember = "Value";
            cmbFilter.SelectedIndex = 0;
            cmbFilter.SelectedIndexChanged += (s, ev) => LoadProductList();

            DataTable nccData = cls_Product._showNCC();
            if (nccData != null && nccData.Rows.Count > 0)
            {
                cmbNhaCungCap.DropDownStyle = ComboBoxStyle.DropDownList; // Thêm để không cho phép chỉnh sửa
                cmbNhaCungCap.DataSource = nccData;
                cmbNhaCungCap.ValueMember = "MaNCC";
                cmbNhaCungCap.DisplayMember = "TenNCC";
            }
            else
            {
                MessageBox.Show("Không có dữ liệu nhà cung cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtTimKiem.TextChanged += (s, ev) => LoadProductList();
        }

        private void LoadProductList()
        {
            DataTable dt = cls_Product._showDoAn();
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu sản phẩm để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtListProduct.DataSource = null;
                return;
            }

            if (!dt.Columns.Contains("SoLuongDaBan"))
            {
                dt.Columns.Add("SoLuongDaBan", typeof(int)).DefaultValue = 0;
            }

            // Thêm cột chuỗi tạm thời cho GiaTien và GiamGia nếu chưa có
            if (!dt.Columns.Contains("GiaTienStr"))
            {
                dt.Columns.Add("GiaTienStr", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    row["GiaTienStr"] = row["GiaTien"].ToString();
                }
            }
            if (!dt.Columns.Contains("GiamGiaStr"))
            {
                dt.Columns.Add("GiamGiaStr", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    row["GiamGiaStr"] = row["GiamGia"].ToString();
                }
            }

            dtListProduct.DataSource = dt;

            if (dtListProduct.Columns.Contains("IsDeleted"))
                dtListProduct.Columns["IsDeleted"].Visible = false;
            if (dtListProduct.Columns.Contains("MaNCC"))
                dtListProduct.Columns["MaNCC"].Visible = false;
            if (dtListProduct.Columns.Contains("GiaTienStr"))
                dtListProduct.Columns["GiaTienStr"].Visible = false; // Ẩn cột
            if (dtListProduct.Columns.Contains("GiamGiaStr"))
                dtListProduct.Columns["GiamGiaStr"].Visible = false; // Ẩn cột

            if (dtListProduct.Columns.Contains("TenNhaCungCap"))
            {
                dtListProduct.Columns["TenNhaCungCap"].HeaderText = "Nhà cung cấp";
                dtListProduct.Columns["TenNhaCungCap"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            string filterColumn = "TenMon"; // Giá trị mặc định
            if (cmbFilter.SelectedValue != null)
            {
                filterColumn = cmbFilter.SelectedValue.ToString();
            }
            else if (cmbFilter.SelectedIndex >= 0 && cmbFilter.Items.Count > 0)
            {
                cmbFilter.SelectedIndex = 0;
                filterColumn = cmbFilter.SelectedValue.ToString();
            }

            string searchText = txtTimKiem.Text.Trim();

            dt.DefaultView.RowFilter = $"IsDeleted = 0";
            if (!string.IsNullOrEmpty(searchText))
            {
                if (dt.Columns.Contains(filterColumn)) // Kiểm tra cột tồn tại
                {
                    string filterColumnStr = filterColumn; // Giá trị gốc
                    if (filterColumn == "GiaTien")
                    {
                        filterColumnStr = "GiaTienStr"; // Sử dụng cột chuỗi
                    }
                    else if (filterColumn == "GiamGia")
                    {
                        filterColumnStr = "GiamGiaStr"; // Sử dụng cột chuỗi
                    }

                    if (filterColumnStr != filterColumn || dt.Columns.Contains(filterColumnStr))
                    {
                        if (filterColumn == "TenMon")
                        {
                            dt.DefaultView.RowFilter += $" AND {filterColumnStr} LIKE '%{searchText}%'";
                        }
                        else
                        {
                            dt.DefaultView.RowFilter += $" AND {filterColumnStr} LIKE '{searchText}%'";
                        }

                    }
                    else
                    {
                        MessageBox.Show($"Cột {filterColumnStr} không tồn tại để lọc. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Cột {filterColumn} không tồn tại trong DataTable. Vui lòng kiểm tra cấu hình bộ lọc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            dtListProduct.DataSource = dt.DefaultView.ToTable();
            _formatDT();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            check = 2;
            if (dtListProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _sttButton(false, false, false, true, true, true);
                txtSoLuong.Focus();

                int index = dtListProduct.CurrentCell.RowIndex;
                txtMaMonAn.Enabled = false;

                string maColumn = "MaSanPham";
                string tenColumn = "TenSanPham";

                if (dtListProduct.Columns.Contains(maColumn) && dtListProduct.Columns.Contains(tenColumn) &&
                    dtListProduct.Columns.Contains("GiaTien") && dtListProduct.Columns.Contains("MaNCC") &&
                    dtListProduct.Columns.Contains("GiamGia") && dtListProduct.Columns.Contains("SoLuong"))
                {
                    txtMaMonAn.Text = dtListProduct.Rows[index].Cells[maColumn].Value?.ToString() ?? "";
                    txtTenMonAn.Text = dtListProduct.Rows[index].Cells[tenColumn].Value?.ToString() ?? "";
                    txtGiaTien.Text = dtListProduct.Rows[index].Cells["GiaTien"].Value?.ToString() ?? "";
                    string maNCCValue = dtListProduct.Rows[index].Cells["MaNCC"].Value?.ToString() ?? "";
                    if (cmbNhaCungCap.Items.Count > 0 && !string.IsNullOrEmpty(maNCCValue))
                    {
                        cmbNhaCungCap.SelectedValue = maNCCValue;
                    }
                    else
                    {
                        cmbNhaCungCap.SelectedIndex = -1; // Không chọn nhà cung cấp nếu MaNCC là NULL
                    }
                    txtGiamGia.Text = dtListProduct.Rows[index].Cells["GiamGia"].Value?.ToString() ?? "0";
                    txtSoLuong.Text = dtListProduct.Rows[index].Cells["SoLuong"].Value?.ToString() ?? "0";
                }
                else
                {
                    string missingColumns = "";
                    if (!dtListProduct.Columns.Contains(maColumn)) missingColumns += $"{maColumn}, ";
                    if (!dtListProduct.Columns.Contains(tenColumn)) missingColumns += $"{tenColumn}, ";
                    if (!dtListProduct.Columns.Contains("GiaTien")) missingColumns += "GiaTien, ";
                    if (!dtListProduct.Columns.Contains("MaNCC")) missingColumns += "MaNCC, ";
                    if (!dtListProduct.Columns.Contains("GiamGia")) missingColumns += "GiamGia, ";
                    if (!dtListProduct.Columns.Contains("SoLuong")) missingColumns += "SoLuong, ";
                    missingColumns = missingColumns.TrimEnd(',', ' ');
                    MessageBox.Show($"Dữ liệu sản phẩm không đầy đủ. Thiếu cột: {missingColumns}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _reset();
                    _sttButton(true, true, true, false, false, false);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtListProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = dtListProduct.CurrentCell.RowIndex;
            string maColumn = "MaSanPham";
            if (!dtListProduct.Columns.Contains(maColumn))
            {
                MessageBox.Show($"Không tìm thấy cột {maColumn}. Vui lòng kiểm tra cấu hình DataGridView.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maMon = dtListProduct.Rows[index].Cells[maColumn].Value?.ToString() ?? "";

            if (string.IsNullOrEmpty(maMon))
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm có mã {maMon}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string query = "UPDATE DoAn SET IsDeleted = 1 WHERE MaMon = @MaMon";
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaMon", maMon) };
                if (cls_DatabaseManager.ExecuteNonQuery(query, parameters) > 0)
                {
                    MessageBox.Show($"Đã đánh dấu sản phẩm có mã {maMon} là đã xóa!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProductList();
                    _reset();
                }
                else
                {
                    MessageBox.Show($"Không thể đánh dấu sản phẩm có mã {maMon} là đã xóa. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                var getChildControls = GetAll(this, typeof(TextBox));
                var listOfErrors = getChildControls.Select(c => errorProvider.GetError(c))
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .ToList();
                MessageBox.Show("Vui lòng kiểm tra lại thông tin sản phẩm:\n - " + string.Join("\n - ", listOfErrors.ToArray()), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (check == 1)
                {
                    string genMaSP = _genIdProduct();
                    decimal giaTien = decimal.Parse(txtGiaTien.Text, new CultureInfo("vi-VN"));
                    bool addProduct = cls_Product._addDoAn(genMaSP, txtTenMonAn.Text, cmbNhaCungCap.SelectedValue?.ToString(), giaTien, int.Parse(txtGiamGia.Text), int.Parse(txtSoLuong.Text));

                    if (addProduct)
                    {
                        MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _reset();
                        _sttButton(true, true, true, false, false, false);
                        LoadProductList();
                        _formatDT();
                        txtTenMonAn.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm sản phẩm này. Vui lòng kiểm tra lại hoặc đảm bảo mã không trùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    decimal giaTien = decimal.Parse(txtGiaTien.Text, new CultureInfo("vi-VN"));
                    bool updateProduct = cls_Product._updateDoAn(txtMaMonAn.Text, txtTenMonAn.Text, cmbNhaCungCap.SelectedValue?.ToString(), giaTien, int.Parse(txtGiamGia.Text), int.Parse(txtSoLuong.Text));
                    if (updateProduct)
                    {
                        MessageBox.Show("Cập nhật nhà cung cấp và thông tin sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProductList();
                        _formatDT();
                        _reset();
                        txtTimKiem.Focus();
                        _sttButton(true, true, true, false, false, false);
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật sản phẩm này. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, true, false, false, false);
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Không cần xử lý Enter nữa vì đã dùng TextChanged
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            check = 1;
            _sttButton(false, false, false, true, true, true);
            txtMaMonAn.Enabled = false;
            txtMaMonAn.Text = _genIdProduct();
            txtSoLuong.Focus();
        }

        private void btnQuanLyMonAn_Click(object sender, EventArgs e)
        {
            LoadProductList();
        }

        private void txtSoLuong_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                errorProvider.SetError(txtSoLuong, "Số lượng sản phẩm không được để trống.");
                e.Cancel = true;
            }
            else if (!regex.IsMatch(txtSoLuong.Text))
            {
                errorProvider.SetError(txtSoLuong, "Số lượng sản phẩm phải là số.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtSoLuong, "");
            }
        }

        private void txtTenMonAn_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenMonAn.Text))
            {
                errorProvider.SetError(txtTenMonAn, "Tên sản phẩm không được trống.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtTenMonAn, "");
            }
        }

        private void txtGiaTien_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+(,[0-9]{1,2})?$");
            if (string.IsNullOrWhiteSpace(txtGiaTien.Text))
            {
                errorProvider.SetError(txtGiaTien, "Giá tiền không được trống");
                e.Cancel = true;
            }
            else if (!regex.IsMatch(txtGiaTien.Text.Replace(".", "")))
            {
                errorProvider.SetError(txtGiaTien, "Giá tiền sản phẩm phải là số hợp lệ (ví dụ: 25000 hoặc 25000,00).");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtGiaTien, "");
            }
        }

        private void txtGiamGia_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (string.IsNullOrWhiteSpace(txtGiamGia.Text))
            {
                errorProvider.SetError(txtGiamGia, "% giảm giá không được trống");
                e.Cancel = true;
            }
            else if (!regex.IsMatch(txtGiamGia.Text))
            {
                errorProvider.SetError(txtGiamGia, "% giảm giá phải là số.");
                e.Cancel = true;
            }
            else if (int.Parse(txtGiamGia.Text) > 100)
            {
                errorProvider.SetError(txtGiamGia, "% giảm giá không được vượt quá 100%.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtGiamGia, "");
            }
        }
    }
}