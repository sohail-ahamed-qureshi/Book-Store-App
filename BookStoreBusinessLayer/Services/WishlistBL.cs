using BookStoreBusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;

namespace BookStoreBusinessLayer.Services
{
    public class WishlistBL : IWishlistBL
    {
        private IWishlistRL wishListRL;
        public WishlistBL(IWishlistRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }

        public List<CartResponse> GetAllItemsInWishList(int userId)
        {
            if(userId != 0)
            {
                var wishListItems = wishListRL.GetAllItemsInWishList(userId);
                if(wishListItems != null)
                {
                    return wishListItems;
                }
            }
            return null;
        }
    }
}
