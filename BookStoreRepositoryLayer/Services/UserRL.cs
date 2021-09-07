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
                    int  row =  command.ExecuteNonQuery();
                    return row == 1 ? userData : null;
                }
            }
            catch(Exception)
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
