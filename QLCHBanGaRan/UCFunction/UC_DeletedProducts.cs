using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_DeletedProducts : UserControl
    {
        public UC_DeletedProducts()
        {
            InitializeComponent();
            // Gán sự kiện CellClick
            dtDeletedProducts.CellClick += new DataGridViewCellEventHandler(dtDeletedProducts_CellClick);
            LoadDeletedProducts();
            cbCategory.SelectedIndex = 0; // Mặc định chọn "Đồ ăn"
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Category"].BringToFront();
        }

        private void LoadDeletedProducts()
        {
            try
            {
                string category = "";
                if (cbCategory.SelectedIndex >= 0)
                {
                    category = cbCategory.SelectedItem.ToString() == "Đồ ăn" ? "DoAn" : "DoUong";
                }
                else
                {
                    category = "DoAn"; // Giá trị mặc định nếu không có mục nào được chọn
                    cbCategory.SelectedIndex = 0; // Đặt lại mặc định
                }

                string searchText = txtSearch.Text.Trim();
                string idColumn = category == "DoAn" ? "MaMon" : "MaDoUong";
                string nameColumn = category == "DoAn" ? "TenMon" : "TenDoUong";

                string query = $@"
                    SELECT {idColumn} AS MaSP, {nameColumn} AS TenSP, GiaTien, SoLuong, GiamGia 
                    FROM {category} 
                    WHERE IsDeleted = 1 
                    {(string.IsNullOrEmpty(searchText) ? "" : $"AND {nameColumn} LIKE @SearchText")}";
                SqlParameter[] parameters = string.IsNullOrEmpty(searchText) ? null : new SqlParameter[] { new SqlParameter("@SearchText", $"%{searchText}%") };
                DataTable dt = cls_DatabaseManager.TableRead(query, parameters);

                // Gán DataSource trước
                dtDeletedProducts.DataSource = dt;

                // Tự động điều chỉnh cột và hàng
                dtDeletedProducts.AutoResizeColumns();
                dtDeletedProducts.AutoResizeRows();

                // Kiểm tra và gán HeaderText một cách an toàn
                if (dtDeletedProducts.Columns.Contains("MaSP"))
                    dtDeletedProducts.Columns["MaSP"].HeaderText = "Mã sản phẩm";
                if (dtDeletedProducts.Columns.Contains("TenSP"))
                    dtDeletedProducts.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                if (dtDeletedProducts.Columns.Contains("GiaTien"))
                    dtDeletedProducts.Columns["GiaTien"].HeaderText = "Giá tiền";
                if (dtDeletedProducts.Columns.Contains("SoLuong"))
                    dtDeletedProducts.Columns["SoLuong"].HeaderText = "Số lượng";
                if (dtDeletedProducts.Columns.Contains("GiamGia"))
                    dtDeletedProducts.Columns["GiamGia"].HeaderText = "Giảm giá";

                // Debug: Hiển thị số cột để kiểm tra
                Console.WriteLine($"Số cột trong DataGridView: {dtDeletedProducts.Columns.Count}");
                foreach (DataGridViewColumn col in dtDeletedProducts.Columns)
                {
                    Console.WriteLine($"Cột: {col.Name}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtDeletedProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu nhấp vào ô hợp lệ (không phải header)
            if (e.RowIndex >= 0)
            {
                // Chọn hàng hiện tại
                dtDeletedProducts.Rows[e.RowIndex].Selected = true;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDeletedProducts();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDeletedProducts();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (dtDeletedProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sản phẩm để khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int successCount = 0;
                string tableName = cbCategory.SelectedItem.ToString() == "Đồ ăn" ? "DoAn" : "DoUong";
                string idColumn = tableName == "DoAn" ? "MaMon" : "MaDoUong";
                foreach (DataGridViewRow row in dtDeletedProducts.SelectedRows)
                {
                    string maSP = row.Cells["MaSP"].Value.ToString();
                    string updateQuery = $"UPDATE {tableName} SET IsDeleted = 0 WHERE {idColumn} = @MaSP";
                    SqlParameter[] parameters = { new SqlParameter("@MaSP", maSP) };
                    if (cls_DatabaseManager.ExecuteNonQuery(updateQuery, parameters) > 0)
                    {
                        successCount++;
                    }
                }
                MessageBox.Show($"Đã khôi phục {successCount} sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDeletedProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khôi phục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeletePermanently_Click(object sender, EventArgs e)
        {
            if (dtDeletedProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sản phẩm để xóa vĩnh viễn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa vĩnh viễn {dtDeletedProducts.SelectedRows.Count} sản phẩm đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int successCount = 0;
                    string tableName = cbCategory.SelectedItem.ToString() == "Đồ ăn" ? "DoAn" : "DoUong";
                    string idColumn = tableName == "DoAn" ? "MaMon" : "MaDoUong";

                    foreach (DataGridViewRow row in dtDeletedProducts.SelectedRows)
                    {
                        string maSP = row.Cells["MaSP"].Value.ToString();

                        // Lấy MaNCC từ bảng DoAn hoặc DoUong
                        string getMaNCCQuery = $"SELECT MaNCC FROM {tableName} WHERE {idColumn} = @MaSP";
                        SqlParameter[] getParams = { new SqlParameter("@MaSP", maSP) };
                        string maNCC = cls_DatabaseManager.ExecuteScalar(getMaNCCQuery, getParams)?.ToString();

                        if (!string.IsNullOrEmpty(maNCC))
                        {
                            // Cập nhật MaNCC = NULL trong DoAn và DoUong
                            string updateDoAnQuery = "UPDATE DoAn SET MaNCC = NULL WHERE MaNCC = @MaNCC";
                            string updateDoUongQuery = "UPDATE DoUong SET MaNCC = NULL WHERE MaNCC = @MaNCC";
                            SqlParameter[] updateParams = { new SqlParameter("@MaNCC", maNCC) };
                            cls_DatabaseManager.ExecuteNonQuery(updateDoAnQuery, updateParams);
                            cls_DatabaseManager.ExecuteNonQuery(updateDoUongQuery, updateParams);
                        }

                        // Xóa vĩnh viễn sản phẩm
                        string deleteQuery = $"DELETE FROM {tableName} WHERE {idColumn} = @MaSP";
                        SqlParameter[] deleteParams = { new SqlParameter("@MaSP", maSP) };
                        if (cls_DatabaseManager.ExecuteNonQuery(deleteQuery, deleteParams) > 0)
                        {
                            successCount++;
                        }
                    }
                    MessageBox.Show($"Đã xóa vĩnh viễn {successCount} sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDeletedProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa vĩnh viễn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}