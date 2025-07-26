namespace QLCHBanGaRan.UCFunction
{
    partial class UC_DeletedProducts
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
            this.grpDeletedProducts = new System.Windows.Forms.GroupBox();
            this.btnDeletePermanently = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.dtDeletedProducts = new System.Windows.Forms.DataGridView();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpDeletedProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtDeletedProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDeletedProducts
            // 
            this.grpDeletedProducts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpDeletedProducts.BackColor = System.Drawing.Color.Transparent;
            this.grpDeletedProducts.Controls.Add(this.btnDeletePermanently);
            this.grpDeletedProducts.Controls.Add(this.btnRestore);
            this.grpDeletedProducts.Controls.Add(this.dtDeletedProducts);
            this.grpDeletedProducts.Controls.Add(this.cbCategory);
            this.grpDeletedProducts.Controls.Add(this.label1);
            this.grpDeletedProducts.Controls.Add(this.txtSearch);
            this.grpDeletedProducts.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDeletedProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpDeletedProducts.Location = new System.Drawing.Point(20, 38);
            this.grpDeletedProducts.Name = "grpDeletedProducts";
            this.grpDeletedProducts.Size = new System.Drawing.Size(853, 561);
            this.grpDeletedProducts.TabIndex = 7;
            this.grpDeletedProducts.TabStop = false;
            this.grpDeletedProducts.Text = "Quản lý sản phẩm đã xóa";
            // 
            // btnDeletePermanently
            // 
            this.btnDeletePermanently.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnDeletePermanently.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnDeletePermanently.ForeColor = System.Drawing.Color.White;
            this.btnDeletePermanently.Location = new System.Drawing.Point(460, 475);
            this.btnDeletePermanently.Name = "btnDeletePermanently";
            this.btnDeletePermanently.Size = new System.Drawing.Size(211, 78);
            this.btnDeletePermanently.TabIndex = 5;
            this.btnDeletePermanently.Text = "Xóa vĩnh viễn";
            this.btnDeletePermanently.UseVisualStyleBackColor = false;
            this.btnDeletePermanently.Click += new System.EventHandler(this.btnDeletePermanently_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.Location = new System.Drawing.Point(243, 475);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(211, 78);
            this.btnRestore.TabIndex = 4;
            this.btnRestore.Text = "Khôi phục";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // dtDeletedProducts
            // 
            this.dtDeletedProducts.AllowUserToAddRows = false;
            this.dtDeletedProducts.AllowUserToDeleteRows = false;
            this.dtDeletedProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDeletedProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtDeletedProducts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtDeletedProducts.BackgroundColor = System.Drawing.Color.White;
            this.dtDeletedProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtDeletedProducts.Location = new System.Drawing.Point(20, 103);
            this.dtDeletedProducts.Name = "dtDeletedProducts";
            this.dtDeletedProducts.ReadOnly = true;
            this.dtDeletedProducts.RowHeadersVisible = false;
            this.dtDeletedProducts.Size = new System.Drawing.Size(813, 334);
            this.dtDeletedProducts.TabIndex = 3;
            // 
            // cbCategory
            // 
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Items.AddRange(new object[] {
            "Đồ ăn",
            "Đồ uống"});
            this.cbCategory.Location = new System.Drawing.Point(600, 52);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(233, 28);
            this.cbCategory.TabIndex = 2;
            this.cbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.label1.Location = new System.Drawing.Point(16, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên sản phẩm:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(130, 52);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(464, 27);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Image = global::QLCHBanGaRan.Properties.Resources.left_round_32px;
            this.btnBack.Location = new System.Drawing.Point(20, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(32, 32);
            this.btnBack.TabIndex = 66;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(61, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(82, 21);
            this.lblTitle.TabIndex = 67;
            this.lblTitle.Text = "QUAY LẠI";
            // 
            // UC_DeletedProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.grpDeletedProducts);
            this.Name = "UC_DeletedProducts";
            this.Size = new System.Drawing.Size(893, 619);
            this.grpDeletedProducts.ResumeLayout(false);
            this.grpDeletedProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtDeletedProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDeletedProducts;
        private System.Windows.Forms.Button btnDeletePermanently;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.DataGridView dtDeletedProducts;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblTitle;
    }
}