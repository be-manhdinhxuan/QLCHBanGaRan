namespace QLCHBanGaRan.Forms
{
    partial class frm_RpProductInventory
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
            this.cmbLoaiSP = new System.Windows.Forms.ComboBox();
            this.lblBoPhan = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(10, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(255, 21);
            this.lblTitle.TabIndex = 72;
            this.lblTitle.Text = "THỐNG KÊ SẢN PHẨM TỒN KHO";
            // 
            // rpProfile
            // 
            this.rpProfile.ActiveViewIndex = -1;
            this.rpProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rpProfile.Cursor = System.Windows.Forms.Cursors.Default;
            this.rpProfile.DisplayStatusBar = false;
            this.rpProfile.Location = new System.Drawing.Point(3, 88);
            this.rpProfile.Name = "rpProfile";
            this.rpProfile.Size = new System.Drawing.Size(887, 528);
            this.rpProfile.TabIndex = 78;
            this.rpProfile.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // cmbLoaiSP
            // 
            this.cmbLoaiSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoaiSP.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLoaiSP.FormattingEnabled = true;
            this.cmbLoaiSP.Location = new System.Drawing.Point(161, 44);
            this.cmbLoaiSP.Name = "cmbLoaiSP";
            this.cmbLoaiSP.Size = new System.Drawing.Size(186, 28);
            this.cmbLoaiSP.TabIndex = 80;
            this.cmbLoaiSP.SelectedIndexChanged += new System.EventHandler(this.cmbLoaiSP_SelectedIndexChanged);
            // 
            // lblBoPhan
            // 
            this.lblBoPhan.AutoSize = true;
            this.lblBoPhan.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoPhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblBoPhan.Location = new System.Drawing.Point(50, 47);
            this.lblBoPhan.Name = "lblBoPhan";
            this.lblBoPhan.Size = new System.Drawing.Size(105, 20);
            this.lblBoPhan.TabIndex = 79;
            this.lblBoPhan.Text = "Loại sản phẩm";
            // 
            // frm_RpProductInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(877, 580);
            this.Controls.Add(this.cmbLoaiSP);
            this.Controls.Add(this.lblBoPhan);
            this.Controls.Add(this.rpProfile);
            this.Controls.Add(this.lblTitle);
            this.Name = "frm_RpProductInventory";
            this.Load += new System.EventHandler(this.frm_RpProductInventory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer rpProfile;
        private System.Windows.Forms.ComboBox cmbLoaiSP;
        private System.Windows.Forms.Label lblBoPhan;
    }
}