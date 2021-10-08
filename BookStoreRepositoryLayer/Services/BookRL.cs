using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.Services
{

    public class BookRL : IBookRL
    {
        private string connectionString;
        public BookRL(IConfiguration configuration)
        {
            //Database connections
            connectionString = configuration.GetSection("ConnectionStrings").GetSection("BookStoreDB").Value;
        }

        public async Task<BooksResponse> AddBook(BooksRequest reqData)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                BooksResponse book = new BooksResponse();
                await Task.Run(() =>
                 {
                     using (connection)
                     {
                         string spName = "spAddBook";
                         SqlCommand command = new SqlCommand(spName, connection);
                         command.CommandType = CommandType.StoredProcedure;
                         command.Parameters.AddWithValue("@bookName", reqData.BookName);
                         command.Parameters.AddWithValue("@author", reqData.Author);
                         command.Parameters.AddWithValue("@description", reqData.Description);
                         command.Parameters.AddWithValue("@price", reqData.Price);
                         command.Parameters.AddWithValue("@quantity", reqData.Quantity);
                         command.Parameters.AddWithValue("@image", reqData.Image);
                         command.Parameters.AddWithValue("@rating", reqData.Rating);
                         connection.Open();
                         SqlDataReader dataReader = command.ExecuteReader();
                         if (dataReader.HasRows)
                         {
                            
                             while (dataReader.Read())
                             {
                                 book = new BooksResponse
                                 {
                                     BookId = dataReader.GetInt32(0),
                                     BookName = dataReader.GetString(1) == null ? string.Empty : dataReader.GetString(1),
                                     Author = dataReader.GetString(2) == null ? string.Empty : dataReader.GetString(2),
                                     Description = dataReader.GetString(3) == null ? string.Empty : dataReader.GetString(3),
                                     Price = Convert.ToDouble(dataReader.GetDecimal(4)),
                                     Rating = Convert.ToDouble(dataReader.GetDecimal(5)),
                                     Image = dataReader.GetString(6),
                                     Quantity = dataReader.GetInt32(7)
                                 };
                             }
                             return book;
                         }
                         return null;
                     }
                 });
                return book;
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

        public async Task<BooksResponse> DeleteBook(int bookId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                BooksResponse book = new BooksResponse();
                await Task.Run(() =>
                {
                    using (connection)
                    {
                        string spName = "spDeleteBook";
                        SqlCommand command = new SqlCommand(spName, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@bookId", bookId);                
                        connection.Open();
                        SqlDataReader dataReader = command.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                book = new BooksResponse
                                {
                                    BookId = dataReader.GetInt32(0),
                                    BookName = dataReader.GetString(1) == null ? string.Empty : dataReader.GetString(1),
                                    Author = dataReader.GetString(2) == null ? string.Empty : dataReader.GetString(2),
                                    Description = dataReader.GetString(3) == null ? string.Empty : dataReader.GetString(3),
                                    Price = Convert.ToDouble(dataReader.GetDecimal(4)),
                                    Rating = Convert.ToDouble(dataReader.GetDecimal(5)),
                                    Image = dataReader.GetString(6),
                                    Quantity = dataReader.GetInt32(7)
                                };
                            }
                            return book;
                        }
                        return null;
                    }
                });
                return book;
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

        public List<Book> GetAllBooks()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string spName = "spGetAllBooks";
                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {

                        List<Book> allBooks = new List<Book>();
                        while (dataReader.Read())
                        {
                            Book book = new Book
                            {
                                BookId = dataReader.GetInt32(0),
                                BookName = dataReader.GetString(1) == null ? string.Empty : dataReader.GetString(1),
                                Author = dataReader.GetString(2) == null ? string.Empty : dataReader.GetString(2),
                                Description = dataReader.GetString(3) == null ? string.Empty : dataReader.GetString(3),
                                Price = Convert.ToDouble(dataReader.GetDecimal(4)),
                                Rating = Convert.ToDouble(dataReader.GetDecimal(5)),
                                Image = dataReader.GetString(6),
                                Quantity = dataReader.GetInt32(7)
                            };
                            allBooks.Add(book);
                        }
                        return allBooks;
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

        public async Task<BooksResponse> UpdateBook(BooksResponse reqData)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                BooksResponse book = new BooksResponse();
                await Task.Run(() =>
                {
                    using (connection)
                    {
                        string spName = "spUpdateBook";
                        SqlCommand command = new SqlCommand(spName, connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@bookId", reqData.BookId);
                        command.Parameters.AddWithValue("@bookName", reqData.BookName);
                        command.Parameters.AddWithValue("@author", reqData.Author);
                        command.Parameters.AddWithValue("@description", reqData.Description);
                        command.Parameters.AddWithValue("@price", reqData.Price);
                        command.Parameters.AddWithValue("@quantity", reqData.Quantity);
                        command.Parameters.AddWithValue("@image", reqData.Image);
                        command.Parameters.AddWithValue("@rating", reqData.Rating);
                        connection.Open();
                        SqlDataReader dataReader = command.ExecuteReader();
                        if (dataReader.HasRows)
                        {

                            while (dataReader.Read())
                            {
                                book = new BooksResponse
                                {
                                    BookId = dataReader.GetInt32(0),
                                    BookName = dataReader.GetString(1) == null ? string.Empty : dataReader.GetString(1),
                                    Author = dataReader.GetString(2) == null ? string.Empty : dataReader.GetString(2),
                                    Description = dataReader.GetString(3) == null ? string.Empty : dataReader.GetString(3),
                                    Price = Convert.ToDouble(dataReader.GetDecimal(4)),
                                    Rating = Convert.ToDouble(dataReader.GetDecimal(5)),
                                    Image = dataReader.GetString(6),
                                    Quantity = dataReader.GetInt32(7)
                                };
                            }
                            return book;
                        }
                        return null;
                    }
                });
                return book;
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
