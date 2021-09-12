using BookStoreBusinessLayer.Interface;
using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public IEnumerable<AddressResponse> GetAddresses(int userId)
        {
            if(userId != 0)
            {
               var addressList = addressRL.GetAddresses(userId);
                if(addressList != null)
                {
                    return addressList;
                }
            }
            return null;
        }
    }
}
