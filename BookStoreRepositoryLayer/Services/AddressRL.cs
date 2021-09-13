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
    public class AddressRL : IAddressRL
    {
        readonly string connectionString;
        public AddressRL(IConfiguration configuration)
        {
            //Database connections
            connectionString = configuration.GetSection("ConnectionStrings").GetSection("BookStoreDB").Value;
        }

        public AddressResponse AddAddress(AddressRequest reqData, int userId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spAddAddress";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@address", reqData.Addresses);
                    command.Parameters.AddWithValue("@mobileNumber", reqData.MobileNumber);
                    command.Parameters.AddWithValue("@city", reqData.City);
                    command.Parameters.AddWithValue("@state", reqData.State);
                    command.Parameters.AddWithValue("@typeOf", reqData.typeOf);

                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        AddressResponse addressResponse = new AddressResponse();
                        while (dataReader.Read())
                        {
                             addressResponse = new AddressResponse
                            {
                                AddressId = dataReader.GetInt32(0),
                                UserId = dataReader.GetInt32(1),
                                FullName= dataReader.GetString(2),
                                MobileNumber= dataReader.GetInt64(3),
                                Addresses= dataReader.GetString(4),
                                City=dataReader.GetString(5),
                                State=dataReader.GetString(6),
                                typeOf=dataReader.GetString(7)
                            };
                        }
                        return addressResponse;
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

        public IEnumerable<AddressResponse> GetAddresses(int userId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spGetAddress";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        List<AddressResponse> addressList = new List<AddressResponse>();
                        while (dataReader.Read())
                        {
                            AddressResponse address = new AddressResponse
                            {
                                AddressId = dataReader.GetInt32(0),
                                UserId = dataReader.GetInt32(1),
                                FullName = dataReader.GetString(2),
                                Addresses = dataReader.GetString(3),
                                City = dataReader.GetString(4),
                                State = dataReader.GetString(5),
                                MobileNumber = dataReader.GetInt64(6),
                                typeOf = dataReader.GetString(7)
                            };
                            addressList.Add(address);
                        }
                        return addressList;
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
