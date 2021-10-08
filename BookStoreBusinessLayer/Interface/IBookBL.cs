using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Interface
{
    public interface IBookBL
    {
        List<Book> GetAllBooks();
        Task<BooksResponse> AddBook(BooksRequest reqData);

        Task<BooksResponse> UpdateBook(BooksResponse reqData);
    }
}
