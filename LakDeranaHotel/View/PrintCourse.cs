using DGVPrinterHelper;
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
    public partial class PrintCourse : Form
    {
        CourseController Course = new CourseController();
        DGVPrinter printer = new DGVPrinter();

        public PrintCourse()
        {
            InitializeComponent();
            showDataTable();
        }

        private void PrintCourse_Load(object sender, EventArgs e)
        {

        }

        public void showDataTable()
        {
            try
            {
                dgvCourse.DataSource = Course.getCourseList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvCourse.DataSource = Course.searchCourse(txtSearch.Text);
                txtSearch.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            printer.Title = "OMP Course List";
            printer.SubTitle = string.Format("Date : {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Far;
            printer.Footer = "E-pupil";
            printer.FooterSpacing = 12;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(dgvCourse);
        }
    }
}
