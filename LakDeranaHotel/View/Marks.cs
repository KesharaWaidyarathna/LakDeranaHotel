using LakDeranaHotel.Controller;
using LakDeranaHotel.Model;
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
    public partial class Marks : Form
    {
        CourseController Course = new CourseController();
        Customer Customer = new Customer();
        MarkController Mark = new MarkController();
        DataTable MainMask = new DataTable();
        public Marks()
        {
            InitializeComponent();
        }

        private void Marks_Load(object sender, EventArgs e)
        {
            showDataTable();
        }

        private void showDataTable()
        {
            dgvStudent.DataSource = Customer.getList(new SqlCommand("SELECT CustomerID,FirstName FROM Customer"));
            dgvMarks.DataSource = Mark.getList(new SqlCommand("SELECT  StudentName,CourseName,Marks,Description FROM Marks WHERE Marks!=0"));
            MainMask= Mark.getList(new SqlCommand("SELECT * FROM Marks"));

        }


        bool Validation()
        {
            if ((txtStudentId.Text == "") || (txtStudentName.Text == "") || (txtDescription.Text == "") || (cmbCourse.SelectedValue==null))
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

                MarksDAO marks = new MarksDAO();

                marks.StudentId = int.Parse(txtStudentId.Text);
                //marks.StudentName = txtStudentName.Text;
                //marks.CourseName = cmbCourse.Text;
                marks.CourseId = int.Parse(cmbCourse.SelectedValue.ToString());
                marks.Discription = txtDescription.Text;
                marks.Marks = int.Parse(txtMarks.Text);

                //if (Mark.IsMarksAdd(marks)) 
                //{
                //    MessageBox.Show("Marks Already add for this Customer,Please Update Marks", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}


                if (Mark.updateStudentMarkDetail(marks))
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStudentId.Clear();
            txtStudentName.Clear();
            txtDescription.Clear();
            txtMarks.Clear();
            cmbCourse.DataSource = null;
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtStudentId.Text = dgvStudent.CurrentRow.Cells[0].Value.ToString();
            txtStudentName.Text = dgvStudent.CurrentRow.Cells[1].Value.ToString();
            cmbCourse.DataSource = null;
            cmbCourse.DataSource = Mark.getCourseList(txtStudentId.Text);
            cmbCourse.DisplayMember = "CourseName";
            cmbCourse.ValueMember = "CourseId";
            txtMarks.Clear();
            txtDescription.Clear();
            
        }

        private void cmbCourse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataRow[] dt = MainMask.Select("CustomerId=" + txtStudentId.Text + " AND CourseId=" + cmbCourse.SelectedValue.ToString() + "");
            if (dt[0][3].ToString() != "0")
            {
                txtMarks.Text = dt[0][3].ToString();
            }
            else
            {
                txtMarks.Text = null;
            }
            txtDescription.Text = dt[0][6].ToString();
        }

    }
}
