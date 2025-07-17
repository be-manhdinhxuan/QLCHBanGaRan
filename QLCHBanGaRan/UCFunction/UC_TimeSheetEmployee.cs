using QLCHBanGaRan.lib;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_TimeSheetEmployee : UserControl
    {
        private string currentMaND; // Mã người dùng hiện tại
        private string currentMaNV; // Mã nhân viên tương ứng với MaND

        public UC_TimeSheetEmployee(string maND)
        {
            InitializeComponent();
            currentMaND = maND;
            InitializeTimesheet();
            StartClock();
            LoadEmployeeInfo();
        }

        private void StartClock()
        {
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy");
        }

        private void LoadEmployeeInfo()
        {
            if (string.IsNullOrEmpty(currentMaND)) return;
            string query = "SELECT MaNV FROM NguoiDung WHERE MaND = @MaND";
            var parameters = new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@MaND", currentMaND)
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);
            if (dt.Rows.Count > 0)
            {
                currentMaNV = dt.Rows[0]["MaNV"].ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeTimesheet()
        {
            // Thiết lập các cột cho dgvTimesheet
            dgvTimesheet.Columns.Clear();
            dgvTimesheet.Columns.Add("Ngay", "Ngày");
            dgvTimesheet.Columns.Add("GioVao", "Giờ vào");
            dgvTimesheet.Columns.Add("TrangThai", "Trạng thái");
            dgvTimesheet.Columns.Add("LyDo", "Lý do nghỉ");
            dgvTimesheet.Columns["LyDo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Tải dữ liệu từ SQL
            LoadTimesheetData();
        }

        private void LoadTimesheetData()
        {
            if (string.IsNullOrEmpty(currentMaNV)) return;

            DateTime today = DateTime.Today;
            int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
            dgvTimesheet.Rows.Clear();

            string query = @"
                SELECT CONVERT(VARCHAR(10), NgayChamCong, 103) AS Ngay, 
                       GioVao, 
                       TrangThai, LyDoNghi AS LyDo
                FROM ChamCongTheoNgay
                WHERE MaNV = @MaNV AND YEAR(NgayChamCong) = @Year AND MONTH(NgayChamCong) = @Month
                ORDER BY NgayChamCong";
            var parameters = new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@MaNV", currentMaNV),
                new System.Data.SqlClient.SqlParameter("@Year", today.Year),
                new System.Data.SqlClient.SqlParameter("@Month", today.Month)
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);

            int currentRowIndex = -1;
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime date = new DateTime(today.Year, today.Month, day);
                int rowIndex = dgvTimesheet.Rows.Add();
                dgvTimesheet.Rows[rowIndex].Cells["Ngay"].Value = date.ToString("dd/MM/yyyy");

                // Tìm dữ liệu từ SQL nếu có
                DataRow[] rows = dt.Select($"Ngay = '{date.ToString("dd/MM/yyyy")}'");
                if (rows.Length > 0)
                {
                    dgvTimesheet.Rows[rowIndex].Cells["GioVao"].Value = rows[0]["GioVao"].ToString() ?? "-";
                    string trangThai = ConvertToStatus(Convert.ToInt32(rows[0]["TrangThai"]));
                    dgvTimesheet.Rows[rowIndex].Cells["TrangThai"].Value = trangThai;
                    dgvTimesheet.Rows[rowIndex].Cells["LyDo"].Value = rows[0]["LyDo"].ToString() ?? "";
                }
                else
                {
                    dgvTimesheet.Rows[rowIndex].Cells["GioVao"].Value = "-";
                    dgvTimesheet.Rows[rowIndex].Cells["TrangThai"].Value = "Chưa chấm công";
                    dgvTimesheet.Rows[rowIndex].Cells["LyDo"].Value = "";
                }

                // Vô hiệu hóa các ngày trong tương lai
                if (date > today)
                {
                    dgvTimesheet.Rows[rowIndex].ReadOnly = true;
                    dgvTimesheet.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                }

                // Lưu chỉ số hàng của ngày hiện tại
                if (date.Date == today.Date)
                {
                    currentRowIndex = rowIndex;
                }
            }

            // Focus vào ngày hiện tại
            if (currentRowIndex >= 0 && currentRowIndex < dgvTimesheet.Rows.Count)
            {
                dgvTimesheet.CurrentCell = dgvTimesheet.Rows[currentRowIndex].Cells[0]; // Focus vào cột "Ngày"
                dgvTimesheet.FirstDisplayedScrollingRowIndex = currentRowIndex; // Cuộn đến hàng hiện tại
            }

            // Cập nhật trạng thái nút dựa trên ngày hiện tại
            UpdateButtonStates(today);
        }

        private string ConvertToStatus(int trangThai)
        {
            switch (trangThai)
            {
                case 1: return "Chưa chấm công";
                case 2: return "Đi làm";
                default: return "Nghỉ";
            }
        }

        private int ConvertToStatusCode(string trangThai)
        {
            switch (trangThai.ToLower())
            {
                case "chưa chấm công": return 1;
                case "đi làm": return 2;
                case "nghỉ": return 3;
                default: return 1;
            }
        }

        private void UpdateButtonStates(DateTime today)
        {
            if (string.IsNullOrEmpty(currentMaNV)) return;

            string query = @"
                SELECT TrangThai
                FROM ChamCongTheoNgay
                WHERE MaNV = @MaNV AND CONVERT(DATE, NgayChamCong) = @Ngay";
            var parameters = new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@MaNV", currentMaNV),
                new System.Data.SqlClient.SqlParameter("@Ngay", today)
            };
            DataTable dt = cls_DatabaseManager.TableRead(query, parameters);

            bool isCheckedInOrOff = dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["TrangThai"]) != 1;
            btnCheckIn.Enabled = !isCheckedInOrOff; // Vô hiệu nếu đã chấm công hoặc nghỉ
            btnXinNghi.Enabled = !isCheckedInOrOff; // Vô hiệu nếu đã chấm công hoặc nghỉ
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            if (!btnCheckIn.Enabled) return; // Không cho chấm công nếu đã vô hiệu

            if (string.IsNullOrEmpty(currentMaNV)) return;

            DateTime today = DateTime.Today;
            string queryCheck = "SELECT MaChamCong FROM ChamCongTheoNgay WHERE MaNV = @MaNV AND CONVERT(DATE, NgayChamCong) = @Ngay";
            var checkParams = new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@MaNV", currentMaNV),
                new System.Data.SqlClient.SqlParameter("@Ngay", today)
            };
            DataTable dtCheck = cls_DatabaseManager.TableRead(queryCheck, checkParams);

            if (dtCheck.Rows.Count > 0)
            {
                string maChamCong = dtCheck.Rows[0]["MaChamCong"].ToString();
                string updateQuery = "UPDATE ChamCongTheoNgay SET GioVao = @GioVao, TrangThai = @TrangThai, LyDoNghi = @LyDo WHERE MaChamCong = @MaChamCong";
                var updateParams = new System.Data.SqlClient.SqlParameter[] {
                    new System.Data.SqlClient.SqlParameter("@GioVao", DateTime.Now.ToString("HH:mm:ss")),
                    new System.Data.SqlClient.SqlParameter("@TrangThai", 2),
                    new System.Data.SqlClient.SqlParameter("@LyDo", ""),
                    new System.Data.SqlClient.SqlParameter("@MaChamCong", maChamCong)
                };
                if (cls_DatabaseManager.ExecuteNonQuery(updateQuery, updateParams) > 0)
                {
                    LoadTimesheetData();
                    MessageBox.Show("Chấm công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                string insertQuery = "INSERT INTO ChamCongTheoNgay (MaNV, NgayChamCong, GioVao, TrangThai, LyDoNghi) VALUES (@MaNV, @Ngay, @GioVao, @TrangThai, @LyDo)";
                var insertParams = new System.Data.SqlClient.SqlParameter[] {
                    new System.Data.SqlClient.SqlParameter("@MaNV", currentMaNV),
                    new System.Data.SqlClient.SqlParameter("@Ngay", today),
                    new System.Data.SqlClient.SqlParameter("@GioVao", DateTime.Now.ToString("HH:mm:ss")),
                    new System.Data.SqlClient.SqlParameter("@TrangThai", 2),
                    new System.Data.SqlClient.SqlParameter("@LyDo", "")
                };
                if (cls_DatabaseManager.ExecuteNonQuery(insertQuery, insertParams) > 0)
                {
                    LoadTimesheetData();
                    MessageBox.Show("Chấm công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXinNghi_Click(object sender, EventArgs e)
        {
            if (!btnXinNghi.Enabled) return; // Không cho xin nghỉ nếu đã vô hiệu

            if (string.IsNullOrWhiteSpace(txtLyDo.Text))
            {
                MessageBox.Show("Vui lòng nhập lý do xin nghỉ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(currentMaNV)) return;

            DateTime today = DateTime.Today;
            string queryCheck = "SELECT MaChamCong FROM ChamCongTheoNgay WHERE MaNV = @MaNV AND CONVERT(DATE, NgayChamCong) = @Ngay";
            var checkParams = new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@MaNV", currentMaNV),
                new System.Data.SqlClient.SqlParameter("@Ngay", today)
            };
            DataTable dtCheck = cls_DatabaseManager.TableRead(queryCheck, checkParams);

            if (dtCheck.Rows.Count > 0)
            {
                string maChamCong = dtCheck.Rows[0]["MaChamCong"].ToString();
                string updateQuery = "UPDATE ChamCongTheoNgay SET TrangThai = @TrangThai, LyDoNghi = @LyDo WHERE MaChamCong = @MaChamCong";
                var updateParams = new System.Data.SqlClient.SqlParameter[] {
                    new System.Data.SqlClient.SqlParameter("@TrangThai", 3),
                    new System.Data.SqlClient.SqlParameter("@LyDo", txtLyDo.Text.Trim()),
                    new System.Data.SqlClient.SqlParameter("@MaChamCong", maChamCong)
                };
                if (cls_DatabaseManager.ExecuteNonQuery(updateQuery, updateParams) > 0)
                {
                    LoadTimesheetData();
                    txtLyDo.Clear();
                    MessageBox.Show("Đã ghi nhận xin nghỉ hôm nay.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                string insertQuery = "INSERT INTO ChamCongTheoNgay (MaNV, NgayChamCong, GioVao, TrangThai, LyDoNghi) VALUES (@MaNV, @Ngay, @GioVao, @TrangThai, @LyDo)";
                var insertParams = new System.Data.SqlClient.SqlParameter[] {
                    new System.Data.SqlClient.SqlParameter("@MaNV", currentMaNV),
                    new System.Data.SqlClient.SqlParameter("@Ngay", today),
                    new System.Data.SqlClient.SqlParameter("@GioVao", DateTime.Now.ToString("HH:mm:ss")), // Lưu giờ khi xin nghỉ
                    new System.Data.SqlClient.SqlParameter("@TrangThai", 3),
                    new System.Data.SqlClient.SqlParameter("@LyDo", txtLyDo.Text.Trim())
                };
                if (cls_DatabaseManager.ExecuteNonQuery(insertQuery, insertParams) > 0)
                {
                    LoadTimesheetData();
                    txtLyDo.Clear();
                    MessageBox.Show("Đã ghi nhận xin nghỉ hôm nay.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this); // Quay lại màn hình trước
        }

        private void UC_TimeSheetEmployee_Load(object sender, EventArgs e)
        {
            // Gọi các phương thức khởi tạo khi tải form
            if (string.IsNullOrEmpty(currentMaND))
            {
                MessageBox.Show("Vui lòng đăng nhập để sử dụng chức năng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            InitializeTimesheet();
            LoadEmployeeInfo();
        }
    }
}