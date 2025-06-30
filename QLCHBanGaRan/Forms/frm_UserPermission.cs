using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_UserPermission : Form
    {
        private string connectionString = "Data Source=YourServer;Initial Catalog=QLCHBanGaRan;Integrated Security=True"; // Thay bằng chuỗi kết nối thực tế

        public frm_UserPermission()
        {
            InitializeComponent();
        }

        private void frm_UserPermission_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            // Tải ChucDanh
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryChucDanh = "SELECT MaChucDanh, TenChucDanh FROM ChucDanh";
                using (SqlDataAdapter da = new SqlDataAdapter(queryChucDanh, conn))
                {
                    DataTable dtChucDanh = new DataTable();
                    da.Fill(dtChucDanh);
                    cmbChucDanh.DataSource = dtChucDanh;
                    cmbChucDanh.DisplayMember = "TenChucDanh";
                    cmbChucDanh.ValueMember = "MaChucDanh";
                }

                // Tải NhanVien
                string queryNhanVien = "SELECT MaNV, TenNV FROM NhanVien WHERE TrangThai = 1";
                using (SqlDataAdapter da = new SqlDataAdapter(queryNhanVien, conn))
                {
                    DataTable dtNhanVien = new DataTable();
                    da.Fill(dtNhanVien);
                    cmbNhanVien.DataSource = dtNhanVien;
                    cmbNhanVien.DisplayMember = "TenNV";
                    cmbNhanVien.ValueMember = "MaNV";
                }

                // Tải NguoiDung
                string queryNguoiDung = "SELECT MaND, TenDangNhap FROM NguoiDung";
                using (SqlDataAdapter da = new SqlDataAdapter(queryNguoiDung, conn))
                {
                    DataTable dtNguoiDung = new DataTable();
                    da.Fill(dtNguoiDung);
                    cmbNguoiDung.DataSource = dtNguoiDung;
                    cmbNguoiDung.DisplayMember = "TenDangNhap";
                    cmbNguoiDung.ValueMember = "MaND";
                }
            }

            // Tải phân quyền mặc định (giả định)
            LoadPermissions();
        }

        private void LoadPermissions()
        {
            if (cmbNguoiDung.SelectedValue != null)
            {
                string maND = cmbNguoiDung.SelectedValue.ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM NguoiDung nd WHERE nd.MaND = @MaND";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaND", maND);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Giả định phân quyền được lưu trong một bảng khác hoặc thuộc tính LaQuanTri
                                bool isAdmin = reader["LaQuanTri"].ToString() == "1";
                                cbHeThong.Checked = isAdmin;
                                cbNhanSu.Checked = isAdmin;
                                cbTienLuong.Checked = isAdmin;
                                cbThongKe.Checked = isAdmin;
                                cbSanPham.Checked = isAdmin;
                                cbDanhMuc.Checked = isAdmin;
                                cbGoiMon.Checked = !isAdmin; // Nhân viên có thể gọi món
                            }
                        }
                    }
                }
            }
        }

        private void cmbNguoiDung_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPermissions();
        }

        private void cmbNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cập nhật NguoiDung dựa trên MaNV
            if (cmbNhanVien.SelectedValue != null)
            {
                string maNV = cmbNhanVien.SelectedValue.ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MaND, TenDangNhap FROM NguoiDung WHERE MaNV = @MaNV";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@MaNV", maNV);
                        DataTable dtNguoiDung = new DataTable();
                        da.Fill(dtNguoiDung);
                        cmbNguoiDung.DataSource = dtNguoiDung;
                        cmbNguoiDung.DisplayMember = "TenDangNhap";
                        cmbNguoiDung.ValueMember = "MaND";
                    }
                }
                LoadPermissions();
            }
        }

        private void cmbChucDanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Có thể lọc NhanVien theo ChucDanh nếu cần
            if (cmbChucDanh.SelectedValue != null)
            {
                string maChucDanh = cmbChucDanh.SelectedValue.ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MaNV, TenNV FROM NhanVien WHERE MaChucDanh = @MaChucDanh AND TrangThai = 1";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@MaChucDanh", maChucDanh);
                        DataTable dtNhanVien = new DataTable();
                        da.Fill(dtNhanVien);
                        cmbNhanVien.DataSource = dtNhanVien;
                        cmbNhanVien.DisplayMember = "TenNV";
                        cmbNhanVien.ValueMember = "MaNV";
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Đóng form khi nhấn nút Close
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cmbNguoiDung.SelectedValue != null)
            {
                string maND = cmbNguoiDung.SelectedValue.ToString();
                int permissions = 0;
                if (cbHeThong.Checked) permissions += 1;
                if (cbNhanSu.Checked) permissions += 2;
                if (cbTienLuong.Checked) permissions += 4;
                if (cbThongKe.Checked) permissions += 8;
                if (cbSanPham.Checked) permissions += 16;
                if (cbDanhMuc.Checked) permissions += 32;
                if (cbGoiMon.Checked) permissions += 64;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE NguoiDung SET LaQuanTri = @LaQuanTri WHERE MaND = @MaND";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaND", maND);
                        cmd.Parameters.AddWithValue("@LaQuanTri", permissions == 127 ? 1 : 0); // 127 là tổng quyền (admin)
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Phân quyền đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}