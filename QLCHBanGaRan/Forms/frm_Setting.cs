using System;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_Setting : Form
    {
        private bool isEditing = false;

        public frm_Setting()
        {
            InitializeComponent();
        }

        private void frm_Setting_Load(object sender, EventArgs e)
        {
            // Tải thông tin mặc định (giả định)
            LoadSettings();
            UpdateControls();
        }

        private void LoadSettings()
        {
            // Giả định tải dữ liệu từ database hoặc cấu hình
            txtStoreID.Text = "STORE001";
            txtCuaHang.Text = "Cửa hàng Gà Rán Chicken Bông";
            txtDiaChi.Text = "123 Đường Lê Lợi, Quận 1, TP.HCM";
            // Tải dữ liệu cho cmbFilter (giả định)
            cmbFilter.Items.AddRange(new string[] { "Tất cả", "Khu vực 1", "Khu vực 2" });
            cmbFilter.SelectedIndex = 0;
        }

        private void UpdateControls()
        {
            txtStoreID.Enabled = isEditing;
            txtCuaHang.Enabled = isEditing;
            txtDiaChi.Enabled = isEditing;
            btnLuu.Enabled = isEditing;
            btnSua.Text = isEditing ? "HỦY" : "SỬA";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Đóng form khi nhấn nút Close
            this.Close();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                // Hủy chỉnh sửa
                isEditing = false;
                LoadSettings(); // Khôi phục dữ liệu gốc
            }
            else
            {
                // Bật chế độ chỉnh sửa
                isEditing = true;
            }
            UpdateControls();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                // Lưu thông tin (giả định)
                if (SaveSettings())
                {
                    MessageBox.Show("Lưu thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isEditing = false;
                }
                else
                {
                    MessageBox.Show("Lưu thông tin thất bại. Vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                UpdateControls();
            }
        }

        private bool SaveSettings()
        {
            // Thay bằng logic lưu vào cơ sở dữ liệu
            // Ví dụ: Cập nhật StoreID, CuaHang, DiaChi vào database
            return true; // Giả định thành công
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xử lý khi thay đổi filter (giả định)
            string selectedFilter = cmbFilter.SelectedItem?.ToString();
            if (selectedFilter != null)
            {
                // Thêm logic lọc nếu cần
                MessageBox.Show($"Đã chọn: {selectedFilter}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}