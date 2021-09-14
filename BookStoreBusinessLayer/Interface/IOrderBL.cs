using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Interface
{
    public interface IOrderBL
    {
        OrderResponse PlaceOrder(OrderRequest reqData);

        Task<IEnumerable<OrderResponse>> MyOrders(int userId);
    }
}
