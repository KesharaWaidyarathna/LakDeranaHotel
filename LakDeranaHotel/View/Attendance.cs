using LakDeranaHotel.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LakDeranaHotel.View
{
    public partial class Attendance : Form
    {
        StaffController staffController = new StaffController();
        AttendanceController attendance = new AttendanceController();
        public Attendance()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (btnLogin.Text.Equals("Back"))
            {
                lblNotice.Visible = true;
                lblWelcome.Visible = false;
                btnLogin.Text = "Sign In/Sign Out";
                return;
            }
            var dateAndTime = DateTime.Now;
            string date = dateAndTime.ToString("yyyy-MM-dd");
            lblNotice.Visible = false;
            if (String.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please enter the employee Id", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (staffController.IsEmployeeIdExsist(txtId.Text))
            {

                if (attendance.IsEmployeeLogedIn(date, txtId.Text))
                {
                    if (attendance.UpdateLogedOut(date, txtId.Text))
                    {
                        lblWelcome.Visible = true;
                        lblWelcome.Text = "Sing Out !  Have a Nice Day";
                    }
                    else
                    {
                        lblWelcome.Visible = true;
                        lblWelcome.ForeColor = Color.Red;
                        lblWelcome.Text = "Error occurred ,Please Contact Support Team !";
                    }
                }
                else
                {
                    if (attendance.insertReservation(txtId.Text))
                    {
                        lblWelcome.Visible = true;
                        lblWelcome.Text = "Good Morning! Sign In ";
                    }
                    else
                    {
                        lblWelcome.Visible = true;
                        lblWelcome.ForeColor = Color.Red;
                        lblWelcome.Text = "Error occurred ,Please Contact Support Team !";
                    }
                }

               
            }
            else
            {
                MessageBox.Show("Invalid employee Id, Please enter valid employee Id ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnLogin.Text = "Back";
            txtId.Clear();
        }
    }
}
