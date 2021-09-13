using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Interface
{
    public interface IAddressBL
    {
        IEnumerable<AddressResponse> GetAddresses(int userId);
        AddressResponse AddAddress(AddressRequest reqData, int userId);
    }
}
