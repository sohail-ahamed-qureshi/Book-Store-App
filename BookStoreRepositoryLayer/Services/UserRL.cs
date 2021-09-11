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
    public class UserRL : IUserRL
    {
        readonly string connectionString;
        public UserRL(IConfiguration configuration)
        {
            //Database connections
            connectionString = configuration.GetSection("ConnectionStrings").GetSection("BookStoreDB").Value;
        }

        /// <summary>
        /// ability to register a new User
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public User Register(User userData)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spRegisterUser";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@fullName", userData.FullName);
                    command.Parameters.AddWithValue("@email", userData.Email);
                    command.Parameters.AddWithValue("@password", userData.Password);
                    command.Parameters.AddWithValue("@mobileNumber", userData.MobileNumber);
                    command.Parameters.AddWithValue("@role", Role.User);
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

        /// <summary>
        /// ability to login user
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public User Login(Login userData)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spLoginUser";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.Parameters.AddWithValue("@email", userData.Email);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        User existingUser = new User();
                        while (dataReader.Read())
                        {
                            existingUser.FullName = dataReader.GetString(0);
                            existingUser.Email = dataReader.GetString(1);
                            existingUser.Password = dataReader.GetString(2);
                            existingUser.UserId = dataReader.GetInt32(3);
                            existingUser.Role = dataReader.GetString(4);
                        }
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

        public User ForgotPassword(string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spGetUser";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.Parameters.AddWithValue("@email", email);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        User existingUser = new User();
                        while (dataReader.Read())
                        {
                            existingUser.FullName = dataReader.GetString(0);
                            existingUser.Email = dataReader.GetString(1);
                            existingUser.Password = dataReader.GetString(2);
                            existingUser.UserId = dataReader.GetInt32(3);
                        }
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

        public User ResetPassword(User existingUser, string newPassword)
        {
            existingUser.Password = newPassword;
            existingUser.UpdatedDateTime = DateTime.Now;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spResetPassword";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", existingUser.Email);
                    command.Parameters.AddWithValue("@newPassword", existingUser.Password);
                    command.Parameters.AddWithValue("@updatedDate", existingUser.UpdatedDateTime);
                    connection.Open();
                    int row = command.ExecuteNonQuery();
                    return row >= 1 ? existingUser : null;
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
