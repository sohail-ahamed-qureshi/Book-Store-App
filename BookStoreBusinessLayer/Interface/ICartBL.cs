using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Interface
{
    public interface ICartBL
    {
        List<CartResponse> GetAllItemsInCart(int userId);

        bool AddItemToCart(CartRequest reqData);
        bool RemoveItemFromCart(CartRequest reqData);

        CartResponse IncreaseItemCart(CartRequest reqData);
    }
}
