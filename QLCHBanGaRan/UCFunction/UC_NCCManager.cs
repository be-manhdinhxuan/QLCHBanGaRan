using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_NCCManager : UserControl
    {
        public UC_NCCManager()
        {
            InitializeComponent();
            // Đảm bảo sự kiện TextChanged được gắn
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
        }

        int check = 0;

        private void _sttButton(bool add, bool edit, bool delete, bool update, bool cancel, bool grpncc)
        {
            btnThem.Enabled = add;
            btnSua.Enabled = edit;
            btnXoa.Enabled = delete;
            btnCapNhat.Enabled = update;
            btnHuyBo.Enabled = cancel;
            grpThongTin.Enabled = grpncc;
        }

        private void _formatDT()
        {
            if (dtListNCC.Columns.Contains("IsDeleted"))
                dtListNCC.Columns["IsDeleted"].Visible = false;
        }

        private void _loadFilterOptions()
        {
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("Tên NCC");
            cmbFilter.Items.Add("Địa chỉ");
            cmbFilter.Items.Add("SĐT");
            cmbFilter.SelectedIndex = 0;
        }

        private void _searchNCC()
        {
            string filter = cmbFilter.SelectedItem?.ToString();
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                dtListNCC.DataSource = cls_NCC._showDetailNCC();
            }
            else
            {
                dtListNCC.DataSource = cls_NCC._searchNCC(filter, keyword);
            }
            _formatDT();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            check = 1;
            _sttButton(false, false, false, true, true, true);
            txtMaNCC.Text = cls_NCC.GenerateMaNCC();
            txtMaNCC.Enabled = false; // Không cho phép sửa mã NCC khi thêm mới
            txtTenNCC.Focus();
            txtDiaChi.Text = "";
            txtSDT.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            check = 2;
            if (dtListNCC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _sttButton(false, false, false, true, true, true);
                int index = dtListNCC.CurrentCell.RowIndex;
                txtMaNCC.Text = dtListNCC.Rows[index].Cells["MaNCC"].Value.ToString();
                txtTenNCC.Text = dtListNCC.Rows[index].Cells["TenNhaCungCap"].Value.ToString();
                txtDiaChi.Text = dtListNCC.Rows[index].Cells["DiaChi"].Value.ToString();
                txtSDT.Text = dtListNCC.Rows[index].Cells["SDT"].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtListNCC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int index = dtListNCC.CurrentCell.RowIndex;
                string maNCC = dtListNCC.Rows[index].Cells["MaNCC"].Value.ToString();

                DialogResult result = MessageBox.Show($"Bạn muốn đánh dấu nhà cung cấp có mã {maNCC} là đã xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cls_NCC._delNCC(maNCC))
                    {
                        MessageBox.Show($"Đã đánh dấu nhà cung cấp có mã {maNCC} là đã xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtListNCC.DataSource = cls_NCC._showDetailNCC(); // Cập nhật lại danh sách
                        _formatDT();
                    }
                    else
                    {
                        MessageBox.Show($"Không thể đánh dấu nhà cung cấp có mã {maNCC} là đã xóa. Vui lòng thử lại! (Chi tiết: Xem log hoặc liên hệ admin)", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (check == 1)
            {
                if (string.IsNullOrWhiteSpace(txtTenNCC.Text) || string.IsNullOrWhiteSpace(txtDiaChi.Text) || string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin (Tên NCC, Địa chỉ, SĐT)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (cls_NCC._addNCC(txtMaNCC.Text, txtTenNCC.Text, txtDiaChi.Text, txtSDT.Text))
                    {
                        MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _sttButton(true, true, true, false, false, false);
                        dtListNCC.DataSource = cls_NCC._showDetailNCC();
                        txtMaNCC.Text = "";
                        txtTenNCC.Text = "";
                        txtDiaChi.Text = "";
                        txtSDT.Text = "";
                        _formatDT();
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm nhà cung cấp này. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (check == 2)
            {
                if (string.IsNullOrWhiteSpace(txtTenNCC.Text) || string.IsNullOrWhiteSpace(txtDiaChi.Text) || string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Vui lòng kiểm tra lại các thông tin. Đảm bảo thông tin nhập đầy đủ và chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int index = dtListNCC.CurrentCell.RowIndex;
                    string maNCC = dtListNCC.Rows[index].Cells["MaNCC"].Value.ToString();

                    if (cls_NCC._updateNCC(maNCC, txtTenNCC.Text, txtDiaChi.Text, txtSDT.Text))
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _sttButton(true, true, true, false, false, false);
                        dtListNCC.DataSource = cls_NCC._showDetailNCC();
                        txtMaNCC.Text = "";
                        txtTenNCC.Text = "";
                        txtDiaChi.Text = "";
                        txtSDT.Text = "";
                        _formatDT();
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật nhà cung cấp này. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            _sttButton(true, true, true, false, false, false);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _sttButton(true, true, true, false, false, false);
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Product"].BringToFront();
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
        }

        private void UC_NCCManager_Load(object sender, EventArgs e)
        {
            dtListNCC.DataSource = cls_NCC._showDetailNCC();
            _formatDT();
            _sttButton(true, true, true, false, false, false);
            _loadFilterOptions();
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            _searchNCC(); // Gọi phương thức lọc ngay khi text thay đổi
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _searchNCC();
        }
    }
}