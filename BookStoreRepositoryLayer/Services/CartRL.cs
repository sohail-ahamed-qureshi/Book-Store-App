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
    public class CartRL : ICartRL
    {
        private readonly string connectionString;
        public CartRL(IConfiguration configuration)
        {
            connectionString = configuration.GetSection("ConnectionStrings").GetSection("BookStoreDB").Value;
        }


        public bool RemoveItemFromCart(CartRequest reqData)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spRemoveItemToCart";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", reqData.UserId);
                    command.Parameters.AddWithValue("@bookId", reqData.BookId);
                    int row = command.ExecuteNonQuery();
                    return row == 1;
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

        public bool AddItemToCart(CartRequest reqData)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spAddItemToCart";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", reqData.UserId);
                    command.Parameters.AddWithValue("@bookId", reqData.BookId);
                    command.Parameters.AddWithValue("@quantity", reqData.Quantity);
                    int row = command.ExecuteNonQuery();
                    return row == 1;
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

        public List<CartResponse> GetAllItemsInCart(int userId)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spGetAllItemsInCart";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        List<CartResponse> cartItems = new List<CartResponse>();
                        while (dataReader.Read())
                        {
                            CartResponse cart = new CartResponse
                            {
                                FullName = dataReader.GetString(0),
                                BookName = dataReader.GetString(1) == null ? string.Empty : dataReader.GetString(1),
                                Author = dataReader.GetString(2) == null ? string.Empty : dataReader.GetString(2),
                                Description = dataReader.GetString(3) == null ? string.Empty : dataReader.GetString(3),
                                Price = dataReader.GetDecimal(4),
                                Quantity = dataReader.GetInt32(5)
                            };
                            cartItems.Add(cart);
                        }
                        return cartItems;
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

        public CartResponse IncreaseItemCart(CartRequest reqData)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spIncreaseQuantityInCart";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", reqData.UserId);
                    command.Parameters.AddWithValue("@bookId", reqData.BookId);
                    command.Parameters.AddWithValue("@quantity", reqData.Quantity);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        CartResponse cart = new CartResponse();
                        while (dataReader.Read())
                        {
                            cart = new CartResponse
                            {
                                BookId = dataReader.GetInt32(0),
                                FullName = dataReader.GetString(1),
                                BookName = dataReader.GetString(2) ?? string.Empty,
                                Author = dataReader.GetString(3) ?? string.Empty,
                                Description = dataReader.GetString(4) ?? string.Empty,
                                Price = dataReader.GetDecimal(5),
                                Quantity = dataReader.GetInt32(6)
                            };
                        }
                        return cart;
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
