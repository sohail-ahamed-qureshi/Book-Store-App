using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreCommonLayer
{
  
    public class BooksResponse
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }

    public class BooksRequest
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
