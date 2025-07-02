using QLCHBanGaRan.lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHBanGaRan.UCFunction
{
    public partial class UC_OfficeManager : UserControl
    {
        private int check = 0;
        private string _maNV = null;

        public UC_OfficeManager()
        {
            InitializeComponent();
        }

        private IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                           .Concat(controls)
                           .Where(c => c.GetType() == type);
        }

        private void _sttButton(bool add, bool edit, bool delete, bool update, bool cancel, bool grpinfo)
        {
            btnThem.Enabled = add;
            btnSua.Enabled = edit;
            btnXoa.Enabled = delete;
            btnCapNhat.Enabled = update;
            btnHuyBo.Enabled = cancel;
            grpThongTin.Enabled = grpinfo;
        }

        private void UC_OfficeManager_Load(object sender, EventArgs e)
        {
            _sttButton(true, false, true, false, false, false);
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            dtList.DataSource = cls_EmployeeTitleManagement.GetEmployeeTitles();
            cmbChucDanh.DataSource = cls_EmployeeTitleManagement.GetTitles();
            cmbChucDanh.ValueMember = "MaChucDanh";
            cmbChucDanh.DisplayMember = "TenChucDanh";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _sttButton(true, false, true, false, false, false);
            Forms.frm_Main.Instance.pnlContainer.Controls["UC_Personnel"].BringToFront();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            check = 1;
            _sttButton(false, false, false, true, true, true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            check = 2;
            _sttButton(false, false, false, true, true, true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa chức danh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (cls_EmployeeTitleManagement.DeleteEmployeeTitle(_maNV))
                {
                    MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _sttButton(true, false, true, false, false, false);
                    dtList.DataSource = cls_EmployeeTitleManagement.GetEmployeeTitles();
                    _maNV = null;
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra và thử lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                var getChildControls = GetAll(this, typeof(ComboBox));
                var listOfErrors = getChildControls.Select(c => errorProvider.GetError(c))
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .ToList();
                MessageBox.Show("Vui lòng kiểm tra lại thông tin:\n - " + string.Join("\n - ", listOfErrors.ToArray()), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (check == 1)
                {
                    bool insertNVCD = cls_EmployeeTitleManagement.InsertEmployeeTitle(cmbTenNV.SelectedValue.ToString(), Convert.ToInt32(cmbChucDanh.SelectedValue));
                    if (insertNVCD)
                    {
                        MessageBox.Show("Thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _sttButton(true, false, true, false, false, false);
                        dtList.DataSource = cls_EmployeeTitleManagement.GetEmployeeTitles();
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra và thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool updateNVCD = cls_EmployeeTitleManagement.UpdateEmployeeTitle(cmbTenNV.SelectedValue.ToString(), Convert.ToInt32(cmbChucDanh.SelectedValue));
                    if (updateNVCD)
                    {
                        MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _sttButton(true, false, true, false, false, false);
                        dtList.DataSource = cls_EmployeeTitleManagement.GetEmployeeTitles();
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra. Vui lòng kiểm tra và thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            _sttButton(true, false, true, false, false, false);
            errorProvider.Clear();
        }

        private void cmbTenNV_Validating(object sender, CancelEventArgs e)
        {
            if (cmbTenNV.SelectedValue == null)
            {
                errorProvider.SetError(cmbTenNV, "Vui lòng chọn tên nhân viên.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cmbTenNV, "");
            }
        }

        private void cmbChucDanh_Validating(object sender, CancelEventArgs e)
        {
            if (cmbChucDanh.SelectedValue == null)
            {
                errorProvider.SetError(cmbChucDanh, "Vui lòng chọn chức danh.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(cmbChucDanh, "");
            }
        }

        private void dtList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cls_EmployeeTitleManagement.GetEmployeeTitles().Rows.Count > 0)
            {
                int index = dtList.CurrentCell.RowIndex;
                _maNV = dtList.Rows[index].Cells["MaNV"].Value.ToString();
                cmbTenNV.SelectedValue = dtList.Rows[index].Cells["MaNV"].Value.ToString();
                cmbChucDanh.SelectedValue = dtList.Rows[index].Cells["MaChucDanh"].Value; // Giả định cột MaChucDanh tồn tại
                btnSua.Enabled = true;
            }
        }
    }
}