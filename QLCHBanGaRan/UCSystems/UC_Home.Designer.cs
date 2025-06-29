namespace QLCHBanGaRan.UCSystems
{
    partial class UC_Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </param>
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
            this.panelTitle = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelBanner = new System.Windows.Forms.Panel();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.panelTitle.SuspendLayout();
            this.panelBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.SuspendLayout();
            //
            // panelTitle
            //
            this.panelTitle.Controls.Add(this.labelTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(893, 70);
            this.panelTitle.TabIndex = 2;
            //
            // labelTitle
            //
            this.labelTitle.BackColor = System.Drawing.Color.Transparent; // Đảm bảo trong suốt
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42))))); // Màu chữ đỏ
            this.labelTitle.Location = new System.Drawing.Point(0, 15);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(893, 40); // Kích thước đầy đủ chiều ngang
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "QUẢN LÝ CỬA HÀNG FASTFOOD CHICKEN BÔNG";
            this.labelTitle.TabStop = false;
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; // Căn giữa nội dung
            //
            // panelBanner
            //
            this.panelBanner.Controls.Add(this.pictureBoxBanner);
            this.panelBanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBanner.Location = new System.Drawing.Point(0, 70);
            this.panelBanner.Name = "panelBanner";
            this.panelBanner.Size = new System.Drawing.Size(893, 549);
            this.panelBanner.TabIndex = 3;
            //
            // pictureBoxBanner
            //
            this.pictureBoxBanner.Image = global::QLCHBanGaRan.Properties.Resources.banner_home; // Sử dụng tài nguyên banner_home
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(893, 549);
            this.pictureBoxBanner.Dock = System.Windows.Forms.DockStyle.Fill; // Sử dụng Fill trong panel container
            this.pictureBoxBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBanner.TabIndex = 1;
            this.pictureBoxBanner.TabStop = false;
            //
            // UC_Home
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelBanner);
            this.Controls.Add(this.panelTitle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UC_Home";
            this.Size = new System.Drawing.Size(893, 619);
            this.panelTitle.ResumeLayout(false);
            this.panelBanner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox pictureBoxBanner;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Panel panelBanner;
    }
}