namespace QLCHBanGaRan.Forms
{
    partial class frm_SalaryManager
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblMaChucDanh = new System.Windows.Forms.Label();
            this.txtMaChucDanh = new System.Windows.Forms.TextBox();
            this.lblThuongChucDanh = new System.Windows.Forms.Label();
            this.txtThuongChucDanh = new System.Windows.Forms.TextBox();
            this.txtLuongTheoGio = new System.Windows.Forms.TextBox();
            this.lblLuongTheoGio = new System.Windows.Forms.Label();
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
            this.LuongTheoGio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThuongChucDanh = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.lblTitle.Location = new System.Drawing.Point(17, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(241, 21);
            this.lblTitle.TabIndex = 66;
            this.lblTitle.Text = "QUẢN LÝ LƯƠNG CHỨC DANH";
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.lblMaChucDanh);
            this.grpThongTin.Controls.Add(this.txtMaChucDanh);
            this.grpThongTin.Controls.Add(this.lblThuongChucDanh);
            this.grpThongTin.Controls.Add(this.txtThuongChucDanh);
            this.grpThongTin.Controls.Add(this.txtLuongTheoGio);
            this.grpThongTin.Controls.Add(this.lblLuongTheoGio);
            this.grpThongTin.Controls.Add(this.txtChucDanh);
            this.grpThongTin.Controls.Add(this.lblChucDanh);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpThongTin.Location = new System.Drawing.Point(42, 59);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(802, 150);
            this.grpThongTin.TabIndex = 68;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin lương chức danh";
            // 
            // lblMaChucDanh
            // 
            this.lblMaChucDanh.AutoSize = true;
            this.lblMaChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaChucDanh.Location = new System.Drawing.Point(20, 30);
            this.lblMaChucDanh.Name = "lblMaChucDanh";
            this.lblMaChucDanh.Size = new System.Drawing.Size(102, 20);
            this.lblMaChucDanh.TabIndex = 0;
            this.lblMaChucDanh.Text = "Mã chức danh";
            // 
            // txtMaChucDanh
            // 
            this.txtMaChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaChucDanh.Location = new System.Drawing.Point(130, 25);
            this.txtMaChucDanh.Name = "txtMaChucDanh";
            this.txtMaChucDanh.ReadOnly = true;
            this.txtMaChucDanh.Size = new System.Drawing.Size(150, 27);
            this.txtMaChucDanh.TabIndex = 1;
            // 
            // lblThuongChucDanh
            // 
            this.lblThuongChucDanh.AutoSize = true;
            this.lblThuongChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThuongChucDanh.Location = new System.Drawing.Point(20, 110);
            this.lblThuongChucDanh.Name = "lblThuongChucDanh";
            this.lblThuongChucDanh.Size = new System.Drawing.Size(60, 20);
            this.lblThuongChucDanh.TabIndex = 2;
            this.lblThuongChucDanh.Text = "Thưởng";
            // 
            // txtThuongChucDanh
            // 
            this.txtThuongChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThuongChucDanh.Location = new System.Drawing.Point(130, 107);
            this.txtThuongChucDanh.Name = "txtThuongChucDanh";
            this.txtThuongChucDanh.Size = new System.Drawing.Size(259, 27);
            this.txtThuongChucDanh.TabIndex = 3;
            this.txtThuongChucDanh.Validating += new System.ComponentModel.CancelEventHandler(this.txtThuongChucDanh_Validating);
            // 
            // txtLuongTheoGio
            // 
            this.txtLuongTheoGio.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLuongTheoGio.Location = new System.Drawing.Point(530, 107);
            this.txtLuongTheoGio.Name = "txtLuongTheoGio";
            this.txtLuongTheoGio.Size = new System.Drawing.Size(259, 27);
            this.txtLuongTheoGio.TabIndex = 5;
            this.txtLuongTheoGio.Validating += new System.ComponentModel.CancelEventHandler(this.txtLuongTheoGio_Validating);
            // 
            // lblLuongTheoGio
            // 
            this.lblLuongTheoGio.AutoSize = true;
            this.lblLuongTheoGio.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLuongTheoGio.Location = new System.Drawing.Point(413, 110);
            this.lblLuongTheoGio.Name = "lblLuongTheoGio";
            this.lblLuongTheoGio.Size = new System.Drawing.Size(111, 20);
            this.lblLuongTheoGio.TabIndex = 4;
            this.lblLuongTheoGio.Text = "Lương theo giờ";
            // 
            // txtChucDanh
            // 
            this.txtChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChucDanh.Location = new System.Drawing.Point(130, 65);
            this.txtChucDanh.Name = "txtChucDanh";
            this.txtChucDanh.Size = new System.Drawing.Size(259, 27);
            this.txtChucDanh.TabIndex = 1;
            this.txtChucDanh.Validating += new System.ComponentModel.CancelEventHandler(this.txtChucDanh_Validating);
            // 
            // lblChucDanh
            // 
            this.lblChucDanh.AutoSize = true;
            this.lblChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChucDanh.Location = new System.Drawing.Point(20, 70);
            this.lblChucDanh.Name = "lblChucDanh";
            this.lblChucDanh.Size = new System.Drawing.Size(79, 20);
            this.lblChucDanh.TabIndex = 0;
            this.lblChucDanh.Text = "Chức danh";
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
            this.btnHuyBo.TabIndex = 10;
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
            this.btnCapNhat.TabIndex = 9;
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
            this.btnXoa.TabIndex = 8;
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
            this.btnSua.TabIndex = 7;
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
            this.btnThem.TabIndex = 6;
            this.btnThem.Text = "THÊM";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // grpDanhSach
            // 
            this.grpDanhSach.Controls.Add(this.dtList);
            this.grpDanhSach.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpDanhSach.Location = new System.Drawing.Point(23, 265);
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Size = new System.Drawing.Size(850, 337);
            this.grpDanhSach.TabIndex = 78;
            this.grpDanhSach.TabStop = false;
            this.grpDanhSach.Text = "Danh sách chức danh";
            // 
            // dtList
            // 
            this.dtList.AllowUserToAddRows = false;
            this.dtList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            this.dtList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dtList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtList.BackgroundColor = System.Drawing.Color.White;
            this.dtList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dtList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dtList.ColumnHeadersHeight = 50;
            this.dtList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaChucDanh,
            this.ChucDanhID,
            this.TenChucDanh,
            this.LuongTheoGio,
            this.ThuongChucDanh});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dtList.EnableHeadersVisualStyles = false;
            this.dtList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.dtList.Location = new System.Drawing.Point(6, 28);
            this.dtList.MultiSelect = false;
            this.dtList.Name = "dtList";
            this.dtList.ReadOnly = true;
            this.dtList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dtList.RowHeadersVisible = false;
            this.dtList.RowHeadersWidth = 50;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtList.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dtList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtList.Size = new System.Drawing.Size(838, 303);
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
            // LuongTheoGio
            // 
            this.LuongTheoGio.DataPropertyName = "LuongTheoGio";
            this.LuongTheoGio.HeaderText = "Lương theo giờ";
            this.LuongTheoGio.Name = "LuongTheoGio";
            this.LuongTheoGio.ReadOnly = true;
            // 
            // ThuongChucDanh
            // 
            this.ThuongChucDanh.DataPropertyName = "ThuongChucDanh";
            this.ThuongChucDanh.HeaderText = "Thưởng";
            this.ThuongChucDanh.Name = "ThuongChucDanh";
            this.ThuongChucDanh.ReadOnly = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // frm_SalaryManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(877, 580);
            this.Controls.Add(this.grpDanhSach);
            this.Controls.Add(this.btnHuyBo);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.lblTitle);
            this.Name = "frm_SalaryManager";
            this.Load += new System.EventHandler(this.frm_SalaryManager_Load);
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
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblMaChucDanh;
        private System.Windows.Forms.TextBox txtMaChucDanh;
        private System.Windows.Forms.Label lblChucDanh;
        private System.Windows.Forms.TextBox txtChucDanh;
        private System.Windows.Forms.Label lblThuongChucDanh; // Thay lblPhuCap
        private System.Windows.Forms.TextBox txtThuongChucDanh; // Thay txtPhuCap
        private System.Windows.Forms.TextBox txtLuongTheoGio; // Thay txtLuongCung
        private System.Windows.Forms.Label lblLuongTheoGio; // Thay lblLuongCung
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
        private System.Windows.Forms.DataGridViewTextBoxColumn LuongTheoGio; // Thay TienLuong
        private System.Windows.Forms.DataGridViewTextBoxColumn ThuongChucDanh; // Thay PhuCap
    }
}