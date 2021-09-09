using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepositoryLayer.Interface
{
    public interface IBookRL
    {
        List<Book> GetAllBooks();
    }
}
