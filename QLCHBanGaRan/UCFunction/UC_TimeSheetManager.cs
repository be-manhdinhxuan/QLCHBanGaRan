using System;
using System.Data;
using System.Windows.Forms;
using QLCHBanGaRan.lib;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_TimeSheetManager : UserControl
    {
        private string currentMaND; // Lưu mã người dùng đang đăng nhập
        private bool isAdmin; // Biến xác định quyền quản trị

        public UC_TimeSheetManager()
        {
            InitializeComponent();
            // Constructor mặc định, cần kiểm tra đăng nhập sau khi tạo
            currentMaND = null; // Giá trị mặc định
            CheckAdminRole();
        }

        public UC_TimeSheetManager(string maND)
        {
            InitializeComponent();
            currentMaND = maND; // Nhận mã người dùng từ form đăng nhập hoặc hệ thống
            CheckAdminRole();
        }

        private void CheckAdminRole()
        {
            if (string.IsNullOrEmpty(currentMaND)) return;
            string query = "SELECT LaQuanTri FROM NguoiDung WHERE MaND = @MaND";
            var parameters = new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@MaND", currentMaND)
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            if (dt.Rows.Count > 0)
            {
                int laQuanTri = Convert.ToInt32(dt.Rows[0]["LaQuanTri"]);
                isAdmin = (laQuanTri == 1); // LaQuanTri = 1 là quản trị viên
            }
            else
            {
                isAdmin = false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Thay thế bằng logic điều hướng của dự án bạn
            // Ví dụ: Forms.frm_Main.Instance.pnlContainer.Controls["UC_Salary"].BringToFront();
        }

        private void UC_TimeSheetManager_Load(object sender, EventArgs e)
        {
            dtpThang.Value = DateTime.Now; // Đặt giá trị mặc định là tháng hiện tại
            if (string.IsNullOrEmpty(currentMaND))
            {
                lblStatus.Text = "Vui lòng đăng nhập để sử dụng chức năng.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }
            LoadNhanVienInfo();
        }

        private void LoadNhanVienInfo()
        {
            if (string.IsNullOrEmpty(currentMaND)) return;
            string query = @"
                SELECT nv.TenNV, nv.MaChucDanh 
                FROM NguoiDung nd
                LEFT JOIN NhanVien nv ON nd.MaNV = nv.MaNV
                WHERE nd.MaND = @MaND";
            var parameters = new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@MaND", currentMaND)
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            if (dt.Rows.Count > 0)
            {
                lblTitle.Text = isAdmin ? "QUẢN LÝ CHẤM CÔNG (QUẢN TRỊ VIÊN)" :
                    $"QUẢN LÝ CHẤM CÔNG - {dt.Rows[0]["TenNV"]} ({dt.Rows[0]["MaChucDanh"]})";
            }
            else
            {
                lblStatus.Text = "Không tìm thấy thông tin nhân viên.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaND))
            {
                lblStatus.Text = "Vui lòng đăng nhập để chấm công.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            DataTable dtChamCong;
            string maNV = GetMaNVFromMaND(currentMaND);
            if (isAdmin)
            {
                // Hiển thị cho tất cả nhân viên
                dtChamCong = new DataTable();
                dtChamCong.Columns.Add("MaNV", typeof(string));
                dtChamCong.Columns.Add("TenNV", typeof(string));
                dtChamCong.Columns.Add("ChucDanh", typeof(string));

                string queryNhanVien = @"
                    SELECT nv.MaNV, nv.TenNV, nv.MaChucDanh 
                    FROM NguoiDung nd
                    LEFT JOIN NhanVien nv ON nd.MaNV = nv.MaNV
                    WHERE nd.LaQuanTri = 0";
                DataTable dtNhanVien = cls_DatabaseManager.TableRead(queryNhanVien);
                if (dtNhanVien == null || dtNhanVien.Rows.Count == 0)
                {
                    lblStatus.Text = "Không có nhân viên nào để hiển thị.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    dtList.DataSource = null;
                    dtList.Columns.Clear();
                    return;
                }

                int daysInMonth = DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month);
                for (int day = 1; day <= daysInMonth; day++)
                {
                    dtChamCong.Columns.Add($"Day{day}", typeof(string));
                    dtChamCong.Columns.Add($"LyDoNghi{day}", typeof(string));
                }

                foreach (DataRow nvRow in dtNhanVien.Rows)
                {
                    DataRow newRow = dtChamCong.NewRow();
                    newRow["MaNV"] = nvRow["MaNV"].ToString();
                    newRow["TenNV"] = nvRow["TenNV"].ToString();
                    newRow["ChucDanh"] = nvRow["MaChucDanh"].ToString();
                    for (int day = 1; day <= daysInMonth; day++)
                    {
                        newRow[$"Day{day}"] = "Chưa nộp";
                        newRow[$"LyDoNghi{day}"] = "";
                    }
                    dtChamCong.Rows.Add(newRow);
                }

                // Cập nhật từ dữ liệu hiện có
                string queryExisting = @"
                    SELECT cc.MaNV, nv.TenNV, nv.MaChucDanh, CONVERT(VARCHAR(2), DAY(cc.NgayChamCong)) AS Ngay, 
                           cc.TrangThai, cc.LyDoNghi
                    FROM ChamCongTheoNgay cc
                    LEFT JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                    LEFT JOIN NguoiDung nd ON nv.MaNV = nd.MaNV
                    WHERE CONVERT(VARCHAR(6), cc.NgayChamCong, 112) = @Thang AND nd.LaQuanTri = 0";
                var parameters = new System.Data.SqlClient.SqlParameter[] {
                    new System.Data.SqlClient.SqlParameter("@Thang", dtpThang.Value.ToString("yyyyMM"))
                };
                DataTable dtExisting = cls_DatabaseManager.TableRead(queryExisting, parameters);

                if (dtExisting != null && dtExisting.Rows.Count > 0)
                {
                    foreach (DataRow existingRow in dtExisting.Rows)
                    {
                        string maNVExisting = existingRow["MaNV"].ToString();
                        string ngay = existingRow["Ngay"].ToString();
                        string trangThai = ConvertToStatus(Convert.ToInt32(existingRow["TrangThai"]));
                        string lyDoNghi = existingRow["LyDoNghi"].ToString() ?? "";
                        DataRow[] rowsToUpdate = dtChamCong.Select($"MaNV = '{maNVExisting}'");
                        if (rowsToUpdate.Length > 0)
                        {
                            rowsToUpdate[0][$"Day{ngay}"] = trangThai;
                            rowsToUpdate[0][$"LyDoNghi{ngay}"] = lyDoNghi;
                        }
                    }
                }
            }
            else
            {
                // Hiển thị cho nhân viên cá nhân
                dtChamCong = new DataTable();
                dtChamCong.Columns.Add("Ngay", typeof(string));
                dtChamCong.Columns.Add("TrangThai", typeof(string));
                dtChamCong.Columns.Add("LyDoNghi", typeof(string));

                int daysInMonth = DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month);
                for (int day = 1; day <= daysInMonth; day++)
                {
                    DataRow newRow = dtChamCong.NewRow();
                    newRow["Ngay"] = day.ToString();
                    newRow["TrangThai"] = "Chưa nộp";
                    newRow["LyDoNghi"] = "";
                    dtChamCong.Rows.Add(newRow);
                }

                // Cập nhật từ dữ liệu hiện có
                string query = @"
                    SELECT CONVERT(VARCHAR(2), DAY(NgayChamCong)) AS Ngay, TrangThai, LyDoNghi
                    FROM ChamCongTheoNgay
                    WHERE MaNV = @MaNV AND CONVERT(VARCHAR(6), NgayChamCong, 112) = @Thang";
                var parameters = new System.Data.SqlClient.SqlParameter[] {
                    new System.Data.SqlClient.SqlParameter("@MaNV", maNV),
                    new System.Data.SqlClient.SqlParameter("@Thang", dtpThang.Value.ToString("yyyyMM"))
                };
                DataTable dtExisting = cls_DatabaseManager.TableRead(query, parameters);

                if (dtExisting != null && dtExisting.Rows.Count > 0)
                {
                    foreach (DataRow existingRow in dtExisting.Rows)
                    {
                        string ngay = existingRow["Ngay"].ToString();
                        string trangThai = ConvertToStatus(Convert.ToInt32(existingRow["TrangThai"]));
                        string lyDoNghi = existingRow["LyDoNghi"].ToString() ?? "";
                        DataRow[] rowsToUpdate = dtChamCong.Select($"Ngay = '{ngay}'");
                        if (rowsToUpdate.Length > 0)
                        {
                            rowsToUpdate[0]["TrangThai"] = trangThai;
                            rowsToUpdate[0]["LyDoNghi"] = lyDoNghi;
                        }
                    }
                }
            }

            // Cấu hình cột trong dtList
            dtList.Columns.Clear();
            if (isAdmin)
            {
                dtList.Columns.Add("MaNV", "Mã NV");
                dtList.Columns["MaNV"].ReadOnly = true;
                dtList.Columns.Add("TenNV", "Tên NV");
                dtList.Columns["TenNV"].ReadOnly = true;
                dtList.Columns.Add("ChucDanh", "Chức danh");
                dtList.Columns["ChucDanh"].ReadOnly = true;
                int daysInMonth = DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month);
                for (int day = 1; day <= daysInMonth; day++)
                {
                    dtList.Columns.Add($"Day{day}", day.ToString());
                    DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn
                    {
                        Name = $"Day{day}",
                        Items = { "Chưa nộp", "Đã nộp", "Nghỉ" },
                        FlatStyle = FlatStyle.Flat,
                        Width = 80
                    };
                    dtList.Columns.Add(comboColumn);

                    DataGridViewTextBoxColumn lyDoColumn = new DataGridViewTextBoxColumn
                    {
                        Name = $"LyDoNghi{day}",
                        HeaderText = $"Lý do {day}",
                        Width = 120
                    };
                    dtList.Columns.Add(lyDoColumn);
                }
            }
            else
            {
                dtList.Columns.Add("Ngay", "Ngày");
                dtList.Columns["Ngay"].ReadOnly = true;
                DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn
                {
                    Name = "TrangThai",
                    HeaderText = "Trạng thái",
                    Items = { "Chưa nộp", "Đã nộp", "Nghỉ" },
                    FlatStyle = FlatStyle.Flat,
                    Width = 100
                };
                dtList.Columns.Add(comboColumn);
                dtList.Columns.Add("LyDoNghi", "Lý do nghỉ");
                dtList.Columns["LyDoNghi"].Width = 200;
            }

            dtList.DataSource = dtChamCong;
            lblStatus.Text = isAdmin ? $"Đã tải lịch chấm công của tất cả nhân viên cho tháng {dtpThang.Value:MM/yyyy}." :
                $"Đã tải lịch chấm công cho tháng {dtpThang.Value:MM/yyyy}.";
            lblStatus.ForeColor = System.Drawing.Color.Green;
        }

        private string GetMaNVFromMaND(string maND)
        {
            string query = "SELECT MaNV FROM NguoiDung WHERE MaND = @MaND";
            var parameters = new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@MaND", maND)
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            return dt.Rows.Count > 0 ? dt.Rows[0]["MaNV"].ToString() : "";
        }

        private string ConvertToStatus(int trangThai)
        {
            switch (trangThai)
            {
                case 1: return "Chưa nộp";
                case 2: return "Đã nộp";
                default: return "Nghỉ";
            }
        }

        private int ConvertToStatusCode(string trangThai)
        {
            switch (trangThai)
            {
                case "Chưa nộp": return 1;
                case "Đã nộp": return 2;
                case "Nghỉ": return 2;
                default: return 1;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaND))
            {
                lblStatus.Text = "Vui lòng đăng nhập để lưu.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int flag = 0;
            DataTable dtChamCong = dtList.DataSource as DataTable;
            if (dtChamCong == null)
            {
                lblStatus.Text = "Không có dữ liệu để lưu.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            foreach (DataRow row in dtChamCong.Rows)
            {
                string maNV = isAdmin ? row["MaNV"].ToString() : GetMaNVFromMaND(currentMaND);
                int daysInMonth = DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month);
                for (int day = 1; day <= daysInMonth; day++)
                {
                    string trangThai = isAdmin ? row[$"Day{day}"].ToString() : row["TrangThai"].ToString();
                    string lyDoNghi = isAdmin ? row[$"LyDoNghi{day}"].ToString() : row["LyDoNghi"].ToString();
                    DateTime ngayChamCong = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, day);

                    // Kiểm tra dữ liệu hiện có
                    string checkQuery = "SELECT MaChamCong FROM ChamCongTheoNgay WHERE MaNV = @MaNV AND NgayChamCong = @NgayChamCong";
                    var checkParams = new System.Data.SqlClient.SqlParameter[] {
                        new System.Data.SqlClient.SqlParameter("@MaNV", maNV),
                        new System.Data.SqlClient.SqlParameter("@NgayChamCong", ngayChamCong)
                    };
                    DataTable dtCheck = cls_DatabaseManager.TableRead(checkQuery, checkParams);
                    if (dtCheck.Rows.Count > 0)
                    {
                        string maChamCong = dtCheck.Rows[0]["MaChamCong"].ToString();
                        string updateQuery = "UPDATE ChamCongTheoNgay SET TrangThai = @TrangThai, LyDoNghi = @LyDoNghi WHERE MaChamCong = @MaChamCong";
                        bool updateSuccess = cls_DatabaseManager.ExecuteNonQuery(updateQuery, new System.Data.SqlClient.SqlParameter[] {
                            new System.Data.SqlClient.SqlParameter("@TrangThai", ConvertToStatusCode(trangThai)),
                            new System.Data.SqlClient.SqlParameter("@LyDoNghi", lyDoNghi),
                            new System.Data.SqlClient.SqlParameter("@MaChamCong", maChamCong)
                        }) > 0;
                        if (updateSuccess) flag++;
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO ChamCongTheoNgay (MaNV, NgayChamCong, TrangThai, LyDoNghi) VALUES (@MaNV, @NgayChamCong, @TrangThai, @LyDoNghi)";
                        bool insertSuccess = cls_DatabaseManager.ExecuteNonQuery(insertQuery, new System.Data.SqlClient.SqlParameter[] {
                            new System.Data.SqlClient.SqlParameter("@MaNV", maNV),
                            new System.Data.SqlClient.SqlParameter("@NgayChamCong", ngayChamCong),
                            new System.Data.SqlClient.SqlParameter("@TrangThai", ConvertToStatusCode(trangThai)),
                            new System.Data.SqlClient.SqlParameter("@LyDoNghi", lyDoNghi)
                        }) > 0;
                        if (insertSuccess) flag++;
                    }
                }
            }

            if (flag > 0)
            {
                lblStatus.Text = isAdmin ? $"Đã lưu {flag} bản ghi cho tất cả nhân viên." : $"Đã lưu {flag} bản ghi thành công.";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                btnHienThi_Click(sender, e);
            }
            else
            {
                lblStatus.Text = "Không có bản ghi nào thay đổi.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnNop_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaND))
            {
                lblStatus.Text = "Vui lòng đăng nhập để nộp.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có muốn nộp bản chấm công này không? Nếu nộp, bạn sẽ không thể thay đổi dữ liệu!",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int flag = 0;
                DataTable dtChamCong = dtList.DataSource as DataTable;
                if (dtChamCong == null)
                {
                    lblStatus.Text = "Không có dữ liệu để nộp.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                string thang = dtpThang.Value.ToString("yyyyMM");
                foreach (DataRow row in dtChamCong.Rows)
                {
                    string maNV = isAdmin ? row["MaNV"].ToString() : GetMaNVFromMaND(currentMaND);
                    int daysInMonth = DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month);
                    for (int day = 1; day <= daysInMonth; day++)
                    {
                        string trangThai = isAdmin ? row[$"Day{day}"].ToString() : row["TrangThai"].ToString();
                        if (trangThai == "Chưa nộp")
                        {
                            DateTime ngayChamCong = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, day);
                            string checkQuery = "SELECT MaChamCong FROM ChamCongTheoNgay WHERE MaNV = @MaNV AND NgayChamCong = @NgayChamCong";
                            var checkParams = new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@MaNV", maNV),
                                new System.Data.SqlClient.SqlParameter("@NgayChamCong", ngayChamCong)
                            };
                            DataTable dtCheck = cls_DatabaseManager.TableRead(checkQuery, checkParams);
                            if (dtCheck.Rows.Count > 0)
                            {
                                string maChamCong = dtCheck.Rows[0]["MaChamCong"].ToString();
                                string updateQuery = "UPDATE ChamCongTheoNgay SET TrangThai = 2 WHERE MaChamCong = @MaChamCong";
                                if (cls_DatabaseManager.ExecuteNonQuery(updateQuery, new System.Data.SqlClient.SqlParameter[] {
                                    new System.Data.SqlClient.SqlParameter("@MaChamCong", maChamCong)
                                }) > 0)
                                {
                                    string tkQuery = @"
                                        INSERT INTO ThongKeChamCong (MaNV, Thang, NgayCongChuan, NgayDiLam, NgayNghi, TongLuong, ThucLinh, TrangThai)
                                        VALUES (@MaNV, @Thang, 26, 0, 0, 0, 0, 1)";
                                    if (cls_DatabaseManager.ExecuteNonQuery(tkQuery, new System.Data.SqlClient.SqlParameter[] {
                                        new System.Data.SqlClient.SqlParameter("@MaNV", maNV),
                                        new System.Data.SqlClient.SqlParameter("@Thang", thang)
                                    }) > 0)
                                    {
                                        flag++;
                                    }
                                }
                            }
                        }
                    }
                }

                if (flag > 0)
                {
                    lblStatus.Text = isAdmin ? $"Đã nộp {flag} bản ghi cho tất cả nhân viên." : $"Đã nộp {flag} bản ghi thành công.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    btnHienThi_Click(sender, e);
                }
                else
                {
                    lblStatus.Text = "Không có bản ghi nào được nộp.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void dtList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (isAdmin && e.ColumnIndex > 2 && dtList.Columns[e.ColumnIndex].Name.StartsWith("Day"))
            {
                string status = e.Value?.ToString() ?? "Chưa nộp";
                switch (status)
                {
                    case "Chưa nộp":
                        e.CellStyle.BackColor = System.Drawing.Color.LightYellow;
                        break;
                    case "Đã nộp":
                        e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                        break;
                    case "Nghỉ":
                        e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                        break;
                }
            }
            else if (!isAdmin && e.ColumnIndex == 1) // Cột TrangThai cho nhân viên
            {
                string status = e.Value?.ToString() ?? "Chưa nộp";
                switch (status)
                {
                    case "Chưa nộp":
                        e.CellStyle.BackColor = System.Drawing.Color.LightYellow;
                        break;
                    case "Đã nộp":
                        e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                        break;
                    case "Nghỉ":
                        e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                        break;
                }
            }
            else if (e.ColumnIndex == 2 || (isAdmin && e.ColumnIndex > 2 && dtList.Columns[e.ColumnIndex].Name.StartsWith("LyDoNghi")))
            {
                e.Value = e.Value?.ToString() ?? "";
            }
        }

        private void dtList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isAdmin && e.ColumnIndex > 2 && dtList.Columns[e.ColumnIndex].Name.StartsWith("Day"))
            {
                string status = dtList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "Chưa nộp";
                int day = int.Parse(dtList.Columns[e.ColumnIndex].Name.Replace("Day", ""));
                string lyDoColumn = $"LyDoNghi{day}";
                if (status == "Nghỉ" && string.IsNullOrEmpty(dtList.Rows[e.RowIndex].Cells[lyDoColumn].Value?.ToString()))
                {
                    MessageBox.Show("Vui lòng nhập lý do nghỉ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Chưa nộp";
                    dtList.CurrentCell = dtList.Rows[e.RowIndex].Cells[lyDoColumn];
                }
            }
            else if (!isAdmin && e.ColumnIndex == 1) // Cột TrangThai cho nhân viên
            {
                string status = dtList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "Chưa nộp";
                if (status == "Nghỉ" && string.IsNullOrEmpty(dtList.Rows[e.RowIndex].Cells[2].Value?.ToString()))
                {
                    MessageBox.Show("Vui lòng nhập lý do nghỉ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Chưa nộp";
                    dtList.CurrentCell = dtList.Rows[e.RowIndex].Cells[2];
                }
            }
        }
    }
}