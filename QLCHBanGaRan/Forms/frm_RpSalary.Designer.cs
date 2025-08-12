namespace QLCHBanGaRan.Forms
{
    partial class frm_RpSalary
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.rpProfile = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.cmbChucDanh = new System.Windows.Forms.ComboBox();
            this.lblChucDanh = new System.Windows.Forms.Label();
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.lblThang = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(10, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(252, 21);
            this.lblTitle.TabIndex = 76;
            this.lblTitle.Text = "BÁO CÁO LƯƠNG HÀNG THÁNG";
            // 
            // rpProfile
            // 
            this.rpProfile.ActiveViewIndex = -1;
            this.rpProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rpProfile.Cursor = System.Windows.Forms.Cursors.Default;
            this.rpProfile.DisplayStatusBar = false;
            this.rpProfile.Location = new System.Drawing.Point(3, 87);
            this.rpProfile.Name = "rpProfile";
            this.rpProfile.Size = new System.Drawing.Size(887, 529);
            this.rpProfile.TabIndex = 78;
            this.rpProfile.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // cmbChucDanh
            // 
            this.cmbChucDanh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChucDanh.FormattingEnabled = true;
            this.cmbChucDanh.Location = new System.Drawing.Point(523, 47);
            this.cmbChucDanh.Name = "cmbChucDanh";
            this.cmbChucDanh.Size = new System.Drawing.Size(186, 28);
            this.cmbChucDanh.TabIndex = 83;
            this.cmbChucDanh.SelectedIndexChanged += new System.EventHandler(this.cmbChucDanh_SelectedIndexChanged);
            // 
            // lblChucDanh
            // 
            this.lblChucDanh.AutoSize = true;
            this.lblChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChucDanh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblChucDanh.Location = new System.Drawing.Point(438, 50);
            this.lblChucDanh.Name = "lblChucDanh";
            this.lblChucDanh.Size = new System.Drawing.Size(79, 20);
            this.lblChucDanh.TabIndex = 82;
            this.lblChucDanh.Text = "Chức danh";
            // 
            // dtpThang
            // 
            this.dtpThang.CalendarFont = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThang.CustomFormat = "MM/yyyy";
            this.dtpThang.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThang.Location = new System.Drawing.Point(211, 48);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.Size = new System.Drawing.Size(165, 27);
            this.dtpThang.TabIndex = 85;
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblThang.Location = new System.Drawing.Point(155, 50);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(50, 20);
            this.lblThang.TabIndex = 84;
            this.lblThang.Text = "Tháng";
            // 
            // frm_RpSalary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(877, 580);
            this.Controls.Add(this.dtpThang);
            this.Controls.Add(this.lblThang);
            this.Controls.Add(this.cmbChucDanh);
            this.Controls.Add(this.lblChucDanh);
            this.Controls.Add(this.rpProfile);
            this.Controls.Add(this.lblTitle);
            this.Name = "frm_RpSalary";
            this.Load += new System.EventHandler(this.frm_RpSalary_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer rpProfile;
        private System.Windows.Forms.ComboBox cmbChucDanh;
        private System.Windows.Forms.Label lblChucDanh;
        private System.Windows.Forms.DateTimePicker dtpThang;
        private System.Windows.Forms.Label lblThang;
    }
}