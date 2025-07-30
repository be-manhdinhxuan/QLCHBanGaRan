using QLCHBanGaRan.UCFunction;
using QLCHBanGaRan.UCSystems;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_Main : Form
    {
        public static string NguoiDungID; // Giữ biến tĩnh để tương thích với logic cũ nếu cần
        public string CurrentMaND { get; set; } // Truyền maND

        bool checkPer;

        static frm_Main _obj;
        // Gọi userControls
        UC_Home _Home = new UC_Home();
        UC_Order _Order = new UC_Order();
        UC_Product _Product = new UC_Product();
        UC_Personnel _Personnel = new UC_Personnel();
        UC_Salary _Salary = new UC_Salary();
        UC_Report _Report = new UC_Report();
        UC_System _System = new UC_System();
        UC_Category _Category = new UC_Category();
        UC_Noti _Noti = new UC_Noti();

        // Trạng thái zoom & khôi phục
        private bool _isZoomed = false;
        private Rectangle? _restoreBounds = null; // Lưu bounds trước khi phóng to

        public frm_Main(string NguoiDungID_Login)
        {
            InitializeComponent();

            // Chúng ta điều khiển phóng to/thu nhỏ bằng code
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.None;

            addControlsToPanel(_Home);
            NguoiDungID = NguoiDungID_Login;
            CurrentMaND = NguoiDungID_Login;

            // Ẩn các UC ban đầu
            _Order.Visible = false;
            _Personnel.Visible = false;
            _Product.Visible = false;
            _Report.Visible = false;
            _Salary.Visible = false;
            _System.Visible = false;
            _Category.Visible = false;

            this.Text = "Trang chủ";

            // Đăng ký sự kiện từ UC_FoodManager (nằm trong UC_Product)
            if (_Product.Controls.Count > 0 && _Product.Controls[0] is UC_FoodManager foodManager)
            {
                foodManager.ProductAdded += FoodManager_ProductAdded;
            }

            // Event cần thiết
            this.SizeChanged += Frm_Main_SizeChanged;
            this.Shown += Frm_Main_Shown;
            this.ResizeEnd += Frm_Main_ResizeEnd;
        }

        public static frm_Main Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new frm_Main(NguoiDungID);
                }
                return _obj;
            }
        }

        public Panel pnlContainer
        {
            get { return panelControls; }
            set { panelControls = value; }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có muốn thoát không?", "Thông báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result != DialogResult.OK) return;   // Bấm Cancel thì dừng tại đây

            NguoiDungID = null;
            CurrentMaND = null;

            Application.Exit(); // Thoát toàn bộ app
        }


        // Di chuyển side menu
        private void moveSidePanel(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }

        // Add UC vào panel
        private void addControlsToPanel(Control c)
        {
            c.Dock = DockStyle.Fill;
            panelControls.Controls.Clear();
            panelControls.Controls.Add(c);
            c.BringToFront();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnHome);
            addControlsToPanel(_Home);
            this.Text = "Trang chủ";
        }

        public void btnOrder_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnOrder);
            if (_Order.Visible)
            {
                addControlsToPanel(_Order);
                this.Text = "Gọi món";
                if (_Order is UC_Order orderControl)
                    orderControl.RefreshProductList();
            }
            else addControlsToPanel(_Noti);
        }

        public void btnProduct_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnProduct);
            if (_Product.Visible)
            {
                addControlsToPanel(_Product);
                this.Text = "Sản phẩm";
                if (_Product.Controls.Count > 0 && _Product.Controls[0] is UC_FoodManager foodManager)
                    foodManager.ProductAdded += FoodManager_ProductAdded;
            }
            else addControlsToPanel(_Noti);
        }

        private void btnPersonnel_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnPersonnel);
            if (_Personnel.Visible)
            {
                addControlsToPanel(_Personnel);
                this.Text = "Nhân sự";
            }
            else
            {
                addControlsToPanel(_Noti);
                this.Text = "Thông báo";
            }
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnSalary);
            if (_Salary.Visible)
            {
                addControlsToPanel(_Salary);
                this.Text = "Lương";
            }
            else
            {
                addControlsToPanel(_Noti);
                this.Text = "Thông báo";
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnReport);
            if (_Report.Visible)
            {
                addControlsToPanel(_Report);
                this.Text = "Thống kê";
            }
            else
            {
                addControlsToPanel(_Noti);
                this.Text = "Thông báo";
            }
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnCategory);
            if (_Category.Visible)
            {
                addControlsToPanel(_Category);
                this.Text = "Danh mục";
            }
            else
            {
                addControlsToPanel(_Noti);
                this.Text = "Thông báo";
            }
        }

        private void btnSystem_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnSystem);
            if (_System.Visible)
            {
                addControlsToPanel(_System);
                this.Text = "Hệ thống";
            }
            else
            {
                addControlsToPanel(_Noti);
                this.Text = "Thông báo";
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnLogout);
            var result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                NguoiDungID = null;
                CurrentMaND = null;
                this.Hide();
                using (var loginForm = new frm_Login())
                {
                    loginForm.ShowDialog();
                }
                this.Close();
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            using (frm_About about = new frm_About())
                about.ShowDialog();
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            _loadPermission();
            _obj = this;
            lblUserAccount.Text = $"Chào {lib.cls_EmployeeManagement.GetEmployeeInfo(NguoiDungID)[1]}!";

            // Khi load: kẹp vào WorkingArea rồi canh giữa (nếu chưa zoom)
            EnsureFormInWorkingArea();
            if (!_isZoomed) CenterInWorkingArea();
        }

        private void _loadPermission()
        {
            checkPer = lib.cls_EmployeeManagement.CheckPermission(NguoiDungID);

            if (checkPer)
            {
                _Order.Visible = true;
                _Personnel.Visible = true;
                _Product.Visible = true;
                _Report.Visible = true;
                _Salary.Visible = true;
                _System.Visible = true;
                _Category.Visible = true;
            }
            else
            {
                _Order.Visible = true;
                _Product.Visible = true;
                _Salary.Visible = true;     // theo logic hiện tại
                _Personnel.Visible = false;
                _System.Visible = false;
            }
        }

        /// <summary>
        /// Kẹp form vào vùng làm việc (WorkingArea) của màn hình chứa form:
        /// - Nếu kích thước vượt quá WorkingArea -> thu nhỏ lại vừa.
        /// - Nếu vị trí lệch ra ngoài -> dịch vào trong.
        /// Không tự động phóng to full màn hình.
        /// </summary>
        private void EnsureFormInWorkingArea()
        {
            var screen = Screen.FromHandle(this.Handle);
            var wa = screen.WorkingArea;

            int w = Math.Min(this.Width, wa.Width);
            int h = Math.Min(this.Height, wa.Height);

            int x = this.Left;
            int y = this.Top;

            if (x < wa.Left) x = wa.Left;
            if (y < wa.Top) y = wa.Top;
            if (x + w > wa.Right) x = wa.Right - w;
            if (y + h > wa.Bottom) y = wa.Bottom - h;

            if (w != this.Width || h != this.Height)
                this.Size = new Size(w, h);
            if (x != this.Left || y != this.Top)
                this.Location = new Point(x, y);
        }

        /// <summary>
        /// Canh giữa form trong WorkingArea (nếu form nhỏ hơn WorkingArea).
        /// </summary>
        private void CenterInWorkingArea()
        {
            var wa = Screen.FromHandle(this.Handle).WorkingArea;

            // Chỉ canh giữa nếu form nhỏ hơn WorkingArea
            int w = Math.Min(this.Width, wa.Width);
            int h = Math.Min(this.Height, wa.Height);

            int x = wa.Left + (wa.Width - w) / 2;
            int y = wa.Top + (wa.Height - h) / 2;

            // Giữ kích thước hiện tại (đã kẹp nếu cần), chỉ set Location
            this.Location = new Point(x, y);
        }

        /// <summary>
        /// Trả về Bounds đã kẹp nằm trong WorkingArea (giữ kích thước gốc nếu có thể).
        /// </summary>
        private Rectangle ClampToWorkingArea(Rectangle bounds)
        {
            var wa = Screen.FromHandle(this.Handle).WorkingArea;

            int w = Math.Min(bounds.Width, wa.Width);
            int h = Math.Min(bounds.Height, wa.Height);

            int x = bounds.Left;
            int y = bounds.Top;

            if (x < wa.Left) x = wa.Left;
            if (y < wa.Top) y = wa.Top;
            if (x + w > wa.Right) x = wa.Right - w;
            if (y + h > wa.Bottom) y = wa.Bottom - h;

            return new Rectangle(x, y, w, h);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            // Không cho vượt WorkingArea của màn hình chứa form
            var wa = Screen.FromHandle(this.Handle).WorkingArea;

            if (width > wa.Width) width = wa.Width;
            if (height > wa.Height) height = wa.Height;

            if (x < wa.X) x = wa.X;
            if (y < wa.Y) y = wa.Y;
            if (x + width > wa.Right) x = wa.Right - width;
            if (y + height > wa.Bottom) y = wa.Bottom - height;

            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Kẹp & canh giữa khi form load (nếu chưa zoom)
            EnsureFormInWorkingArea();
            if (!_isZoomed) CenterInWorkingArea();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            var wa = Screen.FromHandle(this.Handle).WorkingArea;

            if (!_isZoomed)
            {
                // Lưu lại bounds hiện tại để có thể thu nhỏ về sau
                _restoreBounds = this.Bounds;

                // Phóng to đúng bằng WorkingArea
                this.Location = wa.Location;
                this.Size = wa.Size;

                _isZoomed = true;
            }
            else
            {
                // Thu nhỏ về đúng kích thước/vị trí trước khi phóng
                if (_restoreBounds.HasValue)
                {
                    var r = ClampToWorkingArea(_restoreBounds.Value);
                    this.Bounds = r;
                }
                else
                {
                    // Trường hợp dự phòng: đặt về giữa WorkingArea với kích thước 80%
                    int w = (int)(wa.Width * 0.8);
                    int h = (int)(wa.Height * 0.8);
                    int x = wa.Left + (wa.Width - w) / 2;
                    int y = wa.Top + (wa.Height - h) / 2;
                    this.Bounds = new Rectangle(x, y, w, h);
                }

                _isZoomed = false;
            }
        }

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Frm_Main_SizeChanged(object sender, EventArgs e)
        {
            // Cập nhật vị trí các nút khi form thay đổi kích thước
            btnMinimize.Location = new Point(pnlHeader.Width - 79, 0); // bên trái
            btnZoom.Location = new Point(pnlHeader.Width - 52, 0); // giữa
            btnClose.Location = new Point(pnlHeader.Width - 25, 0); // mép phải

            // Tránh can thiệp khi đang Minimized
            if (this.WindowState == FormWindowState.Minimized) return;

            // Kẹp gọn nếu tràn
            EnsureFormInWorkingArea();
        }

        private void Frm_Main_Shown(object sender, EventArgs e)
        {
            // Lần đầu hiển thị: kẹp & canh giữa (nếu chưa zoom)
            EnsureFormInWorkingArea();
            if (!_isZoomed) CenterInWorkingArea();
        }

        private void Frm_Main_ResizeEnd(object sender, EventArgs e)
        {
            // Khi người dùng resize xong, kẹp lại vào WorkingArea
            EnsureFormInWorkingArea();

            // Không tự chỉnh _isZoomed ở đây; chỉ dùng nút Zoom để đổi trạng thái.
            // Nhưng nếu người dùng tự kéo đúng full WorkingArea, bạn có thể nhận biết như sau (tùy chọn):
            var wa = Screen.FromHandle(this.Handle).WorkingArea;
            if (this.Bounds == wa)
            {
                _isZoomed = true;
            }
        }

        // Khi thêm món, cập nhật list ở UC_Order
        private void FoodManager_ProductAdded(object sender, EventArgs e)
        {
            if (_Order is UC_Order orderControl)
                orderControl.RefreshProductList();
        }
    }
}
