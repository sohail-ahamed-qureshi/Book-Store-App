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
    public class WishlistRL : IWishlistRL
    {
        readonly string connectionString;
        public WishlistRL(IConfiguration configuration)
        {
            //Database connections
            connectionString = configuration.GetSection("ConnectionStrings").GetSection("BookStoreDB").Value;
        }

        public CartResponse AddItemToWishlist(WishlistRequest reqData)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spAddItemtoWishlist";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.Parameters.AddWithValue("@userId", reqData.UserId);
                    command.Parameters.AddWithValue("@bookId", reqData.BookId);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        CartResponse wishListItem = new CartResponse();
                        while (dataReader.Read())
                        {
                            wishListItem = new CartResponse
                            {
                                BookId = dataReader.GetInt32(0),
                                FullName = dataReader.GetString(1),
                                BookName = dataReader.GetString(2) == null ? string.Empty : dataReader.GetString(2),
                                Author = dataReader.GetString(3) == null ? string.Empty : dataReader.GetString(3),
                                Price = dataReader.GetDecimal(4)
                            };
                        }
                        return wishListItem;
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

        public List<CartResponse> GetAllItemsInWishList(int userId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spGetAllItemsInWishlist";
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
                                BookId = dataReader.GetInt32(0),
                                FullName = dataReader.GetString(1),
                                BookName = dataReader.GetString(2) == null ? string.Empty : dataReader.GetString(2),
                                Author = dataReader.GetString(3) == null ? string.Empty : dataReader.GetString(3),
                                Price = dataReader.GetDecimal(4)
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
    }
}
