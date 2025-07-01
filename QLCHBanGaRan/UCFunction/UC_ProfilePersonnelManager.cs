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

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_ProfilePersonnelManager : UserControl
    {
        private int rowSelected;
        private int check = 0;
        private string maNV = "";

        public UC_ProfilePersonnelManager()
        {
            InitializeComponent();
        }

        private void _reset()
        {
            cmbTenNV.SelectedIndex = -1;
            dtpNgaySinh.Value = DateTime.Now;
            cmbGioiTinh.SelectedIndex = -1;
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtCMND.Text = "";
            cmbTrangThai.SelectedIndex = -1;
            cmbMaChucDanh.SelectedIndex = -1;
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

        private void _formatDT()
        {
            dtListProfile.Columns[1].Width = 150;
            dtListProfile.Columns[2].Width = 100;
            dtListProfile.Columns[3].Width = 80;
            dtListProfile.Columns[4].Width = 150;
            dtListProfile.Columns[5].Width = 100;
            dtListProfile.Columns[6].Width = 150;
            dtListProfile.Columns[7].Width = 100;
            dtListProfile.Columns[8].Width = 80;
            dtListProfile.Columns[9].Width = 100;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, false, true, false, false, false);
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Personnel"].BringToFront();
        }

        private void UC_ProfilePersonnelManager_Load(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.EnableAllowFocusChange;

            // Load dữ liệu từ bảng NhanVien và ChucDanh
            DataTable dtNhanVien = lib.cls_Employess._getEmployeeList();
            DataTable dtChucDanh = lib.cls_Employess._getChucDanhList();

            cmbTenNV.DataSource = dtNhanVien;
            cmbTenNV.ValueMember = "MaNV";
            cmbTenNV.DisplayMember = "TenNV";

            cmbMaChucDanh.DataSource = dtChucDanh;
            cmbMaChucDanh.ValueMember = "MaChucDanh";
            cmbMaChucDanh.DisplayMember = "TenChucDanh";

            _reset();
            _sttButton(true, false, true, false, false, false);
            _formatDT();
            dtListProfile.DataSource = lib.cls_Employess._showProfileInfo();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            check = 1;
            _sttButton(false, false, false, true, true, true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            check = 2;
            _sttButton(false, false, false, true, true, true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                bool delProfile = lib.cls_Employess._delEmployee(maNV);
                if (delProfile)
                {
                    MessageBox.Show("Xóa nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _reset();
                    _sttButton(true, false, true, false, false, false);
                    _formatDT();
                    dtListProfile.DataSource = lib.cls_Employess._showProfileInfo();
                    maNV = "";
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra và thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                var getChildControls = GetAll(this, typeof(ComboBox)).Concat(GetAll(this, typeof(TextBox)));
                var listOfErrors = getChildControls.Select(c => errorProvider.GetError(c))
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .ToList();
                MessageBox.Show("Vui lòng kiểm tra lại thông tin nhân viên:\n - " + string.Join("\n - ", listOfErrors.ToArray()), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string maNV = cmbTenNV.SelectedValue.ToString();
                string tenNV = cmbTenNV.Text;
                string ngaySinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd");
                int gioiTinh = cmbGioiTinh.SelectedIndex; // 1: Nam, 0: Nữ
                string diaChi = txtDiaChi.Text;
                string sdt = txtSDT.Text;
                string email = txtEmail.Text;
                string cmnd = txtCMND.Text;
                int trangThai = cmbTrangThai.SelectedIndex; // 1: Đang làm, 0: Đã nghỉ
                string maChucDanh = cmbMaChucDanh.SelectedValue.ToString();

                if (check == 1)
                {
                    if (lib.cls_Employess._checkEmployee(maNV))
                    {
                        bool addProfile = lib.cls_Employess._addEmployee(maNV, tenNV, ngaySinh, gioiTinh, diaChi, sdt, email, cmnd, trangThai, maChucDanh);
                        if (addProfile)
                        {
                            MessageBox.Show("Thêm nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _reset();
                            _sttButton(true, false, true, false, false, false);
                            _formatDT();
                            dtListProfile.DataSource = lib.cls_Employess._showProfileInfo();
                        }
                        else
                        {
                            MessageBox.Show("Không thể thêm nhân viên này. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nhân viên này đã tồn tại. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool updateProfile = lib.cls_Employess._updateEmployee(maNV, tenNV, ngaySinh, gioiTinh, diaChi, sdt, email, cmnd, trangThai, maChucDanh);
                    if (updateProfile)
                    {
                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _reset();
                        _sttButton(true, false, true, false, false, false);
                        _formatDT();
                        dtListProfile.DataSource = lib.cls_Employess._showProfileInfo();
                        rowSelected = -1;
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật nhân viên này. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, false, true, false, false, false);
        }

        private void dtListProfile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtListProfile.Rows.Count > 0)
            {
                int index = dtListProfile.CurrentCell.RowIndex;
                maNV = dtListProfile.Rows[index].Cells[0].Value.ToString();
                cmbTenNV.SelectedValue = maNV;
                dtpNgaySinh.Value = Convert.ToDateTime(dtListProfile.Rows[index].Cells[2].Value);
                cmbGioiTinh.SelectedIndex = Convert.ToInt32(dtListProfile.Rows[index].Cells[3].Value) == 1 ? 0 : 1;
                txtDiaChi.Text = dtListProfile.Rows[index].Cells[4].Value.ToString();
                txtSDT.Text = dtListProfile.Rows[index].Cells[5].Value.ToString();
                txtEmail.Text = dtListProfile.Rows[index].Cells[6].Value.ToString();
                txtCMND.Text = dtListProfile.Rows[index].Cells[7].Value.ToString();
                cmbTrangThai.SelectedIndex = Convert.ToInt32(dtListProfile.Rows[index].Cells[8].Value);
                cmbMaChucDanh.SelectedValue = dtListProfile.Rows[index].Cells[9].Value.ToString();
                rowSelected = index;
                btnSua.Enabled = true;
            }
        }

        private void cmbTenNV_Validating(object sender, CancelEventArgs e)
        {
            if (cmbTenNV.SelectedValue == null)
            {
                errorProvider.SetError(cmbTenNV, "Vui lòng chọn tên nhân viên.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cmbTenNV, "");
            }
        }

        private void cmbGioiTinh_Validating(object sender, CancelEventArgs e)
        {
            if (cmbGioiTinh.SelectedIndex == -1)
            {
                errorProvider.SetError(cmbGioiTinh, "Vui lòng chọn Giới tính.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cmbGioiTinh, "");
            }
        }

        private void cmbTrangThai_Validating(object sender, CancelEventArgs e)
        {
            if (cmbTrangThai.SelectedIndex == -1)
            {
                errorProvider.SetError(cmbTrangThai, "Vui lòng chọn Trạng thái.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cmbTrangThai, "");
            }
        }

        private void cmbMaChucDanh_Validating(object sender, CancelEventArgs e)
        {
            if (cmbMaChucDanh.SelectedValue == null)
            {
                errorProvider.SetError(cmbMaChucDanh, "Vui lòng chọn Chức danh.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cmbMaChucDanh, "");
            }
        }

        private void txtSDT_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSDT.Text) || !Regex.IsMatch(txtSDT.Text, @"^[0-9]{10}$"))
            {
                errorProvider.SetError(txtSDT, "Vui lòng nhập SĐT hợp lệ (10 số).");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtSDT, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider.SetError(txtEmail, "Vui lòng nhập Email hợp lệ.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtEmail, "");
            }
        }

        private void txtCMND_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCMND.Text) && !Regex.IsMatch(txtCMND.Text, @"^[0-9]{12}$"))
            {
                errorProvider.SetError(txtCMND, "Vui lòng nhập CMND/CCCD hợp lệ (12 số).");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtCMND, "");
            }
        }

        private IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                           .Concat(controls)
                           .Where(c => c.GetType() == type);
        }
    }
}