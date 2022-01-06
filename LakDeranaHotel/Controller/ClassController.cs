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
    class ClassController
    {
        DBconnection connection = new DBconnection();

        public bool insertClass(ClassDAO @class)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Class]([ClassName],[Note])VALUES(@ClassName,@Note)", connection.GetConnection());
                command.Parameters.Add("@ClassName", SqlDbType.NVarChar).Value = @class.ClassName;
                command.Parameters.Add("@Note", SqlDbType.NVarChar).Value = @class.Note;

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

        public bool updateClass(ClassDAO @class)
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE [dbo].[Class] SET [ClassName] = @ClassName,[Note] = @Note WHERE ClassID=@ClassId", connection.GetConnection());
                command.Parameters.Add("@ClassId", SqlDbType.Int).Value = @class.ClassId;
                command.Parameters.Add("@ClassName", SqlDbType.NVarChar).Value = @class.ClassName;
                command.Parameters.Add("@Note", SqlDbType.NVarChar).Value = @class.Note;

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

        public DataTable getClassList()
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

        public DataTable getClassDetailList()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[ClassDetails]", connection.GetConnection());
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

        public bool DeleteClass(string IdNo)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Class] WHERE ClassId=" + IdNo + "", connection.GetConnection());
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

        public bool DeleteClassDetails(string IdNo)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM [dbo].[ClassDetails] WHERE ClassId=" + IdNo + "", connection.GetConnection());
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

        public bool insertClassDetails(ClassDetailDAO classDetail)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [dbo].[ClassDetails]([ClassId],[ClassName],[CourseId],[CourseName],[Note])VALUES(@ClassId,@ClassName,@CourseId,@CourseName,@Note)", connection.GetConnection());
                command.Parameters.Add("@ClassId", SqlDbType.Int).Value = classDetail.ClassId;
                command.Parameters.Add("@ClassName", SqlDbType.NVarChar).Value = classDetail.ClassName;
                command.Parameters.Add("@CourseId", SqlDbType.Int).Value = classDetail.CourseId;
                command.Parameters.Add("@CourseName", SqlDbType.VarChar).Value = classDetail.CourseName;
                command.Parameters.Add("@Note", SqlDbType.NVarChar).Value = classDetail.Note;

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

        public DataTable getCourseList()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT c.* FROM [dbo].[Courses] c where CourseId not in (select CourseId from ClassDetails)", connection.GetConnection());
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
