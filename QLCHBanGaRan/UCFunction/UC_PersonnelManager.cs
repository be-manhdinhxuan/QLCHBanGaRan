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
            dtListEmployess.Columns["TenNV"].Width = 120;
            dtListEmployess.Columns["NgaySinh"].Width = 100;
            dtListEmployess.Columns["GioiTinh"].Width = 80;
            dtListEmployess.Columns["TrangThaiID"].Width = 70;
        }

        private void _reset()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            dtpNgaySinh.Value = DateTime.Now; // Đặt ngày mặc định
            cmbGioiTinh.SelectedIndex = -1; // Làm trống
            cbDiLam.Checked = false;
            txtTimKiem.Focus();
            errorProvider.Clear();
            dtListEmployess.Enabled = true; // Đảm bảo DataGridView được kích hoạt lại khi reset
        }

        private void _sttButton(bool edit, bool delete, bool update, bool cancel, bool grpinfo)
        {
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

        private List<string> _checkAvailable(string maNV)
        {
            List<string> msg = new List<string>();
            if (!cls_EmployeeManagement.CheckInNguoiDung(maNV))
            {
                msg.Add("Vui lòng xóa nhân viên này trong Người Dùng trước khi xóa nhân viên.");
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

            // Tải danh sách giới tính (0 = Nam, 1 = Nữ)
            var genderList = new[] {
                new { GioiTinhID = 0, GioiTinh = "Nam" },
                new { GioiTinhID = 1, GioiTinh = "Nữ" }
            };
            cmbGioiTinh.DataSource = genderList;
            cmbGioiTinh.ValueMember = "GioiTinhID";
            cmbGioiTinh.DisplayMember = "GioiTinh";

            dtListEmployess.AutoGenerateColumns = false;
            dtListEmployess.DataSource = cls_EmployeeManagement.ShowEmployees();

            _formatDT();
            _reset();
            _sttButton(true, true, false, false, false);

            // Cập nhật cmbFilter với các lựa chọn: Mã NV, Tên NV, Giới tính
            cmbFilter.ValueMember = "Value";
            cmbFilter.DisplayMember = "Text";
            var items = new[] {
                new { Text = "Mã NV", Value = "MaNV" },
                new { Text = "Tên NV", Value = "TenNV" },
                new { Text = "Giới tính", Value = "GioiTinh" }
            };
            cmbFilter.DataSource = items;
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string filterColumn = cmbFilter.SelectedValue.ToString();
                string searchText = txtTimKiem.Text.Trim();
                dtListEmployess.DataSource = cls_EmployeeManagement.SearchEmployees(filterColumn, searchText);
                _formatDT();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, false, false, false);
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Personnel"].BringToFront();
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
                _sttButton(false, false, true, true, true);
                dtListEmployess.Enabled = false; // Vô hiệu hóa tương tác với DataGridView
                txtTenNV.Focus();
                int index = dtListEmployess.CurrentCell.RowIndex;
                maNV = dtListEmployess.Rows[index].Cells["MaNV"].Value.ToString();
                txtMaNV.Text = maNV;
                txtMaNV.Enabled = false;
                txtTenNV.Text = dtListEmployess.Rows[index].Cells["TenNV"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(dtListEmployess.Rows[index].Cells["NgaySinh"].Value);

                // Ánh xạ: 0 = Nam, 1 = Nữ
                string gioiTinhText = dtListEmployess.Rows[index].Cells["GioiTinh"].Value.ToString();
                int gioiTinhValue = gioiTinhText == "Nam" ? 0 : 1;
                cmbGioiTinh.SelectedValue = gioiTinhValue;
                Console.WriteLine($"CellClick - maNV: {maNV}, GioiTinhText: {gioiTinhText}, GioiTinhValue: {gioiTinhValue}");

                cbDiLam.Checked = Convert.ToBoolean(dtListEmployess.Rows[index].Cells["TrangThaiID"].Value);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, false, false, false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtListEmployess.SelectedRows.Count == 0 || string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
                        _reset();
                        _sttButton(true, false, false, false, false); // Vô hiệu hóa btnSua và btnXoa sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (check == 2) // Chỉ cập nhật
                {
                    DateTime ngaySinh = dtpNgaySinh.Value;
                    int gioiTinhValue = Convert.ToInt32(cmbGioiTinh.SelectedValue); // Lấy giá trị thô (0 hoặc 1)
                    bool gioiTinh = gioiTinhValue == 1; // 1 = Nữ, 0 = Nam
                    int trangThai = cbDiLam.Checked ? 1 : 0;

                    // Lấy dữ liệu cũ từ cơ sở dữ liệu
                    DataTable dtEmployee = cls_EmployeeManagement.GetEmployeeByMaNV(maNV);
                    if (dtEmployee == null || dtEmployee.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy thông tin nhân viên để cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DataRow row = dtEmployee.Rows[0];
                    string sdt = row["SDT"] != DBNull.Value ? row["SDT"].ToString() : null;
                    string diaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : null;
                    string email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null;
                    string cmnd = row["CMND"] != DBNull.Value ? row["CMND"].ToString() : null;
                    string maChucDanh = row["MaChucDanh"] != DBNull.Value ? row["MaChucDanh"].ToString() : null;

                    Console.WriteLine($"CapNhat - maNV: {maNV}, txtMaNV.Text: {txtMaNV.Text}, GioiTinhValue: {gioiTinhValue}, GioiTinh: {gioiTinh}, SDT: {sdt}");
                    // Sử dụng giá trị NULL nếu dữ liệu cũ là NULL, hoặc giữ nguyên giá trị hợp lệ
                    if (cls_EmployeeManagement.UpdateEmployee(maNV, txtMaNV.Text, txtTenNV.Text, ngaySinh, gioiTinh,
                        diaChi, sdt, email, cmnd, trangThai, maChucDanh))
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtListEmployess.DataSource = cls_EmployeeManagement.ShowEmployees();
                        _reset();
                        _sttButton(true, true, false, false, false);
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

        private void dtListEmployess_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtListEmployess.Rows.Count > 0 && e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                maNV = dtListEmployess.Rows[index].Cells["MaNV"].Value.ToString();
                txtMaNV.Text = maNV;
                txtMaNV.Enabled = false;
                txtTenNV.Text = dtListEmployess.Rows[index].Cells["TenNV"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(dtListEmployess.Rows[index].Cells["NgaySinh"].Value);

                // Ánh xạ: 0 = Nam, 1 = Nữ
                string gioiTinhText = dtListEmployess.Rows[index].Cells["GioiTinh"].Value.ToString();
                int gioiTinhValue = gioiTinhText == "Nam" ? 0 : 1;
                cmbGioiTinh.SelectedValue = gioiTinhValue;
                Console.WriteLine($"CellClick - maNV: {maNV}, GioiTinhText: {gioiTinhText}, GioiTinhValue: {gioiTinhValue}");

                cbDiLam.Checked = Convert.ToBoolean(dtListEmployess.Rows[index].Cells["TrangThaiID"].Value);

                btnSua.Enabled = true;
            }
        }
    }
}