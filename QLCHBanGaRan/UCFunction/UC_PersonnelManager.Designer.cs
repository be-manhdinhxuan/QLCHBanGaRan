namespace QLCHBanGaRan.UCFunction
{
    partial class UC_PersonnelManager
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
            this.grpTimKiem = new System.Windows.Forms.GroupBox();
            this.lblTimNhanVien = new System.Windows.Forms.Label();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.cbDiLam = new System.Windows.Forms.CheckBox();
            this.cmbGioiTinh = new System.Windows.Forms.ComboBox();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtCMND = new System.Windows.Forms.TextBox();
            this.lblCMND = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.lblTenNV = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.lblChucDanh = new System.Windows.Forms.Label();
            this.cmbChucDanh = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnHuyBo = new System.Windows.Forms.Button();
            this.dtListEmployess = new System.Windows.Forms.DataGridView();
            this.NhanVienID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgaySinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiaChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GioiTinhID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrangThaiID = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpTimKiem.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtListEmployess)).BeginInit();
            this.grpDanhSach.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(174, 21);
            this.lblTitle.TabIndex = 64;
            this.lblTitle.Text = "QUẢN LÝ NHÂN VIÊN";
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Image = global::QLCHBanGaRan.Properties.Resources.left_round_32px;
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(32, 32);
            this.btnBack.TabIndex = 65;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // grpTimKiem
            // 
            this.grpTimKiem.Controls.Add(this.lblTimNhanVien);
            this.grpTimKiem.Controls.Add(this.cmbFilter);
            this.grpTimKiem.Controls.Add(this.txtTimKiem);
            this.grpTimKiem.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpTimKiem.Location = new System.Drawing.Point(27, 33);
            this.grpTimKiem.Name = "grpTimKiem";
            this.grpTimKiem.Size = new System.Drawing.Size(850, 70);
            this.grpTimKiem.TabIndex = 66;
            this.grpTimKiem.TabStop = false;
            this.grpTimKiem.Text = "Tìm kiếm";
            // 
            // lblTimNhanVien
            // 
            this.lblTimNhanVien.AutoSize = true;
            this.lblTimNhanVien.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimNhanVien.Location = new System.Drawing.Point(155, 31);
            this.lblTimNhanVien.Name = "lblTimNhanVien";
            this.lblTimNhanVien.Size = new System.Drawing.Size(137, 20);
            this.lblTimNhanVien.TabIndex = 2;
            this.lblTimNhanVien.Text = "Tìm kiếm nhân viên";
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Location = new System.Drawing.Point(516, 28);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(111, 28);
            this.cmbFilter.TabIndex = 1;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(298, 28);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(212, 27);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimKiem_KeyPress);
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.cbDiLam);
            this.grpThongTin.Controls.Add(this.cmbGioiTinh);
            this.grpThongTin.Controls.Add(this.lblGioiTinh);
            this.grpThongTin.Controls.Add(this.txtSDT);
            this.grpThongTin.Controls.Add(this.lblSDT);
            this.grpThongTin.Controls.Add(this.txtEmail);
            this.grpThongTin.Controls.Add(this.lblEmail);
            this.grpThongTin.Controls.Add(this.txtDiaChi);
            this.grpThongTin.Controls.Add(this.lblDiaChi);
            this.grpThongTin.Controls.Add(this.txtCMND);
            this.grpThongTin.Controls.Add(this.lblCMND);
            this.grpThongTin.Controls.Add(this.dtpNgaySinh);
            this.grpThongTin.Controls.Add(this.lblNgaySinh);
            this.grpThongTin.Controls.Add(this.txtTenNV);
            this.grpThongTin.Controls.Add(this.lblTenNV);
            this.grpThongTin.Controls.Add(this.txtMaNV);
            this.grpThongTin.Controls.Add(this.lblMaNV);
            this.grpThongTin.Controls.Add(this.lblChucDanh);
            this.grpThongTin.Controls.Add(this.cmbChucDanh);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpThongTin.Location = new System.Drawing.Point(27, 109);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(850, 175);
            this.grpThongTin.TabIndex = 67;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin";
            // 
            // cbDiLam
            // 
            this.cbDiLam.AutoSize = true;
            this.cbDiLam.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDiLam.Location = new System.Drawing.Point(742, 97);
            this.cbDiLam.Name = "cbDiLam";
            this.cbDiLam.Size = new System.Drawing.Size(72, 24);
            this.cbDiLam.TabIndex = 16;
            this.cbDiLam.Text = "Đi làm";
            this.cbDiLam.UseVisualStyleBackColor = true;
            // 
            // cmbGioiTinh
            // 
            this.cmbGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGioiTinh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGioiTinh.FormattingEnabled = true;
            this.cmbGioiTinh.Location = new System.Drawing.Point(628, 93);
            this.cmbGioiTinh.Name = "cmbGioiTinh";
            this.cmbGioiTinh.Size = new System.Drawing.Size(99, 28);
            this.cmbGioiTinh.TabIndex = 15;
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGioiTinh.Location = new System.Drawing.Point(547, 96);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(65, 20);
            this.lblGioiTinh.TabIndex = 14;
            this.lblGioiTinh.Text = "Giới tính";
            // 
            // txtSDT
            // 
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSDT.Location = new System.Drawing.Point(414, 94);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(123, 27);
            this.txtSDT.TabIndex = 13;
            this.txtSDT.Validating += new System.ComponentModel.CancelEventHandler(this.txtSDT_Validating);
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDT.Location = new System.Drawing.Point(311, 98);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(97, 20);
            this.lblSDT.TabIndex = 12;
            this.lblSDT.Text = "Số điện thoại";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(109, 93);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 27);
            this.txtEmail.TabIndex = 11;
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(6, 95);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(46, 20);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "Email";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiaChi.Location = new System.Drawing.Point(315, 59);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(498, 27);
            this.txtDiaChi.TabIndex = 9;
            this.txtDiaChi.Validating += new System.ComponentModel.CancelEventHandler(this.txtDiaChi_Validating);
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiaChi.Location = new System.Drawing.Point(210, 62);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(55, 20);
            this.lblDiaChi.TabIndex = 8;
            this.lblDiaChi.Text = "Địa chỉ";
            // 
            // txtCMND
            // 
            this.txtCMND.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCMND.Location = new System.Drawing.Point(109, 59);
            this.txtCMND.Name = "txtCMND";
            this.txtCMND.Size = new System.Drawing.Size(95, 27);
            this.txtCMND.TabIndex = 7;
            this.txtCMND.Validating += new System.ComponentModel.CancelEventHandler(this.txtCMND_Validating);
            // 
            // lblCMND
            // 
            this.lblCMND.AutoSize = true;
            this.lblCMND.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCMND.Location = new System.Drawing.Point(6, 62);
            this.lblCMND.Name = "lblCMND";
            this.lblCMND.Size = new System.Drawing.Size(74, 20);
            this.lblCMND.TabIndex = 6;
            this.lblCMND.Text = "Số CMND";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(628, 26);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(185, 27);
            this.dtpNgaySinh.TabIndex = 5;
            this.dtpNgaySinh.Value = new System.DateTime(2019, 11, 30, 5, 17, 33, 819);
            this.dtpNgaySinh.Validating += new System.ComponentModel.CancelEventHandler(this.dtpNgaySinh_Validating);
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgaySinh.Location = new System.Drawing.Point(547, 29);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(74, 20);
            this.lblNgaySinh.TabIndex = 4;
            this.lblNgaySinh.Text = "Ngày sinh";
            // 
            // txtTenNV
            // 
            this.txtTenNV.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenNV.Location = new System.Drawing.Point(315, 26);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.Size = new System.Drawing.Size(226, 27);
            this.txtTenNV.TabIndex = 3;
            this.txtTenNV.Validating += new System.ComponentModel.CancelEventHandler(this.txtTenNV_Validating);
            // 
            // lblTenNV
            // 
            this.lblTenNV.AutoSize = true;
            this.lblTenNV.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenNV.Location = new System.Drawing.Point(210, 29);
            this.lblTenNV.Name = "lblTenNV";
            this.lblTenNV.Size = new System.Drawing.Size(99, 20);
            this.lblTenNV.TabIndex = 2;
            this.lblTenNV.Text = "Tên nhân viên";
            // 
            // txtMaNV
            // 
            this.txtMaNV.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaNV.Location = new System.Drawing.Point(109, 26);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(95, 27);
            this.txtMaNV.TabIndex = 1;
            // 
            // lblMaNV
            // 
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaNV.Location = new System.Drawing.Point(6, 29);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Size = new System.Drawing.Size(97, 20);
            this.lblMaNV.TabIndex = 0;
            this.lblMaNV.Text = "Mã nhân viên";
            // 
            // lblChucDanh
            // 
            this.lblChucDanh.AutoSize = true;
            this.lblChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChucDanh.Location = new System.Drawing.Point(6, 128);
            this.lblChucDanh.Name = "lblChucDanh";
            this.lblChucDanh.Size = new System.Drawing.Size(79, 20);
            this.lblChucDanh.TabIndex = 17;
            this.lblChucDanh.Text = "Chức danh";
            // 
            // cmbChucDanh
            // 
            this.cmbChucDanh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChucDanh.FormattingEnabled = true;
            this.cmbChucDanh.Location = new System.Drawing.Point(109, 125);
            this.cmbChucDanh.Name = "cmbChucDanh";
            this.cmbChucDanh.Size = new System.Drawing.Size(200, 28);
            this.cmbChucDanh.TabIndex = 18;
            // 

            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(130, 266);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(125, 44);
            this.btnThem.TabIndex = 68;
            this.btnThem.Text = "THÊM";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(261, 266);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(126, 44);
            this.btnSua.TabIndex = 69;
            this.btnSua.Text = "SỬA";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(393, 266);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(125, 44);
            this.btnXoa.TabIndex = 70;
            this.btnXoa.Text = "XÓA";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(524, 266);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(125, 44);
            this.btnCapNhat.TabIndex = 71;
            this.btnCapNhat.Text = "CẬP NHẬT";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnHuyBo
            // 
            this.btnHuyBo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnHuyBo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyBo.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnHuyBo.ForeColor = System.Drawing.Color.White;
            this.btnHuyBo.Location = new System.Drawing.Point(655, 266);
            this.btnHuyBo.Name = "btnHuyBo";
            this.btnHuyBo.Size = new System.Drawing.Size(125, 44);
            this.btnHuyBo.TabIndex = 72;
            this.btnHuyBo.Text = "HỦY BỎ";
            this.btnHuyBo.UseVisualStyleBackColor = false;
            this.btnHuyBo.Click += new System.EventHandler(this.btnHuyBo_Click);
            // 
            // dtListEmployess
            // 
            this.dtListEmployess.AllowUserToAddRows = false;
            this.dtListEmployess.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dtListEmployess.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtListEmployess.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtListEmployess.BackgroundColor = System.Drawing.Color.White;
            this.dtListEmployess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtListEmployess.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtListEmployess.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtListEmployess.ColumnHeadersHeight = 50;
            this.dtListEmployess.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NhanVienID,
            this.MaNV,
            this.TenNV,
            this.NgaySinh,
            this.CMND,
            this.DiaChi,
            this.Email,
            this.SDT,
            this.GioiTinh,
            this.GioiTinhID,
            this.TrangThaiID});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtListEmployess.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtListEmployess.EnableHeadersVisualStyles = false;
            this.dtListEmployess.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.dtListEmployess.Location = new System.Drawing.Point(6, 28);
            this.dtListEmployess.MultiSelect = false;
            this.dtListEmployess.Name = "dtListEmployess";
            this.dtListEmployess.ReadOnly = true;
            this.dtListEmployess.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtListEmployess.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtListEmployess.RowHeadersVisible = false;
            this.dtListEmployess.RowHeadersWidth = 50;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtListEmployess.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dtListEmployess.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtListEmployess.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtListEmployess.Size = new System.Drawing.Size(838, 272);
            this.dtListEmployess.TabIndex = 1;
            this.dtListEmployess.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtListEmployess_CellClick);
            // 
            // NhanVienID
            // 
            this.NhanVienID.DataPropertyName = "MaNV";
            this.NhanVienID.HeaderText = "MaNV";
            this.NhanVienID.Name = "NhanVienID";
            this.NhanVienID.ReadOnly = true;
            this.NhanVienID.Visible = false;
            // 
            // MaNV
            // 
            this.MaNV.DataPropertyName = "MaNV";
            this.MaNV.HeaderText = "Mã NV";
            this.MaNV.Name = "MaNV";
            this.MaNV.ReadOnly = true;
            // 
            // TenNV
            // 
            this.TenNV.DataPropertyName = "TenNV";
            this.TenNV.HeaderText = "Tên NV";
            this.TenNV.Name = "TenNV";
            this.TenNV.ReadOnly = true;
            // 
            // NgaySinh
            // 
            this.NgaySinh.DataPropertyName = "NgaySinh";
            this.NgaySinh.HeaderText = "Ngày sinh";
            this.NgaySinh.Name = "NgaySinh";
            this.NgaySinh.ReadOnly = true;
            // 
            // CMND
            // 
            this.CMND.DataPropertyName = "CMND";
            this.CMND.HeaderText = "Số CMND";
            this.CMND.Name = "CMND";
            this.CMND.ReadOnly = true;
            // 
            // DiaChi
            // 
            this.DiaChi.DataPropertyName = "DiaChi";
            this.DiaChi.HeaderText = "Địa chỉ";
            this.DiaChi.Name = "DiaChi";
            this.DiaChi.ReadOnly = true;
            // 
            // Email
            // 
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // SDT
            // 
            this.SDT.DataPropertyName = "SDT";
            this.SDT.HeaderText = "SDT";
            this.SDT.Name = "SDT";
            this.SDT.ReadOnly = true;
            // 
            // GioiTinh
            // 
            this.GioiTinh.DataPropertyName = "GioiTinh";
            this.GioiTinh.HeaderText = "Giới tính";
            this.GioiTinh.Name = "GioiTinh";
            this.GioiTinh.ReadOnly = true;
            // 
            this.MaChucDanh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaChucDanh.DataPropertyName = "TenChucDanh";
            this.MaChucDanh.HeaderText = "Chức danh";
            this.MaChucDanh.Name = "MaChucDanh";
            this.MaChucDanh.ReadOnly = true;
            this.dtListEmployess.Columns.Add(this.MaChucDanh);
            // GioiTinhID
            // 
            this.GioiTinhID.DataPropertyName = "GioiTinhID";
            this.GioiTinhID.HeaderText = "GioiTinhID";
            this.GioiTinhID.Name = "GioiTinhID";
            this.GioiTinhID.ReadOnly = true;
            this.GioiTinhID.Visible = false;
            // 

            // TrangThaiID
            // 
            this.TrangThaiID.DataPropertyName = "TrangThai";
            this.TrangThaiID.HeaderText = "Đi làm";
            this.TrangThaiID.Name = "TrangThaiID";
            this.TrangThaiID.ReadOnly = true;
            // 
            // grpDanhSach
            // 
            this.grpDanhSach.Controls.Add(this.dtListEmployess);
            this.grpDanhSach.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpDanhSach.Location = new System.Drawing.Point(27, 310);
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Size = new System.Drawing.Size(850, 306);
            this.grpDanhSach.TabIndex = 73;
            this.grpDanhSach.TabStop = false;
            this.grpDanhSach.Text = "Danh sách nhân viên";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // UC_PersonnelManager
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
            this.Controls.Add(this.grpTimKiem);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnBack);
            this.Name = "UC_PersonnelManager";
            this.Size = new System.Drawing.Size(893, 619);
            this.Load += new System.EventHandler(this.UC_PersonnelManager_Load);
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtListEmployess)).EndInit();
            this.grpDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.GroupBox grpTimKiem;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.Label lblTimNhanVien;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.Label lblTenNV;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.TextBox txtCMND;
        private System.Windows.Forms.Label lblCMND;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.ComboBox cmbGioiTinh;
        private System.Windows.Forms.CheckBox cbDiLam;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnHuyBo;
        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.DataGridView dtListEmployess;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn NhanVienID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgaySinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMND;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiaChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn GioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn GioiTinhID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TrangThaiID;
        private System.Windows.Forms.ComboBox cmbChucDanh;
        private System.Windows.Forms.Label lblChucDanh; // Nhãn cho ComboBox
        private System.Windows.Forms.DataGridViewTextBoxColumn MaChucDanh;
    }
}