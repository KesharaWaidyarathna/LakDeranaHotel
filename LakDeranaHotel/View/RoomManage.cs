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
    public partial class RoomManage : Form
    {
        RoomController RoomController = new RoomController();
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
                dgvRoomMange.DataSource = RoomController.getRoomList();
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
            txtRoomid.Clear();
            loadInitialData();
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
                RoomDAO room = new RoomDAO();
                room.Roomid= int.Parse(txtRoomid.Text);
                room.BedCount = int.Parse(txtBedCount.Text);
                room.Price = int.Parse(txtPrice.Text);
                room.Note = txtNote.Text;
                room.RoomCategory = cmbRoomCategory.Text;

                if (RoomController.updateRoom(room))
                {
                    loadInitialData();
                    MessageBox.Show("The room updated successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error occur during saving ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                btnClear_Click(sender, e);
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
                if (String.IsNullOrEmpty(txtRoomid.Text))
                {
                    MessageBox.Show("Please select revent recode to delete ", "Warrning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnClear_Click(sender, e);
                    return;
                }
                   
                DialogResult result = MessageBox.Show("Are you sure delete Room?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (RoomController.DeleteRoom(txtRoomid.Text))
                    {
                        MessageBox.Show("The room delete successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error occur during saving ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                else
                {
                    return;
                }
                loadInitialData();
                btnClear_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnClear_Click(sender, e);
            }
        }

        private void dgvClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtRoomid.Text = dgvRoomMange.CurrentRow.Cells[0].Value.ToString();
            txtBedCount.Text = dgvRoomMange.CurrentRow.Cells[2].Value.ToString();
            txtPrice.Text = dgvRoomMange.CurrentRow.Cells[3].Value.ToString();
            txtNote.Text = dgvRoomMange.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvRoomMange.DataSource = RoomController.searchRooms(txtSearch.Text);
                txtSearch.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        bool Validation()
        {

            if ((txtBedCount.Text == "") || (txtPrice.Text == "") || (txtRoomid.Text == "") || String.IsNullOrEmpty(cmbRoomCategory.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
