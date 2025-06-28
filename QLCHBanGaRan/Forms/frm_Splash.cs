using QLCHBanGaRan.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_Splash : Form
    {
        private int frameCount = 0;
        private const int TOTAL_FRAMES = 60;

        public frm_Splash()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(228, 0, 42);
        }

        private void frm_Splash_Load(object sender, EventArgs e)
        {
            loadingAnimation = new LoadingAnimation(pictureBoxLoading.Width, pictureBoxLoading.Height);
            timerLoading.Start();

            Task.Run(() =>
            {
                this.Invoke(new Action(() => lblStatus.Text = "Kiểm tra kết nối..."));

                string connectionString = File.Exists("config.env") ? File.ReadAllText("config.env").Trim() : "";
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    this.Invoke(new Action(() =>
                    {
                        lblStatus.Text = "!Lỗi kết nối...";
                        timerLoading.Stop();
                        Task.Delay(2000).Wait();
                        MessageBox.Show("Lỗi kết nối! Vui lòng thao tác thủ công.", "Lỗi");
                        this.Hide();
                        new frm_Connect().ShowDialog();
                    }));
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        Task.Delay(3000).Wait();
                        this.Invoke(new Action(() =>
                        {
                            lblStatus.Text = ".Kết nối thành công. Đang mở ứng dụng...";
                            timerLoading.Stop();
                        }));
                        Task.Delay(2000).Wait();
                        this.Invoke(new Action(() =>
                        {
                            this.Hide();
                            new frm_Login().ShowDialog();
                        }));
                    }
                    catch (Exception)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblStatus.Text = "!Lỗi kết nối...";
                            timerLoading.Stop();
                            Task.Delay(2000).Wait();
                            MessageBox.Show("Không thể kết nối đến Database! Vui lòng cấu hình thủ công.", "Lỗi");
                            this.Hide();
                            new frm_Connect().ShowDialog();
                        }));
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open) conn.Close();
                    }
                }
            });
        }

        private void timerLoading_Tick(object sender, EventArgs e)
        {
            loadingAnimation.UpdateFrame();
            pictureBoxLoading.Image = loadingAnimation.GetBitmap();
            pictureBoxLoading.Refresh();
            frameCount++;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            timerLoading.Stop();
            Application.Exit();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPosition = Cursor.Position;
                lastFormPosition = this.Location;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentCursorPosition = Cursor.Position;
                int deltaX = currentCursorPosition.X - lastCursorPosition.X;
                int deltaY = currentCursorPosition.Y - lastCursorPosition.Y;
                this.Location = new Point(lastFormPosition.X + deltaX, lastFormPosition.Y + deltaY);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
    }
}

