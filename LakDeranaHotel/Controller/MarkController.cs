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
    class MarkController
    {
        DBconnection connection = new DBconnection();

        public bool insertMarks(MarksDAO marks)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Marks]([StudentId],[StudentName],[Marks],[CourseId],[CourseName],[Description])VALUES(@StudentId,@StudentName ,@Marks,@CourseId,@CourseName,@Description)", connection.GetConnection());
                command.Parameters.Add("@StudentId", SqlDbType.Int).Value = marks.StudentId;
                command.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = marks.StudentName;
                command.Parameters.Add("@Marks", SqlDbType.Int).Value = marks.Marks;
                command.Parameters.Add("@CourseId", SqlDbType.Int).Value = marks.CourseId;
                command.Parameters.Add("@CourseName", SqlDbType.VarChar).Value = marks.CourseName;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = marks.Discription;


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

        public bool updateStudentMarkDetail(MarksDAO marks)
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE Marks SET Marks=" + marks.Marks + ", Description='" + marks.Discription+"'  WHERE Studentid=" + marks.StudentId + " AND CourseID="+marks.CourseId+"", connection.GetConnection());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
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


        public bool insertStudentCoursDetails(List<MarksDAO> markslist)
        {
            try
            {
                bool status = true;
                foreach (MarksDAO marks in markslist)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Marks]([StudentId],[StudentName],[Marks],[CourseId],[CourseName],[Description])VALUES(@StudentId,@StudentName ,@Marks,@CourseId,@CourseName,@Description)", connection.GetConnection());
                    command.Parameters.Add("@StudentId", SqlDbType.Int).Value = marks.StudentId;
                    command.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = marks.StudentName;
                    command.Parameters.Add("@Marks", SqlDbType.Int).Value = marks.Marks;
                    command.Parameters.Add("@CourseId", SqlDbType.Int).Value = marks.CourseId;
                    command.Parameters.Add("@CourseName", SqlDbType.VarChar).Value = marks.CourseName;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = marks.Discription;


                    connection.openConnection();
                    if (command.ExecuteNonQuery() != 1)
                    {
                        connection.closeConnection();
                        return status=false;
                    }

                }

                connection.closeConnection();
                return status;
            }
            catch (Exception ex)
            {
                connection.closeConnection();
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

        public bool IsMarksAdd(MarksDAO marks)
        {
            try
            {
                SqlCommand command = new SqlCommand("IF EXISTS(SELECT * FROM Marks WHERE STudentID="+marks.StudentId+" AND CourseId="+marks.CourseId+")BEGIN Select  '1' END", connection.GetConnection());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                string result = "";
                connection.openConnection();
                result =(string)command.ExecuteScalar();
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

        public bool updateStudentName(CustomerDAO customer)
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE Marks SET StudentName='"+customer.firstName+"' WHERE Studentid="+customer.idNo+"", connection.GetConnection());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                connection.openConnection();
                if (command.ExecuteNonQuery() == 2)
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

        public bool DeleteStudentMarks(string IdNo)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Marks] WHERE StudentId=" + IdNo + "", connection.GetConnection());
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

        public DataTable getCourseList(string Studentid)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT courseId,CourseName FROM [dbo].[Marks] WHERE StudentId=" + Studentid + "", connection.GetConnection());
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

        public DataTable searchMarks(string serach)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT customerid,customerName,Marks,Courseid,coursename,Description from Marks where Marks!=0 AND CONCAT(customerid,customerName,Marks,Courseid,coursename,Description) LIKE '%" + serach + "%'", connection.GetConnection());
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
