using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.Interface
{
    public interface IBookRL
    {
        List<Book> GetAllBooks();
        Task<BooksResponse> AddBook(BooksRequest reqData);
        Task<BooksResponse> UpdateBook(BooksResponse reqData);
        Task<BooksResponse> DeleteBook(int bookId);
    }
}
