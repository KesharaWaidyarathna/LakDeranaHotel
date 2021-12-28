using DGVPrinterHelper;
using LakDeranaHotel.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LakDeranaHotel.View
{
    public partial class PrintStaff : Form
    {
        StaffController StaffController = new StaffController();
        DGVPrinter printer = new DGVPrinter();
        string gender = "All";
        string role = "All";
        public PrintStaff()
        {
            InitializeComponent();
        }

        private void PrintStaff_Load(object sender, EventArgs e)
        {
            showDataTable();
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

        private void chkAll_Click(object sender, EventArgs e)
        {
            gender = "All";
        }

        private void chkFemale_Click(object sender, EventArgs e)
        {
            gender = "Female";
        }

        private void chkMale_Click(object sender, EventArgs e)
        {
            gender = "Male";
        }

        private void chkAllroles_Click(object sender, EventArgs e)
        {
            role = "All";
            chkAllroles.Checked = true;
            chkAdmin.Checked = false;
            chkStaff.Checked = false;
        }

        private void chkAdmin_Click(object sender, EventArgs e)
        {
            role = "Admin";
            chkAllroles.Checked = false;
            chkAdmin.Checked = true;
            chkStaff.Checked = false;
        }

        private void chkStaff_Click(object sender, EventArgs e)
        {
            role = "Staff";
            chkAllroles.Checked = false;
            chkAdmin.Checked = false;
            chkStaff.Checked = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = "";
            string serach = txtSerach.Text;
            if (!String.IsNullOrEmpty(serach))
            {
                if (role == "All" && gender=="All")
                {
                    query = "Select Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender,Image FROM [dbo].[Employee] WHERE CONCAT (Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender) LIKE '%"+ serach + "%'";
                }else if(role == "All" && gender != "All")
                {
                    query = "Select Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender,Image FROM [dbo].[Employee] WHERE CONCAT (Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender) LIKE '%" + serach + "%' AND Gender='" + gender+"'";

                } else if (role != "All" && gender == "All")
                {
                    query = "Select Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender,Image FROM [dbo].[Employee] WHERE CONCAT (Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender) LIKE '%" + serach + "%' AND EmployeeType='" + role + "'";

                }
                else
                {
                    query = "Select Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender,Image FROM [dbo].[Employee] WHERE CONCAT (Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender) LIKE '%" + serach + "%' AND EmployeeType='" + role + "' AND Gender='" + gender + "'";
                }
            }
            else
            {
                if (role == "All" && gender == "All")
                {
                    query = "Select Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender,Image FROM [dbo].[Employee]";
                }
                else if (role == "All" && gender != "All")
                {
                    query = "Select Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender,Image FROM [dbo].[Employee] WHERE Gender='" + gender + "'";

                }
                else if (role != "All" && gender == "All")
                {
                    query = "Select Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender,Image FROM [dbo].[Employee] WHERE EmployeeType='" + role + "'";

                }
                else
                {
                    query = "Select Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender,Image FROM [dbo].[Employee] WHERE  EmployeeType='" + role + "' AND Gender='" + gender + "'";
                }

            }


            showData(new SqlCommand(query));
        }

        public void showData(SqlCommand command)
        {
            dgvStaff.ReadOnly = true;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            dgvStaff.DataSource = StaffController.getList(command);
            imageColumn = (DataGridViewImageColumn)dgvStaff.Columns[9];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printer.Title = "OMP Staff Detail";
            printer.SubTitle = string.Format("Date : {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Far;
            printer.Footer = "E-pupil";
            printer.FooterSpacing = 12;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(dgvStaff);

        }

        
    }
}
