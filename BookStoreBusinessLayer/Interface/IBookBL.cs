using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Interface
{
    public interface IBookBL
    {
        List<Book> GetAllBooks();
    }
}
