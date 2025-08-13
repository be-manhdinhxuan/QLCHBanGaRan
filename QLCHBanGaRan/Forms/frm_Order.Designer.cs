using System.Windows.Forms;

namespace QLCHBanGaRan.Forms
{
    partial class frm_Order
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
            this.components = new System.ComponentModel.Container();
            this.grpSearchProduct = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelSearchContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelSearch = new System.Windows.Forms.TableLayoutPanel();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dtSearch = new System.Windows.Forms.DataGridView();
            this.MaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiaTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiamGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpListProduct = new System.Windows.Forms.GroupBox();
            this.dtChoose = new System.Windows.Forms.DataGridView();
            this.grpInfoInvoice = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelInfoInvoice = new System.Windows.Forms.TableLayoutPanel();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblEmployess = new System.Windows.Forms.Label();
            this.txtEmployess = new System.Windows.Forms.TextBox();
            this.grpPayment = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelPayment = new System.Windows.Forms.TableLayoutPanel();
            this.lblMoney = new System.Windows.Forms.Label();
            this.txtMoney = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblReceive = new System.Windows.Forms.Label();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.lblReturnPayment = new System.Windows.Forms.Label();
            this.txtReturnPayment = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalMoney = new System.Windows.Forms.Label();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnDelProduct = new System.Windows.Forms.Button();
            this.btnPrintInvoice = new System.Windows.Forms.Button();
            this.btnSaveDB = new System.Windows.Forms.Button();
            this.check = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelLeft = new System.Windows.Forms.TableLayoutPanel();
            this.panelInvoiceButtons = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.panelActionButtons = new System.Windows.Forms.TableLayoutPanel();
            this.grpSearchProduct.SuspendLayout();
            this.tableLayoutPanelSearchContainer.SuspendLayout();
            this.tableLayoutPanelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtSearch)).BeginInit();
            this.grpListProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtChoose)).BeginInit();
            this.grpInfoInvoice.SuspendLayout();
            this.tableLayoutPanelInfoInvoice.SuspendLayout();
            this.grpPayment.SuspendLayout();
            this.tableLayoutPanelPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.check)).BeginInit();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelLeft.SuspendLayout();
            this.panelInvoiceButtons.SuspendLayout();
            this.tableLayoutPanelRight.SuspendLayout();
            this.panelActionButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSearchProduct
            // 
            this.grpSearchProduct.Controls.Add(this.tableLayoutPanelSearchContainer);
            this.grpSearchProduct.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.grpSearchProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpSearchProduct.Location = new System.Drawing.Point(3, 3);
            this.grpSearchProduct.Name = "grpSearchProduct";
            this.grpSearchProduct.Size = new System.Drawing.Size(429, 370);
            this.grpSearchProduct.TabIndex = 0;
            this.grpSearchProduct.TabStop = false;
            this.grpSearchProduct.Text = "Chọn món";
            // 
            // tableLayoutPanelSearchContainer
            // 
            this.tableLayoutPanelSearchContainer.ColumnCount = 1;
            this.tableLayoutPanelSearchContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSearchContainer.Controls.Add(this.tableLayoutPanelSearch, 0, 0);
            this.tableLayoutPanelSearchContainer.Controls.Add(this.dtSearch, 0, 1);
            this.tableLayoutPanelSearchContainer.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanelSearchContainer.Name = "tableLayoutPanelSearchContainer";
            this.tableLayoutPanelSearchContainer.RowCount = 2;
            this.tableLayoutPanelSearchContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelSearchContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSearchContainer.Size = new System.Drawing.Size(423, 352);
            this.tableLayoutPanelSearchContainer.TabIndex = 4;
            // 
            // tableLayoutPanelSearch
            // 
            this.tableLayoutPanelSearch.ColumnCount = 2;
            this.tableLayoutPanelSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanelSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanelSearch.Controls.Add(this.cmbFilter, 1, 0);
            this.tableLayoutPanelSearch.Controls.Add(this.txtSearch, 0, 0);
            this.tableLayoutPanelSearch.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelSearch.Name = "tableLayoutPanelSearch";
            this.tableLayoutPanelSearch.RowCount = 1;
            this.tableLayoutPanelSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSearch.Size = new System.Drawing.Size(417, 34);
            this.tableLayoutPanelSearch.TabIndex = 3;
            // 
            // cmbFilter
            // 
            this.cmbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Items.AddRange(new object[] {
            "Mã SP",
            "Tên SP",
            "Giá tiền"});
            this.cmbFilter.Location = new System.Drawing.Point(300, 3);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(114, 28);
            this.cmbFilter.TabIndex = 3;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(3, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(291, 27);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Tìm kiếm...";
            // 
            // dtSearch
            // 
            this.dtSearch.AllowUserToAddRows = false;
            this.dtSearch.AllowUserToDeleteRows = false;
            this.dtSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaSP,
            this.TenSP,
            this.GiaTien,
            this.SoLuong,
            this.GiamGia});
            this.dtSearch.Location = new System.Drawing.Point(3, 43);
            this.dtSearch.Name = "dtSearch";
            this.dtSearch.RowHeadersVisible = false;
            this.dtSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtSearch.Size = new System.Drawing.Size(417, 306);
            this.dtSearch.TabIndex = 1;
            // 
            // MaSP
            // 
            this.MaSP.DataPropertyName = "MaSP";
            this.MaSP.HeaderText = "Mã SP";
            this.MaSP.Name = "MaSP";
            this.MaSP.ReadOnly = true;
            this.MaSP.Width = 70;
            // 
            // TenSP
            // 
            this.TenSP.DataPropertyName = "TenSP";
            this.TenSP.HeaderText = "Tên SP";
            this.TenSP.Name = "TenSP";
            this.TenSP.ReadOnly = true;
            this.TenSP.Width = 150;
            // 
            // GiaTien
            // 
            this.GiaTien.DataPropertyName = "GiaTien";
            this.GiaTien.HeaderText = "Giá tiền";
            this.GiaTien.Name = "GiaTien";
            this.GiaTien.ReadOnly = true;
            // 
            // SoLuong
            // 
            this.SoLuong.DataPropertyName = "SoLuong";
            this.SoLuong.HeaderText = "Số lượng";
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.ReadOnly = true;
            this.SoLuong.Width = 80;
            // 
            // GiamGia
            // 
            this.GiamGia.DataPropertyName = "GiamGia";
            this.GiamGia.HeaderText = "Giảm giá (%)";
            this.GiamGia.Name = "GiamGia";
            this.GiamGia.ReadOnly = true;
            // 
            // grpListProduct
            // 
            this.grpListProduct.Controls.Add(this.dtChoose);
            this.grpListProduct.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.grpListProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpListProduct.Location = new System.Drawing.Point(3, 3);
            this.grpListProduct.Name = "grpListProduct";
            this.grpListProduct.Size = new System.Drawing.Size(429, 370);
            this.grpListProduct.TabIndex = 1;
            this.grpListProduct.TabStop = false;
            this.grpListProduct.Text = "Danh sách gọi món";
            // 
            // dtChoose
            // 
            this.dtChoose.AllowUserToAddRows = false;
            this.dtChoose.AllowUserToDeleteRows = false;
            this.dtChoose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtChoose.Location = new System.Drawing.Point(3, 23);
            this.dtChoose.Name = "dtChoose";
            this.dtChoose.RowHeadersVisible = false;
            this.dtChoose.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtChoose.Size = new System.Drawing.Size(423, 352);
            this.dtChoose.TabIndex = 0;
            // 
            // grpInfoInvoice
            // 
            this.grpInfoInvoice.Controls.Add(this.tableLayoutPanelInfoInvoice);
            this.grpInfoInvoice.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.grpInfoInvoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpInfoInvoice.Location = new System.Drawing.Point(3, 379);
            this.grpInfoInvoice.Name = "grpInfoInvoice";
            this.grpInfoInvoice.Size = new System.Drawing.Size(429, 143);
            this.grpInfoInvoice.TabIndex = 19;
            this.grpInfoInvoice.TabStop = false;
            this.grpInfoInvoice.Text = "Thông tin hóa đơn";
            // 
            // tableLayoutPanelInfoInvoice
            // 
            this.tableLayoutPanelInfoInvoice.ColumnCount = 2;
            this.tableLayoutPanelInfoInvoice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanelInfoInvoice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelInfoInvoice.Controls.Add(this.lblUser, 0, 0);
            this.tableLayoutPanelInfoInvoice.Controls.Add(this.txtUser, 1, 0);
            this.tableLayoutPanelInfoInvoice.Controls.Add(this.lblEmployess, 0, 2);
            this.tableLayoutPanelInfoInvoice.Controls.Add(this.txtEmployess, 1, 2);
            this.tableLayoutPanelInfoInvoice.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanelInfoInvoice.Name = "tableLayoutPanelInfoInvoice";
            this.tableLayoutPanelInfoInvoice.RowCount = 3;
            this.tableLayoutPanelInfoInvoice.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInfoInvoice.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelInfoInvoice.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInfoInvoice.Size = new System.Drawing.Size(423, 117);
            this.tableLayoutPanelInfoInvoice.TabIndex = 0;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblUser.Location = new System.Drawing.Point(3, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(124, 41);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Khách hàng:";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUser
            // 
            this.txtUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txtUser.Location = new System.Drawing.Point(133, 3);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(287, 27);
            this.txtUser.TabIndex = 1;
            // 
            // lblEmployess
            // 
            this.lblEmployess.AutoSize = true;
            this.lblEmployess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmployess.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblEmployess.Location = new System.Drawing.Point(3, 76);
            this.lblEmployess.Name = "lblEmployess";
            this.lblEmployess.Size = new System.Drawing.Size(124, 41);
            this.lblEmployess.TabIndex = 2;
            this.lblEmployess.Text = "Nhân viên:";
            this.lblEmployess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEmployess
            // 
            this.txtEmployess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmployess.Enabled = false;
            this.txtEmployess.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txtEmployess.Location = new System.Drawing.Point(133, 79);
            this.txtEmployess.Name = "txtEmployess";
            this.txtEmployess.Size = new System.Drawing.Size(287, 27);
            this.txtEmployess.TabIndex = 3;
            // 
            // grpPayment
            // 
            this.grpPayment.Controls.Add(this.tableLayoutPanelPayment);
            this.grpPayment.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.grpPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpPayment.Location = new System.Drawing.Point(3, 379);
            this.grpPayment.Name = "grpPayment";
            this.grpPayment.Size = new System.Drawing.Size(429, 143);
            this.grpPayment.TabIndex = 21;
            this.grpPayment.TabStop = false;
            this.grpPayment.Text = "Thanh toán";
            // 
            // tableLayoutPanelPayment
            // 
            this.tableLayoutPanelPayment.ColumnCount = 4;
            this.tableLayoutPanelPayment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelPayment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelPayment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelPayment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelPayment.Controls.Add(this.lblMoney, 0, 0);
            this.tableLayoutPanelPayment.Controls.Add(this.txtMoney, 1, 0);
            this.tableLayoutPanelPayment.Controls.Add(this.lblDiscount, 2, 0);
            this.tableLayoutPanelPayment.Controls.Add(this.txtDiscount, 3, 0);
            this.tableLayoutPanelPayment.Controls.Add(this.lblReceive, 0, 1);
            this.tableLayoutPanelPayment.Controls.Add(this.txtReceive, 1, 1);
            this.tableLayoutPanelPayment.Controls.Add(this.lblReturnPayment, 2, 1);
            this.tableLayoutPanelPayment.Controls.Add(this.txtReturnPayment, 3, 1);
            this.tableLayoutPanelPayment.Controls.Add(this.lblTotal, 0, 2);
            this.tableLayoutPanelPayment.Controls.Add(this.lblTotalMoney, 1, 2);
            this.tableLayoutPanelPayment.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanelPayment.Name = "tableLayoutPanelPayment";
            this.tableLayoutPanelPayment.RowCount = 3;
            this.tableLayoutPanelPayment.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelPayment.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelPayment.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPayment.Size = new System.Drawing.Size(423, 117);
            this.tableLayoutPanelPayment.TabIndex = 0;
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMoney.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblMoney.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblMoney.Location = new System.Drawing.Point(3, 0);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(78, 40);
            this.lblMoney.TabIndex = 1;
            this.lblMoney.Text = "Thành tiền:";
            this.lblMoney.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMoney
            // 
            this.txtMoney.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMoney.Enabled = false;
            this.txtMoney.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txtMoney.Location = new System.Drawing.Point(87, 3);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.ReadOnly = true;
            this.txtMoney.Size = new System.Drawing.Size(120, 27);
            this.txtMoney.TabIndex = 10;
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblDiscount.Location = new System.Drawing.Point(213, 0);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(78, 40);
            this.lblDiscount.TabIndex = 11;
            this.lblDiscount.Text = "Giảm giá:";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiscount.Enabled = false;
            this.txtDiscount.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txtDiscount.Location = new System.Drawing.Point(297, 3);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.ReadOnly = true;
            this.txtDiscount.Size = new System.Drawing.Size(123, 27);
            this.txtDiscount.TabIndex = 12;
            // 
            // lblReceive
            // 
            this.lblReceive.AutoSize = true;
            this.lblReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceive.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblReceive.Location = new System.Drawing.Point(3, 40);
            this.lblReceive.Name = "lblReceive";
            this.lblReceive.Size = new System.Drawing.Size(78, 40);
            this.lblReceive.TabIndex = 13;
            this.lblReceive.Text = "Khách đưa:";
            this.lblReceive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceive
            // 
            this.txtReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReceive.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txtReceive.Location = new System.Drawing.Point(87, 43);
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.Size = new System.Drawing.Size(120, 27);
            this.txtReceive.TabIndex = 14;
            // 
            // lblReturnPayment
            // 
            this.lblReturnPayment.AutoSize = true;
            this.lblReturnPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReturnPayment.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblReturnPayment.Location = new System.Drawing.Point(213, 40);
            this.lblReturnPayment.Name = "lblReturnPayment";
            this.lblReturnPayment.Size = new System.Drawing.Size(78, 40);
            this.lblReturnPayment.TabIndex = 15;
            this.lblReturnPayment.Text = "Tiền thừa:";
            this.lblReturnPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReturnPayment
            // 
            this.txtReturnPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReturnPayment.Enabled = false;
            this.txtReturnPayment.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txtReturnPayment.Location = new System.Drawing.Point(297, 43);
            this.txtReturnPayment.Name = "txtReturnPayment";
            this.txtReturnPayment.ReadOnly = true;
            this.txtReturnPayment.Size = new System.Drawing.Size(123, 27);
            this.txtReturnPayment.TabIndex = 16;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.tableLayoutPanelPayment.SetColumnSpan(this.lblTotal, 2);
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblTotal.Location = new System.Drawing.Point(3, 80);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(204, 37);
            this.lblTotal.TabIndex = 17;
            this.lblTotal.Text = "TỔNG CỘNG:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalMoney
            // 
            this.lblTotalMoney.AutoSize = true;
            this.tableLayoutPanelPayment.SetColumnSpan(this.lblTotalMoney, 2);
            this.lblTotalMoney.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalMoney.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTotalMoney.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblTotalMoney.Location = new System.Drawing.Point(213, 80);
            this.lblTotalMoney.Name = "lblTotalMoney";
            this.lblTotalMoney.Size = new System.Drawing.Size(207, 37);
            this.lblTotalMoney.TabIndex = 18;
            this.lblTotalMoney.Text = "0";
            this.lblTotalMoney.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDone
            // 
            this.btnDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnDone.ForeColor = System.Drawing.Color.White;
            this.btnDone.Location = new System.Drawing.Point(217, 3);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(209, 83);
            this.btnDone.TabIndex = 1;
            this.btnDone.Text = "XONG";
            this.btnDone.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(208, 83);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "HỦY";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnAddProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddProduct.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.Location = new System.Drawing.Point(3, 3);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(101, 83);
            this.btnAddProduct.TabIndex = 23;
            this.btnAddProduct.Text = "THÊM MÓN";
            this.btnAddProduct.UseVisualStyleBackColor = false;
            // 
            // btnDelProduct
            // 
            this.btnDelProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(42)))));
            this.btnDelProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelProduct.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnDelProduct.ForeColor = System.Drawing.Color.White;
            this.btnDelProduct.Location = new System.Drawing.Point(110, 3);
            this.btnDelProduct.Name = "btnDelProduct";
            this.btnDelProduct.Size = new System.Drawing.Size(101, 83);
            this.btnDelProduct.TabIndex = 24;
            this.btnDelProduct.Text = "XÓA MÓN";
            this.btnDelProduct.UseVisualStyleBackColor = false;
            // 
            // btnPrintInvoice
            // 
            this.btnPrintInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnPrintInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintInvoice.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnPrintInvoice.ForeColor = System.Drawing.Color.White;
            this.btnPrintInvoice.Location = new System.Drawing.Point(217, 3);
            this.btnPrintInvoice.Name = "btnPrintInvoice";
            this.btnPrintInvoice.Size = new System.Drawing.Size(101, 83);
            this.btnPrintInvoice.TabIndex = 25;
            this.btnPrintInvoice.Text = "IN HÓA ĐƠN";
            this.btnPrintInvoice.UseVisualStyleBackColor = false;
            // 
            // btnSaveDB
            // 
            this.btnSaveDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnSaveDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveDB.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSaveDB.ForeColor = System.Drawing.Color.White;
            this.btnSaveDB.Location = new System.Drawing.Point(324, 3);
            this.btnSaveDB.Name = "btnSaveDB";
            this.btnSaveDB.Size = new System.Drawing.Size(102, 83);
            this.btnSaveDB.TabIndex = 26;
            this.btnSaveDB.Text = "LƯU CSDL";
            this.btnSaveDB.UseVisualStyleBackColor = false;
            this.btnSaveDB.Click += new System.EventHandler(this.btnSaveDB_Click);
            // 
            // check
            // 
            this.check.ContainerControl = this;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelLeft, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelRight, 1, 0);
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(931, 634);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelLeft
            // 
            this.tableLayoutPanelLeft.ColumnCount = 1;
            this.tableLayoutPanelLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLeft.Controls.Add(this.grpSearchProduct, 0, 0);
            this.tableLayoutPanelLeft.Controls.Add(this.grpInfoInvoice, 0, 1);
            this.tableLayoutPanelLeft.Controls.Add(this.panelInvoiceButtons, 0, 2);
            this.tableLayoutPanelLeft.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            this.tableLayoutPanelLeft.RowCount = 3;
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelLeft.Size = new System.Drawing.Size(435, 628);
            this.tableLayoutPanelLeft.TabIndex = 0;
            // 
            // panelInvoiceButtons
            // 
            this.panelInvoiceButtons.ColumnCount = 4;
            this.panelInvoiceButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelInvoiceButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelInvoiceButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelInvoiceButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelInvoiceButtons.Controls.Add(this.btnAddProduct, 0, 0);
            this.panelInvoiceButtons.Controls.Add(this.btnDelProduct, 1, 0);
            this.panelInvoiceButtons.Controls.Add(this.btnPrintInvoice, 2, 0);
            this.panelInvoiceButtons.Controls.Add(this.btnSaveDB, 3, 0);
            this.panelInvoiceButtons.Location = new System.Drawing.Point(3, 536);
            this.panelInvoiceButtons.Name = "panelInvoiceButtons";
            this.panelInvoiceButtons.RowCount = 1;
            this.panelInvoiceButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelInvoiceButtons.Size = new System.Drawing.Size(429, 89);
            this.panelInvoiceButtons.TabIndex = 20;
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRight.Controls.Add(this.grpListProduct, 0, 0);
            this.tableLayoutPanelRight.Controls.Add(this.grpPayment, 0, 1);
            this.tableLayoutPanelRight.Controls.Add(this.panelActionButtons, 0, 2);
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(468, 3);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 3;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(460, 628);
            this.tableLayoutPanelRight.TabIndex = 1;
            // 
            // panelActionButtons
            // 
            this.panelActionButtons.ColumnCount = 2;
            this.panelActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelActionButtons.Controls.Add(this.btnCancel, 0, 0);
            this.panelActionButtons.Controls.Add(this.btnDone, 1, 0);
            this.panelActionButtons.Location = new System.Drawing.Point(3, 536);
            this.panelActionButtons.Name = "panelActionButtons";
            this.panelActionButtons.RowCount = 1;
            this.panelActionButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelActionButtons.Size = new System.Drawing.Size(429, 89);
            this.panelActionButtons.TabIndex = 22;
            // 
            // frm_Order
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(932, 636);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "frm_Order";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frm_Order_Load);
            this.grpSearchProduct.ResumeLayout(false);
            this.tableLayoutPanelSearchContainer.ResumeLayout(false);
            this.tableLayoutPanelSearch.ResumeLayout(false);
            this.tableLayoutPanelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtSearch)).EndInit();
            this.grpListProduct.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtChoose)).EndInit();
            this.grpInfoInvoice.ResumeLayout(false);
            this.tableLayoutPanelInfoInvoice.ResumeLayout(false);
            this.tableLayoutPanelInfoInvoice.PerformLayout();
            this.grpPayment.ResumeLayout(false);
            this.tableLayoutPanelPayment.ResumeLayout(false);
            this.tableLayoutPanelPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.check)).EndInit();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelLeft.ResumeLayout(false);
            this.panelInvoiceButtons.ResumeLayout(false);
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.panelActionButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSearchProduct;
        private System.Windows.Forms.GroupBox grpListProduct;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.GroupBox grpInfoInvoice;
        private System.Windows.Forms.GroupBox grpPayment;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.TextBox txtMoney;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblReceive;
        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.Label lblReturnPayment;
        private System.Windows.Forms.TextBox txtReturnPayment;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnDelProduct;
        private System.Windows.Forms.Button btnPrintInvoice;
        private System.Windows.Forms.Button btnSaveDB;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalMoney;
        private System.Windows.Forms.DataGridView dtSearch;
        private System.Windows.Forms.DataGridView dtChoose;
        private System.Windows.Forms.ErrorProvider check;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiaTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiamGia;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInfoInvoice;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPayment;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLeft;
        private System.Windows.Forms.TableLayoutPanel panelInvoiceButtons;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.TableLayoutPanel panelActionButtons;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSearchContainer;
        private System.Windows.Forms.Label lblEmployess;
        private System.Windows.Forms.TextBox txtEmployess;
    }
}