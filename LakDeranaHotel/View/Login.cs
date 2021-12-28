using E_pupi;
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
    public partial class Login : Form
    {
        StaffController StaffController = new StaffController();
        string role = "";
        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void lblClose_MouseEnter(object sender, EventArgs e)
        {
            lblClose.ForeColor = Color.Red;
        }

        private void lblClose_MouseLeave(object sender, EventArgs e)
        {
            lblClose.ForeColor = Color.Transparent;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if ((txtUsername.Text == "") || (txtPassword.Text == ""))
            {
                MessageBox.Show("Please enter Username and password", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                role = StaffController.Login(txtUsername.Text, txtPassword.Text);

                if (!String.IsNullOrEmpty(role))
                {
                    Main main = new Main(role, txtUsername.Text);
                    this.Hide();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Username or password incorrect", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

   
    }
}
