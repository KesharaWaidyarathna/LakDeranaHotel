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
    public partial class Resvation : Form
    {
        ReservationController reservationController = new ReservationController();

        public Resvation()
        {
            InitializeComponent();
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
                ResvationDAO reservation = new ResvationDAO();
                reservation.CustomerId = int.Parse(txtCustomerId.Text);
                reservation.RoomId = int.Parse(txtRoomId.Text);
                reservation.IsFullPaid = chkFullPaid.Checked;
                reservation.Total = int.Parse(txtPrice.Text);
                reservation.FromDate = dtpFromDate.Value;
                reservation.ToDate = dtpToDate.Value;
                reservation.Note = txtNote.Text;
                reservation.CustomerName = txtCustomerName.Text;

                if (reservationController.insertReservation(reservation))
                {
                    reservationController.reserveRoom(reservation);
                    loadInitialData();
                    MessageBox.Show("The new reservation save successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error occur during saving ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                btnClear_Click(sender, e);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtNote.Clear();
            txtPrice.Clear();
            txtRoomId.Clear();
            dtpFromDate.MinDate = DateTime.Now;
            dtpToDate.MinDate = DateTime.Now.AddDays(1);
            chkFullPaid.Checked = false;
        }
        public void loadInitialData()
        {
            try
            {
                dgvResevation.DataSource = reservationController.getReservationList();
                dtpFromDate.MinDate = DateTime.Now;
                dtpToDate.MinDate = DateTime.Now.AddDays(1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool Validation()
        {
            if ((txtRoomId.Text == "") || (txtPrice.Text == "") || (txtCustomerId.Text == "") || (txtCustomerName.Text == "") || dtpFromDate.Value==null|| dtpToDate.Value == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void txtRoomId_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtRoomId.Text))
            {
                dgvBooked.DataSource = reservationController.getRoomResverDates(txtRoomId.Text);
            }

        }

        private void txtRoomId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCustomerId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCustomerId_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCustomerId.Text))
            {
                txtCustomerName.Text = reservationController.getCustomerName(txtCustomerId.Text);
            }
        }
    }
}
