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
        private string _chucDanhID = ""; // Sử dụng string cho MaChucDanh

        public UC_SalaryManager()
        {
            InitializeComponent();
        }

        private void _reset()
        {
            txtChucDanh.Text = "";
            txtLuongCung.Text = "";
            txtPhuCap.Text = "";
            txtMaChucDanh.Text = GenerateNewMaChucDanh(); // Tự động tạo mã mới khi reset
            errorProvider.Clear();
            dtList.Enabled = true; // Đảm bảo DataGridView được kích hoạt lại
        }

        private void _sttButton(bool add, bool edit, bool delete, bool update, bool cancel, bool grpinfo, bool dtListEnabled)
        {
            btnThem.Enabled = add;
            btnSua.Enabled = edit;
            btnXoa.Enabled = delete;
            btnCapNhat.Enabled = update;
            btnHuyBo.Enabled = cancel;
            grpThongTin.Enabled = grpinfo;
            dtList.Enabled = dtListEnabled; // Thêm điều khiển trạng thái của dtList
        }

        private IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                           .Concat(controls)
                           .Where(c => c.GetType() == type);
        }

        private string GenerateNewMaChucDanh()
        {
            DataTable dt = lib.cls_Salary.GetChucDanh();
            if (dt.Rows.Count == 0)
            {
                return "CD001"; // Mã mặc định nếu không có dữ liệu
            }
            else
            {
                string maxMaChucDanh = dt.AsEnumerable()
                    .Max(row => row.Field<string>("MaChucDanh"));
                int number = int.Parse(maxMaChucDanh.Replace("CD", ""));
                return "CD" + (number + 1).ToString("D3"); // Tăng số và định dạng 3 chữ số
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _sttButton(true, false, true, false, false, false, true);
            _reset();
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Salary"].BringToFront();
        }

        private void UC_SalaryManager_Load(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, false, true, false, false, false, true);
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            dtList.AutoGenerateColumns = false;

            // Gán dữ liệu
            dtList.DataSource = lib.cls_Salary.GetChucDanh();
            dtList.Enabled = true; // Đảm bảo DataGridView có thể tương tác

            // Log để kiểm tra
            Console.WriteLine("UC_SalaryManager_Load: DataSource assigned");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            check = 1;
            _sttButton(false, false, false, true, true, true, false); // Vô hiệu hóa dtList khi thêm
            txtChucDanh.Text = ""; // Làm sạch
            txtLuongCung.Text = ""; // Làm sạch
            txtPhuCap.Text = ""; // Làm sạch
            txtMaChucDanh.Text = GenerateNewMaChucDanh(); // Chỉ hiển thị mã mới
            txtChucDanh.Focus();
            Console.WriteLine("btnThem_Click: check set to 1, btnCapNhat enabled, MaChucDanh = " + txtMaChucDanh.Text);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            check = 2;
            if (string.IsNullOrEmpty(_chucDanhID))
            {
                MessageBox.Show("Vui lòng chọn một mã chức danh để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _sttButton(false, false, false, true, true, true, false); // Vô hiệu hóa dtList khi sửa
            txtChucDanh.Focus();
            Console.WriteLine("btnSua_Click: check set to 2, btnCapNhat enabled, _chucDanhID = " + _chucDanhID);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_chucDanhID))
            {
                MessageBox.Show("Vui lòng chọn thông tin cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn muốn xóa lương chức danh " + txtChucDanh.Text + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (!lib.cls_Salary.CheckChucDanh(_chucDanhID))
                    {
                        // Lấy danh sách nhân viên liên quan để hiển thị
                        DataTable dtNhanVien = lib.cls_Salary.GetNhanVienByMaChucDanh(_chucDanhID);
                        if (dtNhanVien != null && dtNhanVien.Rows.Count > 0)
                        {
                            string nhanVienList = string.Join(", ", dtNhanVien.AsEnumerable().Select(r => r.Field<string>("TenNV")));
                            DialogResult confirmDelete = MessageBox.Show($"Chức danh này đang được sử dụng bởi các nhân viên: {nhanVienList}.\nBạn có muốn xóa cả các nhân viên này không?",
                                "Xác nhận xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                            if (confirmDelete == DialogResult.Yes)
                            {
                                bool _delCD = lib.cls_Salary.DeleteChucDanh(_chucDanhID, true); // Xóa cascade
                                if (_delCD)
                                {
                                    MessageBox.Show("Xóa thành công, bao gồm các nhân viên liên quan.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    _sttButton(true, false, true, false, false, false, true);
                                    _reset();
                                    dtList.DataSource = lib.cls_Salary.GetChucDanh();
                                    _chucDanhID = "";
                                }
                                else
                                {
                                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra và thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (confirmDelete == DialogResult.No)
                            {
                                MessageBox.Show("Vui lòng cập nhật hoặc xóa các nhân viên liên quan trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        bool _delCD = lib.cls_Salary.DeleteChucDanh(_chucDanhID);
                        if (_delCD)
                        {
                            MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _sttButton(true, false, true, false, false, false, true);
                            _reset();
                            dtList.DataSource = lib.cls_Salary.GetChucDanh();
                            _chucDanhID = "";
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
            Console.WriteLine("btnCapNhat_Click: Started, check = " + check);
            if (!this.ValidateChildren())
            {
                var getChildControls = GetAll(this, typeof(TextBox));
                var listOfErrors = getChildControls.Select(c => errorProvider.GetError(c))
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .ToList();
                MessageBox.Show("Vui lòng kiểm tra lại thông tin lương:\n - " + string.Join("\n - ", listOfErrors.ToArray()), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("btnCapNhat_Click: Validation failed");
                return;
            }

            decimal luongCoBan, phuCap;
            // Loại bỏ định dạng hàng nghìn khi phân tích
            if (!decimal.TryParse(txtLuongCung.Text.Replace(".", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out luongCoBan) ||
                !decimal.TryParse(txtPhuCap.Text.Replace(".", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out phuCap))
            {
                MessageBox.Show("Lương cơ bản và phụ cấp phải là số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("btnCapNhat_Click: Parse failed");
                return;
            }

            if (check == 1)
            {
                bool _insertCD = lib.cls_Salary.InsertChucDanh(txtMaChucDanh.Text, txtChucDanh.Text, luongCoBan, phuCap);
                if (_insertCD)
                {
                    MessageBox.Show("Thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _sttButton(true, false, true, false, false, false, true); // Kích hoạt lại dtList
                    _reset();
                    dtList.DataSource = lib.cls_Salary.GetChucDanh();
                }
                else
                {
                    MessageBox.Show("Không thể thêm thông tin lương. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (check == 2)
            {
                bool _updateCD = lib.cls_Salary.UpdateChucDanh(txtChucDanh.Text, luongCoBan, phuCap, _chucDanhID);
                if (_updateCD)
                {
                    MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _sttButton(true, false, true, false, false, false, true); // Kích hoạt lại dtList
                    _reset();
                    dtList.DataSource = lib.cls_Salary.GetChucDanh();
                    _chucDanhID = "";
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật thông tin lương. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chế độ Thêm hoặc Sửa trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("btnCapNhat_Click: Invalid check value");
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _sttButton(true, false, true, false, false, false, true); // Kích hoạt lại dtList
            _reset();
        }

        private void dtList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dtList.Rows.Count > 0 && dtList.Enabled) // Chỉ cho phép khi dtList Enabled
            {
                DataGridViewRow row = dtList.Rows[e.RowIndex];
                // Lấy MaChucDanh nguyên bản
                _chucDanhID = row.Cells["MaChucDanh"].Value?.ToString() ?? "";
                txtMaChucDanh.Text = _chucDanhID; // Sử dụng tên cột
                txtChucDanh.Text = row.Cells["TenChucDanh"].Value?.ToString() ?? ""; // Sử dụng tên cột
                // Lấy giá trị gốc từ DataRow thay vì chuỗi đã định dạng
                txtLuongCung.Text = (row.DataBoundItem as DataRowView)?.Row.Field<decimal>("LuongCoBan").ToString("N0") ?? "0";
                txtPhuCap.Text = (row.DataBoundItem as DataRowView)?.Row.Field<decimal>("PhuCap").ToString("N0") ?? "0";
                btnSua.Enabled = true;
                Console.WriteLine("dtList_CellClick: _chucDanhID = " + _chucDanhID + ", LuongCoBan = " + (row.DataBoundItem as DataRowView)?.Row.Field<decimal>("LuongCoBan") + ", PhuCap = " + (row.DataBoundItem as DataRowView)?.Row.Field<decimal>("PhuCap"));
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
            else if (!decimal.TryParse(txtLuongCung.Text.Replace(".", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out _))
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
            else if (!decimal.TryParse(txtPhuCap.Text.Replace(".", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out _))
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
            if (dtList.Columns[e.ColumnIndex].Name == "TienLuong" || dtList.Columns[e.ColumnIndex].Name == "PhuCap") // Áp dụng cho cả hai cột
            {
                e.Value = e.Value == null ? "0" : e.Value;
                e.Value = decimal.Parse(e.Value.ToString()).ToString("N0"); // Định dạng với dấu phân cách hàng nghìn
                e.FormattingApplied = true;
            }
        }
    }
}