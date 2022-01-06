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
            MessageBox.Show("Add new reservation successfully ", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnClear_Click(this, null);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }
        public void loadInitialData()
        {
            try
            {
                dgvResevation.DataSource = reservationController.getReservationList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
