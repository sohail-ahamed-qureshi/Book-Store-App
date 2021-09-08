using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        private readonly string connectionString;
        public AdminRL(IConfiguration configuration)
        {
            //Database connections
            connectionString = configuration.GetSection("ConnectionStrings").GetSection("BookStoreDB").Value;
        }

        public User Register(User userData)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spRegisterAdmin";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@fullName", userData.FullName);
                    command.Parameters.AddWithValue("@email", userData.Email);
                    command.Parameters.AddWithValue("@password", userData.Password);
                    command.Parameters.AddWithValue("@mobileNumber", userData.MobileNumber);
                    command.Parameters.AddWithValue("@role", userData.Role);
                    command.Parameters.AddWithValue("@createdDate", userData.CreatedDateTime);
                    command.Parameters.AddWithValue("@updatedDate", userData.UpdatedDateTime);
                    connection.Open();
                    int row = command.ExecuteNonQuery();
                    return row >= 1 ? userData : null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

        }

        public User LoginAdmin(string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spLoginAdmin";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.Parameters.AddWithValue("@email", email);
                    SqlDataReader dataReader = command.ExecuteReader();
                    User existingUser = new User();
                    while (dataReader.Read())
                    {
                        existingUser.FullName = dataReader.GetString(0);
                        existingUser.Email = dataReader.GetString(1);
                        existingUser.Password = dataReader.GetString(2);
                        existingUser.UserId = dataReader.GetInt32(3);
                        existingUser.Role = dataReader.GetString(4);
                    }

                    if (existingUser != null)
                    {
                        return existingUser;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
