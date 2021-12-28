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
    public partial class ManageClass : Form
    {
        ClassController ClassController = new ClassController();
        string Role = "";

        public ManageClass(string role)
        {
            InitializeComponent();
            Role = role;
        }

        private void ManageClass_Load(object sender, EventArgs e)
        {
            showDataTable();
            roleLemitation();

        }


        private void roleLemitation()
        {
            if (Role == "Admin")
            {
                cmbCourse.Visible = false;
                btnAdd.Visible = false;
                lblCourse.Visible = false;
            }
            else
            {
                btnDelete.Visible = false;
                btnUpdate.Visible = false;
                txtClassName.Enabled = false;
                txtNote.Enabled = false;
            }
        }
        public void showDataTable()
        {
            try
            {
                dgvClass.DataSource = ClassController.getClassList();
                dgvClassDetail.DataSource = ClassController.getClassDetailList();
                cmbCourse.DataSource = ClassController.getCourseList();
                cmbCourse.DisplayMember = "CourseName";
                cmbCourse.ValueMember = "CourseId";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtClassName.Clear();
            txtClassId.Clear();
            txtNote.Clear();
        }

        private void dgvClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtClassId.Text = dgvClass.CurrentRow.Cells[0].Value.ToString();
            txtClassName.Text = dgvClass.CurrentRow.Cells[1].Value.ToString();
            txtNote.Text = dgvClass.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClassName.Text == "" || txtClassId.Text=="")
                {
                    MessageBox.Show("Please select class from the table", "Empty Fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ClassDAO @class = new ClassDAO();
                @class.ClassId = int.Parse(txtClassId.Text);
                @class.ClassName = txtClassName.Text;
                @class.Note = txtNote.Text == "" ? " " : txtNote.Text;

                if (ClassController.updateClass(@class))
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
                DialogResult result = MessageBox.Show("Are you sure delete Class?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ClassController.DeleteClassDetails(txtClassId.Text);
                    ClassController.DeleteClass(txtClassId.Text);
                    MessageBox.Show("The customer Delete successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return;
                }
                showDataTable();
                btnClear_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClassName.Text == "" || txtClassId.Text == "")
                {
                    MessageBox.Show("Please select class from the table", "Empty Fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cmbCourse.Text=="")
                {
                    MessageBox.Show("There are no courses to add", "Empty Fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ClassDetailDAO classDetail = new ClassDetailDAO();
                classDetail.ClassId = int.Parse(txtClassId.Text);
                classDetail.ClassName = txtClassName.Text;
                classDetail.CourseId = int.Parse(cmbCourse.SelectedValue.ToString());
                classDetail.CourseName = cmbCourse.Text;
                classDetail.Note = txtNote.Text == "" ? " " : txtNote.Text;

                if (ClassController.insertClassDetails(classDetail))
                {
                    showDataTable();
                    btnClear_Click(sender, e);
                    MessageBox.Show("The Course mapped to class successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error occur during saving ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
