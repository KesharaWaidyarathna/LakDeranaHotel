using LakDeranaHotel;
using LakDeranaHotel.Controller;
using LakDeranaHotel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace E_pupi
{
    public partial class Register : Form
    {
        Customer Customer = new Customer();

        public Register()
        {
            InitializeComponent();
            showDataTable();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if(opf.ShowDialog()== DialogResult.OK)
                 pBoxStudent.Image = Image.FromFile(opf.FileName);

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if ((DateTime.Now.Year- dtpDOB.Value.Year) < 10 || (DateTime.Now.Year- dtpDOB.Value.Year) > 60)
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
                    MessageBox.Show("Phone number not in valid  format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }


                CustomerDAO customer = new CustomerDAO();
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
                    if(customer.gender== "Male")
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

                if (Customer.instertCustomer(customer))
                {
                    showDataTable();
                    btnClear_Click(sender, e);
                    MessageBox.Show("The customer inquiry save successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error occur during saving ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtNic.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            dtpDOB.Value = DateTime.Now;
            pBoxStudent.Image = null;
        }

        private void chkMale_CheckedChanged(object sender, EventArgs e)
        {
            chkFemale.Checked = false;
        }

        private void chkFemale_CheckedChanged(object sender, EventArgs e)
        {
            chkMale.Checked = false;
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
