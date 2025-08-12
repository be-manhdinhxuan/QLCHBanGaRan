namespace QLCHBanGaRan.Forms
{
    partial class frm_Personnel
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
            this.grpQuanLyNhanSu = new System.Windows.Forms.GroupBox();
            this.grpHoSoNhanVien = new System.Windows.Forms.GroupBox();
            this.grpQuanLyNhanVien = new System.Windows.Forms.GroupBox();
            this.btnHoSoNhanVien = new System.Windows.Forms.Button();
            this.btnQuanLyNhanVien = new System.Windows.Forms.Button();
            this.grpQuanLyNhanSu.SuspendLayout();
            this.grpHoSoNhanVien.SuspendLayout();
            this.grpQuanLyNhanVien.SuspendLayout();
            this.SuspendLayout();
            //
            // grpQuanLyNhanSu
            //
            this.grpQuanLyNhanSu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpQuanLyNhanSu.BackColor = System.Drawing.Color.Transparent;
            this.grpQuanLyNhanSu.Controls.Add(this.grpHoSoNhanVien);
            this.grpQuanLyNhanSu.Controls.Add(this.grpQuanLyNhanVien);
            this.grpQuanLyNhanSu.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLyNhanSu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLyNhanSu.Location = new System.Drawing.Point(168, 157);
            this.grpQuanLyNhanSu.Name = "grpQuanLyNhanSu";
            this.grpQuanLyNhanSu.Size = new System.Drawing.Size(536, 264);
            this.grpQuanLyNhanSu.TabIndex = 5;
            this.grpQuanLyNhanSu.TabStop = false;
            this.grpQuanLyNhanSu.Text = "Quản lý nhân sự";
            //
            // grpHoSoNhanVien
            //
            this.grpHoSoNhanVien.Controls.Add(this.btnHoSoNhanVien);
            this.grpHoSoNhanVien.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpHoSoNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpHoSoNhanVien.Location = new System.Drawing.Point(283, 28);
            this.grpHoSoNhanVien.Name = "grpHoSoNhanVien";
            this.grpHoSoNhanVien.Size = new System.Drawing.Size(207, 200);
            this.grpHoSoNhanVien.TabIndex = 1;
            this.grpHoSoNhanVien.TabStop = false;
            this.grpHoSoNhanVien.Text = "Hồ sơ nhân viên";
            //
            // grpQuanLyNhanVien
            //
            this.grpQuanLyNhanVien.Controls.Add(this.btnQuanLyNhanVien);
            this.grpQuanLyNhanVien.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLyNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLyNhanVien.Location = new System.Drawing.Point(43, 28);
            this.grpQuanLyNhanVien.Name = "grpQuanLyNhanVien";
            this.grpQuanLyNhanVien.Size = new System.Drawing.Size(207, 200);
            this.grpQuanLyNhanVien.TabIndex = 0;
            this.grpQuanLyNhanVien.TabStop = false;
            this.grpQuanLyNhanVien.Text = "Quản lý nhân viên";
            //
            // btnHoSoNhanVien
            //
            this.btnHoSoNhanVien.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.resume_100px;
            this.btnHoSoNhanVien.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHoSoNhanVien.Location = new System.Drawing.Point(35, 28);
            this.btnHoSoNhanVien.Name = "btnHoSoNhanVien";
            this.btnHoSoNhanVien.Size = new System.Drawing.Size(145, 145);
            this.btnHoSoNhanVien.TabIndex = 0;
            this.btnHoSoNhanVien.UseVisualStyleBackColor = true;
            this.btnHoSoNhanVien.Click += new System.EventHandler(this.btnHoSoNhanVien_Click);
            //
            // btnQuanLyNhanVien
            //
            this.btnQuanLyNhanVien.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.edit_user_100px;
            this.btnQuanLyNhanVien.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnQuanLyNhanVien.Location = new System.Drawing.Point(35, 28);
            this.btnQuanLyNhanVien.Name = "btnQuanLyNhanVien";
            this.btnQuanLyNhanVien.Size = new System.Drawing.Size(145, 145);
            this.btnQuanLyNhanVien.TabIndex = 0;
            this.btnQuanLyNhanVien.UseVisualStyleBackColor = true;
            this.btnQuanLyNhanVien.Click += new System.EventHandler(this.btnQuanLyNhanVien_Click);
            //
            //
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grpQuanLyNhanSu);
            this.Name = "frm_Personnel";
            this.Size = new System.Drawing.Size(893, 619);
            this.grpQuanLyNhanSu.ResumeLayout(false);
            this.grpHoSoNhanVien.ResumeLayout(false);
            this.grpQuanLyNhanVien.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpQuanLyNhanSu;
        private System.Windows.Forms.GroupBox grpHoSoNhanVien;
        private System.Windows.Forms.Button btnHoSoNhanVien;
        private System.Windows.Forms.GroupBox grpQuanLyNhanVien;
        private System.Windows.Forms.Button btnQuanLyNhanVien;
    }
}