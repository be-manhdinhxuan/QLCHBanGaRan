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
    public partial class frm_TimeSheetManager : Form
    {
        private DateTimePicker dtpGioRaPicker;

        public frm_TimeSheetManager()
        {
            InitializeComponent();
            dtpThang.Value = DateTime.Now; // Đặt giá trị mặc định là tháng hiện tại
            LoadNhanVienComboBox(); // Tải danh sách nhân viên vào combobox

            dtList.CellBeginEdit += (s, e) =>
            {
                if (e.ColumnIndex != dtList.Columns["gioRaDataGridViewTextBoxColumn"].Index &&
                    e.ColumnIndex != dtList.Columns["lyDoNghiDataGridViewTextBoxColumn"].Index)
                {
                    dtpGioRaPicker.Visible = false;
                }
            };

            // Gán sự kiện cuộn để ẩn DateTimePicker
            dtList.Scroll += (s, e) => dtpGioRaPicker.Visible = false;
        }

        private void LoadNhanVienComboBox()
        {
            string query = "SELECT MaNV, TenNV FROM NhanVien WHERE IsDeleted = 0 ORDER BY TenNV";
            DataTable dtNhanVien = cls_DatabaseManager.TableRead(query);
            cmbFilterNV.DropDownStyle = ComboBoxStyle.DropDownList; // Đặt kiểu dropdown list
            cmbFilterNV.Items.Clear(); // Xóa các mục cũ trong combobox
            cmbFilterNV.DataSource = dtNhanVien;
            cmbFilterNV.DisplayMember = "TenNV"; // Hiển thị tên nhân viên
            cmbFilterNV.ValueMember = "MaNV";    // Giá trị thực sự là mã nhân viên
            cmbFilterNV.SelectedIndex = -1;      // Không chọn mặc định, hiển thị tất cả nếu không chọn
        }

        private void frm_TimeSheetManager_Load(object sender, EventArgs e)
        {
            btnHienThi_Click(sender, e); // Tự động hiển thị dữ liệu khi load

            dtpGioRaPicker = new DateTimePicker();
            dtpGioRaPicker.Format = DateTimePickerFormat.Custom;
            dtpGioRaPicker.CustomFormat = "HH:mm";
            dtpGioRaPicker.ShowUpDown = true; // Chỉ hiển thị giờ, không có lịch
            dtpGioRaPicker.Visible = false;
            dtpGioRaPicker.Width = 100;

            dtpGioRaPicker.CloseUp += DtpGioRaPicker_CloseUp;
            dtpGioRaPicker.ValueChanged += DtpGioRaPicker_ValueChanged;

            dtList.Controls.Add(dtpGioRaPicker);
            dtList.CellDoubleClick += dtList_CellDoubleClick;
        }

        private void dtList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dtList.Columns["gioRaDataGridViewTextBoxColumn"] != null &&
                e.ColumnIndex == dtList.Columns["gioRaDataGridViewTextBoxColumn"].Index)
            {
                Rectangle rect = dtList.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                dtpGioRaPicker.Location = new Point(rect.X, rect.Y);
                dtpGioRaPicker.Width = rect.Width;
                dtpGioRaPicker.Height = rect.Height;
                dtpGioRaPicker.Visible = true;
                dtpGioRaPicker.BringToFront();
                dtpGioRaPicker.Focus();

                var cellValue = dtList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (TimeSpan.TryParse(cellValue, out TimeSpan parsedTime))
                {
                    dtpGioRaPicker.Value = DateTime.Today.Add(parsedTime);
                }
                else
                {
                    dtpGioRaPicker.Value = DateTime.Today; // Đặt giờ mặc định nếu không parse được
                }

                dtpGioRaPicker.Tag = new Point(e.RowIndex, e.ColumnIndex); // Ghi lại vị trí
            }
            else
            {
                dtpGioRaPicker.Visible = false;
            }
        }

        private void DtpGioRaPicker_ValueChanged(object sender, EventArgs e)
        {
            if (dtpGioRaPicker.Tag is Point cellPos)
            {
                int row = cellPos.X;
                int col = cellPos.Y;
                string gioMoi = dtpGioRaPicker.Value.ToString("HH:mm");

                var cell = dtList.Rows[row].Cells[col];
                var gioCu = cell.Value?.ToString();

                if (gioCu != gioMoi)
                {
                    cell.Value = gioMoi;

                    // Tính lại số giờ làm và hiển thị dưới dạng HH:mm
                    var gioVaoStr = dtList.Rows[row].Cells["gioVaoDataGridViewTextBoxColumn"].Value?.ToString();
                    if (TimeSpan.TryParse(gioVaoStr, out TimeSpan gioVao) && TimeSpan.TryParse(gioMoi, out TimeSpan gioRa))
                    {
                        TimeSpan thoiGianLam = gioRa - gioVao;
                        if (thoiGianLam.TotalMinutes >= 0)
                        {
                            double soGioThapPhan = thoiGianLam.TotalHours;
                            dtList.Rows[row].Cells["soGioLamDataGridViewTextBoxColumn"].Value = TimeSpan.FromHours(soGioThapPhan).ToString(@"hh\:mm");
                        }
                        else
                        {
                            // Nếu giờ ra nhỏ hơn giờ vào, giả định ca làm qua ngày (nửa đêm)
                            thoiGianLam = gioRa.Add(new TimeSpan(24, 0, 0)) - gioVao;
                            double soGioThapPhan = thoiGianLam.TotalHours;
                            dtList.Rows[row].Cells["soGioLamDataGridViewTextBoxColumn"].Value = TimeSpan.FromHours(soGioThapPhan).ToString(@"hh\:mm");
                        }
                    }
                    else
                    {
                        dtList.Rows[row].Cells["soGioLamDataGridViewTextBoxColumn"].Value = "00:00"; // Mặc định nếu không tính được
                    }
                }
            }
        }

        private void DtpGioRaPicker_CloseUp(object sender, EventArgs e)
        {
            dtpGioRaPicker.Visible = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Forms.frm_Main.Instance.Controls["frm_Salary"].BringToFront();
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string thang = dtpThang.Value.ToString("yyyyMM");
            string maNV = cmbFilterNV.SelectedValue != null ? cmbFilterNV.SelectedValue.ToString() : null;

            // Gọi stored procedure để lấy dữ liệu chấm công
            var parameters = new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@MaND", maNV ?? (object)DBNull.Value),
                new System.Data.SqlClient.SqlParameter("@Thang", thang),
                new System.Data.SqlClient.SqlParameter("@IsAdmin", DBNull.Value)
            };
            DataTable dtChamCong = cls_DatabaseManager.TableReadStoredProc("sp_GetChamCong", parameters);

            // Tạo DataTable với cấu trúc phù hợp
            DataTable dtFormatted = new DataTable();
            dtFormatted.Columns.Add("MaNV", typeof(string));
            dtFormatted.Columns.Add("TenNV", typeof(string));
            dtFormatted.Columns.Add("ChucDanh", typeof(string));
            dtFormatted.Columns.Add("Ngay", typeof(DateTime));
            dtFormatted.Columns.Add("GioVao", typeof(string));
            dtFormatted.Columns.Add("GioRa", typeof(string));
            dtFormatted.Columns.Add("TrangThai", typeof(string));
            dtFormatted.Columns.Add("LyDoNghi", typeof(string));
            dtFormatted.Columns.Add("SoGioLam", typeof(string));
            dtFormatted.Columns.Add("MaChamCong", typeof(long));

            // Thêm dữ liệu mặc định cho nhân viên được chọn
            if (maNV != null)
            {
                string queryNhanVien = @"
                    SELECT nv.MaNV, nv.TenNV, cd.TenChucDanh AS ChucDanh 
                    FROM NhanVien nv 
                    LEFT JOIN ChucDanh cd ON nv.MaChucDanh = cd.MaChucDanh 
                    WHERE nv.MaNV = @MaNV";
                var nvParameters = new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@MaNV", maNV) };
                DataTable dtNhanVien = cls_DatabaseManager.TableRead(queryNhanVien, nvParameters);

                if (dtNhanVien == null || dtNhanVien.Rows.Count == 0)
                {
                    lblStatus.Text = "Không có nhân viên nào để hiển thị.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    dtList.DataSource = null;
                    return;
                }

                int daysInMonth = DateTime.DaysInMonth(dtpThang.Value.Year, dtpThang.Value.Month);
                foreach (DataRow nvRow in dtNhanVien.Rows)
                {
                    for (int day = 1; day <= daysInMonth; day++)
                    {
                        DateTime ngay = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, day);
                        DataRow newRow = dtFormatted.NewRow();
                        newRow["MaNV"] = nvRow["MaNV"].ToString();
                        newRow["TenNV"] = nvRow["TenNV"].ToString();
                        newRow["ChucDanh"] = nvRow["ChucDanh"].ToString();
                        newRow["Ngay"] = ngay;
                        newRow["GioVao"] = "";
                        newRow["GioRa"] = "";
                        newRow["TrangThai"] = "Chưa chấm công";
                        newRow["LyDoNghi"] = "";
                        newRow["SoGioLam"] = "00:00";
                        newRow["MaChamCong"] = DBNull.Value;
                        dtFormatted.Rows.Add(newRow);
                    }
                }
            }

            // Cập nhật dữ liệu từ stored procedure nếu có
            if (dtChamCong != null && dtChamCong.Rows.Count > 0)
            {
                foreach (DataRow row in dtChamCong.Rows)
                {
                    string dbMaNV = row["MaNV"].ToString();
                    if (maNV == null || dbMaNV == maNV)
                    {
                        DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                        string gioVao = row["GioVao"].ToString();
                        string gioRa = row["GioRa"].ToString() ?? "";
                        int trangThaiNum = row["TrangThai"] != DBNull.Value ? Convert.ToInt32(row["TrangThai"]) : 1;
                        string lyDoNghi = row["LyDoNghi"].ToString() ?? "";
                        string soGioLam = "00:00"; // Không lấy từ cơ sở dữ liệu vì SoGioLam nằm ở bảng ThongKeChamCong

                        string trangThaiText = "Chưa chấm công";
                        switch (trangThaiNum)
                        {
                            case 1:
                                trangThaiText = "Chưa chấm công";
                                break;
                            case 2:
                                trangThaiText = "Đi làm";
                                break;
                            case 3:
                                trangThaiText = "Nghỉ";
                                break;
                            default:
                                trangThaiText = "Chưa chấm công";
                                break;
                        }

                        DataRow[] rowsToUpdate = dtFormatted.Select($"MaNV = '{dbMaNV}' AND Ngay = '{ngay:yyyy-MM-dd}'");
                        if (rowsToUpdate.Length > 0)
                        {
                            rowsToUpdate[0]["GioVao"] = gioVao;
                            rowsToUpdate[0]["GioRa"] = gioRa;
                            rowsToUpdate[0]["TrangThai"] = trangThaiText;
                            rowsToUpdate[0]["LyDoNghi"] = lyDoNghi;
                            rowsToUpdate[0]["SoGioLam"] = soGioLam; // Hiển thị mặc định, sẽ tính lại khi chỉnh sửa GioRa
                            rowsToUpdate[0]["MaChamCong"] = row["MaChamCong"];
                        }
                    }
                }
            }

            // Gán DataSource
            dtList.DataSource = dtFormatted;

            lblStatus.Text = $"Đã tải lịch chấm công cho tháng {dtpThang.Value:MM/yyyy}.";
            if (maNV != null)
            {
                lblStatus.Text += $" - Nhân viên: {cmbFilterNV.Text}";
            }
            lblStatus.ForeColor = System.Drawing.Color.Green;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtList.Rows.Count == 0)
            {
                lblStatus.Text = "Không có dữ liệu để lưu.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.ConnectionString))
            {
                try
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dtList.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            // Lấy các giá trị cần thiết
                            long maChamCong = row.Cells["maChamCongDataGridViewTextBoxColumn"].Value != DBNull.Value ? Convert.ToInt64(row.Cells["maChamCongDataGridViewTextBoxColumn"].Value) : 0;
                            string gioRa = row.Cells["gioRaDataGridViewTextBoxColumn"].Value?.ToString() ?? "";
                            string lyDoNghi = row.Cells["lyDoNghiDataGridViewTextBoxColumn"].Value?.ToString() ?? "";
                            string maNV = row.Cells["maNVDataGridViewTextBoxColumn"].Value?.ToString();
                            DateTime ngay = Convert.ToDateTime(row.Cells["ngayDataGridViewTextBoxColumn"].Value);

                            if (string.IsNullOrEmpty(maNV) || ngay == default)
                            {
                                continue; // Bỏ qua nếu thiếu thông tin cần thiết
                            }

                            if (maChamCong != 0)
                            
                            {
                                // Cập nhật bản ghi hiện có
                                using (SqlCommand cmd = new SqlCommand("sp_UpdateChamCong", conn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@MaChamCong", maChamCong);
                                    cmd.Parameters.AddWithValue("@GioRa", string.IsNullOrEmpty(gioRa) ? (object)DBNull.Value : gioRa);
                                    cmd.Parameters.AddWithValue("@LyDoNghi", string.IsNullOrEmpty(lyDoNghi) ? (object)DBNull.Value : lyDoNghi);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    lblStatus.Text = "Đã lưu dữ liệu thành công.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Lỗi khi lưu dữ liệu: " + ex.Message;
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void btnNop_Click(object sender, EventArgs e)
        {
            if (dtList.Rows.Count == 0 || cmbFilterNV.SelectedValue == null)
            {
                lblStatus.Text = "Vui lòng chọn nhân viên và hiển thị dữ liệu trước khi nộp.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string maNV = cmbFilterNV.SelectedValue.ToString();
            string thang = dtpThang.Value.ToString("yyyyMM");

            using (SqlConnection conn = new SqlConnection(cls_DatabaseManager.ConnectionString))
            {
                try
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dtList.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string gioVao = row.Cells["gioVaoDataGridViewTextBoxColumn"].Value?.ToString() ?? "";
                            string gioRa = row.Cells["gioRaDataGridViewTextBoxColumn"].Value?.ToString() ?? "";
                            DateTime ngay = Convert.ToDateTime(row.Cells["ngayDataGridViewTextBoxColumn"].Value);

                            // Tính số giờ làm
                            double soGioLamValue = 0.0;
                            if (TimeSpan.TryParse(gioVao, out TimeSpan gioVaoTime) && TimeSpan.TryParse(gioRa, out TimeSpan gioRaTime))
                            {
                                TimeSpan thoiGianLam = gioRaTime - gioVaoTime;
                                if (thoiGianLam.TotalMinutes >= 0)
                                {
                                    soGioLamValue = thoiGianLam.TotalHours;
                                }
                                else
                                {
                                    // Giả định ca làm qua ngày
                                    thoiGianLam = gioRaTime.Add(new TimeSpan(24, 0, 0)) - gioVaoTime;
                                    soGioLamValue = thoiGianLam.TotalHours;
                                }
                            }

                            // Cập nhật hoặc thêm vào bảng ThongKeChamCong
                            using (SqlCommand cmd = new SqlCommand("sp_InsertThongKeChamCong", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MaNV", maNV);
                                cmd.Parameters.AddWithValue("@Thang", thang);
                                cmd.Parameters.AddWithValue("@SoGioLam", soGioLamValue);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    lblStatus.Text = "Đã nộp và thống kê thành công cho tháng " + thang.Substring(4, 2) + "/" + thang.Substring(0, 4) + ".";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Lỗi khi nộp thống kê: " + ex.Message;
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}