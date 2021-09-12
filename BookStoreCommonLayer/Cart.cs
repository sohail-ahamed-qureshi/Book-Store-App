using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreCommonLayer
{
    public class Cart
    {

    }

    public class CartRequest
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
    }

    public class QuantityRequest
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }

    public class CartResponse
    {
        public int BookId { get; set; }
        public string FullName { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price  { get; set; }
        public int Quantity { get; set; }
    }

    public class WishlistRequest
    {
        public int BookId { get; set; }
        public int UserId { get; set; }

    }
}
