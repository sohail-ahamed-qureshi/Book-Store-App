using BookStoreBusinessLayer.Interface;
using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
         public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public async Task<BooksResponse> AddBook(BooksRequest reqData)
        {
            if (reqData != null)
            {
                return await bookRL.AddBook(reqData);
            }
            return null;
        }

        public List<Book> GetAllBooks()
        {
            var allbooks = bookRL.GetAllBooks();
            if(allbooks.Count != 0)
            {
                return allbooks;
            }
            return null;
        }

        public async Task<BooksResponse> UpdateBook(BooksResponse reqData)
        {
            if (reqData != null)
            {
                return await bookRL.UpdateBook(reqData);
            }
            return null;
        }
    }
}
