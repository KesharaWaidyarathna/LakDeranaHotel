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
using DGVPrinterHelper;
using LakDeranaHotel.Controller;

namespace LakDeranaHotel.View
{
    public partial class PrintStudent : Form
    {
        Customer Customer = new Customer();
        string query = "";
        DGVPrinter printer = new DGVPrinter();
        CourseController Course = new CourseController();

        public PrintStudent()
        {
            InitializeComponent();
        }

        private void PrintStudent_Load(object sender, EventArgs e)
        {
            showDataTable();
            cmbClass.DataSource = Course.getCourseList();
            cmbClass.DisplayMember = "CourseName";
            cmbClass.ValueMember = "CourseId";
        }

        public void showData(SqlCommand command)
        {
            dgvStudent.ReadOnly = true;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            dgvStudent.DataSource = Customer.getList(command);
            imageColumn = (DataGridViewImageColumn)dgvStudent.Columns[8];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

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
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (chkClass.Checked)
            {
                string id = cmbClass.SelectedValue.ToString();
                if (chkAll.Checked)
                {
                    query = "SELECT S.* FROM  Customer S Inner join Marks M on M.customerid=S.customerid where M.CourseId="+id+"";
                }
                else if (chkFemale.Checked)
                {
                    query = "SELECT S.* FROM  Customer S Inner join Marks M on M.customerid=S.customerid where M.CourseId=" + id + " AND S.Gender='Female'";
                }
                else if (chkMale.Checked)
                {
                    query = "SELECT S.* FROM  Customer S Inner join Marks M on M.customerid=S.customerid where M.CourseId=" + id + " AND S.Gender='Male'";
                }
            }
            else
            {
                if (chkAll.Checked)
                {
                    query = "SELECT * FROM  Customer";
                }
                else if (chkFemale.Checked)
                {
                    query = "SELECT * FROM  Customer WHERE Gender='Female'";
                }
                else if (chkMale.Checked)
                {
                    query = "SELECT * FROM  Customer WHERE Gender='Male'";
                }
            }
            
            showData(new SqlCommand(query));
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printer.Title = "OMP Customer List";
            printer.SubTitle = string.Format("Date : {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Far;
            printer.Footer = "E-pupil";
            printer.FooterSpacing = 12;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(dgvStudent);
        }

        private void chkClass_Click(object sender, EventArgs e)
        {
            chkClass.Checked = true;
        }
    }
}
