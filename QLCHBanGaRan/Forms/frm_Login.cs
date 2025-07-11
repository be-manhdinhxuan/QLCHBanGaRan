﻿using MaterialSkin;
using MaterialSkin.Controls;
using QLCHBanGaRan.Forms;
using QLCHBanGaRan.lib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLCHBanGaRan
{
    public partial class frm_Login : MaterialForm
    {
        public frm_Login()
        {
            InitializeComponent();

            // Tùy chỉnh thêm nếu cần
            txtPassword.UseSystemPasswordChar = true;

            // Đặt TabIndex hợp lý
            txtTenDangNhap.TabIndex = 0;
            txtPassword.TabIndex = 1;
            btnLogin.TabIndex = 2;

            // Xử lý tab thủ công
            txtTenDangNhap.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Tab && !e.Shift)
                {
                    e.SuppressKeyPress = true;
                    this.SelectNextControl(txtTenDangNhap, true, true, true, true);
                }
            };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát chương trình?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ username và password!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maND = cls_EmployeeManagement.CheckLogin(username, password);

            if (maND != "ERROR")
            {
                // Đăng nhập thành công
                MessageBox.Show("Đăng nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                frm_Main mainForm = new frm_Main(maND); // Truyền maND vào form chính nếu cần
                mainForm.Show();
            }
            else
            {
                // Đăng nhập thất bại
                MessageBox.Show("Username hoặc password không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void picEyeToggle_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = false;
                picEyeToggle.Image = global::QLCHBanGaRan.Properties.Resources.eye_open;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                picEyeToggle.Image = global::QLCHBanGaRan.Properties.Resources.eye_closed;
            }
        }

        private void pnlLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPosition = Cursor.Position;
                lastFormPosition = this.Location;
            }
        }

        private void pnlLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentCursorPosition = Cursor.Position;
                int deltaX = currentCursorPosition.X - lastCursorPosition.X;
                int deltaY = currentCursorPosition.Y - lastCursorPosition.Y;
                this.Location = new Point(lastFormPosition.X + deltaX, lastFormPosition.Y + deltaY);
            }
        }

        private void pnlLeft_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void pnlRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPosition = Cursor.Position;
                lastFormPosition = this.Location;
            }
        }

        private void pnlRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentCursorPosition = Cursor.Position;
                int deltaX = currentCursorPosition.X - lastCursorPosition.X;
                int deltaY = currentCursorPosition.Y - lastCursorPosition.Y;
                this.Location = new Point(lastFormPosition.X + deltaX, lastFormPosition.Y + deltaY);
            }
        }

        private void pnlRight_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
    }
}