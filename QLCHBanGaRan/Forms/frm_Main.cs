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
        public string CurrentMaND { get; set; } // Thêm thuộc tính để truyền maND

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

        // Thêm biến lưu trạng thái gốc
        private Size _originalSize;
        private Point _originalLocation;
        private bool _isZoomed = false;

        public frm_Main(string NguoiDungID_Login)
        {
            InitializeComponent();

            // Đảm bảo form không che thanh taskbar ngay từ đầu
            this.MaximizeBox = false; // Tắt nút maximize mặc định
            this.FormBorderStyle = FormBorderStyle.None; // Giữ nguyên border style

            addControlsToPanel(_Home);
            NguoiDungID = NguoiDungID_Login; // Gán giá trị tĩnh
            CurrentMaND = NguoiDungID_Login; // Gán giá trị cho thuộc tính mới

            // Ẩn tất cả các UC ban đầu
            _Order.Visible = false;
            _Personnel.Visible = false;
            _Product.Visible = false;
            _Report.Visible = false;
            _Salary.Visible = false;
            _System.Visible = false;
            _Category.Visible = false;

            // Đặt tiêu đề mặc định là "Trang chủ" khi vào Home
            this.Text = "Trang chủ";

            // Đăng ký sự kiện từ UC_FoodManager (nằm trong UC_Product)
            if (_Product.Controls.Count > 0 && _Product.Controls[0] is UC_FoodManager foodManager)
            {
                foodManager.ProductAdded += FoodManager_ProductAdded;
            }

            // Chỉ giữ lại những event handler cần thiết
            this.SizeChanged += Frm_Main_SizeChanged;
            this.Shown += Frm_Main_Shown;
        }

        public static frm_Main Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new frm_Main(NguoiDungID); // Sử dụng NguoiDungID tĩnh
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
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                NguoiDungID = null;
                CurrentMaND = null;
                this.Dispose();
            }
            Application.Exit(); // Thoát ứng dụng
        }

        // Function di chuyển side menu
        private void moveSidePanel(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }

        // Khi chọn menu sẽ add UC_Controls tương ứng và cập nhật tiêu đề
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
            if (_Order.Visible == true)
            {
                addControlsToPanel(_Order);
                this.Text = "Gọi món";
                if (_Order is UC_Order orderControl)
                {
                    orderControl.RefreshProductList();
                }
            }
            else
            {
                addControlsToPanel(_Noti);
            }
        }

        public void btnProduct_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnProduct);
            if (_Product.Visible == true)
            {
                addControlsToPanel(_Product);
                this.Text = "Sản phẩm";
                if (_Product.Controls.Count > 0 && _Product.Controls[0] is UC_FoodManager foodManager)
                {
                    foodManager.ProductAdded += FoodManager_ProductAdded;
                }
            }
            else
            {
                addControlsToPanel(_Noti);
            }
        }

        private void btnPersonnel_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnPersonnel);
            if (_Personnel.Visible == true)
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
            if (_Category.Visible == true)
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
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                NguoiDungID = null;
                CurrentMaND = null;
                this.Hide(); // Ẩn form hiện tại thay vì Dispose
                frm_Login loginForm = new frm_Login();
                loginForm.ShowDialog();
                this.Close(); // Đóng form sau khi loginForm được xử lý
            }
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {
            using (frm_About about = new frm_About())
            {
                about.ShowDialog();
            }
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            _loadPermission();
            _obj = this;
            lblUserAccount.Text = $"Chào {lib.cls_EmployeeManagement.GetEmployeeInfo(NguoiDungID)[1]}!";

            // Đảm bảo form không che thanh taskbar khi load
            EnsureFormInWorkingArea();
        }

        private void _loadPermission()
        {
            checkPer = lib.cls_EmployeeManagement.CheckPermission(NguoiDungID);

            if (checkPer)
            {
                // Nếu là quản trị viên (LaQuanTri = true), hiển thị tất cả
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
                // Nếu không phải quản trị viên, chỉ hiển thị một số màn hình nhất định
                _Order.Visible = true;      // User thường có thể gọi món
                _Product.Visible = true;    // User thường có thể xem sản phẩm
                _Salary.Visible = true;    // Quản lý lương - chỉ admin
                // Ẩn các màn hình quản trị
                _Personnel.Visible = false; // Quản lý nhân sự - chỉ admin
                _System.Visible = false;    // Quản lý hệ thống - chỉ admin
            }
        }

        private void EnsureFormInWorkingArea()
        {
            // Đảm bảo form luôn nằm trong vùng làm việc (không che thanh taskbar)
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

            // Kiểm tra nếu form đang ở chế độ phóng to (kích thước bằng working area)
            if (this.Size == workingArea.Size && this.Location == workingArea.Location)
            {
                // Đã đúng, không cần làm gì
                return;
            }

            // Nếu form vượt quá working area, điều chỉnh lại
            if (this.Width > workingArea.Width || this.Height > workingArea.Height ||
                this.Location.X < workingArea.X || this.Location.Y < workingArea.Y)
            {
                this.Location = workingArea.Location;
                this.Size = workingArea.Size;
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            // Đảm bảo form không vượt quá vùng làm việc
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

            if (width > workingArea.Width)
                width = workingArea.Width;
            if (height > workingArea.Height)
                height = workingArea.Height;
            if (x < workingArea.X)
                x = workingArea.X;
            if (y < workingArea.Y)
                y = workingArea.Y;
            if (x + width > workingArea.Right)
                x = workingArea.Right - width;
            if (y + height > workingArea.Bottom)
                y = workingArea.Bottom - height;

            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Đảm bảo form không che thanh taskbar khi load
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            if (this.Size.Width > workingArea.Width || this.Size.Height > workingArea.Height)
            {
                this.Location = workingArea.Location;
                this.Size = workingArea.Size;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            if (!_isZoomed)
            {
                // Lưu lại trạng thái gốc
                _originalSize = this.Size;
                _originalLocation = this.Location;

                // Phóng to
                this.Location = workingArea.Location;
                this.Size = workingArea.Size;
                _isZoomed = true;
            }
            else
            {
                // Thu nhỏ về trạng thái gốc
                this.Size = _originalSize;
                this.Location = _originalLocation;
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
            btnMinimize.Location = new Point(pnlHeader.Width - 79, 0); // btnMinimize bên trái nhất
            btnZoom.Location = new Point(pnlHeader.Width - 52, 0); // btnZoom giữa
            btnClose.Location = new Point(pnlHeader.Width - 25, 0); // btnClose mép phải

            // Đảm bảo form không che thanh taskbar
            EnsureFormInWorkingArea();
        }

        private void Frm_Main_Shown(object sender, EventArgs e)
        {
            // Đảm bảo form không che thanh taskbar khi được hiển thị
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            if (this.Size.Width > workingArea.Width || this.Size.Height > workingArea.Height)
            {
                this.Location = workingArea.Location;
                this.Size = workingArea.Size;
            }
        }

        // Thêm phương thức xử lý sự kiện ProductAdded
        private void FoodManager_ProductAdded(object sender, EventArgs e)
        {
            // Gửi tín hiệu đến UC_Order để làm mới
            if (_Order is UC_Order orderControl)
            {
                orderControl.RefreshProductList(); // Gọi phương thức làm mới trong UC_Order
            }
        }
    }
}