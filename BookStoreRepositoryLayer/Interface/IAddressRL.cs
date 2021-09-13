using System;
using System.Collections.Generic;
using System.Text;
using BookStoreCommonLayer;

namespace BookStoreRepositoryLayer.Interface
{
    public interface IAddressRL
    {
        IEnumerable<AddressResponse> GetAddresses(int userId);
        AddressResponse AddAddress(AddressRequest reqData, int userId);
    }
}
