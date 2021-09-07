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
        string connectionString;
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
                    command.Parameters.AddWithValue("@createdDate", userData.CreatedDateTime);
                    command.Parameters.AddWithValue("@updatedDate", userData.UpdatedDateTime);
                    connection.Open();
                    int row = command.ExecuteNonQuery();
                    return row == 1 ? userData : null;
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
            User existingUser = GetUserDetails(userData.Email);
            return existingUser;
        }

        private User GetUserDetails(string email)
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
                    User existingUser = new User();
                    while (dataReader.Read())
                    {
                        existingUser.FullName = dataReader.GetString(0);
                        existingUser.Email = dataReader.GetString(1);
                        existingUser.Password = dataReader.GetString(2);
                        existingUser.UserId = dataReader.GetInt32(3);
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

        public User ForgotPassword(string email)
        {
            try
            {
                User existingUser = GetUserDetails(email);
                return existingUser;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public User ResetPassword(User existingUser, string password)
        {
            throw new NotImplementedException();
        }

        bool IUserRL.ResetPassword(User existingUser, string password)
        {
            throw new NotImplementedException();
        }
    }
}
