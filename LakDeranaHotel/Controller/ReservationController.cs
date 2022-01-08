using LakDeranaHotel.Model;
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

        public bool insertReservation(ResvationDAO resvation)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Reservation]([Customerid],[RoomId],[CreatedDate],[FromDate],[ToDate],[isPaid] ,[total],[Note],[CustomerName]) VALUES(@Customerid,@RoomId,GETDATE(),@FromDate,@ToDate,@isPaid,@total,@Note,@CustomerName)", connection.GetConnection());
                command.Parameters.Add("@Customerid", SqlDbType.Int).Value = resvation.CustomerId;
                command.Parameters.Add("@RoomId", SqlDbType.Int).Value = resvation.RoomId;
                command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = resvation.FromDate;
                command.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = resvation.ToDate;
                command.Parameters.Add("@isPaid", SqlDbType.Bit).Value = resvation.IsFullPaid;
                command.Parameters.Add("@total", SqlDbType.Int).Value = resvation.Total;
                command.Parameters.Add("@Note", SqlDbType.NVarChar).Value = resvation.Note;
                command.Parameters.Add("@CustomerName", SqlDbType.NVarChar).Value = resvation.CustomerName;

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

        public bool UpdateReservation(ResvationDAO resvation)
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE [dbo].[Reservation] SET [Customerid] = @Customerid, [RoomId] = @RoomId,[FromDate] = @FromDate,[ToDate] = @ToDate,[isPaid] = @isPaid,[total] = @total,[Note] = @Note,[CustomerName] = @CustomerName  WHERE Reservationid=@Reservationid", connection.GetConnection());
                command.Parameters.Add("@Reservationid", SqlDbType.Int).Value = resvation.ReservationId;
                command.Parameters.Add("@Customerid", SqlDbType.Int).Value = resvation.CustomerId;
                command.Parameters.Add("@RoomId", SqlDbType.Int).Value = resvation.RoomId;
                command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = resvation.FromDate;
                command.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = resvation.ToDate;
                command.Parameters.Add("@isPaid", SqlDbType.Bit).Value = resvation.IsFullPaid;
                command.Parameters.Add("@total", SqlDbType.Int).Value = resvation.Total;
                command.Parameters.Add("@Note", SqlDbType.NVarChar).Value = resvation.Note;
                command.Parameters.Add("@CustomerName", SqlDbType.NVarChar).Value = resvation.CustomerName;

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


        public bool reserveRoom(ResvationDAO resvation)
        {
            try
            {
                SqlCommand command = new SqlCommand("Update Room Set IsReserved=1 ,FromDate=@FromDate,ToDate=@ToDate where RoomId=@RoomId", connection.GetConnection());
                command.Parameters.Add("@RoomId", SqlDbType.Int).Value = resvation.RoomId;
                command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = resvation.FromDate;
                command.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = resvation.ToDate;

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

        public DataTable getRoomResverDates(string id)
        {
            try
            {
                SqlCommand command = new SqlCommand("Select CAST( FromDate AS Date ) AS [From],CAST( Todate AS Date ) AS [To] FROM Reservation where RoomId=" + id+" AND (FromDate>GETDATE() OR ToDate>GETDATE())", connection.GetConnection());
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

        public string getCustomerName(string id)
        {
            try
            {
                SqlCommand command = new SqlCommand("select FirstName from Customer where CustomerId=" + id + "", connection.GetConnection());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                string result = "";
                connection.openConnection();
                result = (string)command.ExecuteScalar();
                if (String.IsNullOrEmpty(result))
                {
                    connection.closeConnection();
                    return result;
                }
                connection.closeConnection();
                return result;

            }
            catch (Exception ex)
            {
                connection.closeConnection();
                throw ex;
            }

        }

        public bool DeleteReservation(string IdNo)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Reservation] WHERE Reservationid=" + IdNo + "", connection.GetConnection());
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

    }
}
