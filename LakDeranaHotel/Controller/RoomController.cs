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
    class RoomController
    {
        DBconnection connection = new DBconnection();

        public bool insertRoom(RoomDAO room)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Room] ([RoomCategory],[BedCount],[Price],[Note]) VALUES (@RoomCategory,@BedCount,@Price,@Note)", connection.GetConnection());
                command.Parameters.Add("@RoomCategory", SqlDbType.NVarChar).Value = room.RoomCategory;
                command.Parameters.Add("@BedCount", SqlDbType.Int).Value = room.BedCount;
                command.Parameters.Add("@Price", SqlDbType.Int).Value = room.Price;
                command.Parameters.Add("@Note", SqlDbType.NVarChar).Value = room.Note;

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

        public bool updateRoom(RoomDAO  room)
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE [dbo].[Room] SET [RoomCategory] = @RoomCategory,[BedCount] = @BedCount,[Price] = @Price,[Note] = @Note WHERE RoomId= @RoomId", connection.GetConnection());
                command.Parameters.Add("@RoomId", SqlDbType.Int).Value = room.Roomid;
                command.Parameters.Add("@RoomCategory", SqlDbType.NVarChar).Value = room.RoomCategory;
                command.Parameters.Add("@BedCount", SqlDbType.Int).Value = room.BedCount;
                command.Parameters.Add("@Price", SqlDbType.Int).Value = room.Price;
                command.Parameters.Add("@Note", SqlDbType.NVarChar).Value = room.Note;

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


        public DataTable getRoomList()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Room]", connection.GetConnection());
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

        public bool DeleteRoom(string IdNo)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Room] WHERE RoomId=" + IdNo + "", connection.GetConnection());
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

        public DataTable searchRooms(string serach)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT RoomId,RoomCategory,BedCount,Price,Note,FromDate,ToDate from Room where CONCAT(RoomId,RoomCategory,BedCount,Price,Note,FromDate,ToDate) LIKE '%" + serach + "%'", connection.GetConnection());
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
