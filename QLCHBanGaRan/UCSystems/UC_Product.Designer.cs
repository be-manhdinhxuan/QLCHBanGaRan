namespace QLCHBanGaRan.UCSystems
{
    partial class UC_Product
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
            this.grpQuanLyMonAn = new System.Windows.Forms.GroupBox();
            this.btnQuanLyMonAn = new System.Windows.Forms.Button();
            this.grpQuanLyNuocUong = new System.Windows.Forms.GroupBox();
            this.btnQuanLyNuocUong = new System.Windows.Forms.Button();
            this.grpQuanLySanPham = new System.Windows.Forms.GroupBox();
            this.grpQuanLyLoaiSP = new System.Windows.Forms.GroupBox();
            this.btnQuanLySP = new System.Windows.Forms.Button();
            this.grpQuanLyNCC = new System.Windows.Forms.GroupBox();
            this.btnQuanLyNCC = new System.Windows.Forms.Button();
            this.grpQuanLyDanhMuc = new System.Windows.Forms.GroupBox();
            this.grpQuanLyMonAn.SuspendLayout();
            this.grpQuanLyNuocUong.SuspendLayout();
            this.grpQuanLySanPham.SuspendLayout();
            this.grpQuanLyLoaiSP.SuspendLayout();
            this.grpQuanLyNCC.SuspendLayout();
            this.grpQuanLyDanhMuc.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpQuanLyMonAn
            // 
            this.grpQuanLyMonAn.Controls.Add(this.btnQuanLyMonAn);
            this.grpQuanLyMonAn.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLyMonAn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLyMonAn.Location = new System.Drawing.Point(43, 28);
            this.grpQuanLyMonAn.Name = "grpQuanLyMonAn";
            this.grpQuanLyMonAn.Size = new System.Drawing.Size(207, 200);
            this.grpQuanLyMonAn.TabIndex = 0;
            this.grpQuanLyMonAn.TabStop = false;
            this.grpQuanLyMonAn.Text = "Quản lý món ăn";
            // 
            // btnQuanLyMonAn
            // 
            this.btnQuanLyMonAn.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.hamburger_100px; // Cần cập nhật tài nguyên
            this.btnQuanLyMonAn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnQuanLyMonAn.Location = new System.Drawing.Point(35, 28);
            this.btnQuanLyMonAn.Name = "btnQuanLyMonAn";
            this.btnQuanLyMonAn.Size = new System.Drawing.Size(145, 145);
            this.btnQuanLyMonAn.TabIndex = 0;
            this.btnQuanLyMonAn.UseVisualStyleBackColor = true;
            this.btnQuanLyMonAn.Click += new System.EventHandler(this.btnQuanLyMonAn_Click);
            // 
            // grpQuanLyNuocUong
            // 
            this.grpQuanLyNuocUong.Controls.Add(this.btnQuanLyNuocUong);
            this.grpQuanLyNuocUong.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLyNuocUong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLyNuocUong.Location = new System.Drawing.Point(283, 28);
            this.grpQuanLyNuocUong.Name = "grpQuanLyNuocUong";
            this.grpQuanLyNuocUong.Size = new System.Drawing.Size(207, 200);
            this.grpQuanLyNuocUong.TabIndex = 1;
            this.grpQuanLyNuocUong.TabStop = false;
            this.grpQuanLyNuocUong.Text = "Quản lý nước uống";
            // 
            // btnQuanLyNuocUong
            // 
            this.btnQuanLyNuocUong.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.cafe_100px; // Cần cập nhật tài nguyên
            this.btnQuanLyNuocUong.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnQuanLyNuocUong.Location = new System.Drawing.Point(35, 28);
            this.btnQuanLyNuocUong.Name = "btnQuanLyNuocUong";
            this.btnQuanLyNuocUong.Size = new System.Drawing.Size(145, 145);
            this.btnQuanLyNuocUong.TabIndex = 0;
            this.btnQuanLyNuocUong.UseVisualStyleBackColor = true;
            this.btnQuanLyNuocUong.Click += new System.EventHandler(this.btnQuanLyNuocUong_Click);
            // 
            // grpQuanLySanPham
            // 
            this.grpQuanLySanPham.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpQuanLySanPham.BackColor = System.Drawing.Color.Transparent;
            this.grpQuanLySanPham.Controls.Add(this.grpQuanLyNuocUong);
            this.grpQuanLySanPham.Controls.Add(this.grpQuanLyMonAn);
            this.grpQuanLySanPham.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLySanPham.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLySanPham.Location = new System.Drawing.Point(179, 32);
            this.grpQuanLySanPham.Name = "grpQuanLySanPham";
            this.grpQuanLySanPham.Size = new System.Drawing.Size(536, 264);
            this.grpQuanLySanPham.TabIndex = 3;
            this.grpQuanLySanPham.TabStop = false;
            this.grpQuanLySanPham.Text = "Quản lý sản phẩm";
            // 
            // grpQuanLyLoaiSP
            // 
            this.grpQuanLyLoaiSP.Controls.Add(this.btnQuanLySP);
            this.grpQuanLyLoaiSP.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLyLoaiSP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLyLoaiSP.Location = new System.Drawing.Point(43, 28);
            this.grpQuanLyLoaiSP.Name = "grpQuanLyLoaiSP";
            this.grpQuanLyLoaiSP.Size = new System.Drawing.Size(207, 200);
            this.grpQuanLyLoaiSP.TabIndex = 0;
            this.grpQuanLyLoaiSP.TabStop = false;
            this.grpQuanLyLoaiSP.Text = "Quản lý loại sản phẩm";
            // 
            // btnQuanLySP
            // 
            this.btnQuanLySP.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.ingredients_100px; // Cần cập nhật tài nguyên
            this.btnQuanLySP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnQuanLySP.Location = new System.Drawing.Point(35, 28);
            this.btnQuanLySP.Name = "btnQuanLySP";
            this.btnQuanLySP.Size = new System.Drawing.Size(145, 145);
            this.btnQuanLySP.TabIndex = 0;
            this.btnQuanLySP.UseVisualStyleBackColor = true;
            this.btnQuanLySP.Click += new System.EventHandler(this.btnQuanLySP_Click);
            // 
            // grpQuanLyNCC
            // 
            this.grpQuanLyNCC.Controls.Add(this.btnQuanLyNCC);
            this.grpQuanLyNCC.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLyNCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLyNCC.Location = new System.Drawing.Point(283, 28);
            this.grpQuanLyNCC.Name = "grpQuanLyNCC";
            this.grpQuanLyNCC.Size = new System.Drawing.Size(207, 200);
            this.grpQuanLyNCC.TabIndex = 1;
            this.grpQuanLyNCC.TabStop = false;
            this.grpQuanLyNCC.Text = "Quản lý nhà cung cấp";
            // 
            // btnQuanLyNCC
            // 
            this.btnQuanLyNCC.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.small_business_100px; // Cần cập nhật tài nguyên
            this.btnQuanLyNCC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnQuanLyNCC.Location = new System.Drawing.Point(35, 28);
            this.btnQuanLyNCC.Name = "btnQuanLyNCC";
            this.btnQuanLyNCC.Size = new System.Drawing.Size(145, 145);
            this.btnQuanLyNCC.TabIndex = 0;
            this.btnQuanLyNCC.UseVisualStyleBackColor = true;
            this.btnQuanLyNCC.Click += new System.EventHandler(this.btnQuanLyNCC_Click);
            // 
            // grpQuanLyDanhMuc
            // 
            this.grpQuanLyDanhMuc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpQuanLyDanhMuc.BackColor = System.Drawing.Color.Transparent;
            this.grpQuanLyDanhMuc.Controls.Add(this.grpQuanLyNCC);
            this.grpQuanLyDanhMuc.Controls.Add(this.grpQuanLyLoaiSP);
            this.grpQuanLyDanhMuc.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLyDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLyDanhMuc.Location = new System.Drawing.Point(179, 323);
            this.grpQuanLyDanhMuc.Name = "grpQuanLyDanhMuc";
            this.grpQuanLyDanhMuc.Size = new System.Drawing.Size(536, 264);
            this.grpQuanLyDanhMuc.TabIndex = 4;
            this.grpQuanLyDanhMuc.TabStop = false;
            this.grpQuanLyDanhMuc.Text = "Quản lý danh mục";
            // 
            // UC_Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grpQuanLySanPham);
            this.Controls.Add(this.grpQuanLyDanhMuc);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_Product";
            this.Size = new System.Drawing.Size(893, 619);
            this.grpQuanLyMonAn.ResumeLayout(false);
            this.grpQuanLyNuocUong.ResumeLayout(false);
            this.grpQuanLySanPham.ResumeLayout(false);
            this.grpQuanLyLoaiSP.ResumeLayout(false);
            this.grpQuanLyNCC.ResumeLayout(false);
            this.grpQuanLyDanhMuc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpQuanLyMonAn;
        private System.Windows.Forms.Button btnQuanLyMonAn;
        private System.Windows.Forms.GroupBox grpQuanLyNuocUong;
        private System.Windows.Forms.Button btnQuanLyNuocUong;
        private System.Windows.Forms.GroupBox grpQuanLySanPham;
        private System.Windows.Forms.GroupBox grpQuanLyLoaiSP;
        private System.Windows.Forms.Button btnQuanLySP;
        private System.Windows.Forms.GroupBox grpQuanLyNCC;
        private System.Windows.Forms.Button btnQuanLyNCC;
        private System.Windows.Forms.GroupBox grpQuanLyDanhMuc;
    }
}