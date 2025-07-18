namespace QLCHBanGaRan.UCFunction
{
    partial class UC_TimeSheetEmployee
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblClock = new System.Windows.Forms.Label();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.btnXinNghi = new System.Windows.Forms.Button();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.dgvTimesheet = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnBack = new System.Windows.Forms.Button();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.grpTimesheet = new System.Windows.Forms.GroupBox();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimesheet)).BeginInit();
            this.grpActions.SuspendLayout();
            this.grpTimesheet.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = true;
            this.lblClock.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblClock.Location = new System.Drawing.Point(367, 23);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(210, 25);
            this.lblClock.TabIndex = 70;
            this.lblClock.Text = "23:05:00 - 17/07/2025";
            this.lblClock.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnCheckIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckIn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckIn.ForeColor = System.Drawing.Color.White;
            this.btnCheckIn.Location = new System.Drawing.Point(20, 38);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(125, 44);
            this.btnCheckIn.TabIndex = 0;
            this.btnCheckIn.Text = "Chấm công";
            this.btnCheckIn.UseVisualStyleBackColor = false;
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // btnXinNghi
            // 
            this.btnXinNghi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.btnXinNghi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXinNghi.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXinNghi.ForeColor = System.Drawing.Color.White;
            this.btnXinNghi.Location = new System.Drawing.Point(190, 38);
            this.btnXinNghi.Name = "btnXinNghi";
            this.btnXinNghi.Size = new System.Drawing.Size(125, 44);
            this.btnXinNghi.TabIndex = 1;
            this.btnXinNghi.Text = "Xin nghỉ";
            this.btnXinNghi.UseVisualStyleBackColor = false;
            this.btnXinNghi.Click += new System.EventHandler(this.btnXinNghi_Click);
            // 
            // txtLyDo
            // 
            this.txtLyDo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLyDo.Location = new System.Drawing.Point(20, 120);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(840, 60);
            this.txtLyDo.TabIndex = 2;
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Font = new System.Drawing.Font("Segoe UI", 12.5F);
            this.lblLyDo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblLyDo.Location = new System.Drawing.Point(20, 92);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(116, 23);
            this.lblLyDo.TabIndex = 3;
            this.lblLyDo.Text = "Lý do xin nghỉ";
            // 
            // dgvTimesheet
            // 
            this.dgvTimesheet.AllowUserToAddRows = false;
            this.dgvTimesheet.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvTimesheet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTimesheet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTimesheet.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTimesheet.BackgroundColor = System.Drawing.Color.White;
            this.dgvTimesheet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTimesheet.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTimesheet.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTimesheet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTimesheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTimesheet.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTimesheet.EnableHeadersVisualStyles = false;
            this.dgvTimesheet.GridColor = System.Drawing.Color.LightGray;
            this.dgvTimesheet.Location = new System.Drawing.Point(6, 28);
            this.dgvTimesheet.Name = "dgvTimesheet";
            this.dgvTimesheet.ReadOnly = true;
            this.dgvTimesheet.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvTimesheet.RowHeadersVisible = false;
            this.dgvTimesheet.RowHeadersWidth = 50;
            this.dgvTimesheet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTimesheet.Size = new System.Drawing.Size(850, 350);
            this.dgvTimesheet.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = global::QLCHBanGaRan.Properties.Resources.left_round_32px;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(32, 32);
            this.btnBack.TabIndex = 69;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // grpActions
            // 
            this.grpActions.Controls.Add(this.btnCheckIn);
            this.grpActions.Controls.Add(this.btnXinNghi);
            this.grpActions.Controls.Add(this.txtLyDo);
            this.grpActions.Controls.Add(this.lblLyDo);
            this.grpActions.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpActions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpActions.Location = new System.Drawing.Point(30, 60);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(870, 190);
            this.grpActions.TabIndex = 5;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "Thao tác";
            // 
            // grpTimesheet
            // 
            this.grpTimesheet.Controls.Add(this.dgvTimesheet);
            this.grpTimesheet.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTimesheet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.grpTimesheet.Location = new System.Drawing.Point(30, 260);
            this.grpTimesheet.Name = "grpTimesheet";
            this.grpTimesheet.Size = new System.Drawing.Size(870, 390);
            this.grpTimesheet.TabIndex = 6;
            this.grpTimesheet.TabStop = false;
            this.grpTimesheet.Text = "Lịch sử chấm công";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(0)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(38, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(241, 21);
            this.lblTitle.TabIndex = 71;
            this.lblTitle.Text = "CHẤM CÔNG NHÂN VIÊN";
            // 
            // UC_TimeSheetEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpTimesheet);
            this.Controls.Add(this.grpActions);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblClock);
            this.Name = "UC_TimeSheetEmployee";
            this.Size = new System.Drawing.Size(930, 660);
            this.Load += new System.EventHandler(this.UC_TimeSheetEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimesheet)).EndInit();
            this.grpActions.ResumeLayout(false);
            this.grpActions.PerformLayout();
            this.grpTimesheet.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.Button btnXinNghi;
        private System.Windows.Forms.TextBox txtLyDo;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.DataGridView dgvTimesheet;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.GroupBox grpTimesheet;
        private System.Windows.Forms.Label lblTitle;
    }
}