namespace QLCHBanGaRan.UCSystems
{
    partial class UC_System
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
            this.grpPhanQuyen = new System.Windows.Forms.GroupBox();
            this.grpPhanQuyenNguoiDung = new System.Windows.Forms.GroupBox();
            this.grpQuanLyNguoiDung = new System.Windows.Forms.GroupBox();
            this.btnPhanQuyenNguoiDung = new System.Windows.Forms.Button();
            this.btnQuanLyNguoiDung = new System.Windows.Forms.Button();
            this.grpPhanQuyen.SuspendLayout();
            this.grpPhanQuyenNguoiDung.SuspendLayout();
            this.grpQuanLyNguoiDung.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPhanQuyen
            // 
            this.grpPhanQuyen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpPhanQuyen.BackColor = System.Drawing.Color.Transparent;
            this.grpPhanQuyen.Controls.Add(this.grpPhanQuyenNguoiDung);
            this.grpPhanQuyen.Controls.Add(this.grpQuanLyNguoiDung);
            this.grpPhanQuyen.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPhanQuyen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpPhanQuyen.Location = new System.Drawing.Point(182, 201);
            this.grpPhanQuyen.Name = "grpPhanQuyen";
            this.grpPhanQuyen.Size = new System.Drawing.Size(509, 193);
            this.grpPhanQuyen.TabIndex = 6;
            this.grpPhanQuyen.TabStop = false;
            this.grpPhanQuyen.Text = "Phân quyền";
            // 
            // grpPhanQuyenNguoiDung
            // 
            this.grpPhanQuyenNguoiDung.Controls.Add(this.btnPhanQuyenNguoiDung);
            this.grpPhanQuyenNguoiDung.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPhanQuyenNguoiDung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpPhanQuyenNguoiDung.Location = new System.Drawing.Point(276, 22);
            this.grpPhanQuyenNguoiDung.Name = "grpPhanQuyenNguoiDung";
            this.grpPhanQuyenNguoiDung.Size = new System.Drawing.Size(189, 160);
            this.grpPhanQuyenNguoiDung.TabIndex = 1;
            this.grpPhanQuyenNguoiDung.TabStop = false;
            this.grpPhanQuyenNguoiDung.Text = "Phân quyền người dùng";
            // 
            // grpQuanLyNguoiDung
            // 
            this.grpQuanLyNguoiDung.Controls.Add(this.btnQuanLyNguoiDung);
            this.grpQuanLyNguoiDung.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLyNguoiDung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLyNguoiDung.Location = new System.Drawing.Point(69, 22);
            this.grpQuanLyNguoiDung.Name = "grpQuanLyNguoiDung";
            this.grpQuanLyNguoiDung.Size = new System.Drawing.Size(164, 160);
            this.grpQuanLyNguoiDung.TabIndex = 0;
            this.grpQuanLyNguoiDung.TabStop = false;
            this.grpQuanLyNguoiDung.Text = "Quản lý người dùng";
            // 
            // btnPhanQuyenNguoiDung
            // 
            this.btnPhanQuyenNguoiDung.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.administrative_tools_120px;
            this.btnPhanQuyenNguoiDung.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPhanQuyenNguoiDung.Location = new System.Drawing.Point(33, 27);
            this.btnPhanQuyenNguoiDung.Name = "btnPhanQuyenNguoiDung";
            this.btnPhanQuyenNguoiDung.Size = new System.Drawing.Size(121, 113);
            this.btnPhanQuyenNguoiDung.TabIndex = 0;
            this.btnPhanQuyenNguoiDung.UseVisualStyleBackColor = true;
            this.btnPhanQuyenNguoiDung.Click += new System.EventHandler(this.btnPhanQuyenNguoiDung_Click);
            // 
            // btnQuanLyNguoiDung
            // 
            this.btnQuanLyNguoiDung.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.admin_settings_male_120px;
            this.btnQuanLyNguoiDung.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnQuanLyNguoiDung.Location = new System.Drawing.Point(22, 27);
            this.btnQuanLyNguoiDung.Name = "btnQuanLyNguoiDung";
            this.btnQuanLyNguoiDung.Size = new System.Drawing.Size(121, 113);
            this.btnQuanLyNguoiDung.TabIndex = 0;
            this.btnQuanLyNguoiDung.UseVisualStyleBackColor = true;
            this.btnQuanLyNguoiDung.Click += new System.EventHandler(this.btnQuanLyNguoiDung_Click);
            // 
            // UC_System
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grpPhanQuyen);
            this.Name = "UC_System";
            this.Size = new System.Drawing.Size(893, 619);
            this.grpPhanQuyen.ResumeLayout(false);
            this.grpPhanQuyenNguoiDung.ResumeLayout(false);
            this.grpQuanLyNguoiDung.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPhanQuyen;
        private System.Windows.Forms.GroupBox grpPhanQuyenNguoiDung;
        private System.Windows.Forms.Button btnPhanQuyenNguoiDung;
        private System.Windows.Forms.GroupBox grpQuanLyNguoiDung;
        private System.Windows.Forms.Button btnQuanLyNguoiDung;
    }
}