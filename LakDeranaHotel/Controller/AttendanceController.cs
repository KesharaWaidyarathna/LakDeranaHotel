using LakDeranaHotel.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakDeranaHotel.Controller
{
    class AttendanceController
    {
        DBconnection connection = new DBconnection();

        public bool insertReservation(string id)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Attendence] ([EmployeeID],[LogedIn],[Date])VALUES ("+ id + ",GETDATE(), CAST( GETDATE() AS Date ))", connection.GetConnection());

                connection.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    connection.closeConnection();
                    return true;
                }
                else
                {
                    connection.closeConnection();
                    return false;
                }
            }
            catch (Exception ex)
            {
                connection.closeConnection();
                throw ex;
            }
        }

        public bool UpdateLogedOut(string date, string id)
        {
            try
            {
                SqlCommand command = new SqlCommand("Update [Attendence] SET [Logedout]=GETDATE() where EmployeeId="+id+" AND [Date]='"+date+"'", connection.GetConnection());

                connection.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    connection.closeConnection();
                    return true;
                }
                else
                {
                    connection.closeConnection();
                    return false;
                }
            }
            catch (Exception ex)
            {
                connection.closeConnection();
                throw ex;
            }
        }


        public bool IsEmployeeLogedIn(string date,string id)
        {
            try
            {
                SqlCommand command = new SqlCommand("IF EXISTS(SELECT * FROM Attendence WHERE EmployeeID=" + id + " AND Date='"+date+"' )BEGIN Select  '1' END", connection.GetConnection());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                string result = "";
                connection.openConnection();
                result = (string)command.ExecuteScalar();
                if (String.IsNullOrEmpty(result))
                {
                    connection.closeConnection();
                    return false;
                }
                connection.closeConnection();
                return true;

            }
            catch (Exception ex)
            {
                connection.closeConnection();
                throw ex;
            }

        }
    }
}
