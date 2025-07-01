using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_UserManager : UserControl
    {
        private int check = 0;
        private string _MaND = "";

        public UC_UserManager()
        {
            InitializeComponent();
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

        private void _reset()
        {
            txtTenDangNhap.Text = "";
            txtMatKhau.Text = "";
            cbQuanTri.Checked = false;
            cmbNhanVien.SelectedIndex = -1;
            _MaND = "";
        }

        private void UC_UserManager_Load(object sender, EventArgs e)
        {
            _sttButton(true, false, true, false, false, false);
            _reset();
            LoadNhanVien();
            LoadNguoiDung();
        }

        private void LoadNhanVien()
        {
            string query = "SELECT MaNV, TenNV FROM NhanVien WHERE TrangThai = 1";
            DataTable dt = cls_DatabaseManager.TableRead(query);
            cmbNhanVien.ValueMember = "MaNV";
            cmbNhanVien.DisplayMember = "TenNV";
            cmbNhanVien.DataSource = dt;
        }

        private void LoadNguoiDung()
        {
            string query = @"
                SELECT nd.MaND, nv.TenNV, nd.TenDangNhap, nd.MatKhau, nd.LaQuanTri, nv.MaNV
                FROM NguoiDung nd
                LEFT JOIN NhanVien nv ON nd.MaNV = nv.MaNV";
            dtList.DataSource = cls_DatabaseManager.TableRead(query);
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
            if (string.IsNullOrEmpty(_MaND))
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult result = MessageBox.Show("Xóa người dùng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM NguoiDung WHERE MaND = @MaND";
                    SqlParameter[] parameters = { new SqlParameter("@MaND", _MaND) }; // Fix: Wrap the parameter in an array
                    bool delUser = cls_DatabaseManager.ExecuteNonQuery(query, parameters) > 0; // Pass the array instead of a single parameter
                    if (delUser)
                    {
                        MessageBox.Show("Xóa thành công người dùng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _reset();
                        _sttButton(true, false, true, false, false, false);
                        LoadNguoiDung();
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra và thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, false, true, false, false, false);
        }

        private void dtList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtList.Rows.Count > 0)
            {
                int index = dtList.CurrentCell.RowIndex;
                _MaND = dtList.Rows[index].Cells["NguoiDungID"].Value.ToString();
                cmbNhanVien.SelectedValue = dtList.Rows[index].Cells["MaNV"].Value;
                txtTenDangNhap.Text = dtList.Rows[index].Cells["TenDangNhap"].Value.ToString();
                txtMatKhau.Text = cls_EnCrypt.DecryptMD5(dtList.Rows[index].Cells["MatKhau"].Value.ToString()); // Giải mã để hiển thị
                cbQuanTri.Checked = Convert.ToBoolean(dtList.Rows[index].Cells["QuanTri"].Value);
                btnSua.Enabled = true;
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtMatKhau.Text.Length < 8)
            {
                MessageBox.Show("Mật khẩu ít nhất phải có 8 kí tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maNV = cmbNhanVien.SelectedValue.ToString();
            string tenDangNhap = txtTenDangNhap.Text;
            string matKhau = cls_EnCrypt.EncryptMD5(txtMatKhau.Text); // Mã hóa mật khẩu
            int laQuanTri = cbQuanTri.Checked ? 1 : 0;

            if (check == 1)
            {
                if (cls_DatabaseManager.CheckDuplicate("NguoiDung", "MaNV", maNV))
                {
                    MessageBox.Show("Nhân viên này đã có tài khoản. Vui lòng kiểm tra và thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cls_DatabaseManager.CheckDuplicate("NguoiDung", "TenDangNhap", tenDangNhap))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng kiểm tra và thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string maND = cls_DatabaseManager.GenerateRandomMaND();
                while (cls_DatabaseManager.CheckDuplicate("NguoiDung", "MaND", maND))
                {
                    maND = cls_DatabaseManager.GenerateRandomMaND();
                }

                string query = "INSERT INTO NguoiDung (MaND, TenDangNhap, MatKhau, LaQuanTri, MaNV) VALUES (@MaND, @TenDangNhap, @MatKhau, @LaQuanTri, @MaNV)";
                SqlParameter[] parameters = {
                    new SqlParameter("@MaND", maND),
                    new SqlParameter("@TenDangNhap", tenDangNhap),
                    new SqlParameter("@MatKhau", matKhau),
                    new SqlParameter("@LaQuanTri", laQuanTri),
                    new SqlParameter("@MaNV", maNV)
                }; // Fix: Wrap parameters in an array
                bool addUser = cls_DatabaseManager.ExecuteNonQuery(query, parameters) > 0; // Pass the array instead of individual parameters

                if (addUser)
                {
                    MessageBox.Show("Tạo tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _reset();
                    _sttButton(true, false, true, false, false, false);
                    LoadNguoiDung();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string query = "UPDATE NguoiDung SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, LaQuanTri = @LaQuanTri, MaNV = @MaNV WHERE MaND = @MaND";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenDangNhap", tenDangNhap),
                    new SqlParameter("@MatKhau", matKhau),
                    new SqlParameter("@LaQuanTri", laQuanTri),
                    new SqlParameter("@MaNV", maNV),
                    new SqlParameter("@MaND", _MaND)
                }; // Fix: Wrap parameters in an array
                bool updateUser = cls_DatabaseManager.ExecuteNonQuery(query, parameters) > 0; // Pass the array instead of individual parameters

                if (updateUser)
                {
                    MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _reset();
                    _sttButton(true, false, true, false, false, false);
                    LoadNguoiDung();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra và thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, false, true, false, false, false);
            // Thay thế bằng logic điều hướng của dự án bạn
            // Ví dụ: Forms.frm_Main.Instance.pnlContainer.Controls["UC_System"].BringToFront();
        }
    }
}