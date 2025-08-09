namespace QLCHBanGaRan.UCFunction
{
    partial class UC_RpRevenue
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnThongKe = new Bunifu.Framework.UI.BunifuFlatButton();
            this.lblThang = new System.Windows.Forms.Label();
            this.rpProfile = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.btnBack = new Bunifu.Framework.UI.BunifuImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(41, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(191, 21);
            this.lblTitle.TabIndex = 76;
            this.lblTitle.Text = "THỐNG KÊ DOANH THU";
            // 
            // btnThongKe
            // 
            this.btnThongKe.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnThongKe.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnThongKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnThongKe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnThongKe.BorderRadius = 5;
            this.btnThongKe.ButtonText = "THỐNG KÊ";
            this.btnThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThongKe.DisabledColor = System.Drawing.Color.Gray;
            this.btnThongKe.Iconcolor = System.Drawing.Color.Transparent;
            this.btnThongKe.Iconimage = null;
            this.btnThongKe.Iconimage_right = null;
            this.btnThongKe.Iconimage_right_Selected = null;
            this.btnThongKe.Iconimage_Selected = null;
            this.btnThongKe.IconMarginLeft = 0;
            this.btnThongKe.IconMarginRight = 0;
            this.btnThongKe.IconRightVisible = true;
            this.btnThongKe.IconRightZoom = 0D;
            this.btnThongKe.IconVisible = true;
            this.btnThongKe.IconZoom = 90D;
            this.btnThongKe.IsTab = false;
            this.btnThongKe.Location = new System.Drawing.Point(631, 48);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnThongKe.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(23)))), ((int)(((byte)(43)))));
            this.btnThongKe.OnHoverTextColor = System.Drawing.Color.White;
            this.btnThongKe.selected = false;
            this.btnThongKe.Size = new System.Drawing.Size(95, 28);
            this.btnThongKe.TabIndex = 91;
            this.btnThongKe.Text = "THỐNG KÊ";
            this.btnThongKe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnThongKe.Textcolor = System.Drawing.Color.White;
            this.btnThongKe.TextFont = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
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
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.Image = global::QLCHBanGaRan.Properties.Resources.left_round_32px;
            this.btnBack.ImageActive = null;
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(32, 32);
            this.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnBack.TabIndex = 77;
            this.btnBack.TabStop = false;
            this.btnBack.Zoom = 10;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // UC_RpRevenue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rpProfile);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.dtpThang);
            this.Controls.Add(this.lblThang);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblTitle);
            this.Name = "UC_RpRevenue";
            this.Size = new System.Drawing.Size(893, 619);
            this.Load += new System.EventHandler(this.UC_RpRevenue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuImageButton btnBack;
        private System.Windows.Forms.Label lblTitle;
        private Bunifu.Framework.UI.BunifuFlatButton btnThongKe;
        private System.Windows.Forms.Label lblThang;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer rpProfile;
        private System.Windows.Forms.DateTimePicker dtpThang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilter;
    }
}
