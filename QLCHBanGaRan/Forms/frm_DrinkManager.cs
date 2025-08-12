using QLCHBanGaRan.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_DrinkManager : Form
    {
        private int check = 0;

        public frm_DrinkManager()
        {
            InitializeComponent();
        }

        private void _formatDT()
        {
            if (dtListProduct.Columns.Contains("MaDoUong"))
                dtListProduct.Columns["MaDoUong"].Width = 60;
            if (dtListProduct.Columns.Contains("TenDoUong"))
                dtListProduct.Columns["TenDoUong"].Width = 180;
            if (dtListProduct.Columns.Contains("TenNhaCungCap"))
                dtListProduct.Columns["TenNhaCungCap"].Width = 250;
            // Ẩn cột IsDeleted và MaNCC nếu có
            if (dtListProduct.Columns.Contains("IsDeleted"))
                dtListProduct.Columns["IsDeleted"].Visible = false;
            if (dtListProduct.Columns.Contains("MaNCC"))
                dtListProduct.Columns["MaNCC"].Visible = false;
        }

        private void _reset()
        {
            txtMaDoUong.Text = "";
            txtTenDoUong.Text = "";
            txtGiaTien.Text = "";
            txtGiamGia.Text = "";
            txtSoLuong.Text = "";
            txtTimKiem.Focus();
            errorProvidera.Clear();
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
                    string maDoUong = row[0].ToString();
                    if (maDoUong.StartsWith("DU") && maDoUong.Length >= 5)
                    {
                        if (int.TryParse(maDoUong.Substring(2), out int num) && num > maxNumber)
                        {
                            maxNumber = num;
                        }
                    }
                }
                temp = maxNumber + 1;
            }

            if (temp < 10)
            {
                return "DU00" + temp;
            }
            if (temp < 100)
            {
                return "DU0" + temp;
            }
            return "DU" + temp;
        }

        private DataTable _getIDProduct()
        {
            return cls_Product._getIDDoUong();
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, true, false, false, false);
            if (Forms.frm_Main.Instance.Controls.ContainsKey("frm_Product"))
            {
                Forms.frm_Main.Instance.Controls["frm_Product"].BringToFront();
            }
        }

        private void LoadProductList()
        {
            DataTable dt = _showProduct();
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu đồ uống để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtListProduct.DataSource = null;
                return;
            }

            // Thêm cột chuỗi tạm thời cho GiaTien và GiamGia nếu chưa có
            if (!dt.Columns.Contains("GiaTienStr"))
            {
                dt.Columns.Add("GiaTienStr", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    row["GiaTienStr"] = row["GiaTien"]?.ToString() ?? "";
                }
            }
            if (!dt.Columns.Contains("GiamGiaStr"))
            {
                dt.Columns.Add("GiamGiaStr", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    row["GiamGiaStr"] = row["GiamGia"]?.ToString() ?? "";
                }
            }

            dtListProduct.DataSource = dt;

            if (dtListProduct.Columns.Contains("GiaTienStr"))
                dtListProduct.Columns["GiaTienStr"].Visible = false;
            if (dtListProduct.Columns.Contains("GiamGiaStr"))
                dtListProduct.Columns["GiamGiaStr"].Visible = false;
            if (dtListProduct.Columns.Contains("IsDeleted"))
                dtListProduct.Columns["IsDeleted"].Visible = false;
            if (dtListProduct.Columns.Contains("MaNCC"))
                dtListProduct.Columns["MaNCC"].Visible = false;

            if (dtListProduct.Columns.Contains("TenNhaCungCap"))
            {
                dtListProduct.Columns["TenNhaCungCap"].HeaderText = "Nhà cung cấp";
                dtListProduct.Columns["TenNhaCungCap"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            _formatDT();
        }

        private string EscapeFilterValue(string input)
        {
            return input.Replace("'", "''")
                        .Replace("[", "[[]")
                        .Replace("%", "[%]")
                        .Replace("*", "[*]")
                        .Replace(",", "[,]");
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = _showProduct();
            if (dt == null || dt.Rows.Count == 0)
            {
                dtListProduct.DataSource = null;
                return;
            }

            // Thêm cột chuỗi tạm thời cho GiaTien và GiamGia nếu chưa có
            if (!dt.Columns.Contains("GiaTienStr"))
            {
                dt.Columns.Add("GiaTienStr", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    row["GiaTienStr"] = row["GiaTien"]?.ToString() ?? "";
                }
            }
            if (!dt.Columns.Contains("GiamGiaStr"))
            {
                dt.Columns.Add("GiamGiaStr", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    row["GiamGiaStr"] = row["GiamGia"]?.ToString() ?? "";
                }
            }

            string filterColumn = "TenDoUong";
            if (cmbFilter.SelectedValue != null)
            {
                filterColumn = cmbFilter.SelectedValue.ToString();
            }
            else if (cmbFilter.SelectedIndex >= 0 && cmbFilter.Items.Count > 0)
            {
                cmbFilter.SelectedIndex = 0;
                filterColumn = cmbFilter.SelectedValue.ToString();
            }

            string searchText = EscapeFilterValue(txtTimKiem.Text.Trim());

            dt.DefaultView.RowFilter = "";

            if (!string.IsNullOrEmpty(searchText))
            {
                string filterColumnStr = filterColumn;
                if (filterColumn == "GiaTien")
                    filterColumnStr = "GiaTienStr";
                else if (filterColumn == "GiamGia")
                    filterColumnStr = "GiamGiaStr";

                if (dt.Columns.Contains(filterColumnStr))
                {
                    string likePattern = filterColumn == "TenDoUong" ? $"'%{searchText}%'" : $"'{searchText}%'";
                    dt.DefaultView.RowFilter += $"CONVERT([{filterColumnStr}], System.String) LIKE {likePattern}";
                }
            }

            DataTable dtFiltered = dt.DefaultView.ToTable();

            if (dtFiltered.Columns.Contains("GiaTienStr"))
                dtFiltered.Columns.Remove("GiaTienStr");
            if (dtFiltered.Columns.Contains("GiamGiaStr"))
                dtFiltered.Columns.Remove("GiamGiaStr");

            dtListProduct.DataSource = dtFiltered;

            _formatDT();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            check = 1;
            _sttButton(false, false, false, true, true, true);
            txtMaDoUong.Text = _genIdProduct();
            txtMaDoUong.Enabled = false;
            txtSoLuong.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            check = 2;
            if (dtListProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đồ uống cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _sttButton(false, false, false, true, true, true);
                txtSoLuong.Focus();

                int index = dtListProduct.CurrentCell.RowIndex;
                txtMaDoUong.Enabled = false;

                string maColumn = "MaDoUong";
                string tenColumn = "TenDoUong";

                if (dtListProduct.Columns.Contains(maColumn) && dtListProduct.Columns.Contains(tenColumn) &&
                    dtListProduct.Columns.Contains("GiaTien") && dtListProduct.Columns.Contains("MaNCC") &&
                    dtListProduct.Columns.Contains("GiamGia") && dtListProduct.Columns.Contains("SoLuong"))
                {
                    txtMaDoUong.Text = dtListProduct.Rows[index].Cells[maColumn].Value?.ToString() ?? "";
                    txtTenDoUong.Text = dtListProduct.Rows[index].Cells[tenColumn].Value?.ToString() ?? "";
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
                    MessageBox.Show($"Dữ liệu đồ uống không đầy đủ. Thiếu cột: {missingColumns}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _reset();
                    _sttButton(true, true, true, false, false, false);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtListProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đồ uống cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int index = dtListProduct.CurrentCell.RowIndex;
                string maDoUong = dtListProduct.Rows[index].Cells["MaDoUong"].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn có muốn xóa đồ uống này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DataTable dtCheck = cls_Product._checkDoUong(maDoUong);
                    if (dtCheck.Rows.Count > 0)
                    {
                        MessageBox.Show("Không thể xóa vì đồ uống đã có trong hóa đơn chi tiết!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (cls_Product._delDoUong(maDoUong))
                    {
                        MessageBox.Show($"Đã xóa đồ uống có mã {maDoUong}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtListProduct.DataSource = _showProduct();
                        _formatDT();
                    }
                    else
                    {
                        MessageBox.Show($"Không thể xóa đồ uống có mã {maDoUong}. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                var getChildControls = GetAll(this, typeof(TextBox));
                var listOfErrors = getChildControls.Select(c => errorProvidera.GetError(c))
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .ToList();
                MessageBox.Show("Vui lòng kiểm tra lại thông tin đồ uống:\n - " + string.Join("\n - ", listOfErrors.ToArray()), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (check == 1)
                {
                    string genMaDoUong = _genIdProduct();
                    decimal giaTien;
                    if (!decimal.TryParse(txtGiaTien.Text.Replace(".", ","), NumberStyles.Any, CultureInfo.GetCultureInfo("vi-VN"), out giaTien))
                    {
                        MessageBox.Show("Giá tiền không hợp lệ. Vui lòng nhập số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string maNCC = cmbNhaCungCap.SelectedValue?.ToString() ?? "";
                    if (string.IsNullOrEmpty(maNCC) && cmbNhaCungCap.Items.Count > 0)
                    {
                        MessageBox.Show("Vui lòng chọn nhà cung cấp hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int giamGia;
                    if (!int.TryParse(txtGiamGia.Text, out giamGia))
                    {
                        MessageBox.Show("Phần trăm giảm giá không hợp lệ. Vui lòng nhập số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int soLuong;
                    if (!int.TryParse(txtSoLuong.Text, out soLuong))
                    {
                        MessageBox.Show("Số lượng không hợp lệ. Vui lòng nhập số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (cls_Product._addDoUong(genMaDoUong, txtTenDoUong.Text, maNCC, giaTien, giamGia, soLuong))
                    {
                        MessageBox.Show("Thêm đồ uống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _reset();
                        _formatDT();
                        _sttButton(true, true, true, false, false, false);
                        dtListProduct.DataSource = _showProduct();
                        txtTenDoUong.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm đồ uống này. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    decimal giaTien;
                    if (!decimal.TryParse(txtGiaTien.Text.Replace(".", ","), NumberStyles.Any, CultureInfo.GetCultureInfo("vi-VN"), out giaTien))
                    {
                        MessageBox.Show("Giá tiền không hợp lệ. Vui lòng nhập số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string maNCC = cmbNhaCungCap.SelectedValue?.ToString() ?? "";
                    if (string.IsNullOrEmpty(maNCC) && cmbNhaCungCap.Items.Count > 0)
                    {
                        MessageBox.Show("Vui lòng chọn nhà cung cấp hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int giamGia;
                    if (!int.TryParse(txtGiamGia.Text, out giamGia))
                    {
                        MessageBox.Show("Phần trăm giảm giá không hợp lệ. Vui lòng nhập số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int soLuong;
                    if (!int.TryParse(txtSoLuong.Text, out soLuong))
                    {
                        MessageBox.Show("Số lượng không hợp lệ. Vui lòng nhập số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (cls_Product._updateDoUong(txtMaDoUong.Text, txtTenDoUong.Text, maNCC, giaTien, giamGia, soLuong))
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtListProduct.DataSource = _showProduct();
                        _reset();
                        _formatDT();
                        _sttButton(true, true, true, false, false, false);
                        txtTimKiem.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật đồ uống này. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private DataTable _showProduct()
        {
            return cls_Product._showDoUong();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, true, false, false, false);
        }

        private void frm_DrinkManager_Load(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            LoadProductList();
            _sttButton(true, true, true, false, false, false);
            _reset();
            DataTable dtNCC = cls_Product._showNCC(); // Lấy danh sách nhà cung cấp
            if (dtNCC.Rows.Count > 0)
            {
                cmbNhaCungCap.DataSource = dtNCC;
                cmbNhaCungCap.ValueMember = "MaNCC";
                cmbNhaCungCap.DisplayMember = "TenNCC";
            }
            else
            {
                MessageBox.Show("Không có dữ liệu nhà cung cấp. Vui lòng thêm nhà cung cấp trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            var items = new[] {
                new { Text = "Tên đồ uống", Value = "TenDoUong" },
                new { Text = "Giá tiền", Value = "GiaTien" },
                new { Text = "Giảm giá (%)", Value = "GiamGia" }
            };
            cmbFilter.DataSource = items;
            cmbFilter.DisplayMember = "Text";
            cmbFilter.ValueMember = "Value";
            cmbFilter.SelectedIndex = 0;
            cmbFilter.SelectedIndexChanged += (s, ev) => LoadProductList();

            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
        }

        private void txtSoLuong_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                errorProvidera.SetError(txtSoLuong, "Số lượng đồ uống không được để trống.");
                e.Cancel = true;
            }
            else if (!regex.IsMatch(txtSoLuong.Text))
            {
                errorProvidera.SetError(txtSoLuong, "Số lượng đồ uống phải là số.");
                e.Cancel = true;
            }
            else
            {
                errorProvidera.SetError(txtSoLuong, "");
            }
        }

        private void txtTenDoUong_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDoUong.Text))
            {
                errorProvidera.SetError(txtTenDoUong, "Tên đồ uống không được trống.");
                e.Cancel = true;
            }
            else
            {
                errorProvidera.SetError(txtTenDoUong, "");
            }
        }

        private void txtGiaTien_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+(,[0-9]{1,2})?$");
            if (string.IsNullOrWhiteSpace(txtGiaTien.Text))
            {
                errorProvidera.SetError(txtGiaTien, "Giá tiền không được trống.");
                e.Cancel = true;
            }
            else if (!regex.IsMatch(txtGiaTien.Text.Replace(".", "")))
            {
                errorProvidera.SetError(txtGiaTien, "Giá tiền đồ uống phải là số hợp lệ (ví dụ: 25000 hoặc 25000,00).");
                e.Cancel = true;
            }
            else
            {
                errorProvidera.SetError(txtGiaTien, "");
            }
        }

        private void txtGiamGia_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (string.IsNullOrWhiteSpace(txtGiamGia.Text))
            {
                errorProvidera.SetError(txtGiamGia, "% giảm giá không được trống.");
                e.Cancel = true;
            }
            else if (!regex.IsMatch(txtGiamGia.Text))
            {
                errorProvidera.SetError(txtGiamGia, "% giảm giá phải là số.");
                e.Cancel = true;
            }
            else if (int.Parse(txtGiamGia.Text) > 100)
            {
                errorProvidera.SetError(txtGiamGia, "% giảm giá không được vượt quá 100%.");
                e.Cancel = true;
            }
            else
            {
                errorProvidera.SetError(txtGiamGia, "");
            }
        }
    }
}
