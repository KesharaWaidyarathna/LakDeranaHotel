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
    public partial class PrintMarks : Form
    {
        MarkController MarkController = new MarkController();
        DGVPrinter printer = new DGVPrinter();

        public PrintMarks()
        {
            InitializeComponent();
        }

        private void ManageMarks_Load(object sender, EventArgs e)
        {
            dgvMarks.DataSource = MarkController.getList(new SqlCommand("SELECT  StudentId,StudentName,Marks,CourseID,CourseName,Description FROM Marks WHERE Marks!=0"));
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvMarks.DataSource = MarkController.searchMarks(txtSearch.Text);
                txtSearch.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printer.Title = "OMP Customer Marks List";
            printer.SubTitle = string.Format("Date : {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Far;
            printer.Footer = "E-pupil";
            printer.FooterSpacing = 12;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(dgvMarks);
        }
    }
}
