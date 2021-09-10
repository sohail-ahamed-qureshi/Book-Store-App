using BookStoreBusinessLayer.Interface;
using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public bool RemoveItemFromCart(CartRequest reqData)
        {
            if (reqData != null)
            {
                return cartRL.RemoveItemFromCart(reqData);
            }
            return false;
        }

        public bool AddItemToCart(CartRequest reqData)
        {
            if (reqData != null)
            {
                return cartRL.AddItemToCart(reqData);
            }
            return false;
        }

        public List<CartResponse> GetAllItemsInCart(int userId)
        {
            if (userId != 0)
            {
                var cartItems = cartRL.GetAllItemsInCart(userId);
                if (cartItems != null)
                {
                    return cartItems;
                }
            }
            return null;
        }

        public CartResponse IncreaseItemCart(CartRequest reqData)
        {
            if (reqData != null)
            {
                CartResponse cartItem = cartRL.IncreaseItemCart(reqData);
                return cartItem;
            }
            return null;
        }
    }
}
