
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

namespace QLCHBanGaRan.Forms
{
    public partial class frm_Main : Form
    {
        public static string NguoiDungID; // Giữ biến tĩnh để tương thích với logic cũ nếu cần
        public string CurrentMaND { get; set; } // Truyền maND

        bool checkPer;

        static frm_Main _obj;
        // Gọi userControls (dùng tạm thời cho _Home hoặc _Noti, các form con sẽ thay thế)
        frm_Home _Home = new frm_Home();
        frm_Noti _Noti = new frm_Noti();

        // Dictionary để theo dõi tab tương ứng với form MDI
        private Dictionary<Form, TabPage> _formTabMapping = new Dictionary<Form, TabPage>();

        // Trạng thái zoom & khôi phục
        private bool _isZoomed = false;
        private Rectangle? _restoreBounds = null; // Lưu bounds trước khi phóng to

        public frm_Main(string NguoiDungID_Login)
        {
            InitializeComponent();

            // Kích hoạt MDI và cho phép form có border
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.IsMdiContainer = true; // Kích hoạt MDI

            // Debug: In thông tin về form chính khi khởi tạo
            Console.WriteLine($"frm_Main constructor: IsMdiContainer={this.IsMdiContainer}, MaximizeBox={this.MaximizeBox}, MinimizeBox={this.MinimizeBox}");

            // Đảm bảo MDI container có kích thước đủ lớn
            this.MinimumSize = new Size(1200, 800);

            // Đảm bảo MDI container có đủ không gian cho child forms
            this.Padding = new Padding(0);

            // Debug: In thông tin về form chính khi khởi tạo
            Console.WriteLine($"frm_Main constructor: MinimumSize={this.MinimumSize}, Padding={this.Padding}");

            // Không thêm _Home vào Controls ngay lập tức để tránh xung đột với MDI
            // _Home sẽ được hiển thị khi cần thiết trong btnHome_Click

            NguoiDungID = NguoiDungID_Login;
            CurrentMaND = NguoiDungID_Login;

            this.Text = "Trang chủ";

            // Event cần thiết
            this.SizeChanged += Frm_Main_SizeChanged;
            this.Shown += Frm_Main_Shown;
            this.ResizeEnd += Frm_Main_ResizeEnd;
            this.FormClosing += Frm_Main_FormClosing;

            // Đăng ký event cho TabControl
            this.tabControlMain.SelectedIndexChanged += TabControlMain_SelectedIndexChanged;

            // Đảm bảo form được thiết lập đúng cách cho MDI
            this.WindowState = FormWindowState.Normal;

            // Debug: In thông tin về form chính
            Console.WriteLine($"frm_Main initialized: ClientSize={this.ClientSize}, IsMdiContainer={this.IsMdiContainer}, WindowState={this.WindowState}");
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



        // Di chuyển side menu
        private void moveSidePanel(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            // Sử dụng method chung để hiển thị frm_Home
            ShowHomeForm();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnOrder);

            // Kiểm tra xem đã có form Order nào đang mở chưa
            foreach (Form child in this.MdiChildren)
            {
                if (child is frm_Order existingOrderForm)
                {
                    existingOrderForm.Activate();
                    existingOrderForm.BringToFront();
                    existingOrderForm.Refresh();

                    // Chọn tab tương ứng
                    if (_formTabMapping.ContainsKey(existingOrderForm))
                    {
                        tabControlMain.SelectedTab = _formTabMapping[existingOrderForm];
                    }

                    Console.WriteLine("Existing frm_Order activated and refreshed");
                    return;
                }
            }

            // Tạo form mới
            frm_Order newOrderForm = new frm_Order();
            newOrderForm.MdiParent = this;
            newOrderForm.Text = "Gọi món";
            newOrderForm.WindowState = FormWindowState.Normal;

            // Đặt kích thước form phù hợp với MDI container
            newOrderForm.Size = new Size(1000, 750);
            newOrderForm.StartPosition = FormStartPosition.CenterParent;

            // Tạo tab cho form
            CreateTabForForm(newOrderForm, "Gọi món");

            // Đảm bảo form được hiển thị đúng cách trong MDI container
            newOrderForm.Show();
            newOrderForm.Activate();
            newOrderForm.BringToFront();
            newOrderForm.Refresh();

            // Debug chi tiết
            Console.WriteLine($"frm_Order created and shown: IsDisposed={newOrderForm.IsDisposed}, Visible={newOrderForm.Visible}, " +
                              $"WindowState={newOrderForm.WindowState}, Location={newOrderForm.Location}, Size={newOrderForm.Size}, " +
                              $"MdiParent.ClientSize={this.ClientSize}, MdiClientArea={this.ClientRectangle}");

            // Debug thêm về MdiClient
            foreach (Control control in this.Controls)
            {
                if (control is MdiClient mdiClient)
                {
                    Console.WriteLine($"MdiClient info: Location={mdiClient.Location}, Size={mdiClient.Size}, Visible={mdiClient.Visible}, MdiChildren.Count={mdiClient.MdiChildren.Length}");
                    break;
                }
            }
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnProduct);

