using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_DeletedInvoices : UserControl
    {
        public UC_DeletedInvoices()
        {
            InitializeComponent();
            dtDeletedProducts.CellClick += new DataGridViewCellEventHandler(dtDeletedProducts_CellClick);
            LoadDeletedInvoices();
            cbCategory.SelectedIndex = 0; // Mặc định chọn "Mã hóa đơn"
            this.Name = "UC_DeletedInvoices"; // Gán tên cho control
        }

        private void LoadDeletedInvoices()
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                string searchColumn = "";
                if (cbCategory.SelectedIndex >= 0)
                {
                    searchColumn = cbCategory.SelectedItem.ToString() == "Mã hóa đơn" ? "MaHD" :
                                  cbCategory.SelectedItem.ToString() == "Tên khách hàng" ? "TenKhachHang" :
                                  "TongTien";
                }
                else
                {
                    searchColumn = "MaHD"; // Giá trị mặc định
                    cbCategory.SelectedIndex = 0;
                }

                string query = $@"
                    SELECT MaHD AS MaSP, MaNV, NgayLapHD, TongTien, TenKhachHang, TienKhachDua, TienTraLai 
                    FROM HoaDon 
                    WHERE IsDeleted = 1 
                    {(string.IsNullOrEmpty(searchText) ? "" : $"AND CAST({searchColumn} AS NVARCHAR(100)) LIKE @SearchText")}";
                SqlParameter[] parameters = string.IsNullOrEmpty(searchText) ? null : new SqlParameter[] { new SqlParameter("@SearchText", $"%{searchText}%") };
                DataTable dt = cls_DatabaseManager.TableRead(query, parameters);

                dtDeletedProducts.DataSource = dt;

                dtDeletedProducts.AutoResizeColumns();
                dtDeletedProducts.AutoResizeRows();

                if (dtDeletedProducts.Columns.Contains("MaSP"))
                    dtDeletedProducts.Columns["MaSP"].HeaderText = "Mã hóa đơn";
                if (dtDeletedProducts.Columns.Contains("MaNV"))
                    dtDeletedProducts.Columns["MaNV"].HeaderText = "Mã nhân viên";
                if (dtDeletedProducts.Columns.Contains("NgayLapHD"))
                    dtDeletedProducts.Columns["NgayLapHD"].HeaderText = "Ngày lập hóa đơn";
                if (dtDeletedProducts.Columns.Contains("TongTien"))
                    dtDeletedProducts.Columns["TongTien"].HeaderText = "Tổng tiền";
                if (dtDeletedProducts.Columns.Contains("TenKhachHang"))
                    dtDeletedProducts.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
                if (dtDeletedProducts.Columns.Contains("TienKhachDua"))
                    dtDeletedProducts.Columns["TienKhachDua"].HeaderText = "Tiền khách đưa";
                if (dtDeletedProducts.Columns.Contains("TienTraLai"))
                    dtDeletedProducts.Columns["TienTraLai"].HeaderText = "Tiền trả lại";

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
            if (e.RowIndex >= 0)
            {
                dtDeletedProducts.Rows[e.RowIndex].Selected = true;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDeletedInvoices();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDeletedInvoices();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (dtDeletedProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một hóa đơn để khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int successCount = 0;
                foreach (DataGridViewRow row in dtDeletedProducts.SelectedRows)
                {
                    string maSP = row.Cells["MaSP"].Value.ToString();
                    string updateQuery = "UPDATE HoaDon SET IsDeleted = 0 WHERE MaHD = @MaSP";
                    SqlParameter[] parameters = { new SqlParameter("@MaSP", maSP) };
                    if (cls_DatabaseManager.ExecuteNonQuery(updateQuery, parameters) > 0)
                    {
                        successCount++;
                    }
                }
                MessageBox.Show($"Đã khôi phục {successCount} hóa đơn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDeletedInvoices();
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
                MessageBox.Show("Vui lòng chọn ít nhất một hóa đơn để xóa vĩnh viễn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa vĩnh viễn " + dtDeletedProducts.SelectedRows.Count + " hóa đơn đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int successCount = 0;
                    foreach (DataGridViewRow row in dtDeletedProducts.SelectedRows)
                    {
                        string maSP = row.Cells["MaSP"].Value.ToString();
                        string deleteQuery = "DELETE FROM HoaDon WHERE MaHD = @MaSP";
                        SqlParameter[] parameters = { new SqlParameter("@MaSP", maSP) };
                        if (cls_DatabaseManager.ExecuteNonQuery(deleteQuery, parameters) > 0)
                        {
                            successCount++;
                        }
                    }
                    MessageBox.Show($"Đã xóa vĩnh viễn {successCount} hóa đơn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDeletedInvoices();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa vĩnh viễn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Visible = false; // Ẩn control nhưng không xóa
        }
    }
}