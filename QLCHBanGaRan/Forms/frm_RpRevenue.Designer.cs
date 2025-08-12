namespace QLCHBanGaRan.Forms
{
    partial class frm_RpRevenue
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
            this.lblThang = new System.Windows.Forms.Label();
            this.rpProfile = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(8, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(191, 21);
            this.lblTitle.TabIndex = 76;
            this.lblTitle.Text = "THỐNG KÊ DOANH THU";
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblThang.Location = new System.Drawing.Point(86, 51);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(104, 20);
            this.lblThang.TabIndex = 89;
            this.lblThang.Text = "Thống kê theo";
            // 
            // rpProfile
            // 
            this.rpProfile.ActiveViewIndex = -1;
            this.rpProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rpProfile.Cursor = System.Windows.Forms.Cursors.Default;
            this.rpProfile.DisplayStatusBar = false;
            this.rpProfile.Location = new System.Drawing.Point(3, 82);
            this.rpProfile.Name = "rpProfile";
            this.rpProfile.Size = new System.Drawing.Size(887, 534);
            this.rpProfile.TabIndex = 92;
            this.rpProfile.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // dtpThang
            // 
            this.dtpThang.CalendarFont = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThang.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText;
            this.dtpThang.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.dtpThang.CustomFormat = "yyyy";
            this.dtpThang.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThang.Location = new System.Drawing.Point(546, 49);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.Size = new System.Drawing.Size(79, 27);
            this.dtpThang.TabIndex = 90;
            this.dtpThang.Value = new System.DateTime(2025, 12, 12, 9, 43, 23, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.label1.Location = new System.Drawing.Point(499, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 94;
            this.label1.Text = "Năm";
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Location = new System.Drawing.Point(196, 48);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(182, 28);
            this.cmbFilter.TabIndex = 95;
            // 
            // frm_RpRevenue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(877, 580);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rpProfile);
            this.Controls.Add(this.dtpThang);
            this.Controls.Add(this.lblThang);
            this.Controls.Add(this.lblTitle);
            this.Name = "frm_RpRevenue";
            this.Load += new System.EventHandler(this.frm_RpRevenue_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblThang;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer rpProfile;
        private System.Windows.Forms.DateTimePicker dtpThang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilter;
    }
}
