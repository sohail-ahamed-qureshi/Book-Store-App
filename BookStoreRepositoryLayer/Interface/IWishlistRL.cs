using System;
using System.Collections.Generic;
using System.Text;
using BookStoreCommonLayer;

namespace BookStoreRepositoryLayer.Interface
{
    public interface IWishlistRL
    {
        List<CartResponse> GetAllItemsInWishList(int userId);
    }
}
