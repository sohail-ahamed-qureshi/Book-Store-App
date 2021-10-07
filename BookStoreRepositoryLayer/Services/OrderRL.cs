using BookStoreRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using BookStoreCommonLayer;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        private string connectionString;
        public OrderRL(IConfiguration configuration)
        {
            //Database connections
            connectionString = configuration.GetSection("ConnectionStrings").GetSection("BookStoreDB").Value;
        }
        public OrderResponse PlaceOrder(OrderRequest reqData)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spPlaceOrder";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.Parameters.AddWithValue("@cartId", reqData.CartId);
                    command.Parameters.AddWithValue("@addressId", reqData.AddressId);

                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        OrderResponse orderResponse = new OrderResponse();
                        while (dataReader.Read())
                        {
                            orderResponse = new OrderResponse
                            {
                                OrderId = dataReader.GetInt32(0),
                                UserId = dataReader.GetInt32(1),
                                FullName = dataReader.GetString(2),
                                Email = dataReader.GetString(3),
                                BookId = dataReader.GetInt32(4),
                                BookName = dataReader.GetString(5),
                                Price = dataReader.GetDecimal(6),
                                AddressId = dataReader.GetInt32(7),
                                Address = dataReader.GetString(8),
                                Quantity = dataReader.GetInt32(9),
                                OrderDate = dataReader.GetDateTime(10),
                                TotalPrice = dataReader.GetDecimal(11)
                            };
                        }
                        return orderResponse;
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

        public async Task<IEnumerable<OrderResponse>> MyOrders(int userId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                List<OrderResponse> ordersList = new List<OrderResponse>();
                await Task.Run(() =>
                {
                    using (connection)
                    {
                        string spName = "spMyOrders";
                        SqlCommand command = new SqlCommand(spName, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.Parameters.AddWithValue("@userId", userId);

                        SqlDataReader dataReader = command.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                           
                            OrderResponse orderResponse = new OrderResponse();
                            while (dataReader.Read())
                            {
                                orderResponse = new OrderResponse
                                {
                                    OrderId = dataReader.GetInt32(0),
                                    FullName = dataReader.GetString(1),
                                    BookName = dataReader.GetString(2),
                                    Quantity = dataReader.GetInt32(3),
                                    OrderDate = dataReader.GetDateTime(4),
                                    Price = dataReader.GetDecimal(5),
                                    TotalPrice = dataReader.GetDecimal(6),
                                    Image=dataReader.GetString(7),
                                    Author=dataReader.GetString(8)
                                };
                                ordersList.Add(orderResponse);
                            }
                           
                        }
                    }
                });
                return ordersList;

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
