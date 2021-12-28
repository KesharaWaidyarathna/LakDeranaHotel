using LakDeranaHotel.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakDeranaHotel
{
    class Customer
    {
        DBconnection connection = new DBconnection();

        public bool instertCustomer(CustomerDAO customer)
        {
            try{
                SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Customer]([FirstName],[LastName],[DOB],[Address],[NIC],[PhoneNumber],[Gender],[Image])VALUES(@FirstName,@LastName,@DOB ,@Address,@NIC,@PhoneNumber,@Gender,@Image)", connection.GetConnection());
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = customer.firstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = customer.lastname;
                command.Parameters.Add("@DOB", SqlDbType.DateTime).Value = customer.DOB;
                command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = customer.address;
                command.Parameters.Add("@NIC", SqlDbType.NVarChar).Value = customer.NIC;
                command.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = customer.phone;
                command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = customer.gender;
                command.Parameters.Add("@Image", SqlDbType.VarBinary).Value = customer.image;

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
            }catch (Exception ex)
            {
                connection.closeConnection();
                throw ex;
            }
        }

        public bool updateCustomer(CustomerDAO customer)
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE [dbo].[Customer]SET [FirstName] =@FirstName,[LastName] =@LastName,[DOB] = @DOB,[Address] = @Address,[NIC] = @NIC,[PhoneNumber] = @PhoneNumber,[Gender] = @Gender,[Image] = @Image WHERE customerId=@IdNo", connection.GetConnection());
                command.Parameters.Add("@IdNo", SqlDbType.Int).Value = customer.idNo;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = customer.firstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = customer.lastname;
                command.Parameters.Add("@DOB", SqlDbType.DateTime).Value = customer.DOB;
                command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = customer.address;
                command.Parameters.Add("@NIC", SqlDbType.NVarChar).Value = customer.NIC;
                command.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = customer.phone;
                command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = customer.gender;
                command.Parameters.Add("@Image", SqlDbType.VarBinary).Value = customer.image;

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
        public DataTable getCustomerList()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Customer]", connection.GetConnection());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                return table;

            }catch(Exception ex)
            {
                throw ex;
            }

        }

        public DataTable searchCustomer(string serach)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Customer] WHERE CONCAT(FirstName,LastName,DOB,Address,NIC,PhoneNumber,Gender) LIKE '%"+serach+"%'", connection.GetConnection());
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

        public bool DeleteCustomer(string IdNo)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Customer] WHERE StudentId=" + IdNo + "", connection.GetConnection());
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

        public string totalCustomer()
        {
            return exeCount("SELECT COUNT(*) FROM [dbo].[Customer]");
        }

        public string totalMaleCustomer()
        {
            return exeCount("SELECT COUNT(*) FROM [dbo].[Customer] WHERE Gender='Male'");
        }

        public string totalFemaleCustomer()
        {
            return exeCount("SELECT COUNT(*) FROM [dbo].[Customer] WHERE Gender='Female'");
        }

        public string exeCount(string query)
        {
            try
            {
                SqlCommand command = new SqlCommand(query, connection.GetConnection());
                connection.openConnection();
                string count = command.ExecuteScalar().ToString();
                connection.closeConnection();
                return count;
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
    }
}
