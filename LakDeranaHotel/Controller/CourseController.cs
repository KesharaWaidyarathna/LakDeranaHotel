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
    class CourseController
    {
        DBconnection connection = new DBconnection();

        public bool insertCourse(CourseDAO course)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Courses]([CourseName],[Hours],[Description])VALUES(@CourseName,@Hours,@Description)", connection.GetConnection());
                command.Parameters.Add("@CourseName", SqlDbType.VarChar).Value =course.CourseName;
                command.Parameters.Add("@Hours", SqlDbType.Int).Value =course.Hours;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value =course.Description;

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
        public bool UpdateCourse(CourseDAO course)
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE [dbo].[Courses]SET [CourseName] = @CourseName,[Hours] = @Hours,[Description] = @Description WHERE Courseid=@Courseid", connection.GetConnection());
                command.Parameters.Add("@CourseName", SqlDbType.VarChar).Value = course.CourseName;
                command.Parameters.Add("@Hours", SqlDbType.Int).Value = course.Hours;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = course.Description;
                command.Parameters.Add("@Courseid", SqlDbType.Int).Value = course.CourseId;

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

        public bool DeleteCourse(string IdNo)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Courses] WHERE Courseid=" + IdNo + "", connection.GetConnection());
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
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Courses]", connection.GetConnection());
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

        public DataTable searchCourse(string serach)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Courses] WHERE CONCAT(Courseid,CourseName,Hours,Description) LIKE '%" + serach + "%'", connection.GetConnection());
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

        public DataTable getList(SqlCommand command)
        {
            try
            {
                command.Connection = connection.GetConnection();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                connection.closeConnection();
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
