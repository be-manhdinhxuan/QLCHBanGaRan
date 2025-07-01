using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using QLCHBanGaRan.lib;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_FoodManager : UserControl
    {
        private int check = 0;
        private string productType = "DoAn"; // Mặc định là món ăn

        public UC_FoodManager()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, true, false, false, false);
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Product"].BringToFront();
        }

        private void _formatDT()
        {
            dtListProduct.Columns["MaSanPham"].HeaderText = productType == "DoAn" ? "MaMon" : "MaDoUong";
            dtListProduct.Columns["TenSanPham"].HeaderText = productType == "DoAn" ? "TenMon" : "TenDoUong";
            dtListProduct.Columns["MaNCC"].Width = 60;
            dtListProduct.Columns["TenNhaCungCap"].Width = 300;
            dtListProduct.Columns["GiaTien"].Width = 100;
            dtListProduct.Columns["GiamGia"].Width = 80;
            dtListProduct.Columns["SoLuong"].Width = 80;
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
            DataTable dt = productType == "DoAn" ? cls_Product._getIDDoAn() : cls_Product._getIDDoUong();
            int temp = 0;

            if (dt.Rows.Count == 0)
            {
                temp = 1;
            }
            else if (dt.Rows.Count == 1 && int.Parse(dt.Rows[0][0].ToString().Substring(1, 3)) == 1)
            {
                temp = 2;
            }
            else if (dt.Rows.Count == 1 && int.Parse(dt.Rows[0][0].ToString().Substring(1, 3)) > 1)
            {
                temp = 1;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    if (int.Parse(dt.Rows[i + 1][0].ToString().Substring(1, 3)) - int.Parse(dt.Rows[i][0].ToString().Substring(1, 3)) > 1)
                    {
                        temp = int.Parse(dt.Rows[i][0].ToString().Substring(1, 3)) + 1;
                        break;
                    }
                }

                if (temp == 0)
                {
                    temp = int.Parse(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(1, 3)) + 1;
                }
            }

            string prefix = productType == "DoAn" ? "M" : "DU";
            if (temp < 10)
            {
                return prefix + "00" + temp;
            }
            if (temp < 100)
            {
                return prefix + "0" + temp;
            }
            return prefix + temp;
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
            cmbNhaCungCap.DataSource = cls_Product._showNCC();
            cmbNhaCungCap.ValueMember = "MaNCC";
            cmbNhaCungCap.DisplayMember = "TenNCC";

            // Thêm ComboBox để chọn loại sản phẩm
            var items = new[] {
                new { Text = "Món ăn", Value = "DoAn" },
                new { Text = "Đồ uống", Value = "DoUong" }
            };
            cmbFilter.Items.Clear();
            cmbFilter.DataSource = items;
            cmbFilter.DisplayMember = "Text";
            cmbFilter.ValueMember = "Value";
            cmbFilter.SelectedIndexChanged += (s, ev) =>
            {
                productType = cmbFilter.SelectedValue.ToString();
                LoadProductList();
                _formatDT();
            };
            cmbFilter.SelectedIndex = 0; // Mặc định chọn món ăn
        }

        private void LoadProductList()
        {
            dtListProduct.DataSource = productType == "DoAn" ? cls_Product._showDoAn() : cls_Product._showDoUong();
            dtListProduct.Columns["MaNCC"].Visible = false; // Ẩn cột MaNCC
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            check = 2;
            if (dtListProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _sttButton(false, false, false, true, true, true);
                txtSoLuong.Focus();

                int index = dtListProduct.CurrentCell.RowIndex;
                txtMaMonAn.Enabled = false;
                txtMaMonAn.Text = dtListProduct.Rows[index].Cells[productType == "DoAn" ? "MaMon" : "MaDoUong"].Value.ToString();
                txtTenMonAn.Text = dtListProduct.Rows[index].Cells[productType == "DoAn" ? "TenMon" : "TenDoUong"].Value.ToString();
                txtGiaTien.Text = dtListProduct.Rows[index].Cells["GiaTien"].Value.ToString();
                cmbNhaCungCap.SelectedValue = dtListProduct.Rows[index].Cells["MaNCC"].Value.ToString();
                txtGiamGia.Text = dtListProduct.Rows[index].Cells["GiamGia"].Value.ToString();
                txtSoLuong.Text = dtListProduct.Rows[index].Cells["SoLuong"].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtListProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int index = dtListProduct.CurrentCell.RowIndex;
                string maProduct = dtListProduct.Rows[index].Cells[productType == "DoAn" ? "MaMon" : "MaDoUong"].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn có muốn xóa sản phẩm này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DataTable dtCTHD = productType == "DoAn" ? cls_Product._checkDoAn(maProduct) : cls_Product._checkDoUong(maProduct);
                    if (dtCTHD.Rows.Count > 0)
                    {
                        MessageBox.Show("Vui lòng xóa sản phẩm trong ChiTietHoaDon", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if ((productType == "DoAn" && cls_Product._delDoAn(maProduct)) || (productType == "DoUong" && cls_Product._delDoUong(maProduct)))
                    {
                        MessageBox.Show(string.Format("Xóa thành công sản phẩm có mã {0}", maProduct), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProductList();
                        _formatDT();
                    }
                    else
                    {
                        MessageBox.Show("Không thể thực hiện xóa sản phẩm này khỏi CSDL. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                    bool addProduct = productType == "DoAn"
                        ? cls_Product._addDoAn(genMaSP, txtTenMonAn.Text, cmbNhaCungCap.SelectedValue.ToString(), decimal.Parse(txtGiaTien.Text), int.Parse(txtGiamGia.Text), int.Parse(txtSoLuong.Text))
                        : cls_Product._addDoUong(genMaSP, txtTenMonAn.Text, cmbNhaCungCap.SelectedValue.ToString(), decimal.Parse(txtGiaTien.Text), int.Parse(txtGiamGia.Text), int.Parse(txtSoLuong.Text));

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
                        MessageBox.Show("Không thể thêm sản phẩm này. Vui lòng kiểm tra lại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool updateProduct = productType == "DoAn"
                        ? cls_Product._updateDoAn(txtMaMonAn.Text, txtTenMonAn.Text, cmbNhaCungCap.SelectedValue.ToString(), decimal.Parse(txtGiaTien.Text), int.Parse(txtGiamGia.Text), int.Parse(txtSoLuong.Text))
                        : cls_Product._updateDoUong(txtMaMonAn.Text, txtTenMonAn.Text, cmbNhaCungCap.SelectedValue.ToString(), decimal.Parse(txtGiaTien.Text), int.Parse(txtGiamGia.Text), int.Parse(txtSoLuong.Text));
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
                dtListProduct.DataSource = productType == "DoAn"
                    ? cls_Product._searchDoAn(txtTimKiem.Text)
                    : cls_Product._searchDoUong(txtTimKiem.Text);
                _formatDT();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            check = 1;
            _sttButton(false, false, false, true, true, true);
            txtMaMonAn.Enabled = false;
            txtMaMonAn.Text = _genIdProduct();
            txtSoLuong.Focus();
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
            Regex regex = new Regex("^[0-9]+$");
            if (string.IsNullOrWhiteSpace(txtGiaTien.Text))
            {
                errorProvider.SetError(txtGiaTien, "Giá tiền không được trống");
                e.Cancel = true;
            }
            else if (!regex.IsMatch(txtGiaTien.Text))
            {
                errorProvider.SetError(txtGiaTien, "Giá tiền sản phẩm phải là số.");
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