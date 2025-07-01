using QLCHBanGaRan.lib; // Thêm namespace của cls_DatabaseManager
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_DrinkManager : UserControl
    {
        private int check = 0;

        public UC_DrinkManager()
        {
            InitializeComponent();
        }

        private void _formatDT()
        {
            dtListProduct.Columns["MaDoUong"].Width = 60;
            dtListProduct.Columns["TenDoUong"].Width = 220;
            dtListProduct.Columns["TenNhaCungCap"].Width = 300;
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

        private string _genIdProduct()
        {
            DataTable dt = _getIDProduct();
            int temp = 0;

            if (dt.Rows.Count == 0)
            {
                temp = 1;
            }
            else if (dt.Rows.Count == 1 && int.Parse(dt.Rows[0][0].ToString().Substring(2, 3)) == 1)
            {
                temp = 2;
            }
            else if (dt.Rows.Count == 1 && int.Parse(dt.Rows[0][0].ToString().Substring(2, 3)) > 1)
            {
                temp = 1;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    if (int.Parse(dt.Rows[i + 1][0].ToString().Substring(2, 3)) - int.Parse(dt.Rows[i][0].ToString().Substring(2, 3)) > 1)
                    {
                        temp = int.Parse(dt.Rows[i][0].ToString().Substring(2, 3)) + 1;
                        break;
                    }
                }

                if (temp == 0)
                {
                    temp = int.Parse(dt.Rows[dt.Rows.Count - 1][0].ToString().Substring(2, 3)) + 1;
                }
            }

            if (temp < 10)
            {
                return "SP00" + temp;
            }
            if (temp < 100)
            {
                return "SP0" + temp;
            }
            return "SP" + temp;
        }

        private DataTable _getIDProduct()
        {
            string query = "SELECT MaDoUong FROM DoUong ORDER BY MaDoUong";
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, true, false, false, false);
            if (Forms.frm_Main.Instance.pnlContainer.Controls.ContainsKey("UC_Product"))
            {
                Forms.frm_Main.Instance.pnlContainer.Controls["UC_Product"].BringToFront();
            }
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string filter = cmbFilter.SelectedValue?.ToString() ?? "TenDoUong";
                dtListProduct.DataSource = _searchProduct(filter, txtTimKiem.Text);
                _formatDT();
            }
        }

        private DataTable _searchProduct(string column, string searchText)
        {
            string query = $"SELECT du.MaDoUong, du.TenDoUong, ncc.TenNCC AS TenNhaCungCap, du.GiaTien, du.GiamGia, du.SoLuong " +
                           $"FROM DoUong du LEFT JOIN NhaCungCap ncc ON du.MaNCC = ncc.MaNCC WHERE {column} LIKE @Search";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Search", "%" + searchText + "%") };
            return cls_DatabaseManager.TableRead(query, parameters);
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
                txtMaDoUong.Text = dtListProduct.Rows[index].Cells["MaDoUong"].Value.ToString();
                txtTenDoUong.Text = dtListProduct.Rows[index].Cells["TenDoUong"].Value.ToString();
                string tenNCC = dtListProduct.Rows[index].Cells["TenNhaCungCap"].Value.ToString();
                DataTable dtNCC = _getNhaCungCap();
                cmbNhaCungCap.SelectedValue = dtNCC.AsEnumerable()
                    .FirstOrDefault(row => row.Field<string>("TenNCC") == tenNCC)?.Field<int>("MaNCC") ?? 0;
                txtGiaTien.Text = dtListProduct.Rows[index].Cells["GiaTien"].Value.ToString();
                txtGiamGia.Text = dtListProduct.Rows[index].Cells["GiamGia"].Value.ToString();
                txtSoLuong.Text = dtListProduct.Rows[index].Cells["SoLuong"].Value.ToString();
            }
        }

        private DataTable _getNhaCungCap()
        {
            string query = "SELECT MaNCC, TenNCC FROM NhaCungCap";
            return cls_DatabaseManager.TableRead(query);
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
                    if (_checkProductInCTHD(maDoUong))
                    {
                        MessageBox.Show("Vui lòng xóa sản phẩm trong ChiTietHoaDon trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (_delProduct(maDoUong))
                    {
                        MessageBox.Show(string.Format("Xóa thành công đồ uống có mã {0}", maDoUong), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtListProduct.DataSource = _showProduct();
                        _formatDT();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa đồ uống này. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool _checkProductInCTHD(string maDoUong)
        {
            string query = "SELECT COUNT(*) FROM ChiTietHoaDon WHERE MaDoUong = @MaDoUong";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaDoUong", maDoUong) };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            return dt.Rows[0].Field<int>(0) > 0;
        }

        private bool _delProduct(string maDoUong)
        {
            string query = "DELETE FROM DoUong WHERE MaDoUong = @MaDoUong";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaDoUong", maDoUong) };
            return cls_DatabaseManager.ExecuteNonQuery(query, parameters) > 0;
        }

        private IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                           .Concat(controls)
                           .Where(c => c.GetType() == type);
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
                    if (_addProduct(genMaDoUong, txtTenDoUong.Text, Convert.ToInt32(cmbNhaCungCap.SelectedValue),
                        int.Parse(txtGiaTien.Text), int.Parse(txtGiamGia.Text), int.Parse(txtSoLuong.Text)))
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
                    if (_updateProduct(txtMaDoUong.Text, txtTenDoUong.Text, Convert.ToInt32(cmbNhaCungCap.SelectedValue),
                        int.Parse(txtGiaTien.Text), int.Parse(txtGiamGia.Text), int.Parse(txtSoLuong.Text)))
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

        private bool _addProduct(string maDoUong, string tenDoUong, int maNCC, int giaTien, int giamGia, int soLuong)
        {
            string query = "INSERT INTO DoUong (MaDoUong, TenDoUong, MaNCC, GiaTien, GiamGia, SoLuong) VALUES (@MaDoUong, @TenDoUong, @MaNCC, @GiaTien, @GiamGia, @SoLuong)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDoUong", maDoUong),
                new SqlParameter("@TenDoUong", tenDoUong),
                new SqlParameter("@MaNCC", maNCC),
                new SqlParameter("@GiaTien", giaTien),
                new SqlParameter("@GiamGia", giamGia),
                new SqlParameter("@SoLuong", soLuong)
            };
            return cls_DatabaseManager.ExecuteNonQuery(query, parameters) > 0;
        }

        private bool _updateProduct(string maDoUong, string tenDoUong, int maNCC, int giaTien, int giamGia, int soLuong)
        {
            string query = "UPDATE DoUong SET TenDoUong = @TenDoUong, MaNCC = @MaNCC, GiaTien = @GiaTien, GiamGia = @GiamGia, SoLuong = @SoLuong WHERE MaDoUong = @MaDoUong";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDoUong", maDoUong),
                new SqlParameter("@TenDoUong", tenDoUong),
                new SqlParameter("@MaNCC", maNCC),
                new SqlParameter("@GiaTien", giaTien),
                new SqlParameter("@GiamGia", giamGia),
                new SqlParameter("@SoLuong", soLuong)
            };
            return cls_DatabaseManager.ExecuteNonQuery(query, parameters) > 0;
        }

        private DataTable _showProduct()
        {
            string query = "SELECT du.MaDoUong, du.TenDoUong, ncc.TenNCC AS TenNhaCungCap, du.GiaTien, du.GiamGia, du.SoLuong " +
                           "FROM DoUong du LEFT JOIN NhaCungCap ncc ON du.MaNCC = ncc.MaNCC";
            return cls_DatabaseManager.TableRead(query);
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, true, false, false, false);
        }

        private void UC_DrinkManager_Load(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            dtListProduct.DataSource = _showProduct();
            _formatDT();
            _sttButton(true, true, true, false, false, false);
            _reset();
            cmbNhaCungCap.DataSource = _getNhaCungCap();
            cmbNhaCungCap.ValueMember = "MaNCC";
            cmbNhaCungCap.DisplayMember = "TenNCC";

            cmbFilter.DisplayMember = "Text";
            cmbFilter.ValueMember = "Value";

            var items = new[] {
                new { Text = "Tên đồ uống", Value = "TenDoUong" },
                new { Text = "Mã đồ uống", Value = "MaDoUong" }
            };
            cmbFilter.DataSource = items;
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
            Regex regex = new Regex("^[0-9]+$");
            if (string.IsNullOrWhiteSpace(txtGiaTien.Text))
            {
                errorProvidera.SetError(txtGiaTien, "Giá tiền không được trống.");
                e.Cancel = true;
            }
            else if (!regex.IsMatch(txtGiaTien.Text))
            {
                errorProvidera.SetError(txtGiaTien, "Giá tiền đồ uống phải là số.");
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