namespace QLCHBanGaRan.Forms
{
    partial class frm_Connect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Connect));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblServerName = new MaterialSkin.Controls.MaterialLabel();
            this.label3 = new MaterialSkin.Controls.MaterialLabel();
            this.lblUserName = new MaterialSkin.Controls.MaterialLabel();
            this.lblPassword = new MaterialSkin.Controls.MaterialLabel();
            this.lblSelectDB = new MaterialSkin.Controls.MaterialLabel();
            this.txtUserName = new System.Windows.Forms.TextBox(); // Thay MaterialTextBox2
            this.txtPassword = new System.Windows.Forms.TextBox(); // Thay MaterialTextBox2
            this.txtServerName = new System.Windows.Forms.TextBox(); // Thay MaterialTextBox2
            this.cmbAuthor = new System.Windows.Forms.ComboBox(); // Thay MaterialComboBox
            this.cmbSelectDB = new System.Windows.Forms.ComboBox(); // Thay MaterialComboBox
            this.btnGetDB = new System.Windows.Forms.Button(); // Thay MaterialButton
            this.btnConnect = new System.Windows.Forms.Button(); // Thay MaterialButton
            this.btnCancel = new System.Windows.Forms.Button(); // Thay MaterialButton
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(434, 28);
            this.panelHeader.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::QLCHBanGaRan.Properties.Resources.btn_close;
            this.btnClose.Location = new System.Drawing.Point(402, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(152, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "CONNECT TO SERVER";
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(4, 411);
            this.panelLeft.TabIndex = 5;
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(430, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(4, 411);
            this.panelRight.TabIndex = 6;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(4, 407);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(426, 4);
            this.panelFooter.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::QLCHBanGaRan.Properties.Resources.sqlsever2;
            this.pictureBox1.Location = new System.Drawing.Point(61, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(314, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // Xóa BackColor để nền trong suốt
            // 
            // lblSeverName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.Depth = 0;
            this.lblServerName.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblServerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblServerName.Location = new System.Drawing.Point(12, 138);
            this.lblServerName.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(89, 19);
            this.lblServerName.TabIndex = 9;
            this.lblServerName.Text = "Server name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Depth = 0;
            this.label3.Font = new System.Drawing.Font("Roboto", 11F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(12, 171);
            this.label3.MouseState = MaterialSkin.MouseState.HOVER;
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "Authentication:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Depth = 0;
            this.lblUserName.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblUserName.Location = new System.Drawing.Point(39, 204);
            this.lblUserName.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(82, 19);
            this.lblUserName.TabIndex = 12;
            this.lblUserName.Text = "User name:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Depth = 0;
            this.lblPassword.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblPassword.Location = new System.Drawing.Point(39, 234);
            this.lblPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(73, 19);
            this.lblPassword.TabIndex = 13;
            this.lblPassword.Text = "Password:";
            // 
            // lblSelectDB
            // 
            this.lblSelectDB.AutoSize = true;
            this.lblSelectDB.Depth = 0;
            this.lblSelectDB.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblSelectDB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSelectDB.Location = new System.Drawing.Point(12, 318);
            this.lblSelectDB.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSelectDB.Name = "lblSelectDB";
            this.lblSelectDB.Size = new System.Drawing.Size(119, 19);
            this.lblSelectDB.TabIndex = 14;
            this.lblSelectDB.Text = "Select Database:";
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Roboto", 11F);
            this.txtUserName.Location = new System.Drawing.Point(174, 201);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(248, 27);
            this.txtUserName.TabIndex = 15;
            this.txtUserName.Text = "sa";
            this.txtUserName.Enabled = true; // Đảm bảo luôn có thể nhập
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Roboto", 11F);
            this.txtPassword.Location = new System.Drawing.Point(174, 234);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(248, 27);
            this.txtPassword.TabIndex = 16;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Enabled = true; // Đảm bảo luôn có thể nhập
            // 
            // txtServerName
            // 
            this.txtServerName.Font = new System.Drawing.Font("Roboto", 11F);
            this.txtServerName.Location = new System.Drawing.Point(174, 135);
            this.txtServerName.MaxLength = 50;
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(248, 27);
            this.txtServerName.TabIndex = 17;
            this.txtServerName.TextChanged += new System.EventHandler(this.txtServerName_TextChanged);
            this.txtServerName.Enabled = true; // Đảm bảo luôn có thể nhập
            // 
            // cmbAuthor
            // 
            this.cmbAuthor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAuthor.Font = new System.Drawing.Font("Roboto", 11F);
            this.cmbAuthor.FormattingEnabled = true;
            this.cmbAuthor.Location = new System.Drawing.Point(174, 167);
            this.cmbAuthor.Name = "cmbAuthor";
            this.cmbAuthor.Size = new System.Drawing.Size(248, 28);
            this.cmbAuthor.TabIndex = 18;
            this.cmbAuthor.SelectedValueChanged += new System.EventHandler(this.cmbAuthor_SelectedValueChanged);
            // 
            // cmbSelectDB
            // 
            this.cmbSelectDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectDB.Font = new System.Drawing.Font("Roboto", 11F);
            this.cmbSelectDB.FormattingEnabled = true;
            this.cmbSelectDB.Location = new System.Drawing.Point(174, 315);
            this.cmbSelectDB.Name = "cmbSelectDB";
            this.cmbSelectDB.Size = new System.Drawing.Size(248, 28);
            this.cmbSelectDB.TabIndex = 20;
            // 
            // btnGetDB
            // 
            this.btnGetDB.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnGetDB.Location = new System.Drawing.Point(237, 269);
            this.btnGetDB.Name = "btnGetDB";
            this.btnGetDB.Size = new System.Drawing.Size(184, 33);
            this.btnGetDB.TabIndex = 80;
            this.btnGetDB.Text = "Lấy danh sách Database";
            this.btnGetDB.UseVisualStyleBackColor = true;
            this.btnGetDB.Click += new System.EventHandler(this.btnGetDB_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnConnect.Location = new System.Drawing.Point(208, 363);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(103, 33);
            this.btnConnect.TabIndex = 81;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(319, 363);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 33);
            this.btnCancel.TabIndex = 82;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frm_Connect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(434, 411);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnGetDB);
            this.Controls.Add(this.cmbSelectDB);
            this.Controls.Add(this.cmbAuthor);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblSelectDB);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblServerName);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::QLCHBanGaRan.Properties.Resources.logo;
            this.Name = "frm_Connect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frm_Connect_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialLabel lblServerName;
        private MaterialSkin.Controls.MaterialLabel label3;
        private MaterialSkin.Controls.MaterialLabel lblUserName;
        private MaterialSkin.Controls.MaterialLabel lblPassword;
        private MaterialSkin.Controls.MaterialLabel lblSelectDB;
        private System.Windows.Forms.TextBox txtUserName; // Thay MaterialTextBox2
        private System.Windows.Forms.TextBox txtPassword; // Thay MaterialTextBox2
        private System.Windows.Forms.TextBox txtServerName; // Thay MaterialTextBox2
        private System.Windows.Forms.ComboBox cmbAuthor; // Thay MaterialComboBox
        private System.Windows.Forms.ComboBox cmbSelectDB; // Thay MaterialComboBox
        private System.Windows.Forms.Button btnGetDB; // Thay MaterialButton
        private System.Windows.Forms.Button btnConnect; // Thay MaterialButton
        private System.Windows.Forms.Button btnCancel; // Thay MaterialButton
    }
}