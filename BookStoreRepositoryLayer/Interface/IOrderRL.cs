using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookStoreCommonLayer;

namespace BookStoreRepositoryLayer.Interface
{
    public interface IOrderRL
    {
        OrderResponse PlaceOrder(OrderRequest reqData);
        Task<IEnumerable<OrderResponse>> MyOrders(int userId);
    }
}
