namespace QLCHBanGaRan.Forms
{
    partial class frm_System
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
            this.grpPhanQuyen = new System.Windows.Forms.GroupBox();
            this.grpQuanLyNguoiDung = new System.Windows.Forms.GroupBox();
            this.btnQuanLyNguoiDung = new System.Windows.Forms.Button();
            this.grpPhanQuyen.SuspendLayout();
            this.grpQuanLyNguoiDung.SuspendLayout();
            this.SuspendLayout();
            //
            // grpPhanQuyen
            //
            this.grpPhanQuyen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpPhanQuyen.BackColor = System.Drawing.Color.Transparent;
            this.grpPhanQuyen.Controls.Add(this.grpQuanLyNguoiDung);
            this.grpPhanQuyen.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPhanQuyen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpPhanQuyen.Location = new System.Drawing.Point(285, 154);
            this.grpPhanQuyen.Name = "grpPhanQuyen";
            this.grpPhanQuyen.Size = new System.Drawing.Size(292, 255);
            this.grpPhanQuyen.TabIndex = 6;
            this.grpPhanQuyen.TabStop = false;
            this.grpPhanQuyen.Text = "Phân quyền";
            //
            // grpQuanLyNguoiDung
            //
            this.grpQuanLyNguoiDung.Controls.Add(this.btnQuanLyNguoiDung);
            this.grpQuanLyNguoiDung.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuanLyNguoiDung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpQuanLyNguoiDung.Location = new System.Drawing.Point(43, 28);
            this.grpQuanLyNguoiDung.Name = "grpQuanLyNguoiDung";
            this.grpQuanLyNguoiDung.Size = new System.Drawing.Size(207, 200);
            this.grpQuanLyNguoiDung.TabIndex = 0;
            this.grpQuanLyNguoiDung.TabStop = false;
            this.grpQuanLyNguoiDung.Text = "Quản lý người dùng";
            //
            // btnQuanLyNguoiDung
            //
            this.btnQuanLyNguoiDung.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.admin_settings_male_120px;
            this.btnQuanLyNguoiDung.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnQuanLyNguoiDung.Location = new System.Drawing.Point(35, 28);
            this.btnQuanLyNguoiDung.Name = "btnQuanLyNguoiDung";
            this.btnQuanLyNguoiDung.Size = new System.Drawing.Size(145, 145);
            this.btnQuanLyNguoiDung.TabIndex = 0;
            this.btnQuanLyNguoiDung.UseVisualStyleBackColor = true;
            this.btnQuanLyNguoiDung.Click += new System.EventHandler(this.btnQuanLyNguoiDung_Click);
            //
            //
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grpPhanQuyen);
            this.Name = "frm_System";
            this.Size = new System.Drawing.Size(893, 619);
            this.grpPhanQuyen.ResumeLayout(false);
            this.grpQuanLyNguoiDung.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPhanQuyen;
        private System.Windows.Forms.GroupBox grpQuanLyNguoiDung;
        private System.Windows.Forms.Button btnQuanLyNguoiDung;
    }
}