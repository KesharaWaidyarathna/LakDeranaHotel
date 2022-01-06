using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakDeranaHotel.Controller
{
    class ReservationController
    {
        DBconnection connection = new DBconnection();

        public DataTable getReservationList()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Reservation", connection.GetConnection());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                return table;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
