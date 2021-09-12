using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreCommonLayer
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public long MobileNumber { get; set; }
        public string Addresses { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string TypeOf { get; set; }

    }

    public class AddressRequest
    {
        public long MobileNumber { get; set; }
        public string Addresses { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string typeOf { get; set; }
    }

    public class AddressResponse
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public long MobileNumber { get; set; }
        public string Addresses { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string typeOf { get; set; }
    }
}
