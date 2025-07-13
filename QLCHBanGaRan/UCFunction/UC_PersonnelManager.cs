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
            dtListEmployess.Columns["CMND"].Width = 100;
            dtListEmployess.Columns["SDT"].Width = 110;
            dtListEmployess.Columns["MaChucDanh"].Width = 150; // Thêm cột MaChucDanh
        }

        private void _reset()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            dtpNgaySinh.Value = DateTime.Now; // Đặt ngày mặc định (có thể điều chỉnh)
            txtCMND.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            cbDiLam.Checked = false;
            cmbGioiTinh.SelectedIndex = -1; // Làm trống hoặc đặt về giá trị mặc định
            cmbChucDanh.SelectedIndex = -1; // Làm trống hoặc đặt về giá trị mặc định
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
            int maxNumber = 0;

            if (dt.Rows.Count > 0)
            {
                // Lấy số lớn nhất từ MaNV (giả định định dạng NVxxx)
                maxNumber = dt.Rows.Cast<DataRow>()
                              .Max(row => int.Parse(row["MaNV"].ToString().Substring(2, 3)));
            }

            // Tăng số lên 1 và định dạng lại
            int newNumber = maxNumber + 1;
            if (newNumber < 10)
            {
                return "NV00" + newNumber;
            }
            if (newNumber < 100)
            {
                return "NV0" + newNumber;
            }
            return "NV" + newNumber;
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
            // Bỏ các kiểm tra không cần thiết vì các bảng không tồn tại
            // if (!cls_EmployeeManagement.CheckInHoSoNhanVien(maNV))
            // {
            //     msg.Add("Vui lòng xóa nhân viên này trong Hồ Sơ Nhân Viên trước khi xóa nhân viên.");
            // }
            // if (!cls_EmployeeManagement.CheckInNhanVienBoPhan(maNV))
            // {
            //     msg.Add("Vui lòng xóa nhân viên này trong Bộ Phận trước khi xóa nhân viên.");
            // }
            // if (!cls_EmployeeManagement.CheckInNhanVienChucDanh(maNV))
            // {
            //     msg.Add("Vui lòng xóa nhân viên này trong Chức Danh trước khi xóa nhân viên.");
            // }
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

            // Tải danh sách chức danh
            cmbChucDanh.DataSource = cls_EmployeeManagement.GetChucDanh();
            cmbChucDanh.ValueMember = "MaChucDanh";
            cmbChucDanh.DisplayMember = "TenChucDanh";

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
            _reset(); // Làm trống tất cả các trường trước
            txtMaNV.Text = _genIdEmployess(); // Gán mã nhân viên mới
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

                // Ánh xạ đúng: 0 = Nữ, 1 = Nam
                string gioiTinhText = dtListEmployess.Rows[index].Cells["GioiTinh"].Value.ToString();
                int gioiTinhValue = gioiTinhText == "Nam" ? 0 : 1; // Đúng: "Nam" = 1, "Nữ" = 0
                cmbGioiTinh.SelectedValue = gioiTinhValue;

                cbDiLam.Checked = Convert.ToBoolean(dtListEmployess.Rows[index].Cells["TrangThaiID"].Value);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, true, false, false, false);
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
                        _sttButton(true, false, false, false, false, false); // Vô hiệu hóa btnSua và btnXoa sau khi xóa
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
                if (check == 1) // Thêm mới
                {
                    string genMaNV = _genIdEmployess();
                    DateTime ngaySinh = dtpNgaySinh.Value;
                    bool gioiTinh = Convert.ToInt32(cmbGioiTinh.SelectedValue) == 1; // 1 = Nữ, 0 = Nam
                    int trangThai = cbDiLam.Checked ? 1 : 0;
                    string maChucDanh = cmbChucDanh.SelectedValue?.ToString() ?? "";
                    if (cls_EmployeeManagement.AddEmployee(genMaNV, txtTenNV.Text, ngaySinh, gioiTinh, txtDiaChi.Text, txtSDT.Text, txtEmail.Text, txtCMND.Text, trangThai, maChucDanh))
                    {
                        MessageBox.Show("Thêm nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _reset();
                        _sttButton(true, true, true, false, false, false);
                        _formatDT();
                        dtListEmployess.DataSource = cls_EmployeeManagement.ShowEmployees();
                        txtTimKiem.Focus();
                    }
                }
                else // Cập nhật
                {
                    DateTime ngaySinh = dtpNgaySinh.Value;
                    int gioiTinhValue = Convert.ToInt32(cmbGioiTinh.SelectedValue); // Lấy giá trị thô (0 hoặc 1)
                    bool gioiTinh = gioiTinhValue == 1; // 1 = Nữ, 0 = Nam
                    int trangThai = cbDiLam.Checked ? 1 : 0;
                    string maChucDanh = cmbChucDanh.SelectedValue?.ToString() ?? "";
                    Console.WriteLine($"CapNhat - maNV: {maNV}, txtMaNV.Text: {txtMaNV.Text}, GioiTinhValue: {gioiTinhValue}, GioiTinh: {gioiTinh}");
                    if (cls_EmployeeManagement.UpdateEmployee(maNV, txtMaNV.Text, txtTenNV.Text, ngaySinh, gioiTinh, txtDiaChi.Text, txtSDT.Text, txtEmail.Text, txtCMND.Text, trangThai, maChucDanh))
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtListEmployess.DataSource = cls_EmployeeManagement.ShowEmployees();
                        _reset();
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
            else if (txtCMND.Text.Length != 9)
            {
                errorProvider.SetError(txtCMND, "Số CMND phải có đúng 9 số.");
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

                // Ánh xạ đúng: 0 = Nam, 1 = Nữ (theo CSDL và ShowEmployees)
                string gioiTinhText = dtListEmployess.Rows[index].Cells["GioiTinh"].Value.ToString();
                int gioiTinhValue = gioiTinhText == "Nam" ? 0 : 1; // "Nam" = 0, "Nữ" = 1
                cmbGioiTinh.SelectedValue = gioiTinhValue;
                Console.WriteLine($"CellClick - maNV: {maNV}, GioiTinhText: {gioiTinhText}, GioiTinhValue: {gioiTinhValue}");

                cbDiLam.Checked = Convert.ToBoolean(dtListEmployess.Rows[index].Cells["TrangThaiID"].Value);

                DataTable dt = dtListEmployess.DataSource as DataTable;
                if (dt != null && index < dt.Rows.Count)
                {
                    string maChucDanh = dt.Rows[index]["MaChucDanh"].ToString();
                    if (!string.IsNullOrEmpty(maChucDanh))
                    {
                        DataTable dtChucDanh = cmbChucDanh.DataSource as DataTable;
                        DataRow[] rows = dtChucDanh?.Select($"MaChucDanh = '{maChucDanh.Replace("'", "''")}'");
                        if (rows != null && rows.Length > 0)
                        {
                            cmbChucDanh.SelectedValue = maChucDanh;
                        }
                        else
                        {
                            cmbChucDanh.SelectedIndex = -1;
                            Console.WriteLine($"Không tìm thấy MaChucDanh: {maChucDanh}");
                        }
                    }
                    else
                    {
                        cmbChucDanh.SelectedIndex = -1;
                        Console.WriteLine("MaChucDanh rỗng");
                    }
                }

                btnSua.Enabled = true;
            }
        }
    }
}