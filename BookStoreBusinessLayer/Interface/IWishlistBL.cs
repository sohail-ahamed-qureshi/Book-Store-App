﻿using System;
using System.Collections.Generic;
using System.Text;
using BookStoreCommonLayer;

namespace BookStoreBusinessLayer.Interface
{
    public interface IWishlistBL
    {
        List<CartResponse> GetAllItemsInWishList(int userId);
    }
}
