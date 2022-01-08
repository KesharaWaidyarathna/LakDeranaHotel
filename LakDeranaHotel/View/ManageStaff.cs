using LakDeranaHotel.Controller;
using LakDeranaHotel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LakDeranaHotel.View
{
    public partial class ManageStaff : Form
    {
        StaffController StaffController = new StaffController();
        public ManageStaff()
        {
            InitializeComponent();
        }

        private void ManageStaff_Load(object sender, EventArgs e)
        {
            showDataTable();
            txtPassword.PasswordChar = '*';
        }

        public void showDataTable()
        {
            try
            {
                dgvStaff.DataSource = StaffController.getStaffList();
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                dgvStaff.Columns[9].Visible = false;
                dgvStaff.Columns[10].Visible = false;
                imgColumn = (DataGridViewImageColumn)dgvStaff.Columns[11];
                imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
                pBoxstaff.Image = Image.FromFile(opf.FileName);

        }

        bool Validation()
        {

            if ((txtFirstName.Text == "") || (txtLastName.Text == "") || (txtAddress.Text == "") || (txtNic.Text == "") || (txtPhone.Text == "") || (dtpDOB.Value == null) || (txtUserName.Text == "") || (txtPassword.Text == "") || (chkFemale.Checked == false && chkMale.Checked == false) || (chkStaff.Checked == false && chkAdmin.Checked == false && chkBar.Checked == false && chkManager.Checked == false))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtNic.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtUserName.Clear();
            txtPassword.Clear();
            dtpDOB.Value = DateTime.Now;
            pBoxstaff.Image = null;
        }

        private void chkMale_Click(object sender, EventArgs e)
        {
            chkFemale.Checked = false;
            chkMale.Checked = true;
        }

        private void chkFemale_Click(object sender, EventArgs e)
        {
            chkFemale.Checked = true;
            chkMale.Checked = false;
        }

        private void chkAdmin_Click(object sender, EventArgs e)
        {
            chkAdmin.Checked = true;
            chkStaff.Checked = false;
            chkBar.Checked = false;
            chkManager.Checked = false;
        }

        private void chkStaff_Click(object sender, EventArgs e)
        {
            chkAdmin.Checked = false;
            chkStaff.Checked = true;
            chkBar.Checked = false;
            chkManager.Checked = false;
        }

        private void chkBar_Click(object sender, EventArgs e)
        {
            chkAdmin.Checked = false;
            chkStaff.Checked = false;
            chkBar.Checked = true;
            chkManager.Checked = false;

        }

        private void chkManager_Click(object sender, EventArgs e)
        {
            chkAdmin.Checked = false;
            chkStaff.Checked = false;
            chkBar.Checked = false;
            chkManager.Checked = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvStaff.DataSource = StaffController.searchStaff(txtSearch.Text);
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                dgvStaff.Columns[9].Visible = false;
                dgvStaff.Columns[10].Visible = false;
                imgColumn = (DataGridViewImageColumn)dgvStaff.Columns[11];
                imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if ((DateTime.Now.Year - dtpDOB.Value.Year) < 10 || (DateTime.Now.Year - dtpDOB.Value.Year) > 60)
                {
                    MessageBox.Show("The staff age must be between 10 and 60 ", "Invalid Birthday", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!Validation())
                {
                    MessageBox.Show("The Fileds can't be empty", "Empty Fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Regex rgx = new Regex(@"^[0-9]{10}$");

                if (!rgx.IsMatch(txtPhone.Text))
                {
                    MessageBox.Show("Phone number not in correct format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                rgx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
                if (!rgx.IsMatch(txtPassword.Text))
                {
                    MessageBox.Show("Password not meet the requiremnts", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }

                if (StaffController.IsUsernameExists(txtUserName.Text,txtEmployeeId.Text))
                {
                    MessageBox.Show("Username already exists, Please try differnt one", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string role = "";
                StaffDAO staff = new StaffDAO();
                if (chkAdmin.Checked)
                {
                    role = "Admin";
                }
                else if (chkStaff.Checked)
                {
                    role = "Staff";
                }
                else if (chkBar.Checked)
                {
                    role = "Bar";
                }
                else if (chkManager.Checked)
                {
                    role = "Manager";
                }
                staff.EmployeeId = int.Parse(txtEmployeeId.Text);
                staff.EmployeeType = role;
                staff.FirstName = txtFirstName.Text;
                staff.LastName = txtLastName.Text;
                staff.address = txtAddress.Text;
                staff.DOB = dtpDOB.Value;
                staff.phone = int.Parse(txtPhone.Text);
                staff.NIC = txtNic.Text;
                staff.gender = chkMale.Checked ? "Male" : "Female";
                staff.Username = txtUserName.Text;
                staff.Password = txtPassword.Text;
                if (pBoxstaff.Image != null)
                {
                    MemoryStream stream = new MemoryStream();
                    pBoxstaff.Image.Save(stream, pBoxstaff.Image.RawFormat);
                    staff.image = stream.ToArray();
                }
                else
                {
                    if (staff.gender == "Male")
                    {
                        pBoxstaff.Image = Image.FromFile("D:\\Projcts\\SchoolManagement\\E-pupil\\E-pupil\\Resources\\man.png");
                    }
                    else
                    {
                        pBoxstaff.Image = Image.FromFile("D:\\Projcts\\SchoolManagement\\E-pupil\\E-pupil\\Resources\\woman.png");
                    }
                    MemoryStream stream = new MemoryStream();
                    pBoxstaff.Image.Save(stream, pBoxstaff.Image.RawFormat);
                    staff.image = stream.ToArray();
                }

                if (StaffController.updatestaff(staff))
                {
                    showDataTable();
                    btnClear_Click(sender, e);
                    MessageBox.Show("The staff member deatils updated successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error occur during saving ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtEmployeeId.Text = dgvStaff.CurrentRow.Cells[0].Value.ToString();
            if (dgvStaff.CurrentRow.Cells[1].Value.ToString() == "Admin")
            {
                chkAdmin_Click(sender,e);
            }
            else if (dgvStaff.CurrentRow.Cells[1].Value.ToString() == "Staff")
            {
                chkStaff_Click(sender, e);
            }
            else if (dgvStaff.CurrentRow.Cells[1].Value.ToString() == "Bar")
            {
                chkBar_Click(sender, e);
            }
            else if (dgvStaff.CurrentRow.Cells[1].Value.ToString() == "Manager")
            {
                chkManager_Click(sender, e);
            }
            txtFirstName.Text = dgvStaff.CurrentRow.Cells[2].Value.ToString(); 
            txtLastName.Text = dgvStaff.CurrentRow.Cells[3].Value.ToString(); 
            dtpDOB.Value = (DateTime)dgvStaff.CurrentRow.Cells[4].Value;
            txtAddress.Text = dgvStaff.CurrentRow.Cells[5].Value.ToString();
            txtNic.Text = dgvStaff.CurrentRow.Cells[6].Value.ToString();
            txtPhone.Text = dgvStaff.CurrentRow.Cells[7].Value.ToString();
            if(dgvStaff.CurrentRow.Cells[8].Value.ToString() == "Male" )
            {
                chkMale.Checked = true;
                chkFemale.Checked = false;
            }
            else
            {
                chkFemale.Checked = true;
                chkMale.Checked = false;
            }
            txtUserName.Text = dgvStaff.CurrentRow.Cells[9].Value.ToString();
            txtPassword.Text = dgvStaff.CurrentRow.Cells[10].Value.ToString();
            byte[]img = (byte[])dgvStaff.CurrentRow.Cells[11].Value;
            MemoryStream stream = new MemoryStream(img);
            pBoxstaff.Image = Image.FromStream(stream);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure delete Employee?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    StaffController.DeleteEmployee(txtEmployeeId.Text);
                    MessageBox.Show("The Employee Delete successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return;
                }
                showDataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            btnClear_Click(sender, e);

        }
    }
}
