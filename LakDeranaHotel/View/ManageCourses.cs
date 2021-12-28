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
    public partial class ManageCourses : Form
    {
        CourseController Course = new CourseController();
        public ManageCourses()
        {
            InitializeComponent();
            showDataTable();
        }

        private void ManageCourses_Load(object sender, EventArgs e)
        {

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
            txtCourseId.Clear();
            txtCourseName.Clear();
            txtDescription.Clear();
            txtHour.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Validation())
                {
                    MessageBox.Show("The Fileds can't be empty", "Empty Fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CourseDAO course = new CourseDAO();
                course.CourseId = int.Parse(txtCourseId.Text);
                course.CourseName = txtCourseName.Text;
                course.Hours = int.Parse(txtHour.Text);
                course.Description = txtDescription.Text;


                if (Course.UpdateCourse(course))
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you Sure wanna delete ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Course.DeleteCourse(txtCourseId.Text);
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

        private void dgvCourses_Click(object sender, EventArgs e)
        {
            txtCourseId.Text = dgvCourses.CurrentRow.Cells[0].Value.ToString();
            txtCourseName.Text = dgvCourses.CurrentRow.Cells[1].Value.ToString();
            txtHour.Text = dgvCourses.CurrentRow.Cells[2].Value.ToString();
            txtDescription.Text = dgvCourses.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvCourses.DataSource = Course.searchCourse(txtSearch.Text);
                txtSearch.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
