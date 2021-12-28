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
    public partial class Class : Form
    {
        ClassController ClassController = new ClassController();
        public Class()
        {
            InitializeComponent();
        }

        private void Class_Load(object sender, EventArgs e)
        {
            showDataTable();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClassName.Text == "")
                {
                    MessageBox.Show("The Class name can't be empty", "Empty Fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ClassDAO @class = new ClassDAO();
                @class.ClassName = txtClassName.Text;
                @class.Note = txtNote.Text == "" ? " " : txtNote.Text;

                if (ClassController.insertClass(@class))
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
                dgvClass.DataSource = ClassController.getClassList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtClassName.Clear();
            txtNote.Clear();
        }
    }
}
