namespace QLCHBanGaRan.Forms
{
    partial class frm_RpProductSold
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
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.lblThang = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(14, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(244, 21);
            this.lblTitle.TabIndex = 74;
            this.lblTitle.Text = "THỐNG KÊ SẢN PHẨM ĐÃ BÁN";
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
            this.rpProfile.TabIndex = 76;
            this.rpProfile.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // dtpThang
            // 
            this.dtpThang.CalendarFont = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThang.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText;
            this.dtpThang.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.dtpThang.CustomFormat = "MM/yyyy";
            this.dtpThang.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThang.Location = new System.Drawing.Point(390, 43);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.Size = new System.Drawing.Size(165, 27);
            this.dtpThang.TabIndex = 87;
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblThang.Location = new System.Drawing.Point(334, 48);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(50, 20);
            this.lblThang.TabIndex = 86;
            this.lblThang.Text = "Tháng";
            // 
            // frm_RpProductSold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(877, 580);
            this.Controls.Add(this.dtpThang);
            this.Controls.Add(this.lblThang);
            this.Controls.Add(this.rpProfile);
            this.Controls.Add(this.lblTitle);
            this.Name = "frm_RpProductSold";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer rpProfile;
        private System.Windows.Forms.DateTimePicker dtpThang;
        private System.Windows.Forms.Label lblThang;
    }
}