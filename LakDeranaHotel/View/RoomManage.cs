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
    public partial class RoomManage : Form
    {
        ClassController ClassController = new ClassController();
        public RoomManage()
        {
            InitializeComponent();
            loadInitialData();
        }

        public void loadInitialData()
        {
            try
            {
                Dictionary<string, string> comboSource = new Dictionary<string, string>();
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
                dgvRoomMange.DataSource = ClassController.getClassList();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The room details updated successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnClear_Click(this, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The room details deleted successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnClear_Click(this, null);
        }

        private void dgvClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtRoomid.Text = dgvRoomMange.CurrentRow.Cells[0].Value.ToString();
            txtBedCount.Text = dgvRoomMange.CurrentRow.Cells[2].Value.ToString();
            txtPrice.Text = dgvRoomMange.CurrentRow.Cells[3].Value.ToString();
            txtNote.Text = dgvRoomMange.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