            // Ẩn _Home nếu đang hiển thị
            if (this.Controls.Contains(_Home))
            {
                this.Controls.Remove(_Home);
            }

            // Kiểm tra xem đã có form Product nào đang mở chưa
            foreach (Form child in this.MdiChildren)
            {
                if (child is frm_Product)
                {
                    child.Activate();
                    child.BringToFront();

                    // Chọn tab tương ứng
                    if (_formTabMapping.ContainsKey(child))
                    {
                        tabControlMain.SelectedTab = _formTabMapping[child];
                    }
                    return;
                }
            }

            frm_Product productForm = new frm_Product();
            productForm.MdiParent = this;
            productForm.Text = "Sản phẩm";
            productForm.WindowState = FormWindowState.Normal;
            productForm.Size = new Size(1000, 750);
            productForm.StartPosition = FormStartPosition.CenterParent;

            // Tạo tab cho form
            CreateTabForForm(productForm, "Sản phẩm");

            productForm.Show();
            productForm.Activate();
            productForm.BringToFront();

            if (productForm.Controls[0] is frm_Product productControl && productControl.Controls[0] is frm_FoodManager foodManager)
                foodManager.ProductAdded += FoodManager_ProductAdded;
        }

        private void btnPersonnel_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnPersonnel);

            // Ẩn _Home nếu đang hiển thị
            if (this.Controls.Contains(_Home))
            {
                this.Controls.Remove(_Home);
            }

            // Kiểm tra xem đã có form Personnel nào đang mở chưa
            foreach (Form child in this.MdiChildren)
            {
                if (child is frm_Personnel)
                {
                    child.Activate();
                    child.BringToFront();

                    // Chọn tab tương ứng
                    if (_formTabMapping.ContainsKey(child))
                    {
                        tabControlMain.SelectedTab = _formTabMapping[child];
                    }
                    return;
                }
            }

            frm_Personnel personnelForm = new frm_Personnel();
            personnelForm.MdiParent = this;
            personnelForm.Text = "Nhân sự";
            personnelForm.WindowState = FormWindowState.Normal;
            personnelForm.Size = new Size(1000, 750);
            personnelForm.StartPosition = FormStartPosition.CenterParent;

            // Tạo tab cho form
            CreateTabForForm(personnelForm, "Nhân sự");

            personnelForm.Show();
            personnelForm.Activate();
            personnelForm.BringToFront();
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnSalary);

            // Ẩn _Home nếu đang hiển thị
            if (this.Controls.Contains(_Home))
            {
                this.Controls.Remove(_Home);
            }

            // Kiểm tra xem đã có form Salary nào đang mở chưa
            foreach (Form child in this.MdiChildren)
            {
                if (child is frm_Salary)
                {
                    child.Activate();
                    child.BringToFront();

                    // Chọn tab tương ứng
                    if (_formTabMapping.ContainsKey(child))
                    {
                        tabControlMain.SelectedTab = _formTabMapping[child];
                    }
                    return;
                }
            }

            frm_Salary salaryForm = new frm_Salary();
            salaryForm.MdiParent = this;
            salaryForm.Text = "Lương";
            salaryForm.WindowState = FormWindowState.Normal;
            salaryForm.Size = new Size(1000, 750);
            salaryForm.StartPosition = FormStartPosition.CenterParent;

            // Tạo tab cho form
            CreateTabForForm(salaryForm, "Lương");

            salaryForm.Show();
            salaryForm.Activate();
            salaryForm.BringToFront();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnReport);

            // Ẩn _Home nếu đang hiển thị
            if (this.Controls.Contains(_Home))
            {
                this.Controls.Remove(_Home);
            }

            // Kiểm tra xem đã có form Report nào đang mở chưa
            foreach (Form child in this.MdiChildren)
            {
                if (child is frm_Report)
                {
                    child.Activate();
                    child.BringToFront();

                    // Chọn tab tương ứng
                    if (_formTabMapping.ContainsKey(child))
                    {
                        tabControlMain.SelectedTab = _formTabMapping[child];
                    }
                    return;
                }
            }

            frm_Report reportForm = new frm_Report();
            reportForm.MdiParent = this;
            reportForm.Text = "Thống kê";
            reportForm.WindowState = FormWindowState.Normal;
            reportForm.Size = new Size(1000, 750);
            reportForm.StartPosition = FormStartPosition.CenterParent;

            // Tạo tab cho form
            CreateTabForForm(reportForm, "Thống kê");

            reportForm.Show();
            reportForm.Activate();
            reportForm.BringToFront();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnCategory);

            // Ẩn _Home nếu đang hiển thị
            if (this.Controls.Contains(_Home))
            {
                this.Controls.Remove(_Home);
            }

            // Kiểm tra xem đã có form Category nào đang mở chưa
            foreach (Form child in this.MdiChildren)
            {
                if (child is frm_Category)
                {
                    child.Activate();
                    child.BringToFront();

                    // Chọn tab tương ứng
                    if (_formTabMapping.ContainsKey(child))
                    {
                        tabControlMain.SelectedTab = _formTabMapping[child];
                    }
                    return;
                }
            }

            frm_Category categoryForm = new frm_Category();
            categoryForm.MdiParent = this;
            categoryForm.Text = "Danh mục";
            categoryForm.WindowState = FormWindowState.Normal;
            categoryForm.Size = new Size(1000, 750);
            categoryForm.StartPosition = FormStartPosition.CenterParent;

            // Tạo tab cho form
            CreateTabForForm(categoryForm, "Danh mục");

            categoryForm.Show();
            categoryForm.Activate();
            categoryForm.BringToFront();
        }

        private void btnSystem_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnSystem);

            // Ẩn _Home nếu đang hiển thị
            if (this.Controls.Contains(_Home))
            {
                this.Controls.Remove(_Home);
            }

            // Kiểm tra xem đã có form System nào đang mở chưa
            foreach (Form child in this.MdiChildren)
            {
                if (child is frm_System)
                {
                    child.Activate();
                    child.BringToFront();

                    // Chọn tab tương ứng
                    if (_formTabMapping.ContainsKey(child))
                    {
                        tabControlMain.SelectedTab = _formTabMapping[child];
                    }
                    return;
                }
            }

            frm_System systemForm = new frm_System();
            systemForm.MdiParent = this;
            systemForm.Text = "Hệ thống";
            systemForm.WindowState = FormWindowState.Normal;
            systemForm.Size = new Size(1000, 750);
            systemForm.StartPosition = FormStartPosition.CenterParent;

            // Tạo tab cho form
            CreateTabForForm(systemForm, "Hệ thống");

            systemForm.Show();
            systemForm.Activate();
            systemForm.BringToFront();
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

            // Đảm bảo các panel được đặt đúng vị trí
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Size = new Size(229, this.ClientSize.Height);
            tabControlMain.Location = new Point(229, 0);
            tabControlMain.Size = new Size(this.ClientSize.Width - 229, 26);

            // Debug: In thông tin về các panel
            Console.WriteLine($"Panel positions: pnlLeft={pnlLeft.Location}, {pnlLeft.Size}, tabControlMain={tabControlMain.Location}, {tabControlMain.Size}");

            // Tìm và cấu hình vùng MDI client
            foreach (Control control in this.Controls)
            {
                if (control is MdiClient mdiClient)
                {
                    mdiClient.Location = new Point(229, 26);
                    mdiClient.Size = new Size(this.ClientSize.Width - 229, this.ClientSize.Height - 26);
                    mdiClient.BackColor = Color.Gray;
                    mdiClient.SendToBack();

                    // Đảm bảo MdiClient luôn hiển thị
                    mdiClient.Visible = true;

                    // Debug: In thông tin về MdiClient
                    Console.WriteLine($"MdiClient configured: Location={mdiClient.Location}, Size={mdiClient.Size}, Visible={mdiClient.Visible}");
                    break;
                }
            }

            // Khi load: kẹp vào WorkingArea rồi canh giữa (nếu chưa zoom)
            //EnsureFormInWorkingArea();
            //if (!_isZoomed) CenterInWorkingArea();

            // Tự động hiển thị frm_Home khi form chính được hiển thị
            ShowHomeForm();
        }

        private void _loadPermission()
        {
            checkPer = lib.cls_EmployeeManagement.CheckPermission(NguoiDungID);
            
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

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Xử lý khi người dùng đóng cửa sổ
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(
                    "Bạn có muốn thoát không?", "Thông báo",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result != DialogResult.OK)
                {
                    e.Cancel = true; // Hủy việc đóng cửa sổ
                    return;
                }

                // Xóa thông tin người dùng và thoát
                NguoiDungID = null;
                CurrentMaND = null;
                Application.Exit();
            }
        }



        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void tabControlMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Frm_Main_SizeChanged(object sender, EventArgs e)
        {
            // Cập nhật vị trí tabControlMain để luôn nằm trên cùng bên phải
            tabControlMain.Location = new Point(229, 0);
            tabControlMain.Size = new Size(this.ClientSize.Width - 229, 26);

            // Debug: In thông tin về tabControlMain khi resize
            Console.WriteLine($"tabControlMain resized: Location={tabControlMain.Location}, Size={tabControlMain.Size}");

            // Cập nhật vùng MDI client
            foreach (Control control in this.Controls)
            {
                if (control is MdiClient mdiClient)
                {
                    mdiClient.Location = new Point(229, 26);
                    mdiClient.Size = new Size(this.ClientSize.Width - 229, this.ClientSize.Height - 26);
                    // Đảm bảo MdiClient nằm dưới các panel khác
                    mdiClient.SendToBack();

                    // Debug: In thông tin về MdiClient khi resize
                    Console.WriteLine($"MdiClient resized: Location={mdiClient.Location}, Size={mdiClient.Size}");
                    break;
                }
            }

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

            // Tự động hiển thị frm_Home khi form chính được hiển thị
            ShowHomeForm();
        }

        private void Frm_Main_ResizeEnd(object sender, EventArgs e)
        {
            // Khi người dùng resize xong, kẹp lại vào WorkingArea
            EnsureFormInWorkingArea();

            // Không tự chỉnh _isZoomed ở đây; chỉ dùng nút Zoom để đổi trạng thái.
            var wa = Screen.FromHandle(this.Handle).WorkingArea;
            if (this.Bounds == wa)
            {
                _isZoomed = true;
            }
        }

        // Khi thêm món, cập nhật list ở UC_Order
        private void FoodManager_ProductAdded(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is frm_Order orderForm && orderForm.Controls[0] is frm_Order orderControl)
                {
                    orderControl.RefreshProductList();
                    break;
                }
            }
        }

        /// <summary>
        /// Tạo tab mới cho form MDI
        /// </summary>
        /// <param name="form">Form cần tạo tab</param>
        /// <param name="title">Tiêu đề của tab</param>
        public void CreateTabForForm(Form form, string title)
        {
            if (form == null || tabControlMain == null)
            {
                Console.WriteLine("Error: Form or tabControlMain is null.");
                return;
            }

            // Kiểm tra và cập nhật tab hiện có
            if (_formTabMapping.ContainsKey(form))
            {
                TabPage existingTab = _formTabMapping[form];
                if (existingTab.Text != title) // Cập nhật tiêu đề nếu khác
                {
                    existingTab.Text = title;
                    Console.WriteLine($"Tab updated for {title}: {existingTab.Text}");
                }
                tabControlMain.SelectedTab = existingTab; // Đưa tab lên trước
                form.Activate(); // Đảm bảo form được kích hoạt
                return;
            }

            // Tạo tab mới nếu chưa tồn tại
            TabPage newTab = new TabPage(title);
            newTab.Tag = form; // Lưu reference đến form

            // Thêm tab vào TabControl
            tabControlMain.TabPages.Add(newTab);

            // Lưu mapping
            _formTabMapping[form] = newTab;

            // Chọn tab mới
            tabControlMain.SelectedTab = newTab;
            form.Activate(); // Đảm bảo form được hiển thị

            // Đăng ký event khi form đóng
            form.FormClosed += (s, e) => RemoveTabForForm(form);

            // Debug
            Console.WriteLine($"Tab created for {title}: {newTab.Text}, MdiChildren count: {this.MdiChildren.Length}");
        }

        /// <summary>
        /// Xóa tab khi form đóng
        /// </summary>
        /// <param name="form">Form đã đóng</param>
        private void RemoveTabForForm(Form form)
        {
            if (_formTabMapping.ContainsKey(form))
            {
                TabPage tabToRemove = _formTabMapping[form];
                tabControlMain.TabPages.Remove(tabToRemove);
                _formTabMapping.Remove(form);
                Console.WriteLine($"Tab removed for {form.Text}, Remaining tabs: {tabControlMain.TabCount}");
            }
        }

        /// <summary>
        /// Xử lý khi người dùng click vào tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab != null && tabControlMain.SelectedTab.Tag is Form form)
            {
                // Kích hoạt form tương ứng với tab được chọn
                form.Activate();
                form.BringToFront();
                form.Refresh();

                // Đảm bảo form được hiển thị trong vùng MDI
                if (form.WindowState == FormWindowState.Minimized)
                {
                    form.WindowState = FormWindowState.Normal;
                }

                // Cập nhật text của form chính
                this.Text = form.Text;

                // Debug
                Console.WriteLine($"Tab selected: {tabControlMain.SelectedTab.Text}, Form activated: {form.Text}, " +
                                  $"MdiChildren.Count={this.MdiChildren.Length}, Visible={form.Visible}");
            }
        }

        /// <summary>
        /// Hiển thị frm_Home tự động khi cần thiết
        /// </summary>
        private void ShowHomeForm()
        {
            // Kiểm tra xem đã có frm_Home nào đang mở chưa
            foreach (Form child in this.MdiChildren)
            {
                if (child is frm_Home existingHomeForm)
                {
                    existingHomeForm.Activate();
                    existingHomeForm.BringToFront();
                    existingHomeForm.Refresh();

                    // Chọn tab tương ứng
                    if (_formTabMapping.ContainsKey(existingHomeForm))
                    {
                        tabControlMain.SelectedTab = _formTabMapping[existingHomeForm];
                    }
                    return;
                }
            }

            // Tạo và hiển thị frm_Home mới nếu không có
            frm_Home newHomeForm = new frm_Home();
            newHomeForm.MdiParent = this;
            newHomeForm.WindowState = FormWindowState.Maximized;
            newHomeForm.Text = "Trang chủ";
            newHomeForm.Show();

            // Tạo tab cho frm_Home
            CreateTabForForm(newHomeForm, "Trang chủ");

            // Cập nhật text của form chính
            this.Text = "Trang chủ";

            // Di chuyển side panel về nút Home
            moveSidePanel(btnHome);

            // Debug
            Console.WriteLine($"frm_Home auto-displayed: IsDisposed={newHomeForm.IsDisposed}, Visible={newHomeForm.Visible}, " +
                              $"WindowState={newHomeForm.WindowState}, Location={newHomeForm.Location}, Size={newHomeForm.Size}");
        }
    }
}