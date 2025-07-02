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
using System.Globalization;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_SalaryManager : UserControl
    {
        private int check = 0;
        private int _chucDanhID = 0;

        public UC_SalaryManager()
        {
            InitializeComponent();
        }

        private void _reset()
        {
            txtChucDanh.Text = "";
            txtLuongCung.Text = "";
            txtPhuCap.Text = "";
            errorProvider.Clear();
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

        private IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                           .Concat(controls)
                           .Where(c => c.GetType() == type);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _sttButton(true, false, true, false, false, false);
            _reset();
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Salary"].BringToFront();
        }

        private void UC_SalaryManager_Load(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, false, true, false, false, false);
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            dtList.AutoGenerateColumns = false;
            dtList.DataSource = lib.cls_Salary.GetChucDanh(); // Cập nhật tên phương thức
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            check = 1;
            _sttButton(false, false, false, true, true, true);
            txtChucDanh.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            check = 2;
            _sttButton(false, false, false, true, true, true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_chucDanhID == 0)
            {
                MessageBox.Show("Vui lòng chọn thông tin cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn muốn xóa lương chức danh " + txtChucDanh.Text + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (!lib.cls_Salary.CheckChucDanh(_chucDanhID)) // Cập nhật tên phương thức
                    {
                        MessageBox.Show("Vui lòng xóa bản ghi trong thông tin nhân viên trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        bool _delCD = lib.cls_Salary.DeleteChucDanh(_chucDanhID); // Cập nhật tên phương thức
                        if (_delCD)
                        {
                            MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _sttButton(true, false, true, false, false, false);
                            _reset();
                            dtList.DataSource = lib.cls_Salary.GetChucDanh(); // Cập nhật tên phương thức
                            _chucDanhID = 0;
                        }
                        else
                        {
                            MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra và thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                var getChildControls = GetAll(this, typeof(ComboBox));
                var listOfErrors = getChildControls.Select(c => errorProvider.GetError(c))
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .ToList();
                MessageBox.Show("Vui lòng kiểm tra lại thông tin lương:\n - " + string.Join("\n - ", listOfErrors.ToArray()), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                decimal luongCoBan, phuCap;
                if (!decimal.TryParse(txtLuongCung.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out luongCoBan) ||
                    !decimal.TryParse(txtPhuCap.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out phuCap))
                {
                    MessageBox.Show("Lương cơ bản và phụ cấp phải là số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (check == 1)
                {
                    bool _insertCD = lib.cls_Salary.InsertChucDanh(txtChucDanh.Text, luongCoBan, phuCap); // Cập nhật tên phương thức và tham số
                    if (_insertCD)
                    {
                        MessageBox.Show("Thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _sttButton(true, false, true, false, false, false);
                        _reset();
                        dtList.DataSource = lib.cls_Salary.GetChucDanh(); // Cập nhật tên phương thức
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm thông tin lương. Vui lòng kiểm tra lại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool _updateCD = lib.cls_Salary.UpdateChucDanh(txtChucDanh.Text, luongCoBan, phuCap, _chucDanhID); // Cập nhật tên phương thức và tham số
                    if (_updateCD)
                    {
                        MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _sttButton(true, false, true, false, false, false);
                        _reset();
                        dtList.DataSource = lib.cls_Salary.GetChucDanh(); // Cập nhật tên phương thức
                        _chucDanhID = 0;
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật thông tin lương. Vui lòng kiểm tra lại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _sttButton(true, false, true, false, false, false);
            _reset();
        }

        private void dtList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lib.cls_Salary.GetChucDanh().Rows.Count > 0) // Cập nhật tên phương thức
            {
                int index = dtList.CurrentCell.RowIndex;
                _chucDanhID = Convert.ToInt32(dtList.Rows[index].Cells[0].Value);
                txtChucDanh.Text = dtList.Rows[index].Cells[1].Value.ToString();
                txtLuongCung.Text = dtList.Rows[index].Cells[2].Value.ToString();
                txtPhuCap.Text = dtList.Rows[index].Cells[3].Value.ToString();
                btnSua.Enabled = true;
            }
        }

        private void txtChucDanh_Validating(object sender, CancelEventArgs e)
        {
            if (txtChucDanh.Text == "")
            {
                errorProvider.SetError(txtChucDanh, "Tên chức danh không được trống.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtChucDanh, "");
            }
        }

        private void txtLuongCung_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLuongCung.Text))
            {
                errorProvider.SetError(txtLuongCung, "Lương cơ bản không được trống.");
                e.Cancel = true;
            }
            else if (!decimal.TryParse(txtLuongCung.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                errorProvider.SetError(txtLuongCung, "Lương cơ bản phải là số hợp lệ.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtLuongCung, "");
            }
        }

        private void txtPhuCap_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhuCap.Text))
            {
                errorProvider.SetError(txtPhuCap, "Phụ cấp không được trống.");
                e.Cancel = true;
            }
            else if (!decimal.TryParse(txtPhuCap.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                errorProvider.SetError(txtPhuCap, "Phụ cấp phải là số hợp lệ.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtPhuCap, "");
            }
        }

        private void dtList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtList.Columns[e.ColumnIndex].Name == "LuongCoBan") // Cập nhật tên cột
            {
                e.Value = e.Value == null ? "0" : e.Value;
                e.Value = decimal.Parse(e.Value.ToString()).ToString("N0");
                e.FormattingApplied = true;
            }
        }
    }
}