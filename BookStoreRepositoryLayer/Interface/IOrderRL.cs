using System;
using System.Collections.Generic;
using System.Text;
using BookStoreCommonLayer;

namespace BookStoreRepositoryLayer.Interface
{
    public interface IOrderRL
    {
        OrderResponse PlaceOrder(OrderRequest reqData);
    }
}
