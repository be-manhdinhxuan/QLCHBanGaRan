using System;
using System.Data;
using System.Windows.Forms;
using QLCHBanGaRan.lib;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_TimeSheetManager : UserControl
    {
        public UC_TimeSheetManager()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Thay thế bằng logic điều hướng của dự án bạn
            // Ví dụ: Forms.frm_Main.Instance.pnlContainer.Controls["UC_Salary"].BringToFront();
        }

        private void _format()
        {
            dtList.Columns[0].Width = 60;
            dtList.Columns[1].Width = 150;
            dtList.Columns[2].Width = 150;
        }

        private void UC_TimeSheetManager_Load(object sender, EventArgs e)
        {
            _format();
            dtList.AutoGenerateColumns = false;
            dtpThang.Value = DateTime.Now; // Đặt giá trị mặc định là tháng hiện tại
            LoadChucDanh();
        }

        private void LoadChucDanh()
        {
            string query = "SELECT DISTINCT ChucDanh FROM NhanVien WHERE ChucDanh IS NOT NULL";
            DataTable dt = cls_DatabaseManager.TableRead(query);
            cmbChucDanh.DataSource = dt;
            cmbChucDanh.DisplayMember = "ChucDanh";
            cmbChucDanh.SelectedIndex = -1; // Không chọn mặc định
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string thang = dtpThang.Value.ToString("yyyyMM");
            string chucDanh = cmbChucDanh.SelectedItem?.ToString() ?? "";
            string query = @"
                SELECT cc.MaChamCong, cc.MaNV, nv.TenNV, nv.ChucDanh, cc.NgayChamCong, cc.TrangThai, cc.LyDoNghi
                FROM ChamCongTheoNgay cc
                LEFT JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                WHERE CONVERT(VARCHAR(6), cc.NgayChamCong, 112) = @Thang";
            if (!string.IsNullOrEmpty(chucDanh))
            {
                query += " AND nv.ChucDanh = @ChucDanh";
            }
            var parameters = new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@Thang", thang)
            };
            if (!string.IsNullOrEmpty(chucDanh))
            {
                Array.Resize(ref parameters, parameters.Length + 1);
                parameters[parameters.Length - 1] = new System.Data.SqlClient.SqlParameter("@ChucDanh", chucDanh);
            }
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            if (dt.Rows.Count > 0)
            {
                dtList.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Không có dữ liệu chấm công cho tháng và chức danh này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtList.DataSource = null;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int flag = 0;

            foreach (DataGridViewRow row in dtList.Rows)
            {
                if (row != null && !row.IsNewRow)
                {
                    string maChamCong = row.Cells["MaChamCong"].Value?.ToString() ?? "";
                    string maNV = row.Cells["MaNV"].Value?.ToString() ?? "";
                    DateTime ngayChamCong = row.Cells["NgayChamCong"].Value == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row.Cells["NgayChamCong"].Value);
                    int trangThai = row.Cells["TrangThai"].Value == DBNull.Value ? 1 : Convert.ToInt32(row.Cells["TrangThai"].Value);
                    string lyDoNghi = row.Cells["LyDoNghi"].Value?.ToString() ?? "";

                    if (string.IsNullOrEmpty(maChamCong))
                    {
                        string query = "INSERT INTO ChamCongTheoNgay (MaNV, NgayChamCong, TrangThai, LyDoNghi) " +
                                       "VALUES (@MaNV, @NgayChamCong, @TrangThai, @LyDoNghi)";
                        bool insertSuccess = cls_DatabaseManager.ExecuteNonQuery(query, new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@MaNV", maNV),
                            new System.Data.SqlClient.SqlParameter("@NgayChamCong", ngayChamCong),
                            new System.Data.SqlClient.SqlParameter("@TrangThai", trangThai),
                            new System.Data.SqlClient.SqlParameter("@LyDoNghi", lyDoNghi)
                        }) > 0;
                        if (insertSuccess) flag++;
                    }
                    else
                    {
                        if (trangThai == 1) // Chưa nộp
                        {
                            string query = "UPDATE ChamCongTheoNgay SET TrangThai = @TrangThai, LyDoNghi = @LyDoNghi " +
                                           "WHERE MaChamCong = @MaChamCong";
                            bool updateSuccess = cls_DatabaseManager.ExecuteNonQuery(query, new System.Data.SqlClient.SqlParameter[]
                            {
                                new System.Data.SqlClient.SqlParameter("@TrangThai", trangThai),
                                new System.Data.SqlClient.SqlParameter("@LyDoNghi", lyDoNghi),
                                new System.Data.SqlClient.SqlParameter("@MaChamCong", maChamCong)
                            }) > 0;
                            if (updateSuccess) flag++;
                        }
                    }
                }
            }

            btnHienThi_Click(sender, e);
            if (flag == 0)
            {
                MessageBox.Show("Không có bản ghi nào thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lưu " + flag + " bản ghi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNop_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn nộp bản chấm công này không? Nếu nộp, bạn sẽ không thể thay đổi dữ liệu!",
                "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int flag = 0;
                foreach (DataGridViewRow row in dtList.Rows)
                {
                    if (row != null && !row.IsNewRow)
                    {
                        if (row.Cells["TrangThai"].Value?.ToString() == "1") // Chưa nộp
                        {
                            string maChamCong = row.Cells["MaChamCong"].Value?.ToString() ?? "";
                            DateTime ngayChamCong = Convert.ToDateTime(row.Cells["NgayChamCong"].Value);
                            string maNV = row.Cells["MaNV"].Value?.ToString() ?? "";

                            // Cập nhật trạng thái thành 2 (Đã nộp)
                            string updateQuery = "UPDATE ChamCongTheoNgay SET TrangThai = 2 WHERE MaChamCong = @MaChamCong";
                            if (cls_DatabaseManager.ExecuteNonQuery(updateQuery, new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@MaChamCong", maChamCong)
                            }) > 0)
                            {
                                // Thêm vào bảng ThongKeChamCong
                                string thang = ngayChamCong.ToString("yyyyMM");
                                string tkQuery = @"
                                    INSERT INTO ThongKeChamCong (MaNV, Thang, NgayCongChuan, NgayDiLam, NgayNghi, TongLuong, ThucLinh, TrangThai)
                                    SELECT MaNV, @Thang, 26, COUNT(*) AS NgayDiLam, 
                                           (SELECT COUNT(*) FROM ChamCongTheoNgay cc2 WHERE cc2.MaNV = cc1.MaNV AND cc2.TrangThai = 2 AND CONVERT(VARCHAR(6), cc2.NgayChamCong, 112) = @Thang) AS NgayNghi,
                                           0, 0, 1
                                    FROM ChamCongTheoNgay cc1
                                    WHERE cc1.MaChamCong = @MaChamCong
                                    GROUP BY MaNV";
                                if (cls_DatabaseManager.ExecuteNonQuery(tkQuery, new System.Data.SqlClient.SqlParameter[] {
                                    new System.Data.SqlClient.SqlParameter("@Thang", thang),
                                    new System.Data.SqlClient.SqlParameter("@MaChamCong", maChamCong)
                                }) > 0)
                                {
                                    flag++;
                                }
                            }
                        }
                    }
                }
                btnHienThi_Click(sender, e);
                if (flag == 0)
                {
                    MessageBox.Show("Không có bản ghi nào được nộp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nộp " + flag + " bản ghi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dtList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtList.Columns[e.ColumnIndex].Name.Equals("TrangThai"))
            {
                int? stt = e.Value as int?;

                if (stt == 1)
                {
                    e.Value = "Chưa nộp";
                }
                else if (stt == 2)
                {
                    e.Value = "Đã nộp";
                }
                else
                {
                    e.Value = "Unknown";
                }
            }
        }
    }
}