using BookStoreBusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;
        private readonly IEmailSender emailSender;
        public OrderBL(IOrderRL orderRL, IEmailSender emailSender)
        {
            this.orderRL = orderRL;
            this.emailSender = emailSender;
        }


        public OrderResponse PlaceOrder(OrderRequest reqData)
        {
            if (reqData != null)
            {
                var orderResponse = orderRL.PlaceOrder(reqData);
                if (orderResponse != null)
                {
                    //on successfull order placement send invoice to user email
                    var emailMessage = new SuccessMail(new string[] { "sohailqureshi82@gmail.com" }, $"Order Places Successfully!!!, Your order id: #{orderResponse.OrderId}",orderResponse);
                    emailSender.SendSuccessEmail(emailMessage);
                    return orderResponse;
                }
            }
            return null;
        }

        public async Task<IEnumerable<OrderResponse>> MyOrders(int userId)
        {
            if(userId != 0)
            {
                var orderList = await orderRL.MyOrders(userId);
                if(orderList != null)
                {
                    return orderList;
                }
            }
            return null;
        }
    }
}
