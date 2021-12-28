using LakDeranaHotel.Controller;
using LakDeranaHotel.Model;
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
    public partial class Courses : Form
    {
        CourseController Course = new CourseController();
        public Courses()
        {
            InitializeComponent();
            showDataTable();
        }

        bool Validation()
        {
            if ((txtCourseName.Text == "") || (txtHour.Text == "") || (txtDescription.Text == ""))
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
                if (!Validation())
                {
                    MessageBox.Show("The Fileds can't be empty", "Empty Fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CourseDAO course = new CourseDAO();
                course.CourseName = txtCourseName.Text;
                course.Hours = int.Parse(txtHour.Text);
                course.Description = txtDescription.Text;


                if (Course.insertCourse(course))
                {
                    showDataTable();
                    btnClear_Click(sender, e);
                    MessageBox.Show("The customer save successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public void showDataTable()
        {
            try
            {
                dgvCourses.DataSource = Course.getCourseList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCourseName.Clear();
            txtDescription.Clear();
            txtHour.Clear();
        }

        private void Courses_Load(object sender, EventArgs e)
        {

        }
    }
    
}
