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
    public partial class frm_DeletedPositions : Form
    {
        public frm_DeletedPositions()
        {
            InitializeComponent();
            dtDeletedProducts.CellClick += new DataGridViewCellEventHandler(dtDeletedProducts_CellClick);
            LoadDeletedPositions();
            cbCategory.SelectedIndex = 0; // Mặc định chọn "Tên chức danh"
            this.Name = "frm_DeletedPositions"; // Gán tên cho control
        }

        private void LoadDeletedPositions()
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                string searchColumn = "";
                if (cbCategory.SelectedIndex >= 0)
                {
                    searchColumn = cbCategory.SelectedItem.ToString() == "Tên chức danh" ? "TenChucDanh" :
                                  cbCategory.SelectedItem.ToString() == "Lương theo giờ" ? "LuongTheoGio" :
                                  "ThuongChucDanh";
                }
                else
                {
                    searchColumn = "TenChucDanh"; // Giá trị mặc định
                    cbCategory.SelectedIndex = 0;
                }

                string query = $@"
                    SELECT MaChucDanh AS MaSP, TenChucDanh AS TenSP, LuongTheoGio, ThuongChucDanh 
                    FROM ChucDanh 
                    WHERE IsDeleted = 1 
                    {(string.IsNullOrEmpty(searchText) ? "" : $"AND {searchColumn} LIKE @SearchText")}";
                SqlParameter[] parameters = string.IsNullOrEmpty(searchText) ? null : new SqlParameter[] { new SqlParameter("@SearchText", $"%{searchText}%") };
                DataTable dt = cls_DatabaseManager.TableRead(query, parameters);

                dtDeletedProducts.DataSource = dt;

                dtDeletedProducts.AutoResizeColumns();
                dtDeletedProducts.AutoResizeRows();

                if (dtDeletedProducts.Columns.Contains("MaSP"))
                    dtDeletedProducts.Columns["MaSP"].HeaderText = "Mã chức danh";
                if (dtDeletedProducts.Columns.Contains("TenSP"))
                    dtDeletedProducts.Columns["TenSP"].HeaderText = "Tên chức danh";
                if (dtDeletedProducts.Columns.Contains("LuongTheoGio"))
                    dtDeletedProducts.Columns["LuongTheoGio"].HeaderText = "Lương theo giờ";
                if (dtDeletedProducts.Columns.Contains("ThuongChucDanh"))
                    dtDeletedProducts.Columns["ThuongChucDanh"].HeaderText = "Thưởng chức danh";

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
            LoadDeletedPositions();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDeletedPositions();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (dtDeletedProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một chức danh để khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int successCount = 0;
                foreach (DataGridViewRow row in dtDeletedProducts.SelectedRows)
                {
                    string maSP = row.Cells["MaSP"].Value.ToString();
                    string query = "sp_RestoreChucDanh"; // Sử dụng stored procedure
                    SqlParameter[] parameters = { new SqlParameter("@MaChucDanh", maSP) };
                    if (cls_DatabaseManager.ExecuteNonQuery(query, parameters) > 0)
                    {
                        successCount++;
                    }
                }
                MessageBox.Show($"Đã khôi phục {successCount} chức danh thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDeletedPositions(); // Làm mới danh sách chức danh đã xóa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khôi phục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("btnRestore_Click Error: " + ex.Message); // Ghi log lỗi
            }
        }

        private void btnDeletePermanently_Click(object sender, EventArgs e)
        {
            if (dtDeletedProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một chức danh để xóa vĩnh viễn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa vĩnh viễn {dtDeletedProducts.SelectedRows.Count} chức danh đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int successCount = 0;
                    using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.ConnectionString))
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            foreach (DataGridViewRow row in dtDeletedProducts.SelectedRows)
                            {
                                string maChucDanh = row.Cells["MaSP"].Value.ToString();

                                // Cập nhật MaChucDanh trong NhanVien thành NULL
                                string updateNhanVienQuery = "UPDATE NhanVien SET MaChucDanh = NULL WHERE MaChucDanh = @MaChucDanh";
                                using (SqlCommand cmdUpdateNhanVien = new SqlCommand(updateNhanVienQuery, conn, transaction))
                                {
                                    cmdUpdateNhanVien.Parameters.AddWithValue("@MaChucDanh", maChucDanh);
                                    int rowsAffected = cmdUpdateNhanVien.ExecuteNonQuery();
                                    Console.WriteLine($"Updated {rowsAffected} rows in NhanVien for MaChucDanh: {maChucDanh}");
                                }

                                // Xóa từ ChucDanh
                                string deleteChucDanhQuery = "DELETE FROM ChucDanh WHERE MaChucDanh = @MaChucDanh";
                                using (SqlCommand cmdDeleteChucDanh = new SqlCommand(deleteChucDanhQuery, conn, transaction))
                                {
                                    cmdDeleteChucDanh.Parameters.AddWithValue("@MaChucDanh", maChucDanh);
                                    if (cmdDeleteChucDanh.ExecuteNonQuery() > 0)
                                    {
                                        successCount++;
                                        Console.WriteLine($"Successfully deleted MaChucDanh: {maChucDanh} from ChucDanh");
                                    }
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show($"Đã xóa vĩnh viễn {successCount} chức danh thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDeletedPositions();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw; // Ném lỗi để xử lý bên ngoài
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa vĩnh viễn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}