using BookStoreBusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;

namespace BookStoreBusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }


        public OrderResponse PlaceOrder(OrderRequest reqData)
        {
            if (reqData != null)
            {
                var orderResponse = orderRL.PlaceOrder(reqData);
                if (orderResponse != null)
                {
                    //on successfull order placement send invoice to user email
                    return orderResponse;
                }
            }
            return null;
        }
    }
}
