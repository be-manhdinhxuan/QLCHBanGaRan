using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_DeletedEmployees : UserControl
    {
        public UC_DeletedEmployees()
        {
            InitializeComponent();
            dtDeletedProducts.CellClick += new DataGridViewCellEventHandler(dtDeletedProducts_CellClick);
            LoadDeletedEmployees();
            cbCategory.SelectedIndex = 0; // Mặc định chọn "Tên nhân viên"
            this.Name = "UC_DeletedEmployees"; // Gán tên cho control
        }

        private void LoadDeletedEmployees()
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                string searchColumn = "";
                if (cbCategory.SelectedIndex >= 0)
                {
                    searchColumn = cbCategory.SelectedItem.ToString() == "Tên nhân viên" ? "TenNV" :
                                  cbCategory.SelectedItem.ToString() == "Số điện thoại" ? "SDT" :
                                  "DiaChi";
                }
                else
                {
                    searchColumn = "TenNV"; // Giá trị mặc định
                    cbCategory.SelectedIndex = 0;
                }

                string query = $@"
                    SELECT MaNV AS MaSP, TenNV AS TenSP, SDT, DiaChi 
                    FROM NhanVien 
                    WHERE IsDeleted = 1 
                    {(string.IsNullOrEmpty(searchText) ? "" : $"AND {searchColumn} LIKE @SearchText")}";
                SqlParameter[] parameters = string.IsNullOrEmpty(searchText) ? null : new SqlParameter[] { new SqlParameter("@SearchText", $"%{searchText}%") };
                DataTable dt = cls_DatabaseManager.TableRead(query, parameters);

                dtDeletedProducts.DataSource = dt;

                dtDeletedProducts.AutoResizeColumns();
                dtDeletedProducts.AutoResizeRows();

                if (dtDeletedProducts.Columns.Contains("MaSP"))
                    dtDeletedProducts.Columns["MaSP"].HeaderText = "Mã nhân viên";
                if (dtDeletedProducts.Columns.Contains("TenSP"))
                    dtDeletedProducts.Columns["TenSP"].HeaderText = "Tên nhân viên";
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
            LoadDeletedEmployees();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDeletedEmployees();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (dtDeletedProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một nhân viên để khôi phục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int successCount = 0;
                foreach (DataGridViewRow row in dtDeletedProducts.SelectedRows)
                {
                    string maNV = row.Cells["MaSP"].Value.ToString(); // Sử dụng MaSP vì nó đại diện cho MaNV
                    using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_RestoreEmployee", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MaNV", maNV);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                successCount++;
                            }
                        }
                    }
                }
                MessageBox.Show($"Đã khôi phục {successCount} nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDeletedEmployees(); // Tải lại danh sách để cập nhật giao diện
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
                MessageBox.Show("Vui lòng chọn ít nhất một nhân viên để xóa vĩnh viễn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa vĩnh viễn {dtDeletedProducts.SelectedRows.Count} nhân viên đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                                string maNV = row.Cells["MaSP"].Value.ToString(); // Sử dụng MaSP

                                // Xóa từ ChamCongTheoNgay
                                string deleteChamCongTheoNgay = "DELETE FROM ChamCongTheoNgay WHERE MaNV = @MaNV";
                                using (SqlCommand cmdChamCongTheoNgay = new SqlCommand(deleteChamCongTheoNgay, conn, transaction))
                                {
                                    cmdChamCongTheoNgay.Parameters.AddWithValue("@MaNV", maNV);
                                    int rowsAffected = cmdChamCongTheoNgay.ExecuteNonQuery();
                                    Console.WriteLine($"Deleted {rowsAffected} rows from ChamCongTheoNgay for MaNV: {maNV}");
                                }

                                // Xóa từ ThongKeChamCong
                                string deleteThongKeChamCong = "DELETE FROM ThongKeChamCong WHERE MaNV = @MaNV";
                                using (SqlCommand cmdThongKeChamCong = new SqlCommand(deleteThongKeChamCong, conn, transaction))
                                {
                                    cmdThongKeChamCong.Parameters.AddWithValue("@MaNV", maNV);
                                    int rowsAffected = cmdThongKeChamCong.ExecuteNonQuery();
                                    Console.WriteLine($"Deleted {rowsAffected} rows from ThongKeChamCong for MaNV: {maNV}");
                                }

                                // Xóa từ ChiTietHoaDon (thêm mới)
                                string deleteChiTietHoaDon = "DELETE FROM ChiTietHoaDon WHERE MaHD IN (SELECT MaHD FROM HoaDon WHERE MaNV = @MaNV)";
                                using (SqlCommand cmdChiTietHoaDon = new SqlCommand(deleteChiTietHoaDon, conn, transaction))
                                {
                                    cmdChiTietHoaDon.Parameters.AddWithValue("@MaNV", maNV);
                                    int rowsAffected = cmdChiTietHoaDon.ExecuteNonQuery();
                                    Console.WriteLine($"Deleted {rowsAffected} rows from ChiTietHoaDon for MaNV: {maNV}");
                                }

                                // Xóa từ HoaDon
                                string deleteHoaDon = "DELETE FROM HoaDon WHERE MaNV = @MaNV";
                                using (SqlCommand cmdHoaDon = new SqlCommand(deleteHoaDon, conn, transaction))
                                {
                                    cmdHoaDon.Parameters.AddWithValue("@MaNV", maNV);
                                    int rowsAffected = cmdHoaDon.ExecuteNonQuery();
                                    Console.WriteLine($"Deleted {rowsAffected} rows from HoaDon for MaNV: {maNV}");
                                }

                                // Xóa từ NguoiDung
                                string deleteNguoiDung = "DELETE FROM NguoiDung WHERE MaNV = @MaNV";
                                using (SqlCommand cmdNguoiDung = new SqlCommand(deleteNguoiDung, conn, transaction))
                                {
                                    cmdNguoiDung.Parameters.AddWithValue("@MaNV", maNV);
                                    int rowsAffected = cmdNguoiDung.ExecuteNonQuery();
                                    Console.WriteLine($"Deleted {rowsAffected} rows from NguoiDung for MaNV: {maNV}");
                                }

                                // Xóa từ NhanVien
                                string deleteNhanVien = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                                using (SqlCommand cmdNhanVien = new SqlCommand(deleteNhanVien, conn, transaction))
                                {
                                    cmdNhanVien.Parameters.AddWithValue("@MaNV", maNV);
                                    if (cmdNhanVien.ExecuteNonQuery() > 0)
                                    {
                                        successCount++;
                                        Console.WriteLine($"Successfully deleted MaNV: {maNV} from NhanVien");
                                    }
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show($"Đã xóa vĩnh viễn {successCount} nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDeletedEmployees();
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Visible = false; // Ẩn control nhưng không xóa
        }
    }
}