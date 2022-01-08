using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using LakDeranaHotel.Model;
using LakDeranaHotel.Controller;
using System.Data.SqlClient;

namespace LakDeranaHotel.View
{
    public partial class ManageCustomer : Form
    {
        Customer Customer = new Customer();
        string Role = "";

        public ManageCustomer(string role)
        {
            InitializeComponent();
            Role = role;
        }

        private void ManageCustomer_Load(object sender, EventArgs e)
        {
            showDataTable();
            roleRestriction();
        }

        private void roleRestriction()
        {
            if (Role == "Admin")
            {
                lblCourse.Visible = false;
                dgvCourses.Visible = false;
            }
            else
            {
                txtIdNo.Enabled = false;
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                txtNic.Enabled = false;
                txtPhone.Enabled = false;
                txtAddress.Enabled = false;
                btnDelete.Visible = false;
                btnUpdate.Visible = false;
                btnUpload.Visible = false;
                dtpDOB.Enabled = false;
            }

        }

        public void showDataTable()
        {
            try
            {
                dgvStudent.DataSource = Customer.getCustomerList();
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn = (DataGridViewImageColumn)dgvStudent.Columns[8];
                imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

                //if (Role != "Admin")
                //{
                //    dgvCourses.DataSource = Course.getList(new SqlCommand("SELECT  CourseId,CourseName FROM Courses"));
                //    dgvCourses.Columns[0].Visible = false;

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void dgvStudent_Click(object sender, EventArgs e)
        {
            txtIdNo.Text = dgvStudent.CurrentRow.Cells[0].Value.ToString(); 
            txtFirstName.Text = dgvStudent.CurrentRow.Cells[1].Value.ToString(); 
            txtLastName.Text = dgvStudent.CurrentRow.Cells[2].Value.ToString(); 
            dtpDOB.Value = (DateTime)dgvStudent.CurrentRow.Cells[3].Value;
            txtAddress.Text = dgvStudent.CurrentRow.Cells[4].Value.ToString();
            txtNic.Text = dgvStudent.CurrentRow.Cells[5].Value.ToString();
            txtPhone.Text = dgvStudent.CurrentRow.Cells[6].Value.ToString();
            if(dgvStudent.CurrentRow.Cells[7].Value.ToString() == "Male" )
            {
                chkMale.Checked = true;
            }
            else
            {
                chkFemale.Checked = true;
            }
            byte[]img = (byte[])dgvStudent.CurrentRow.Cells[8].Value;
            MemoryStream stream = new MemoryStream(img);
            pBoxStudent.Image = Image.FromStream(stream);



        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtIdNo.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtNic.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            dtpDOB.Value = DateTime.Now;
            pBoxStudent.Image = null;
        }

        bool Validation()
        {
            if ((txtFirstName.Text == "") || (txtLastName.Text == "") || (txtAddress.Text == "") || (txtNic.Text == "") || (txtPhone.Text == "") || (dtpDOB.Value == null) || (chkFemale.Checked == false && chkMale.Checked == false))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if ((DateTime.Now.Year - dtpDOB.Value.Year) < 10 || (DateTime.Now.Year - dtpDOB.Value.Year) > 60)
                {
                    MessageBox.Show("The Customer age must be between 10 and 60 ", "Invalid Birthday", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!Validation())
                {
                    MessageBox.Show("The Fileds can't be empty", "Empty Fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Regex rgx = new Regex(@"^[0-9]*$");

                if (!rgx.IsMatch(txtPhone.Text))
                {
                    MessageBox.Show("Phone number not in correct format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }


                CustomerDAO customer = new CustomerDAO();
                customer.idNo = int.Parse(txtIdNo.Text);
                customer.firstName = txtFirstName.Text;
                customer.lastname = txtLastName.Text;
                customer.address = txtAddress.Text;
                customer.DOB = dtpDOB.Value;
                customer.phone = int.Parse(txtPhone.Text);
                customer.NIC = txtNic.Text;
                customer.gender = chkMale.Checked ? "Male" : "Female";
                if (pBoxStudent.Image != null)
                {
                    MemoryStream stream = new MemoryStream();
                    pBoxStudent.Image.Save(stream, pBoxStudent.Image.RawFormat);
                    customer.image = stream.ToArray();
                }
                else
                {
                    if (customer.gender == "Male")
                    {
                        pBoxStudent.Image = Image.FromFile("D:\\Projcts\\SchoolManagement\\E-pupil\\E-pupil\\Resources\\man.png");
                    }
                    else
                    {
                        pBoxStudent.Image = Image.FromFile("D:\\Projcts\\SchoolManagement\\E-pupil\\E-pupil\\Resources\\woman.png");
                    }
                    MemoryStream stream = new MemoryStream();
                    pBoxStudent.Image.Save(stream, pBoxStudent.Image.RawFormat);
                    customer.image = stream.ToArray();
                }

                if (Customer.updateCustomer(customer))
                {
                    showDataTable();
                    btnClear_Click(sender, e);
                    MessageBox.Show("The customer update successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error occur during update ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure delete customer?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Customer.DeleteCustomer(txtIdNo.Text);
                    MessageBox.Show("The customer Delete successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
                pBoxStudent.Image = Image.FromFile(opf.FileName);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvStudent.DataSource = Customer.searchCustomer(txtSearch.Text);
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn = (DataGridViewImageColumn)dgvStudent.Columns[8];
                imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
