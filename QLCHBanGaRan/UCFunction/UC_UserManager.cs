using QLCHBanGaRan.lib;
using QLCHBanGaRan.UCSystems;
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
        private string _matKhauGiaiMa = ""; // Lưu mật khẩu gốc hoặc mật khẩu đang nhập
        private string _matKhauMoi = ""; // Lưu mật khẩu mới đang nhập
        private bool _isMouseDown = false;
        private string _maNV = ""; // ✅ Biến để lưu MaNV thật sự

        public UC_UserManager()
        {
            InitializeComponent();
            // Gán sự kiện
            txtMatKhau.KeyPress += txtMatKhau_KeyPress;
            txtMatKhau.TextChanged += txtMatKhau_TextChanged;
            // Gán sự kiện CellFormatting và DataError cho dtList
            dtList.CellFormatting += dtList_CellFormatting;
            dtList.DataError += dtList_DataError;
        }

        private void _sttButton(bool add, bool edit, bool delete, bool update, bool cancel, bool grpinfo)
        {
            btnSua.Enabled = edit;
            btnXoa.Enabled = delete;
            btnCapNhat.Enabled = update;
            btnHuyBo.Enabled = cancel;
            grpThongTin.Enabled = grpinfo;
            txtTenDangNhap.Enabled = grpinfo;
            txtMatKhau.Enabled = grpinfo;
            cbQuanTri.Enabled = grpinfo;
        }

        private void _reset()
        {
            txtTenDangNhap.Text = "";
            txtMatKhau.Text = "";
            txtNhanVien.Text = "";
            _maNV = ""; // Reset MaNV
            cbQuanTri.Checked = false;
            _MaND = "";
            _matKhauGiaiMa = "";
            _matKhauMoi = ""; // Reset mật khẩu mới
            txtMatKhau.Tag = null; // Reset Tag
            txtMatKhau.UseSystemPasswordChar = true; // Đặt lại trạng thái mặc định
        }

        private void UC_UserManager_Load(object sender, EventArgs e)
        {
            _sttButton(true, false, false, false, false, false);
            _reset();
            LoadNhanVien();
            LoadNguoiDung();
        }

        private void LoadNhanVien()
        {
            string query = "SELECT MaNV, TenNV FROM NhanVien WHERE TrangThai = 0 AND IsDeleted = 0";
            DataTable dt = cls_DatabaseManager.TableRead(query);
            //txtNhanVien.ValueMember = "MaNV";
            //txtNhanVien.DisplayMember = "TenNV";
            //txtNhanVien.DataSource = dt;
        }

        private void LoadNguoiDung()
        {
            string query = @"
                SELECT nd.MaND AS MaND, nv.TenNV, nd.TenDangNhap, 
                       nd.MatKhau AS MatKhau, -- Lấy mật khẩu đã mã hóa AES
                       nd.LaQuanTri AS QuanTri, nv.MaNV
                FROM NguoiDung nd
                LEFT JOIN NhanVien nv ON nd.MaNV = nv.MaNV
                WHERE nd.IsDeleted = 0 AND (nv.IsDeleted = 0 OR nv.IsDeleted IS NULL)";
            DataTable dt = cls_DatabaseManager.TableRead(query);

            // Đổi tên cột QuanTri thành Quản trị trong DataTable
            if (dt.Columns.Contains("QuanTri"))
            {
                dt.Columns["QuanTri"].ColumnName = "Quản trị"; // Đổi tên cột trong DataTable
            }

            dtList.DataSource = dt;

            // Cấu hình cột Quản trị thành checkbox
            if (dtList.Columns.Contains("Quản trị"))
            {
                dtList.Columns["Quản trị"].HeaderText = "Quản trị";
                ((DataGridViewCheckBoxColumn)dtList.Columns["Quản trị"]).TrueValue = 1;
                ((DataGridViewCheckBoxColumn)dtList.Columns["Quản trị"]).FalseValue = 0;
                dtList.Columns["Quản trị"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }

            // Cấu hình hiển thị cột MatKhau
            if (dtList.Columns.Contains("MatKhau"))
            {
                dtList.Columns["MatKhau"].DefaultCellStyle.Font = new System.Drawing.Font(dtList.Font.FontFamily, 12F, System.Drawing.FontStyle.Regular);
            }
        }

        private void dtList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Ẩn mật khẩu bằng chấm trong cột MatKhau
            if (dtList.Columns[e.ColumnIndex].Name == "MatKhau" && e.Value != null)
            {
                e.Value = new string('•', 5); // Hiển thị 5 chấm
                e.FormattingApplied = true; // Ngăn chặn định dạng mặc định
            }
        }

        private void dtList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Ghi log lỗi và bỏ qua dialog mặc định
            Console.WriteLine($"DataGridView DataError: {e.Exception.Message}");
            e.Cancel = true; // Ngăn hiển thị dialog lỗi
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            check = 2;
            _sttButton(false, false, false, true, true, true);
            // Chỉ giải mã và lưu vào _matKhauGiaiMa, không hiển thị ngay
            if (!string.IsNullOrEmpty(txtMatKhau.Tag?.ToString()))
            {
                _matKhauGiaiMa = cls_Encryption.Decrypt(txtMatKhau.Tag.ToString());
                txtMatKhau.Text = new string('•', Math.Min(_matKhauGiaiMa.Length, 5)); // Giữ nguyên 5 chấm
                txtMatKhau.UseSystemPasswordChar = true; // Đảm bảo ẩn
            }
            _matKhauMoi = ""; // Reset mật khẩu mới khi bắt đầu sửa
            txtMatKhau.Focus(); // Đưa con trỏ vào txtMatKhau để nhập
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_MaND))
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Error: _MaND is null or empty");
            }
            else
            {
                Console.WriteLine($"Attempting to delete MaND: {_MaND}");
                DialogResult result = MessageBox.Show("Xóa người dùng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string query = "EXEC sp_DeleteUser @MaND, @Result OUTPUT";
                        SqlParameter[] parameters = {
                            new SqlParameter("@MaND", _MaND),
                            new SqlParameter("@Result", SqlDbType.Bit) { Direction = ParameterDirection.Output }
                        };
                        int rowsAffected = cls_DatabaseManager.ExecuteNonQuery(query, parameters);
                        bool resultValue = (bool)parameters[1].Value;

                        Console.WriteLine($"ExecuteNonQuery - Rows affected: {rowsAffected}, Result: {resultValue}, Query: {query}");
                        if (resultValue)
                        {
                            MessageBox.Show("Xóa thành công người dùng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _reset();
                            _sttButton(true, false, false, false, false, false);
                            LoadNguoiDung();
                        }
                        else
                        {
                            MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra và thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine($"Error: Rows affected: {rowsAffected}, Result: {resultValue}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"Exception: {ex.Message}");
                    }
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _reset();
            _sttButton(true, false, false, false, false, false);
        }

        private void dtList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtList.Rows.Count > 0 && e.RowIndex >= 0)
            {
                int index = e.RowIndex;

                if (dtList.Rows[index].DataBoundItem is DataRowView rowView)
                {
                    _MaND = rowView["MaND"]?.ToString() ?? "";
                    txtTenDangNhap.Text = rowView["TenDangNhap"]?.ToString() ?? "";
                    _maNV = rowView["MaNV"]?.ToString() ?? "";
                    txtNhanVien.Text = rowView["TenNV"]?.ToString() ?? "";

                    cbQuanTri.Checked = rowView["Quản trị"] != DBNull.Value && Convert.ToBoolean(rowView["Quản trị"]); // Sử dụng cột mới

                    string query = "SELECT MatKhau FROM NguoiDung WHERE MaND = @MaND";
                    SqlParameter[] param = { new SqlParameter("@MaND", _MaND) };
                    DataTable dt = cls_DatabaseManager.TableRead(query, param);
                    if (dt.Rows.Count > 0)
                    {
                        string encryptedPass = dt.Rows[0]["MatKhau"].ToString();
                        txtMatKhau.Tag = encryptedPass; // Gán mật khẩu mã hóa vào Tag
                        txtMatKhau.UseSystemPasswordChar = true;
                        txtMatKhau.Text = new string('•', Math.Min(encryptedPass.Length / 4, 5)); // Hiển thị 5 chấm
                    }

                    grpThongTin.Enabled = true;
                    _sttButton(true, true, true, false, false, false);
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra độ dài mật khẩu dựa trên _matKhauMoi hoặc _matKhauGiaiMa
            string matKhauToCheck = string.IsNullOrEmpty(_matKhauMoi) ? _matKhauGiaiMa : _matKhauMoi;
            if (matKhauToCheck.Length < 8)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 8 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maNV = _maNV; // Sử dụng MaNV đã lưu
            string tenDangNhap = txtTenDangNhap.Text;
            string matKhau = string.IsNullOrEmpty(_matKhauMoi) ? cls_Encryption.Encrypt(_matKhauGiaiMa) : cls_Encryption.Encrypt(_matKhauMoi); // Sử dụng mật khẩu cũ hoặc mới
            int laQuanTri = cbQuanTri.Checked ? 1 : 0;

            if (check == 2)
            {
                string query = "UPDATE NguoiDung SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, LaQuanTri = @LaQuanTri, MaNV = @MaNV WHERE MaND = @MaND";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenDangNhap", tenDangNhap),
                    new SqlParameter("@MatKhau", matKhau),
                    new SqlParameter("@LaQuanTri", laQuanTri),
                    new SqlParameter("@MaNV", maNV),
                    new SqlParameter("@MaND", _MaND)
                };
                bool updateUser = cls_DatabaseManager.ExecuteNonQuery(query, parameters) > 0;

                if (updateUser)
                {
                    MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _reset();
                    _sttButton(true, false, false, false, false, false);
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
            _sttButton(true, false, false, false, false, false);
            Forms.frm_Main.Instance.Controls["frm_System"].BringToFront();
        }

        // Khi giữ chuột
        private void pictureEye_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseDown triggered on pictureEye");
            if (e.Button == MouseButtons.Left)
            {
                // Nếu đang ở chế độ sửa (check == 2), hiển thị mật khẩu mới
                if (check == 2 && !string.IsNullOrEmpty(_matKhauMoi))
                {
                    _matKhauGiaiMa = _matKhauMoi; // Sử dụng mật khẩu mới
                    Console.WriteLine($"Displaying new password: {_matKhauGiaiMa}");
                }
                else if (txtMatKhau.Tag != null)
                {
                    string encryptedPassword = txtMatKhau.Tag.ToString();
                    Console.WriteLine($"Attempting to decrypt: {encryptedPassword}");
                    string decryptedPassword = cls_Encryption.Decrypt(encryptedPassword);
                    if (decryptedPassword != null)
                    {
                        _matKhauGiaiMa = decryptedPassword; // Sử dụng mật khẩu cũ
                        Console.WriteLine($"Decrypted password: {_matKhauGiaiMa}");
                    }
                    else
                    {
                        Console.WriteLine("Decryption failed or returned null");
                    }
                }
                txtMatKhau.Text = _matKhauGiaiMa;
                txtMatKhau.UseSystemPasswordChar = false; // Hiển thị rõ ràng
                txtMatKhau.Refresh(); // Đảm bảo giao diện cập nhật
            }
        }

        // Khi thả chuột
        private void pictureEye_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseUp triggered on pictureEye");
            if (e.Button == MouseButtons.Left)
            {
                txtMatKhau.UseSystemPasswordChar = true; // Ẩn lại
                if (!string.IsNullOrEmpty(_matKhauGiaiMa))
                {
                    txtMatKhau.Text = new string('•', _matKhauGiaiMa.Length); // Số chấm dựa trên độ dài
                }
                else if (txtMatKhau.Tag != null)
                {
                    string encryptedPassword = txtMatKhau.Tag.ToString();
                    string decryptedLength = cls_Encryption.Decrypt(encryptedPassword);
                    txtMatKhau.Text = new string('•', Math.Min(decryptedLength?.Length ?? 5, 5));
                }
                else
                {
                    txtMatKhau.Text = new string('•', 5); // Mặc định
                }
                txtMatKhau.Refresh(); // Đảm bảo giao diện cập nhật
            }
        }

        // Cập nhật _matKhauMoi khi nhập mật khẩu mới
        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            if (check == 2) // Chỉ cập nhật khi đang ở chế độ sửa
            {
                // Không sử dụng GetPlainTextPassword, chỉ dựa vào KeyPress
                Console.WriteLine($"TextChanged - _matKhauMoi unchanged, waiting for KeyPress: {_matKhauMoi}");
            }
        }

        // Bắt từng ký tự nhập để xây dựng _matKhauMoi
        private void txtMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (check == 2) // Chỉ xử lý khi đang ở chế độ sửa
            {
                if (char.IsControl(e.KeyChar)) // Xử lý Backspace hoặc Delete
                {
                    if (_matKhauMoi.Length > 0)
                    {
                        _matKhauMoi = _matKhauMoi.Substring(0, _matKhauMoi.Length - 1);
                    }
                }
                else if (!char.IsControl(e.KeyChar)) // Thêm ký tự mới
                {
                    _matKhauMoi += e.KeyChar;
                }
                Console.WriteLine($"KeyPress - _matKhauMoi updated to: {_matKhauMoi}");
            }
        }
    }
}