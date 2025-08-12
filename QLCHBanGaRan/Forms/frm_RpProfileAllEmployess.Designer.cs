namespace QLCHBanGaRan.Forms
{
    partial class frm_RpProfileAllEmployess
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
            this.cmbChucDanh = new System.Windows.Forms.ComboBox();
            this.lblChucDanh = new System.Windows.Forms.Label();
            this.rpProfile = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(8, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(332, 21);
            this.lblTitle.TabIndex = 70;
            this.lblTitle.Text = "THỐNG KÊ NHÂN VIÊN THEO CHỨC DANH";
            // 
            // cmbChucDanh
            // 
            this.cmbChucDanh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cmbChucDanh.FormattingEnabled = true;
            this.cmbChucDanh.Location = new System.Drawing.Point(142, 46);
            this.cmbChucDanh.Name = "cmbChucDanh";
            this.cmbChucDanh.Size = new System.Drawing.Size(186, 28);
            this.cmbChucDanh.TabIndex = 73;
            this.cmbChucDanh.SelectedIndexChanged += new System.EventHandler(this.cmbChucDanh_SelectedIndexChanged);
            // 
            // lblChucDanh
            // 
            this.lblChucDanh.AutoSize = true;
            this.lblChucDanh.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.lblChucDanh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblChucDanh.Location = new System.Drawing.Point(46, 50);
            this.lblChucDanh.Name = "lblChucDanh";
            this.lblChucDanh.Size = new System.Drawing.Size(79, 20);
            this.lblChucDanh.TabIndex = 72;
            this.lblChucDanh.Text = "Chức danh";
            // 
            // rpProfile
            // 
            this.rpProfile.ActiveViewIndex = -1;
            this.rpProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rpProfile.Cursor = System.Windows.Forms.Cursors.Default;
            this.rpProfile.DisplayStatusBar = false;
            this.rpProfile.Location = new System.Drawing.Point(3, 80);
            this.rpProfile.Name = "rpProfile";
            this.rpProfile.Size = new System.Drawing.Size(887, 536);
            this.rpProfile.TabIndex = 74;
            this.rpProfile.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frm_RpProfileAllEmployess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(877, 580);
            this.Controls.Add(this.rpProfile);
            this.Controls.Add(this.cmbChucDanh);
            this.Controls.Add(this.lblChucDanh);
            this.Controls.Add(this.lblTitle);
            this.Name = "frm_RpProfileAllEmployess";
            this.Load += new System.EventHandler(this.frm_RpProfileAllEmployess_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cmbChucDanh;
        private System.Windows.Forms.Label lblChucDanh;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer rpProfile;
    }
}