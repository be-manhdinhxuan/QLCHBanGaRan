namespace QLCHBanGaRan.UCSystems
{
    partial class UC_Salary
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
            this.grpQuanLyLuong = new System.Windows.Forms.GroupBox();
            this.grpChamCong = new System.Windows.Forms.GroupBox();
            this.btnChamCong = new System.Windows.Forms.Button();
            this.grpQuanLyChucDanh = new System.Windows.Forms.GroupBox();
            this.btnQuanLyChucDanh = new System.Windows.Forms.Button();
            this.grpQuanLyLuong.SuspendLayout();
            this.grpChamCong.SuspendLayout();
            this.grpQuanLyChucDanh.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpQuanLyLuong
            // 
            this.grpQuanLyLuong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpQuanLyLuong.BackColor = System.Drawing.Color.Transparent;
            this.grpQuanLyLuong.Controls.Add(this.grpChamCong);
            this.grpQuanLyLuong.Controls.Add(this.grpQuanLyChucDanh);
            this.grpQuanLyLuong.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLyLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLyLuong.Location = new System.Drawing.Point(190, 72);
            this.grpQuanLyLuong.Name = "grpQuanLyLuong";
            this.grpQuanLyLuong.Size = new System.Drawing.Size(536, 255);
            this.grpQuanLyLuong.TabIndex = 7;
            this.grpQuanLyLuong.TabStop = false;
            this.grpQuanLyLuong.Text = "Quản lý lương";
            // 
            // grpChamCong
            // 
            this.grpChamCong.Controls.Add(this.btnChamCong);
            this.grpChamCong.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpChamCong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpChamCong.Location = new System.Drawing.Point(283, 28);
            this.grpChamCong.Name = "grpChamCong";
            this.grpChamCong.Size = new System.Drawing.Size(207, 200);
            this.grpChamCong.TabIndex = 2;
            this.grpChamCong.TabStop = false;
            this.grpChamCong.Text = "Chấm công";
            // 
            // btnChamCong
            // 
            this.btnChamCong.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.planner_120px;
            this.btnChamCong.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnChamCong.Location = new System.Drawing.Point(35, 28);
            this.btnChamCong.Name = "btnChamCong";
            this.btnChamCong.Size = new System.Drawing.Size(145, 145);
            this.btnChamCong.TabIndex = 0;
            this.btnChamCong.UseVisualStyleBackColor = true;
            this.btnChamCong.Click += new System.EventHandler(this.btnChamCong_Click);
            // 
            // grpQuanLyChucDanh
            // 
            this.grpQuanLyChucDanh.Controls.Add(this.btnQuanLyChucDanh);
            this.grpQuanLyChucDanh.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLyChucDanh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLyChucDanh.Location = new System.Drawing.Point(43, 28);
            this.grpQuanLyChucDanh.Name = "grpQuanLyChucDanh";
            this.grpQuanLyChucDanh.Size = new System.Drawing.Size(207, 200);
            this.grpQuanLyChucDanh.TabIndex = 0;
            this.grpQuanLyChucDanh.TabStop = false;
            this.grpQuanLyChucDanh.Text = "Quản lý lương chức danh";
            // 
            // btnQuanLyChucDanh
            // 
            this.btnQuanLyChucDanh.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.admin_settings_male_120px;
            this.btnQuanLyChucDanh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnQuanLyChucDanh.Location = new System.Drawing.Point(35, 28);
            this.btnQuanLyChucDanh.Name = "btnQuanLyChucDanh";
            this.btnQuanLyChucDanh.Size = new System.Drawing.Size(145, 145);
            this.btnQuanLyChucDanh.TabIndex = 0;
            this.btnQuanLyChucDanh.UseVisualStyleBackColor = true;
            this.btnQuanLyChucDanh.Click += new System.EventHandler(this.btnQuanLyChucDanh_Click);
            // 
            // UC_Salary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grpQuanLyLuong);
            this.Name = "UC_Salary";
            this.Size = new System.Drawing.Size(893, 619);
            this.grpQuanLyLuong.ResumeLayout(false);
            this.grpChamCong.ResumeLayout(false);
            this.grpQuanLyChucDanh.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpQuanLyLuong;
        private System.Windows.Forms.GroupBox grpChamCong;
        private System.Windows.Forms.Button btnChamCong;
        private System.Windows.Forms.GroupBox grpQuanLyChucDanh;
        private System.Windows.Forms.Button btnQuanLyChucDanh;
    }
}