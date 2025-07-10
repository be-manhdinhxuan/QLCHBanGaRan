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
                dtListProduct.Columns["MaSanPham"].HeaderText = "Mã món";
            if (dtListProduct.Columns.Contains("TenSanPham"))
                dtListProduct.Columns["TenSanPham"].HeaderText = "Tên món";
            if (dtListProduct.Columns.Contains("MaNCC"))
                dtListProduct.Columns["MaNCC"].Width = 60;
            if (dtListProduct.Columns.Contains("TenNhaCungCap"))
                dtListProduct.Columns["TenNhaCungCap"].Width = 300;
            if (dtListProduct.Columns.Contains("GiaTien"))
                dtListProduct.Columns["GiaTien"].Width = 100;
            if (dtListProduct.Columns.Contains("GiamGia"))
                dtListProduct.Columns["GiamGia"].Width = 80;
            if (dtListProduct.Columns.Contains("SoLuong"))
                dtListProduct.Columns["SoLuong"].Width = 80;
            if (dtListProduct.Columns.Contains("SoLuongDaBan"))
                dtListProduct.Columns["SoLuongDaBan"].Width = 120; // Đặt chiều rộng cho cột mới
        }

        private void _reset()
        {
            txtMaMonAn.Text = "";
            txtTenMonAn.Text = "";
            txtGiaTien.Text = "";
            txtGiamGia.Text = "";
            txtSoLuong.Text = "";
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
            int temp = 1; // Bắt đầu từ 1 nếu không có dữ liệu

            if (dt.Rows.Count > 0)
            {
                // Lấy số lớn nhất từ MaMon
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

            // Định dạng mã
            if (temp < 10)
            {
                return "M00" + temp;
            }
            if (temp < 100)
            {
                return "M0" + temp;
            }
            return "M" + temp;
        }

        private DataTable _getIDProduct()
        {
            string query = "SELECT MaMon FROM DoAn ORDER BY MaMon";
            return cls_DatabaseManager.TableRead(query); // Giả sử cls_DatabaseManager có thể dùng cho DoAn
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
            DataTable nccData = cls_Product._showNCC();
            if (nccData != null && nccData.Rows.Count > 0)
            {
                cmbNhaCungCap.DataSource = nccData;
                cmbNhaCungCap.ValueMember = "MaNCC";
                cmbNhaCungCap.DisplayMember = "TenNCC";
            }
            else
            {
                MessageBox.Show("Không có dữ liệu nhà cung cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var items = new[] {
                new { Text = "Tên món", Value = "TenSanPham" },
                new { Text = "Mã món", Value = "MaSanPham" }
            };
            cmbFilter.Items.Clear();
            cmbFilter.DataSource = items;
            cmbFilter.DisplayMember = "Text";
            cmbFilter.ValueMember = "Value";
            cmbFilter.SelectedIndexChanged += (s, ev) =>
            {
                LoadProductList();
            };
            cmbFilter.SelectedIndex = 0;
        }

        private void LoadProductList()
        {
            DataTable dt = cls_Product._showDoAn();
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu sản phẩm để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtListProduct.DataSource = null;
            }
            else
            {
                // Đảm bảo cột SoLuongDaBan được thêm vào DataTable nếu chưa có
                if (!dt.Columns.Contains("SoLuongDaBan"))
                {
                    dt.Columns.Add("SoLuongDaBan", typeof(int)).DefaultValue = 0; // Giá trị mặc định là 0
                }

                dtListProduct.DataSource = dt;

                if (dtListProduct.Columns.Contains("MaNCC"))
                    dtListProduct.Columns["MaNCC"].Visible = false;

                if (dtListProduct.Columns.Contains("TenNhaCungCap"))
                {
                    dtListProduct.Columns["TenNhaCungCap"].HeaderText = "Nhà cung cấp";
                    dtListProduct.Columns["TenNhaCungCap"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (!string.IsNullOrEmpty(txtTimKiem.Text))
                {
                    string filterColumn = cmbFilter.SelectedValue.ToString();
                    dt.DefaultView.RowFilter = $"{filterColumn} LIKE '%{txtTimKiem.Text}%'";
                    dtListProduct.DataSource = dt.DefaultView.ToTable();
                }
            }

            DataTable dtNCC = cls_Product._showNCC();
            cmbNhaCungCap.DataSource = dtNCC;
            cmbNhaCungCap.DisplayMember = "TenNCC";
            cmbNhaCungCap.ValueMember = "MaNCC";
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
                        cmbNhaCungCap.SelectedIndex = -1;
                        MessageBox.Show("Không tìm thấy nhà cung cấp phù hợp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // Xác nhận xóa
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm có mã {maMon}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Kiểm tra và xóa các bản ghi liên quan trong ChiTietHoaDon
                if (_checkProductInCTHD(maMon))
                {
                    if (_deleteRelatedChiTietHoaDon(maMon))
                    {
                        if (_delProduct(maMon))
                        {
                            MessageBox.Show($"Xóa sản phẩm có mã {maMon} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadProductList(); // Tải lại danh sách sản phẩm
                            _reset(); // Đặt lại các trường nhập liệu
                        }
                        else
                        {
                            MessageBox.Show($"Không thể xóa sản phẩm có mã {maMon}. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa các bản ghi trong ChiTietHoaDon. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (_delProduct(maMon))
                {
                    MessageBox.Show($"Xóa sản phẩm có mã {maMon} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProductList(); // Tải lại danh sách sản phẩm
                    _reset(); // Đặt lại các trường nhập liệu
                }
                else
                {
                    MessageBox.Show($"Không thể xóa sản phẩm có mã {maMon}. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Thêm các phương thức hỗ trợ tương tự như trong UC_DrinkManager
        private bool _deleteRelatedChiTietHoaDon(string maMon)
        {
            string query = "DELETE FROM ChiTietHoaDon WHERE MaMon = @MaMon";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaMon", maMon) };
            return cls_DatabaseManager.ExecuteNonQuery(query, parameters) > 0;
        }

        private bool _checkProductInCTHD(string maMon)
        {
            string query = "SELECT COUNT(*) FROM ChiTietHoaDon WHERE MaMon = @MaMon";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaMon", maMon) };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            return dt.Rows[0].Field<int>(0) > 0;
        }

        private bool _delProduct(string maMon)
        {
            string query = "DELETE FROM DoAn WHERE MaMon = @MaMon";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaMon", maMon) };
            return cls_DatabaseManager.ExecuteNonQuery(query, parameters) > 0;
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
                        MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProductList();
                        _formatDT();
                        _reset();
                        txtTimKiem.Focus();
                        _sttButton(true, true, true, false, false, false);
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật sản phẩm này. Vui lòng kiểm tra lại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (e.KeyChar == (char)Keys.Enter)
            {
                DataTable dt = cls_Product._showDoAn();
                if (dt != null && dt.Rows.Count > 0)
                {
                    string filterColumn = cmbFilter.SelectedValue.ToString();
                    dt.DefaultView.RowFilter = $"{filterColumn} LIKE '%{txtTimKiem.Text}%'";
                    dtListProduct.DataSource = dt.DefaultView.ToTable();
                    _formatDT();
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            check = 1;
            _sttButton(false, false, false, true, true, true);
            txtMaMonAn.Enabled = false;
            txtMaMonAn.Text = _genIdProduct();
            txtTenMonAn.Focus();
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