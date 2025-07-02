using QLCHBanGaRan.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            dtListProfile.Columns[0].Width = 60; // MaNV
            dtListProfile.Columns[1].Width = 150; // TenNV
            dtListProfile.Columns[2].Width = 100; // NgaySinh
            dtListProfile.Columns[3].Width = 80;  // GioiTinh
            dtListProfile.Columns[4].Width = 150; // DiaChi
            dtListProfile.Columns[5].Width = 100; // SDT
            dtListProfile.Columns[6].Width = 150; // Email
            dtListProfile.Columns[7].Width = 100; // CMND
            dtListProfile.Columns[8].Width = 80;  // TrangThai
            dtListProfile.Columns[9].Width = 100; // ChucDanh
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

            // Load dữ liệu từ bảng NhanVien
            DataTable dtNhanVien = cls_EmployeeManagement.ShowEmployees();
            cmbTenNV.DataSource = dtNhanVien;
            cmbTenNV.ValueMember = "MaNV";
            cmbTenNV.DisplayMember = "TenNV";

            // Load danh sách chức danh
            DataTable dtChucDanh = cls_EmployeeTitleManagement.GetTitles();
            cmbMaChucDanh.DataSource = dtChucDanh;
            cmbMaChucDanh.ValueMember = "MaChucDanh";
            cmbMaChucDanh.DisplayMember = "TenChucDanh";

            // Cài đặt giá trị mặc định cho combo box giới tính và trạng thái
            cmbGioiTinh.Items.AddRange(new string[] { "Nữ", "Nam" }); // 0: Nữ, 1: Nam
            cmbTrangThai.Items.AddRange(new string[] { "Đã nghỉ", "Đang làm" }); // 0: Đã nghỉ, 1: Đang làm

            _reset();
            _sttButton(true, false, true, false, false, false);
            _formatDT();
            dtListProfile.DataSource = cls_EmployeeManagement.ShowEmployees();
            dtListProfile.Columns["GioiTinh"].Visible = false; // Ẩn cột GioiTinh thô để xử lý hiển thị
            dtListProfile.Columns["TrangThai"].Visible = false; // Ẩn cột TrangThai thô
            dtListProfile.Columns.Add("GioiTinhText", "Giới tính");
            dtListProfile.Columns.Add("TrangThaiText", "Trạng thái");
            FormatDataGridView();
        }

        private void FormatDataGridView()
        {
            foreach (DataGridViewRow row in dtListProfile.Rows)
            {
                if (!row.IsNewRow)
                {
                    bool gioiTinh = Convert.ToBoolean(row.Cells["GioiTinh"].Value);
                    row.Cells["GioiTinhText"].Value = gioiTinh ? "Nam" : "Nữ";
                    int trangThai = Convert.ToInt32(row.Cells["TrangThai"].Value);
                    row.Cells["TrangThaiText"].Value = trangThai == 1 ? "Đang làm" : "Đã nghỉ";
                }
            }
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
                bool delProfile = cls_EmployeeManagement.DeleteEmployee(maNV);
                if (delProfile)
                {
                    MessageBox.Show("Xóa nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _reset();
                    _sttButton(true, false, true, false, false, false);
                    _formatDT();
                    dtListProfile.DataSource = cls_EmployeeManagement.ShowEmployees();
                    dtListProfile.Columns["GioiTinh"].Visible = false;
                    dtListProfile.Columns["TrangThai"].Visible = false;
                    dtListProfile.Columns["GioiTinhText"].Visible = true;
                    dtListProfile.Columns["TrangThaiText"].Visible = true;
                    FormatDataGridView();
                    maNV = "";
                }
                else
                {
                    MessageBox.Show("Không thể xóa nhân viên này vì có liên kết dữ liệu. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DateTime ngaySinh = dtpNgaySinh.Value;
                bool gioiTinh = cmbGioiTinh.SelectedIndex == 1; // 1: Nam, 0: Nữ
                string diaChi = txtDiaChi.Text;
                string sdt = txtSDT.Text;
                string email = txtEmail.Text;
                string cmnd = txtCMND.Text;
                int trangThai = cmbTrangThai.SelectedIndex == 1 ? 1 : 0; // 1: Đang làm, 0: Đã nghỉ
                string maChucDanh = cmbMaChucDanh.SelectedValue.ToString();

                // Thêm chức danh vào NhanVienChucDanh nếu chưa có
                if (check == 1 && !cls_EmployeeTitleManagement.GetEmployeeTitles().AsEnumerable().Any(r => r.Field<string>("MaNV") == maNV))
                {
                    cls_EmployeeTitleManagement.InsertEmployeeTitle(maNV, Convert.ToInt32(maChucDanh));
                }
                else if (check == 2)
                {
                    cls_EmployeeTitleManagement.UpdateEmployeeTitle(maNV, Convert.ToInt32(maChucDanh));
                }

                if (check == 1)
                {
                    if (!cls_EmployeeManagement.ShowEmployees().AsEnumerable().Any(r => r.Field<string>("MaNV") == maNV))
                    {
                        bool addProfile = cls_EmployeeManagement.AddEmployee(maNV, tenNV, ngaySinh, gioiTinh, diaChi, sdt, email, cmnd, trangThai);
                        if (addProfile)
                        {
                            MessageBox.Show("Thêm nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _reset();
                            _sttButton(true, false, true, false, false, false);
                            _formatDT();
                            dtListProfile.DataSource = cls_EmployeeManagement.ShowEmployees();
                            dtListProfile.Columns["GioiTinh"].Visible = false;
                            dtListProfile.Columns["TrangThai"].Visible = false;
                            dtListProfile.Columns["GioiTinhText"].Visible = true;
                            dtListProfile.Columns["TrangThaiText"].Visible = true;
                            FormatDataGridView();
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
                    string maNVCu = maNV; // Giữ nguyên mã nhân viên cũ khi cập nhật
                    bool updateProfile = cls_EmployeeManagement.UpdateEmployee(maNVCu, maNV, tenNV, ngaySinh, gioiTinh, diaChi, sdt, email, cmnd, trangThai);
                    if (updateProfile)
                    {
                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _reset();
                        _sttButton(true, false, true, false, false, false);
                        _formatDT();
                        dtListProfile.DataSource = cls_EmployeeManagement.ShowEmployees();
                        dtListProfile.Columns["GioiTinh"].Visible = false;
                        dtListProfile.Columns["TrangThai"].Visible = false;
                        dtListProfile.Columns["GioiTinhText"].Visible = true;
                        dtListProfile.Columns["TrangThaiText"].Visible = true;
                        FormatDataGridView();
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
                cmbGioiTinh.SelectedIndex = Convert.ToBoolean(dtListProfile.Rows[index].Cells[3].Value) ? 1 : 0; // 1: Nam, 0: Nữ
                txtDiaChi.Text = dtListProfile.Rows[index].Cells[4].Value.ToString();
                txtSDT.Text = dtListProfile.Rows[index].Cells[5].Value.ToString();
                txtEmail.Text = dtListProfile.Rows[index].Cells[6].Value.ToString();
                txtCMND.Text = dtListProfile.Rows[index].Cells[7].Value.ToString();
                cmbTrangThai.SelectedIndex = Convert.ToInt32(dtListProfile.Rows[index].Cells[8].Value) == 1 ? 1 : 0; // 1: Đang làm, 0: Đã nghỉ
                // Lấy MaChucDanh từ NhanVienChucDanh
                string query = "SELECT MaChucDanh FROM NhanVienChucDanh WHERE MaNV = @MaNV";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", SqlDbType.NVarChar, 10) { Value = maNV }
                };
                DataTable dtChucDanh = cls_DatabaseManager.TableRead(query, parameters);
                if (dtChucDanh.Rows.Count > 0)
                {
                    cmbMaChucDanh.SelectedValue = dtChucDanh.Rows[0]["MaChucDanh"].ToString();
                }
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