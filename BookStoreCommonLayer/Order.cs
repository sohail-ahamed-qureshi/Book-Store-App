using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreCommonLayer
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public int AddressId { get; set; }
        public DateTime OrderDate  { get; set; }

    }

    public class OrderRequest
    {
        public int CartId { get; set; }
        public int AddressId { get; set; }
    }

    public class OrderResponse
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int CartId { get; set; }
        public int AddressId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
