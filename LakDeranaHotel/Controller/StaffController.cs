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

    class StaffController
    {
        DBconnection connection = new DBconnection();

        public bool instertstaff(StaffDAO staff)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Employee]([EmployeeType],[FirstName],[LastName],[DOB],[Address],[NIC],[PhoneNumber],[Gender],[Username],[Password],[Image])VALUES(@EmployeeType,@FirstName,@LastName,@DOB ,@Address,@NIC,@PhoneNumber,@Gender,@Username,@Password,@Image)", connection.GetConnection());
                command.Parameters.Add("@EmployeeType", SqlDbType.VarChar).Value = staff.EmployeeType;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = staff.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = staff.LastName;
                command.Parameters.Add("@DOB", SqlDbType.DateTime).Value = staff.DOB;
                command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = staff.address;
                command.Parameters.Add("@NIC", SqlDbType.NVarChar).Value = staff.NIC;
                command.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = staff.phone;
                command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = staff.gender;
                command.Parameters.Add("@Username", SqlDbType.VarChar).Value = staff.Username;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = staff.Password;
                command.Parameters.Add("@Image", SqlDbType.VarBinary).Value = staff.image;

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

        public bool updatestaff(StaffDAO staff)
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE [dbo].[Employee] SET [EmployeeType] = @EmployeeType,[FirstName] = @FirstName,[LastName] = @LastName,[DOB] = @DOB,[Address]=@Address,[NIC] = @NIC,[PhoneNumber] = @PhoneNumber,[Gender] = @Gender,[Username] = @Username,[Password] = @Password,[image] = @image WHERE Employeeid=" + staff.EmployeeId+"", connection.GetConnection());
                command.Parameters.Add("@EmpolyeeId", SqlDbType.Int).Value = staff.EmployeeId;
                command.Parameters.Add("@EmployeeType", SqlDbType.VarChar).Value = staff.EmployeeType;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = staff.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = staff.LastName;
                command.Parameters.Add("@DOB", SqlDbType.DateTime).Value = staff.DOB;
                command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = staff.address;
                command.Parameters.Add("@NIC", SqlDbType.NVarChar).Value = staff.NIC;
                command.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = staff.phone;
                command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = staff.gender;
                command.Parameters.Add("@Username", SqlDbType.VarChar).Value = staff.Username;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = staff.Password;
                command.Parameters.Add("@Image", SqlDbType.VarBinary).Value = staff.image;

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

        public DataTable getStaffList()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Employee]", connection.GetConnection());
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

        public bool IsUsernameExists(string Username)
        {
            try
            {
                SqlCommand command = new SqlCommand("IF EXISTS(SELECT * FROM Employee WHERE Username='" + Username + "' )BEGIN Select  '1' END", connection.GetConnection());
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

        public bool IsUsernameExists(string Username,string EmployeeID)
        {
            try
            {
                SqlCommand command = new SqlCommand("IF EXISTS(SELECT * FROM Employee WHERE Username='" + Username + "' AND EmployeeID !="+EmployeeID+" )BEGIN Select  '1' END", connection.GetConnection());
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

        public string Login(string Username,string Password)
        {
            try
            {
                SqlCommand command = new SqlCommand("IF EXISTS(SELECT * FROM Employee WHERE Username='" + Username + "' AND Password='" + Password + "' )BEGIN Select Employeetype FROM Employee WHERE Username='" + Username + "' AND Password='" + Password + "' END", connection.GetConnection());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                string result = "";
                connection.openConnection();
                result = (string)command.ExecuteScalar();
                return result;

            }
            catch (Exception ex)
            {
                connection.closeConnection();
                throw ex;
            }

        }

        public DataTable searchStaff(string serach)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Employee] WHERE CONCAT(Employeeid,EmployeeType,FirstName,Lastname,DOB,Address,NIC,Phonenumber,Gender) LIKE '%" + serach + "%'", connection.GetConnection());
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

        public bool DeleteEmployee(string IdNo)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Employee] WHERE Employeeid=" + IdNo + "", connection.GetConnection());
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

        public bool IsEmployeeIdExsist(string id)
        {
            try
            {
                SqlCommand command = new SqlCommand("IF EXISTS(SELECT * FROM Employee WHERE EmployeeId=" + id + " )BEGIN Select  '1' END", connection.GetConnection());
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
