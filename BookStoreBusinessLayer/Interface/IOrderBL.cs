using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Interface
{
    public interface IOrderBL
    {
        OrderResponse PlaceOrder(OrderRequest reqData);
    }
}
