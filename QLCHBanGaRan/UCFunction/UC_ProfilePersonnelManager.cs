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
    public partial class UC_ProfilePersonnelManager : UserControl
    {
        private int check = 0;
        private string maNV = null;

        public UC_ProfilePersonnelManager()
        {
            InitializeComponent();
        }

        private void _formatDT()
        {
            // Xóa cột TrangThaiID cũ nếu có
            if (dtListProfile.Columns["TrangThaiID"] != null)
            {
                dtListProfile.Columns.Remove("TrangThaiID");
            }

            // Tạo cột checkbox mới
            DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn
            {
                Name = "TrangThaiID",
                HeaderText = "Đi làm",
                DataPropertyName = "TrangThaiID",
                TrueValue = 1,
                FalseValue = 0,
                Width = 70
            };
            dtListProfile.Columns.Add(chkColumn);

            // Định dạng các cột khác
            if (dtListProfile.Columns["MaNV"] != null) dtListProfile.Columns["MaNV"].Width = 60;
            if (dtListProfile.Columns["TenNV"] != null) dtListProfile.Columns["TenNV"].Width = 120;
            if (dtListProfile.Columns["CMND"] != null) dtListProfile.Columns["CMND"].Width = 100;
            if (dtListProfile.Columns["SDT"] != null) dtListProfile.Columns["SDT"].Width = 110;
        }

        private void _reset()
        {
            txtTenNV.Text = "";
            dtpNgaySinh.Value = DateTime.Now; // Đặt ngày mặc định (có thể điều chỉnh)
            txtCMND.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            cbDiLam.Checked = false;
            cmbGioiTinh.SelectedIndex = -1; // Làm trống hoặc đặt về giá trị mặc định
            cmbChucDanh.SelectedIndex = -1; // Làm trống hoặc đặt về giá trị mặc định
            errorProvider.Clear(); // Xóa thông báo lỗi
            txtTimKiem.Focus();
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
            return msg;
        }

        private void UC_ProfilePersonnelManager_Load(object sender, EventArgs e)
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

            // Tạm thời để AutoGenerateColumns = true để kiểm tra
            dtListProfile.AutoGenerateColumns = false;
            DataTable dt = cls_EmployeeManagement.ShowEmployees();
            // Log để kiểm tra giá trị TrangThaiID
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"Load - MaNV: {row["MaNV"]}, TrangThaiID: {row["TrangThaiID"]}, Type: {row["TrangThaiID"].GetType()}");
            }
            dtListProfile.DataSource = dt;
            _formatDT();

            // Debug: Kiểm tra trạng thái checkbox sau khi load
            Console.WriteLine("=== Debug Checkbox Status ===");
            for (int i = 0; i < dtListProfile.Rows.Count; i++)
            {
                var checkboxValue = dtListProfile.Rows[i].Cells["TrangThaiID"].Value;
                var dataRowValue = dt.Rows[i]["TrangThaiID"];
                Console.WriteLine($"Row {i}: Checkbox Value = {checkboxValue} ({checkboxValue?.GetType()}), DataRow Value = {dataRowValue} ({dataRowValue?.GetType()})");
            }
            Console.WriteLine("=== End Debug ===");

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

            // Gán sự kiện CellClick
            dtListProfile.CellClick += new DataGridViewCellEventHandler(dtListProfile_CellClick);
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string searchText = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(searchText))
                {
                    string filterColumn = cmbFilter.SelectedValue?.ToString() ?? "TenNV";
                    dtListProfile.DataSource = cls_EmployeeManagement.SearchEmployees(filterColumn, searchText);
                    _formatDT();
                }
                else
                {
                    dtListProfile.DataSource = cls_EmployeeManagement.ShowEmployees();
                    _formatDT();
                }
            }
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
            txtMaNV.Text = _genIdEmployess(); // Gán mã nhân viên mới
            txtMaNV.Enabled = false; // Không cho chỉnh sửa mã
            _reset(); // Làm trống các trường khác, giữ txtMaNV
            dtListProfile.Enabled = false; // Vô hiệu hóa dtListProfile khi đang thêm
            txtTenNV.Focus(); // Đặt focus vào trường tên để người dùng nhập
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            check = 2;
            if (dtListProfile.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _sttButton(false, false, false, true, true, true);
                txtTenNV.Focus();
                int index = dtListProfile.CurrentCell.RowIndex;
                maNV = dtListProfile.Rows[index].Cells["MaNV"].Value?.ToString();
                txtMaNV.Text = maNV;
                txtMaNV.Enabled = false; // Không cho chỉnh sửa mã
                txtTenNV.Text = dtListProfile.Rows[index].Cells["TenNV"].Value?.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(dtListProfile.Rows[index].Cells["NgaySinh"].Value);
                txtCMND.Text = dtListProfile.Rows[index].Cells["CMND"].Value?.ToString();
                txtDiaChi.Text = dtListProfile.Rows[index].Cells["DiaChi"].Value?.ToString();
                txtEmail.Text = dtListProfile.Rows[index].Cells["Email"].Value?.ToString();
                txtSDT.Text = dtListProfile.Rows[index].Cells["SDT"].Value?.ToString();

                // Ánh xạ đúng: 0 = Nam, 1 = Nữ
                string gioiTinhText = dtListProfile.Rows[index].Cells["GioiTinh"].Value?.ToString();
                int gioiTinhValue = gioiTinhText == "Nam" ? 0 : 1; // "Nam" = 0, "Nữ" = 1
                cmbGioiTinh.SelectedValue = gioiTinhValue;
                Console.WriteLine($"CellClick - maNV: {maNV}, GioiTinhText: {gioiTinhText}, GioiTinhValue: {gioiTinhValue}");

                // Ánh xạ trạng thái đi làm
                int trangThaiValue = Convert.ToInt32(dtListProfile.Rows[index].Cells["TrangThaiID"].Value);
                cbDiLam.Checked = (trangThaiValue == 1); // Đảm bảo ánh xạ 1 = true, 0 = false
                Console.WriteLine($"CellClick - maNV: {maNV}, TrangThaiID: {trangThaiValue}, cbDiLam.Checked: {cbDiLam.Checked}");

                DataTable dt = dtListProfile.DataSource as DataTable;
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
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, true, true, false, false, false);
            dtListProfile.Enabled = true; // Khôi phục dtListProfile khi hủy
            maNV = null; // Xóa biến maNV để tránh nhầm lẫn
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtListProfile.SelectedRows.Count == 0 || string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = dtListProfile.CurrentCell.RowIndex;
            string maNV = dtListProfile.Rows[index].Cells["MaNV"].Value?.ToString();

            // Thêm thông báo hỏi ý kiến trước khi xóa
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa nhân viên {maNV} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        dtListProfile.DataSource = cls_EmployeeManagement.ShowEmployees();
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
            // Nếu chọn No hoặc hủy, không làm gì thêm
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // Kiểm tra tuổi >= 18
            DateTime ngaySinhCheck = dtpNgaySinh.Value;
            int tuoi = DateTime.Now.Year - ngaySinhCheck.Year;
            if (ngaySinhCheck > DateTime.Now.AddYears(-tuoi)) tuoi--;
            if (tuoi < 18)
            {
                MessageBox.Show("Nhân viên phải đủ 18 tuổi trở lên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateChildren())
            {
                var getChildControls = GetAll(this, typeof(TextBox)).Concat(GetAll(this, typeof(DateTimePicker)));
                var listOfErrors = getChildControls.Select(c => errorProvider.GetError(c))
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .ToList();
                MessageBox.Show("Vui lòng kiểm tra lại thông tin nhân viên:\n - " + string.Join("\n - ", listOfErrors.ToArray()), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (check == 1) // Thêm mới
            {
                string genMaNV = txtMaNV.Text; // Sử dụng mã đã gán trong btnThem_Click
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
                    dtListProfile.DataSource = cls_EmployeeManagement.ShowEmployees();
                    dtListProfile.Enabled = true; // Khôi phục dtListProfile khi thêm thành công
                    txtTimKiem.Focus();
                }
                else
                {
                    txtMaNV.Text = genMaNV; // Giữ mã vừa tạo
                }
            }
            else // Cập nhật
            {
                DateTime ngaySinh = dtpNgaySinh.Value;
                int gioiTinhValue = Convert.ToInt32(cmbGioiTinh.SelectedValue); // Lấy giá trị thô (0 hoặc 1)
                bool gioiTinh = gioiTinhValue == 1; // 1 = Nữ, 0 = Nam
                int trangThai = cbDiLam.Checked ? 1 : 0; // Đảm bảo lấy giá trị mới từ cbDiLam
                Console.WriteLine($"btnCapNhat - maNV: {maNV}, TrangThai: {trangThai}, GioiTinhValue: {gioiTinhValue}, GioiTinh: {gioiTinh}, CMND: {txtCMND.Text}");
                string maChucDanh = cmbChucDanh.SelectedValue?.ToString() ?? "";
                bool isSuccess = cls_EmployeeManagement.UpdateEmployee(maNV, txtMaNV.Text, txtTenNV.Text, ngaySinh, gioiTinh, txtDiaChi.Text, txtSDT.Text, txtEmail.Text, txtCMND.Text, trangThai, maChucDanh);
                Console.WriteLine($"Update result: {isSuccess}");
                if (isSuccess)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtListProfile.DataSource = cls_EmployeeManagement.ShowEmployees(); // Làm mới dữ liệu
                    dtListProfile.Refresh(); // Đảm bảo làm mới giao diện
                    Application.DoEvents(); // Đảm bảo giao diện được cập nhật
                                            // Tự động chọn lại dòng vừa cập nhật
                    int targetRowIndex = -1;
                    for (int i = 0; i < dtListProfile.Rows.Count; i++)
                    {
                        if (dtListProfile.Rows[i].Cells["MaNV"].Value?.ToString() == maNV && dtListProfile.Rows[i].Displayed)
                        {
                            targetRowIndex = i;
                            break;
                        }
                    }
                    if (targetRowIndex >= 0)
                    {
                        dtListProfile.ClearSelection();
                        dtListProfile.Rows[targetRowIndex].Selected = true;
                        dtListProfile.FirstDisplayedScrollingRowIndex = targetRowIndex; // Cuộn đến hàng
                        dtListProfile_CellClick(dtListProfile, new DataGridViewCellEventArgs(0, targetRowIndex));
                    }
                    _reset();
                    _sttButton(true, true, true, false, false, false);
                    dtListProfile.Enabled = true; // Khôi phục dtListProfile khi cập nhật thành công
                    txtTimKiem.Focus();
                    maNV = null;
                }
                // Nếu thất bại, không làm gì thêm, giữ nguyên dữ liệu để người dùng sửa
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

        private void dtListProfile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtListProfile.Rows.Count > 0 && e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                maNV = dtListProfile.Rows[index].Cells["MaNV"].Value?.ToString();
                txtMaNV.Text = maNV;
                txtTenNV.Text = dtListProfile.Rows[index].Cells["TenNV"].Value?.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(dtListProfile.Rows[index].Cells["NgaySinh"].Value);
                txtCMND.Text = dtListProfile.Rows[index].Cells["CMND"].Value?.ToString();
                txtDiaChi.Text = dtListProfile.Rows[index].Cells["DiaChi"].Value?.ToString();
                txtEmail.Text = dtListProfile.Rows[index].Cells["Email"].Value?.ToString();
                txtSDT.Text = dtListProfile.Rows[index].Cells["SDT"].Value?.ToString();

                // Ánh xạ đúng: 0 = Nam, 1 = Nữ
                string gioiTinhText = dtListProfile.Rows[index].Cells["GioiTinh"].Value?.ToString();
                int gioiTinhValue = gioiTinhText == "Nam" ? 0 : 1; // "Nam" = 0, "Nữ" = 1
                cmbGioiTinh.SelectedValue = gioiTinhValue;
                Console.WriteLine($"CellClick - maNV: {maNV}, GioiTinhText: {gioiTinhText}, GioiTinhValue: {gioiTinhValue}");

                // Ánh xạ trạng thái đi làm
                int trangThaiValue = Convert.ToInt32(dtListProfile.Rows[index].Cells["TrangThaiID"].Value);
                cbDiLam.Checked = (trangThaiValue == 1); // Đảm bảo ánh xạ 1 = true, 0 = false
                Console.WriteLine($"CellClick - maNV: {maNV}, TrangThaiID: {trangThaiValue}, cbDiLam.Checked: {cbDiLam.Checked}");

                // Lưu giá trị ban đầu từ cơ sở dữ liệu
                cls_EmployeeManagement.SetInitialValues(maNV);

                DataTable dt = dtListProfile.DataSource as DataTable;
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