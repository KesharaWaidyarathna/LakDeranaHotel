using LakDeranaHotel.Controller;
using LakDeranaHotel.Model;
using System;
using System.Collections;
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
    public partial class Room : Form
    {
        ClassController ClassController = new ClassController();
        public Room()
        {
            InitializeComponent();
        }

        private void Class_Load(object sender, EventArgs e)
        {
            showDataTable();
            loadInitialData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtBedCount.Text == "")
                //{
                //    MessageBox.Show("The Class name can't be empty", "Empty Fileds", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //ClassDAO @class = new ClassDAO();
                //@class.ClassName = txtBedCount.Text;
                //@class.Note = txtNote.Text == "" ? " " : txtNote.Text;

                //if (ClassController.insertClass(@class))
                //{
                //    showDataTable();
                //    btnClear_Click(sender, e);
                //    MessageBox.Show("The customer save successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    MessageBox.Show("Error occur during saving ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                MessageBox.Show("The Room added successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(this, null);
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

        public void loadInitialData()
        {
            try
            {
                Dictionary<string,string> comboSource = new Dictionary<string, string>();
                comboSource.Add("1", "Single");
                comboSource.Add("2", "Double");
                comboSource.Add("3", "Triple");
                comboSource.Add("4", "Quad");
                comboSource.Add("5", "Queen");
                comboSource.Add("6", "King");
                comboSource.Add("7", "Twin");
                cmbRoomCategory.DataSource = new BindingSource(comboSource, null);
                cmbRoomCategory.DisplayMember = "Value";
                cmbRoomCategory.ValueMember = "Key";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBedCount.Clear();
            txtPrice.Clear();
            txtNote.Clear();
            loadInitialData();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
