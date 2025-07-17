namespace QLCHBanGaRan.UCFunction
{
    partial class UC_SalaryManager
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblMaChucDanh = new System.Windows.Forms.Label();
            this.txtMaChucDanh = new System.Windows.Forms.TextBox();
            this.lblPhuCap = new System.Windows.Forms.Label();
            this.txtPhuCap = new System.Windows.Forms.TextBox();
            this.txtLuongCung = new System.Windows.Forms.TextBox();
            this.lblLuongCung = new System.Windows.Forms.Label();
            this.txtChucDanh = new System.Windows.Forms.TextBox();
            this.lblChucDanh = new System.Windows.Forms.Label();
            this.btnHuyBo = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();
            this.dtList = new System.Windows.Forms.DataGridView();
            this.MaChucDanh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChucDanhID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenChucDanh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TienLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhuCap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpThongTin.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(38, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(241, 21);
            this.lblTitle.TabIndex = 66;
            this.lblTitle.Text = "QUẢN LÝ LƯƠNG CHỨC DANH";
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.left_round_32px; // Cần cập nhật tài nguyên
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(32, 32);
            this.btnBack.TabIndex = 67;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.lblMaChucDanh);
            this.grpThongTin.Controls.Add(this.txtMaChucDanh);
            this.grpThongTin.Controls.Add(this.lblPhuCap);
            this.grpThongTin.Controls.Add(this.txtPhuCap);
            this.grpThongTin.Controls.Add(this.txtLuongCung);
            this.grpThongTin.Controls.Add(this.lblLuongCung);
            this.grpThongTin.Controls.Add(this.txtChucDanh);
            this.grpThongTin.Controls.Add(this.lblChucDanh);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpThongTin.Location = new System.Drawing.Point(42, 59);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(802, 150); // Tăng chiều cao để chứa các điều khiển
            this.grpThongTin.TabIndex = 68;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin lương chức danh";
            // 
            // lblMaChucDanh
            // 
            this.lblMaChucDanh.AutoSize = true;
            this.lblMaChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaChucDanh.Location = new System.Drawing.Point(20, 30); // Điều chỉnh vị trí
            this.lblMaChucDanh.Name = "lblMaChucDanh";
            this.lblMaChucDanh.Size = new System.Drawing.Size(97, 20);
            this.lblMaChucDanh.TabIndex = 6;
            this.lblMaChucDanh.Text = "Mã chức danh";
            // 
            // txtMaChucDanh
            // 
            this.txtMaChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaChucDanh.Location = new System.Drawing.Point(130, 25); // Điều chỉnh vị trí
            this.txtMaChucDanh.Name = "txtMaChucDanh";
            this.txtMaChucDanh.ReadOnly = true;
            this.txtMaChucDanh.Size = new System.Drawing.Size(150, 27);
            this.txtMaChucDanh.TabIndex = 7;
            // 
            // lblChucDanh
            // 
            this.lblChucDanh.AutoSize = true;
            this.lblChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChucDanh.Location = new System.Drawing.Point(20, 70); // Điều chỉnh vị trí
            this.lblChucDanh.Name = "lblChucDanh";
            this.lblChucDanh.Size = new System.Drawing.Size(79, 20);
            this.lblChucDanh.TabIndex = 0;
            this.lblChucDanh.Text = "Chức danh";
            // 
            // txtChucDanh
            // 
            this.txtChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChucDanh.Location = new System.Drawing.Point(130, 65); // Điều chỉnh vị trí
            this.txtChucDanh.Name = "txtChucDanh";
            this.txtChucDanh.Size = new System.Drawing.Size(300, 27);
            this.txtChucDanh.TabIndex = 1;
            this.txtChucDanh.Validating += new System.ComponentModel.CancelEventHandler(this.txtChucDanh_Validating);
            // 
            // lblLuongCung
            // 
            this.lblLuongCung.AutoSize = true;
            this.lblLuongCung.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLuongCung.Location = new System.Drawing.Point(450, 70); // Điều chỉnh vị trí
            this.lblLuongCung.Name = "lblLuongCung";
            this.lblLuongCung.Size = new System.Drawing.Size(88, 20);
            this.lblLuongCung.TabIndex = 2;
            this.lblLuongCung.Text = "Lương cứng";
            // 
            // txtLuongCung
            // 
            this.txtLuongCung.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLuongCung.Location = new System.Drawing.Point(550, 65); // Điều chỉnh vị trí
            this.txtLuongCung.Name = "txtLuongCung";
            this.txtLuongCung.Size = new System.Drawing.Size(200, 27);
            this.txtLuongCung.TabIndex = 3;
            this.txtLuongCung.Validating += new System.ComponentModel.CancelEventHandler(this.txtLuongCung_Validating);
            // 
            // lblPhuCap
            // 
            this.lblPhuCap.AutoSize = true;
            this.lblPhuCap.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhuCap.Location = new System.Drawing.Point(20, 110); // Điều chỉnh vị trí
            this.lblPhuCap.Name = "lblPhuCap";
            this.lblPhuCap.Size = new System.Drawing.Size(61, 20);
            this.lblPhuCap.TabIndex = 5;
            this.lblPhuCap.Text = "Phụ cấp";
            // 
            // txtPhuCap
            // 
            this.txtPhuCap.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhuCap.Location = new System.Drawing.Point(130, 105); // Điều chỉnh vị trí
            this.txtPhuCap.Name = "txtPhuCap";
            this.txtPhuCap.Size = new System.Drawing.Size(200, 27);
            this.txtPhuCap.TabIndex = 4;
            this.txtPhuCap.Validating += new System.ComponentModel.CancelEventHandler(this.txtPhuCap_Validating);
            // 
            // btnHuyBo
            // 
            this.btnHuyBo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnHuyBo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnHuyBo.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuyBo.ForeColor = System.Drawing.Color.White;
            this.btnHuyBo.Location = new System.Drawing.Point(648, 215);
            this.btnHuyBo.Name = "btnHuyBo";
            this.btnHuyBo.Size = new System.Drawing.Size(125, 44);
            this.btnHuyBo.TabIndex = 77;
            this.btnHuyBo.Text = "HỦY BỎ";
            this.btnHuyBo.UseVisualStyleBackColor = false;
            this.btnHuyBo.Click += new System.EventHandler(this.btnHuyBo_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnCapNhat.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(517, 215);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(125, 44);
            this.btnCapNhat.TabIndex = 76;
            this.btnCapNhat.Text = "CẬP NHẬT";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(386, 215);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(125, 44);
            this.btnXoa.TabIndex = 75;
            this.btnXoa.Text = "XÓA";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(42)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(254, 215);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(126, 44);
            this.btnSua.TabIndex = 74;
            this.btnSua.Text = "SỬA";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(123, 215);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(125, 44);
            this.btnThem.TabIndex = 73;
            this.btnThem.Text = "THÊM";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // grpDanhSach
            // 
            this.grpDanhSach.Controls.Add(this.dtList);
            this.grpDanhSach.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpDanhSach.Location = new System.Drawing.Point(23, 265); // Điều chỉnh vị trí do grpThongTin cao hơn
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Size = new System.Drawing.Size(850, 337); // Điều chỉnh kích thước
            this.grpDanhSach.TabIndex = 78;
            this.grpDanhSach.TabStop = false;
            this.grpDanhSach.Text = "Danh sách chức danh";
            // 
            // dtList
            // 
            this.dtList.AllowUserToAddRows = false;
            this.dtList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dtList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtList.BackgroundColor = System.Drawing.Color.White;
            this.dtList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dtList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtList.ColumnHeadersHeight = 50;
            this.dtList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaChucDanh,
            this.ChucDanhID,
            this.TenChucDanh,
            this.TienLuong,
            this.PhuCap});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtList.DefaultCellStyle = dataGridViewCellStyle3;
            //this.dtList.DoubleBuffered = true;
            this.dtList.EnableHeadersVisualStyles = false;
            this.dtList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.dtList.Location = new System.Drawing.Point(6, 28);
            this.dtList.MultiSelect = false;
            this.dtList.Name = "dtList";
            this.dtList.ReadOnly = true;
            this.dtList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtList.RowHeadersVisible = false;
            this.dtList.RowHeadersWidth = 50;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dtList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtList.Size = new System.Drawing.Size(838, 303); // Điều chỉnh kích thước
            this.dtList.TabIndex = 1;
            this.dtList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtList_CellClick);
            this.dtList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dtList_CellFormatting);
            // 
            // MaChucDanh
            // 
            this.MaChucDanh.DataPropertyName = "MaChucDanh";
            this.MaChucDanh.HeaderText = "Mã chức danh";
            this.MaChucDanh.Name = "MaChucDanh";
            this.MaChucDanh.ReadOnly = true;
            // 
            // ChucDanhID
            // 
            this.ChucDanhID.DataPropertyName = "ChucDanhID";
            this.ChucDanhID.HeaderText = "ChucDanhID";
            this.ChucDanhID.Name = "ChucDanhID";
            this.ChucDanhID.ReadOnly = true;
            this.ChucDanhID.Visible = false;
            // 
            // TenChucDanh
            // 
            this.TenChucDanh.DataPropertyName = "TenChucDanh";
            this.TenChucDanh.HeaderText = "Tên chức danh";
            this.TenChucDanh.Name = "TenChucDanh";
            this.TenChucDanh.ReadOnly = true;
            // 
            // TienLuong
            // 
            this.TienLuong.DataPropertyName = "LuongCoBan";
            this.TienLuong.HeaderText = "Lương cứng";
            this.TienLuong.Name = "TienLuong";
            this.TienLuong.ReadOnly = true;
            // 
            // PhuCap
            // 
            this.PhuCap.DataPropertyName = "PhuCap";
            this.PhuCap.HeaderText = "Phụ cấp";
            this.PhuCap.Name = "PhuCap";
            this.PhuCap.ReadOnly = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // UC_SalaryManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grpDanhSach);
            this.Controls.Add(this.btnHuyBo);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnBack);
            this.Name = "UC_SalaryManager";
            this.Size = new System.Drawing.Size(893, 619);
            this.Load += new System.EventHandler(this.UC_SalaryManager_Load);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.grpDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblMaChucDanh;
        private System.Windows.Forms.TextBox txtMaChucDanh;
        private System.Windows.Forms.Label lblChucDanh;
        private System.Windows.Forms.TextBox txtChucDanh;
        private System.Windows.Forms.Label lblPhuCap;
        private System.Windows.Forms.TextBox txtPhuCap;
        private System.Windows.Forms.TextBox txtLuongCung;
        private System.Windows.Forms.Label lblLuongCung;
        private System.Windows.Forms.Button btnHuyBo;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.DataGridView dtList;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaChucDanh;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChucDanhID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenChucDanh;
        private System.Windows.Forms.DataGridViewTextBoxColumn TienLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhuCap;
    }
}