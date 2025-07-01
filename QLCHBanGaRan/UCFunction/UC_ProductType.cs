using System;
using System.Data;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_ProductType : UserControl
    {
        public UC_ProductType()
        {
            InitializeComponent();
        }

        private void _sttButton(bool add, bool edit, bool delete, bool update, bool cancel, bool grpProductType)
        {
            btnThem.Enabled = add;
            btnSua.Enabled = edit;
            btnXoa.Enabled = delete;
            btnCapNhat.Enabled = update;
            btnHuyBo.Enabled = cancel;
            grpLoaiSanPham.Enabled = grpProductType;
        }

        private void UC_ProductType_Load(object sender, EventArgs e)
        {
            LoadProductTypeData();
            _sttButton(false, true, false, false, false, false);
        }

        private void LoadProductTypeData()
        {
            // Giả định cls_Product có phương thức _showProductType()
            dtListProductType.DataSource = lib.cls_Product._showProductType();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _sttButton(false, true, false, false, false, false);
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Product"].BringToFront();
            txtLoaiSanPham.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoaiSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Giả định cls_Product có phương thức _addProductType(string tenLoaiSP)
            bool success = lib.cls_Product._addProductType(txtLoaiSanPham.Text);
            if (success)
            {
                MessageBox.Show("Thêm loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProductTypeData();
                txtLoaiSanPham.Text = "";
                _sttButton(false, true, false, false, false, false);
            }
            else
            {
                MessageBox.Show("Không thể thêm loại sản phẩm. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtListProductType.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _sttButton(false, false, false, true, true, true);

            int index = dtListProductType.CurrentCell.RowIndex;
            txtLoaiSanPham.Text = dtListProductType.Rows[index].Cells["TenLoaiSP"].Value.ToString();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoaiSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtListProductType.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = dtListProductType.CurrentCell.RowIndex;
            int loaiSPID = Convert.ToInt32(dtListProductType.Rows[index].Cells["LoaiSPID"].Value);

            // Giả định cls_Product có phương thức _updateProductType(int loaiSPID, string tenLoaiSP)
            bool success = lib.cls_Product._updateProductType(loaiSPID, txtLoaiSanPham.Text);
            if (success)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProductTypeData();
                txtLoaiSanPham.Text = "";
                _sttButton(false, true, false, false, false, false);
            }
            else
            {
                MessageBox.Show("Không thể cập nhật loại sản phẩm này. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtListProductType.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = dtListProductType.CurrentCell.RowIndex;
            int loaiSPID = Convert.ToInt32(dtListProductType.Rows[index].Cells["LoaiSPID"].Value);

            // Giả định cls_Product có phương thức _delProductType(int loaiSPID)
            if (lib.cls_Product._delProductType(loaiSPID))
            {
                MessageBox.Show("Xóa loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProductTypeData();
            }
            else
            {
                MessageBox.Show("Không thể xóa loại sản phẩm này. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            txtLoaiSanPham.Text = "";
            _sttButton(false, true, false, false, false, false);
        }
    }
}