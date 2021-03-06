using LakDeranaHotel;
using LakDeranaHotel.Controller;
using LakDeranaHotel.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_pupi
{
    public partial class Main : Form
    {
        private Form activeForm = null;
        Customer Customer = new Customer();
        string Role = "";
        string Username = "";

        public Main(string role,string username)
        {
            InitializeComponent();
            Role = role;
            Username = username;
            customizeDesign();
            rolelimitaion();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            customerCount();
            lblUserName.Text += Username;
            lblUserRole.Text += Role;

        }

        public void customerCount()
        {
            lblTotalStudent.Text = "Total Students : "+Customer.totalCustomer();
            lblFemaleStudents.Text = "Female : "+Customer.totalFemaleCustomer();
            lblMaleStudents.Text = "Male : "+Customer.totalMaleCustomer();
        }

        private void customizeDesign()
        {
            pnlStudentMenue.Visible = false;
            pnlCourseMenu.Visible = false;
            pnlStaffMenu.Visible = false;
            pnlMarksMenu.Visible = false;
            pnlCampus.Visible = false;

        }

        private void hideSubmenu()
        {
            if (pnlStudentMenue.Visible == true)
                pnlStudentMenue.Visible = false;
            if (pnlMarksMenu.Visible == true)
                pnlMarksMenu.Visible = false;
            if (pnlCourseMenu.Visible == true)
                pnlCourseMenu.Visible = false;
            if (pnlStaffMenu.Visible == true)
                pnlStaffMenu.Visible = false;
            if(pnlCampus.Visible == true)
                pnlCampus.Visible = false;
        }

        private void rolelimitaion()
        {
            if (Role == "Admin")
            {
                btnCustomer.Visible = true;
                btnRooms.Visible = true;
                btnReservation.Visible = true;
                btnStaff.Visible = true;
                btnAttendance.Visible = true;
            }
            else if (Role == "Staff")
            {
                btnCustomer.Visible = true;
                btnRooms.Visible = true;
                btnReservation.Visible = false;
                btnStaff.Visible = false;
                btnAttendance.Visible = true;

            }
            else if (Role == "Bar")
            {
                btnCustomer.Visible = false;
                btnRooms.Visible = false;
                btnReservation.Visible = false;
                btnStaff.Visible = false;
                btnAttendance.Visible = true;

            }else if (Role == "Manager")
            {
                btnCustomer.Visible = false;
                btnRooms.Visible = false;
                btnReservation.Visible = true;
                btnStaff.Visible = false;
                btnAttendance.Visible = true;

            }
        }
        private void showSubMenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }
        #region Customer
        private void btnStudent_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlStudentMenue);

        }

        private void btnStudentReg_Click(object sender, EventArgs e)
        {
            openChildForm(new Register());
            hideSubmenu();
        }

        private void btnStudentMange_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageCustomer(Role));
            hideSubmenu();
        }

        private void btnStudentPrint_Click(object sender, EventArgs e)
        {
            //openChildForm(new PrintStudent());
            hideSubmenu();
        }
        #endregion

        #region Course
        private void btnCourse_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlCourseMenu);
        }

        private void btnCourseNew_Click(object sender, EventArgs e)
        {
            openChildForm(new Resvation());
            hideSubmenu();
        }

        private void btnCourseMange_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageReservation());
            hideSubmenu();
        }

        private void btnCoursePrint_Click(object sender, EventArgs e)
        {
            //openChildForm(new PrintCourse());
            hideSubmenu();
        }
        #endregion

        #region Marks
        private void btnMarks_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlMarksMenu);
        }

        private void btnMarksNew_Click(object sender, EventArgs e)
        {
            //openChildForm(new Marks());
            hideSubmenu();
        }

        private void btnMarksMange_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnMarksPrint_Click(object sender, EventArgs e)
        {
            //openChildForm(new PrintMarks());
            hideSubmenu();
        }
        #endregion

        #region Staff
        private void btnStaff_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlStaffMenu);
        }

        private void btnStaffNew_Click(object sender, EventArgs e)
        {
            openChildForm(new Staff());
            hideSubmenu();
        }

        private void btnStaffMange_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageStaff());
            hideSubmenu();
        }

        private void btnStaffPrint_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintStaff());
            hideSubmenu();
        }
        #endregion

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnDashborad_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            pnlMain.Controls.Add(pnlCover);
            customerCount();
            hideSubmenu();

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        #region Class
        private void btnCampus_Click(object sender, EventArgs e)
        {
            showSubMenu(pnlCampus);
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            openChildForm(new Room());
            hideSubmenu();
        }

        private void btnMangeClass_Click(object sender, EventArgs e)
        {
            openChildForm(new RoomManage());
            hideSubmenu();
        }

        private void btnPrintClass_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new Attendance());
            hideSubmenu();
        }
    }
}
