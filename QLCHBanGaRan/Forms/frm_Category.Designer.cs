namespace QLCHBanGaRan.Forms
{
    partial class frm_Category
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
            this.grpDeletedRecords = new System.Windows.Forms.GroupBox();
            this.grpDeletedInvoices = new System.Windows.Forms.GroupBox();
            this.btnManageDeletedInvoices = new System.Windows.Forms.Button();
            this.grpDeletedPositions = new System.Windows.Forms.GroupBox();
            this.btnManageDeletedPositions = new System.Windows.Forms.Button();
            this.grpDeletedEmployees = new System.Windows.Forms.GroupBox();
            this.btnManageDeletedEmployees = new System.Windows.Forms.Button();
            this.grpDeletedSuppliers = new System.Windows.Forms.GroupBox();
            this.btnManageDeletedSuppliers = new System.Windows.Forms.Button();
            this.grpDeletedProducts = new System.Windows.Forms.GroupBox();
            this.btnManageDeletedProducts = new System.Windows.Forms.Button();
            this.grpDeletedRecords.SuspendLayout();
            this.grpDeletedInvoices.SuspendLayout();
            this.grpDeletedPositions.SuspendLayout();
            this.grpDeletedEmployees.SuspendLayout();
            this.grpDeletedSuppliers.SuspendLayout();
            this.grpDeletedProducts.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDeletedRecords
            // 
            this.grpDeletedRecords.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpDeletedRecords.BackColor = System.Drawing.Color.Transparent;
            this.grpDeletedRecords.Controls.Add(this.grpDeletedInvoices);
            this.grpDeletedRecords.Controls.Add(this.grpDeletedPositions);
            this.grpDeletedRecords.Controls.Add(this.grpDeletedEmployees);
            this.grpDeletedRecords.Controls.Add(this.grpDeletedSuppliers);
            this.grpDeletedRecords.Controls.Add(this.grpDeletedProducts);
            this.grpDeletedRecords.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDeletedRecords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpDeletedRecords.Location = new System.Drawing.Point(20, 20);
            this.grpDeletedRecords.Name = "grpDeletedRecords";
            this.grpDeletedRecords.Size = new System.Drawing.Size(853, 579);
            this.grpDeletedRecords.TabIndex = 7;
            this.grpDeletedRecords.TabStop = false;
            this.grpDeletedRecords.Text = "Quản lý bản ghi đã xóa";
            // 
            // grpDeletedInvoices
            // 
            this.grpDeletedInvoices.Controls.Add(this.btnManageDeletedInvoices);
            this.grpDeletedInvoices.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDeletedInvoices.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpDeletedInvoices.Location = new System.Drawing.Point(460, 333);
            this.grpDeletedInvoices.Name = "grpDeletedInvoices";
            this.grpDeletedInvoices.Size = new System.Drawing.Size(192, 183);
            this.grpDeletedInvoices.TabIndex = 8;
            this.grpDeletedInvoices.TabStop = false;
            this.grpDeletedInvoices.Text = "Hóa đơn";
            // 
            // btnManageDeletedInvoices
            // 
            this.btnManageDeletedInvoices.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.invoice_del;
            this.btnManageDeletedInvoices.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnManageDeletedInvoices.Location = new System.Drawing.Point(32, 24);
            this.btnManageDeletedInvoices.Name = "btnManageDeletedInvoices";
            this.btnManageDeletedInvoices.Size = new System.Drawing.Size(128, 136);
            this.btnManageDeletedInvoices.TabIndex = 0;
            this.btnManageDeletedInvoices.UseVisualStyleBackColor = true;
            this.btnManageDeletedInvoices.Click += new System.EventHandler(this.btnManageDeletedInvoices_Click);
            // 
            // grpDeletedPositions
            // 
            this.grpDeletedPositions.Controls.Add(this.btnManageDeletedPositions);
            this.grpDeletedPositions.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDeletedPositions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpDeletedPositions.Location = new System.Drawing.Point(186, 333);
            this.grpDeletedPositions.Name = "grpDeletedPositions";
            this.grpDeletedPositions.Size = new System.Drawing.Size(192, 183);
            this.grpDeletedPositions.TabIndex = 7;
            this.grpDeletedPositions.TabStop = false;
            this.grpDeletedPositions.Text = "Chức danh";
            // 
            // btnManageDeletedPositions
            // 
            this.btnManageDeletedPositions.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.position_del;
            this.btnManageDeletedPositions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnManageDeletedPositions.Location = new System.Drawing.Point(33, 26);
            this.btnManageDeletedPositions.Name = "btnManageDeletedPositions";
            this.btnManageDeletedPositions.Size = new System.Drawing.Size(128, 136);
            this.btnManageDeletedPositions.TabIndex = 0;
            this.btnManageDeletedPositions.UseVisualStyleBackColor = true;
            this.btnManageDeletedPositions.Click += new System.EventHandler(this.btnManageDeletedPositions_Click);
            // 
            // grpDeletedEmployees
            // 
            this.grpDeletedEmployees.Controls.Add(this.btnManageDeletedEmployees);
            this.grpDeletedEmployees.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDeletedEmployees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpDeletedEmployees.Location = new System.Drawing.Point(599, 100);
            this.grpDeletedEmployees.Name = "grpDeletedEmployees";
            this.grpDeletedEmployees.Size = new System.Drawing.Size(192, 183);
            this.grpDeletedEmployees.TabIndex = 6;
            this.grpDeletedEmployees.TabStop = false;
            this.grpDeletedEmployees.Text = "Nhân viên";
            // 
            // btnManageDeletedEmployees
            // 
            this.btnManageDeletedEmployees.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.employee_del;
            this.btnManageDeletedEmployees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnManageDeletedEmployees.Location = new System.Drawing.Point(28, 30);
            this.btnManageDeletedEmployees.Name = "btnManageDeletedEmployees";
            this.btnManageDeletedEmployees.Size = new System.Drawing.Size(128, 136);
            this.btnManageDeletedEmployees.TabIndex = 0;
            this.btnManageDeletedEmployees.UseVisualStyleBackColor = true;
            this.btnManageDeletedEmployees.Click += new System.EventHandler(this.btnManageDeletedEmployees_Click);
            // 
            // grpDeletedSuppliers
            // 
            this.grpDeletedSuppliers.Controls.Add(this.btnManageDeletedSuppliers);
            this.grpDeletedSuppliers.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDeletedSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpDeletedSuppliers.Location = new System.Drawing.Point(325, 100);
            this.grpDeletedSuppliers.Name = "grpDeletedSuppliers";
            this.grpDeletedSuppliers.Size = new System.Drawing.Size(192, 183);
            this.grpDeletedSuppliers.TabIndex = 5;
            this.grpDeletedSuppliers.TabStop = false;
            this.grpDeletedSuppliers.Text = "Nhà cung cấp";
            // 
            // btnManageDeletedSuppliers
            // 
            this.btnManageDeletedSuppliers.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.supplier_del;
            this.btnManageDeletedSuppliers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnManageDeletedSuppliers.Location = new System.Drawing.Point(32, 30);
            this.btnManageDeletedSuppliers.Name = "btnManageDeletedSuppliers";
            this.btnManageDeletedSuppliers.Size = new System.Drawing.Size(128, 136);
            this.btnManageDeletedSuppliers.TabIndex = 0;
            this.btnManageDeletedSuppliers.UseVisualStyleBackColor = true;
            this.btnManageDeletedSuppliers.Click += new System.EventHandler(this.btnManageDeletedSuppliers_Click);
            // 
            // grpDeletedProducts
            // 
            this.grpDeletedProducts.Controls.Add(this.btnManageDeletedProducts);
            this.grpDeletedProducts.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDeletedProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpDeletedProducts.Location = new System.Drawing.Point(51, 100);
            this.grpDeletedProducts.Name = "grpDeletedProducts";
            this.grpDeletedProducts.Size = new System.Drawing.Size(192, 183);
            this.grpDeletedProducts.TabIndex = 4;
            this.grpDeletedProducts.TabStop = false;
            this.grpDeletedProducts.Text = "Sản phẩm";
            // 
            // btnManageDeletedProducts
            // 
            this.btnManageDeletedProducts.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.product_del;
            this.btnManageDeletedProducts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnManageDeletedProducts.Location = new System.Drawing.Point(29, 30);
            this.btnManageDeletedProducts.Name = "btnManageDeletedProducts";
            this.btnManageDeletedProducts.Size = new System.Drawing.Size(128, 136);
            this.btnManageDeletedProducts.TabIndex = 0;
            this.btnManageDeletedProducts.UseVisualStyleBackColor = true;
            this.btnManageDeletedProducts.Click += new System.EventHandler(this.btnManageDeletedProducts_Click);
            // 
            // UC_Category
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grpDeletedRecords);
            this.Name = "UC_Category";
            this.Size = new System.Drawing.Size(893, 619);
            this.grpDeletedRecords.ResumeLayout(false);
            this.grpDeletedInvoices.ResumeLayout(false);
            this.grpDeletedPositions.ResumeLayout(false);
            this.grpDeletedEmployees.ResumeLayout(false);
            this.grpDeletedSuppliers.ResumeLayout(false);
            this.grpDeletedProducts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDeletedRecords;
        private System.Windows.Forms.GroupBox grpDeletedInvoices;
        private System.Windows.Forms.Button btnManageDeletedInvoices;
        private System.Windows.Forms.GroupBox grpDeletedPositions;
        private System.Windows.Forms.Button btnManageDeletedPositions;
        private System.Windows.Forms.GroupBox grpDeletedEmployees;
        private System.Windows.Forms.Button btnManageDeletedEmployees;
        private System.Windows.Forms.GroupBox grpDeletedSuppliers;
        private System.Windows.Forms.Button btnManageDeletedSuppliers;
        private System.Windows.Forms.GroupBox grpDeletedProducts;
        private System.Windows.Forms.Button btnManageDeletedProducts;
    }
}