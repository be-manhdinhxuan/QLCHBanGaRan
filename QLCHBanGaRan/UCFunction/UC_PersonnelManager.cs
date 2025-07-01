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
using QLCHBanGaRan.lib;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_PersonnelManager : UserControl
    {
        private int check = 0;
        private string maNV = null;

        public UC_PersonnelManager()
        {
            InitializeComponent();
        }

        private void _formatDT()
        {
            dtListEmployess.Columns["MaNV"].Width = 60;
            dtListEmployess.Columns["TenNV"].Width = 200;
            dtListEmployess.Columns["CMND"].Width = 100;
            dtListEmployess.Columns["SDT"].Width = 110;
        }

        private void _reset()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtCMND.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            cbDiLam.Checked = false;
            txtTimKiem.Focus();
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

        private string _genIdEmployess()
        {
            DataTable dt = cls_EmployeeManagement.GetIDEmployees();
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
                return "NV00" + temp;
            }
            if (temp < 100)
            {
                return "NV0" + temp;
            }
            return "NV" + temp;
        }

        private List<string> _checkAvailable(string maNV)
        {
            List<string> msg = new List<string>();
            if (!cls_EmployeeManagement.CheckInNguoiDung(maNV))
            {
                msg.Add("Vui lòng xóa nhân viên này trong Người Dùng trước khi xóa nhân viên.");
            }
            if (!cls_EmployeeManagement.CheckInHoSoNhanVien(maNV))
            {
                msg.Add("Vui lòng xóa nhân viên này trong Hồ Sơ Nhân Viên trước khi xóa nhân viên.");
            }
            if (!cls_EmployeeManagement.CheckInNhanVienBoPhan(maNV))
            {
                msg.Add("Vui lòng xóa nhân viên này trong Bộ Phận trước khi xóa nhân viên.");
            }
            if (!cls_EmployeeManagement.CheckInNhanVienChucDanh(maNV))
            {
                msg.Add("Vui lòng xóa nhân viên này trong Chức Danh trước khi xóa nhân viên.");
            }
            if (!cls_EmployeeManagement.CheckInHoaDon(maNV))
            {
                msg.Add("Vui lòng xóa nhân viên này trong Hóa Đơn trước khi xóa nhân viên.");
            }
            return msg;
        }

        private void UC_PersonnelManager_Load(object sender, EventArgs e)
        {
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            cmbGioiTinh.DataSource = cls_EmployeeManagement.GetGender();
            cmbGioiTinh.ValueMember = "GioiTinhID"; // Giả định cột ID
            cmbGioiTinh.DisplayMember = "GioiTinh";  // Giả định cột tên
            dtListEmployess.AutoGenerateColumns = false;
            dtListEmployess.DataSource = cls_EmployeeManagement.ShowEmployees();
            _formatDT();
            _reset();
            _sttButton(true, true, true, false, false, false);

            cmbFilter.ValueMember = "Value";
            cmbFilter.DisplayMember = "Text";
            var items = new[] {
                new { Text = "Tên NV", Value = "TenNV" },
                new { Text = "Số CMND", Value = "CMND" },
                new { Text = "Số điện thoại", Value = "SDT" }
            };
            cmbFilter.DataSource = items;
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Thêm logic tìm kiếm (chưa có phương thức search trong cls_EmployeeManagement)
            dtListEmployess.DataSource = cls_EmployeeManagement.ShowEmployees(); // Giả định tạm thời
            _formatDT();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, true, false, false, false);
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Personnel"].BringToFront();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            check = 1;
            _sttButton(false, false, false, true, true, true);
            txtMaNV.Text = _genIdEmployess();
            txtMaNV.Enabled = false;
            txtTenNV.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            check = 2;
            if (dtListEmployess.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _sttButton(false, false, false, true, true, true);
                txtTenNV.Focus();
                int index = dtListEmployess.CurrentCell.RowIndex;
                maNV = dtListEmployess.Rows[index].Cells["MaNV"].Value.ToString();
                txtMaNV.Text = maNV;
                txtMaNV.Enabled = false;
                txtTenNV.Text = dtListEmployess.Rows[index].Cells["TenNV"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(dtListEmployess.Rows[index].Cells["NgaySinh"].Value);
                txtCMND.Text = dtListEmployess.Rows[index].Cells["CMND"].Value.ToString();
                txtDiaChi.Text = dtListEmployess.Rows[index].Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dtListEmployess.Rows[index].Cells["Email"].Value.ToString();
                txtSDT.Text = dtListEmployess.Rows[index].Cells["SDT"].Value.ToString();
                cmbGioiTinh.SelectedValue = Convert.ToBoolean(dtListEmployess.Rows[index].Cells["GioiTinh"].Value) ? 1 : 0; // Giả định 0: Nam, 1: Nữ
                cbDiLam.Checked = Convert.ToBoolean(dtListEmployess.Rows[index].Cells["TrangThai"].Value);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, true, false, false, false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtListEmployess.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int index = dtListEmployess.CurrentCell.RowIndex;
                string maNV = dtListEmployess.Rows[index].Cells["MaNV"].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn muốn xóa nhân viên này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var checkAvailable = _checkAvailable(maNV);
                    if (checkAvailable.Count > 0)
                    {
                        MessageBox.Show("Đã có lỗi xảy ra:\n - " + string.Join("\n - ", checkAvailable.ToArray()), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (cls_EmployeeManagement.DeleteEmployee(maNV))
                        {
                            MessageBox.Show("Đã xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtListEmployess.DataSource = cls_EmployeeManagement.ShowEmployees();
                            _formatDT();
                        }
                        else
                        {
                            MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                var getChildControls = GetAll(this, typeof(TextBox)).Concat(GetAll(this, typeof(DateTimePicker)));
                var listOfErrors = getChildControls.Select(c => errorProvider.GetError(c))
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .ToList();
                MessageBox.Show("Vui lòng kiểm tra lại thông tin nhân viên:\n - " + string.Join("\n - ", listOfErrors.ToArray()), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (check == 1)
                {
                    string genMaNV = _genIdEmployess();
                    DateTime ngaySinh = dtpNgaySinh.Value;
                    bool gioiTinh = Convert.ToInt32(cmbGioiTinh.SelectedValue) == 1; // Giả định 0: Nam, 1: Nữ
                    int trangThai = cbDiLam.Checked ? 1 : 0;
                    if (cls_EmployeeManagement.AddEmployee(genMaNV, txtTenNV.Text, ngaySinh, gioiTinh, txtDiaChi.Text, txtSDT.Text, txtEmail.Text, txtCMND.Text, trangThai))
                    {
                        MessageBox.Show("Thêm nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _reset();
                        _sttButton(true, true, true, false, false, false);
                        _formatDT();
                        dtListEmployess.DataSource = cls_EmployeeManagement.ShowEmployees();
                        txtTimKiem.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm nhân viên này. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    DateTime ngaySinh = dtpNgaySinh.Value;
                    bool gioiTinh = Convert.ToInt32(cmbGioiTinh.SelectedValue) == 1; // Giả định 0: Nam, 1: Nữ
                    int trangThai = cbDiLam.Checked ? 1 : 0;
                    if (cls_EmployeeManagement.UpdateEmployee(maNV, maNV, txtTenNV.Text, ngaySinh, gioiTinh, txtDiaChi.Text, txtSDT.Text, txtEmail.Text, txtCMND.Text, trangThai))
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtListEmployess.DataSource = cls_EmployeeManagement.ShowEmployees();
                        _reset();
                        _formatDT();
                        _sttButton(true, true, true, false, false, false);
                        txtTimKiem.Focus();
                        maNV = null;
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật nhân viên này. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dtpNgaySinh_Validating(object sender, CancelEventArgs e)
        {
            if (dtpNgaySinh.Value > DateTime.Now)
            {
                errorProvider.SetError(dtpNgaySinh, "Ngày sinh không được lớn hơn ngày hiện tại.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(dtpNgaySinh, "");
            }
        }

        private void txtTenNV_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                errorProvider.SetError(txtTenNV, "Tên nhân viên không được trống.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtTenNV, "");
            }
        }

        private void txtCMND_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (string.IsNullOrWhiteSpace(txtCMND.Text))
            {
                errorProvider.SetError(txtCMND, "Số CMND không được trống.");
                e.Cancel = true;
            }
            else if (!regex.IsMatch(txtCMND.Text))
            {
                errorProvider.SetError(txtCMND, "Số CMND phải là số.");
                e.Cancel = true;
            }
            else if (txtCMND.Text.Length > 12 || txtCMND.Text.Length < 9) // Hỗ trợ CMND 9 hoặc 12 số
            {
                errorProvider.SetError(txtCMND, "Số CMND không đúng (9 hoặc 12 số).");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtCMND, "");
            }
        }

        private void txtDiaChi_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                errorProvider.SetError(txtDiaChi, "Địa chỉ không được trống.");
                e.Cancel = true;
            }
            else if (txtDiaChi.Text.Length > 255)
            {
                errorProvider.SetError(txtDiaChi, "Địa chỉ không được quá 255 kí tự.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtDiaChi, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Email không được trống.");
                e.Cancel = true;
            }
            else if (!regex.IsMatch(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Email không đúng định dạng example@domain.com.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtEmail, "");
            }
        }

        private void txtSDT_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                errorProvider.SetError(txtSDT, "Số điện thoại không được trống.");
                e.Cancel = true;
            }
            else if (txtSDT.Text.Length > 11 || txtSDT.Text.Length < 10) // Hỗ trợ số điện thoại 10-11 số
            {
                errorProvider.SetError(txtSDT, "Số điện thoại không chính xác (10-11 số).");
                e.Cancel = true;
            }
            else if (!regex.IsMatch(txtSDT.Text))
            {
                errorProvider.SetError(txtSDT, "Số điện thoại không chính xác.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtSDT, "");
            }
        }

        private void dtListEmployess_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtListEmployess.Rows.Count > 0 && e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                maNV = dtListEmployess.Rows[index].Cells["MaNV"].Value.ToString();
                txtMaNV.Text = maNV;
                txtTenNV.Text = dtListEmployess.Rows[index].Cells["TenNV"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(dtListEmployess.Rows[index].Cells["NgaySinh"].Value);
                txtCMND.Text = dtListEmployess.Rows[index].Cells["CMND"].Value.ToString();
                txtDiaChi.Text = dtListEmployess.Rows[index].Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dtListEmployess.Rows[index].Cells["Email"].Value.ToString();
                txtSDT.Text = dtListEmployess.Rows[index].Cells["SDT"].Value.ToString();
                cmbGioiTinh.SelectedValue = Convert.ToBoolean(dtListEmployess.Rows[index].Cells["GioiTinh"].Value) ? 1 : 0; // Giả định 0: Nam, 1: Nữ
                cbDiLam.Checked = Convert.ToBoolean(dtListEmployess.Rows[index].Cells["TrangThai"].Value);
                btnSua.Enabled = true;
            }
        }
    }
}