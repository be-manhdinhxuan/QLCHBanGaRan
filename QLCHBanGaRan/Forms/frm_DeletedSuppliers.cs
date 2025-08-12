using QLCHBanGaRan.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_DeletedSuppliers : Form
    {
        public frm_DeletedSuppliers()
        {
            InitializeComponent();
            dtDeletedProducts.CellClick += new DataGridViewCellEventHandler(dtDeletedProducts_CellClick);
            LoadDeletedProducts();
            cbCategory.SelectedIndex = 0; // Mặc định chọn "Tên nhà cung cấp"
            this.Name = "frm_DeletedSuppliers"; // Gán tên cho control
        }

        private void LoadDeletedProducts()
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                string searchColumn = "";
                if (cbCategory.SelectedIndex >= 0)
                {
                    searchColumn = cbCategory.SelectedItem.ToString() == "Tên nhà cung cấp" ? "TenNCC" :
                                  cbCategory.SelectedItem.ToString() == "Số điện thoại" ? "SDT" :
                                  "DiaChi";
                }
                else
                {
                    searchColumn = "TenNCC"; // Giá trị mặc định
                    cbCategory.SelectedIndex = 0;
                }

                string query = $@"
                    SELECT MaNCC AS MaSP, TenNCC AS TenSP, SDT, DiaChi 
                    FROM NhaCungCap 
                    WHERE IsDeleted = 1 
                    {(string.IsNullOrEmpty(searchText) ? "" : $"AND {searchColumn} LIKE @SearchText")}";
                SqlParameter[] parameters = string.IsNullOrEmpty(searchText) ? null : new SqlParameter[] { new SqlParameter("@SearchText", $"%{searchText}%") };
                DataTable dt = cls_DatabaseManager.TableRead(query, parameters);

                dtDeletedProducts.DataSource = dt;

                dtDeletedProducts.AutoResizeColumns();
                dtDeletedProducts.AutoResizeRows();

                if (dtDeletedProducts.Columns.Contains("MaSP"))
                    dtDeletedProducts.Columns["MaSP"].HeaderText = "Mã nhà cung cấp";
                if (dtDeletedProducts.Columns.Contains("TenSP"))
                    dtDeletedProducts.Columns["TenSP"].HeaderText = "Tên nhà cung cấp";
                if (dtDeletedProducts.Columns.Contains("SDT"))
                    dtDeletedProducts.Columns["SDT"].HeaderText = "Số điện thoại";
                if (dtDeletedProducts.Columns.Contains("DiaChi"))
                    dtDeletedProducts.Columns["DiaChi"].HeaderText = "Địa chỉ";

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
                MessageBox.Show("Vui lòng chọn ít nhất một nhà cung cấp để khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int successCount = 0;
                foreach (DataGridViewRow row in dtDeletedProducts.SelectedRows)
                {
                    string maNCC = row.Cells["MaSP"].Value.ToString();
                    using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("sp_RestoreNCC", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                        SqlParameter returnParam = new SqlParameter("@ReturnValue", SqlDbType.Int);
                        returnParam.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(returnParam);
                        cmd.ExecuteNonQuery();
                        if ((int)returnParam.Value == 1) // Kiểm tra giá trị trả về từ stored procedure
                        {
                            successCount++;
                        }
                    }
                }
                MessageBox.Show($"Đã khôi phục {successCount} nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Vui lòng chọn ít nhất một nhà cung cấp để xóa vĩnh viễn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa vĩnh viễn {dtDeletedProducts.SelectedRows.Count} nhà cung cấp đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int successCount = 0;
                    foreach (DataGridViewRow row in dtDeletedProducts.SelectedRows)
                    {
                        string maSP = row.Cells["MaSP"].Value.ToString();
                        string deleteQuery = "DELETE FROM NhaCungCap WHERE MaNCC = @MaSP";
                        SqlParameter[] parameters = { new SqlParameter("@MaSP", maSP) };
                        if (cls_DatabaseManager.ExecuteNonQuery(deleteQuery, parameters) > 0)
                        {
                            successCount++;
                        }
                    }
                    MessageBox.Show($"Đã xóa vĩnh viễn {successCount} nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
