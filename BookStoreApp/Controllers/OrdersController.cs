using BookStoreBusinessLayer.Interface;
using BookStoreCommonLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Role.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        public OrdersController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        private int GetUserIDFromToken()
        {
            return Convert.ToInt32(User.FindFirst(user => user.Type == "userId").Value);
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderRequest reqData)
        {
            try
            {
                int id = GetUserIDFromToken();
                if (id != 0)
                {
                    var orderPlaced = orderBL.PlaceOrder(reqData);
                    if (orderPlaced != null)
                    {
                        return Ok(new { success = true, message = $"hurray!!! your order is confirmed the order id is #{orderPlaced.OrderId} save the order id for further communication..", data = orderPlaced });
                    }
                    return BadRequest(new { success = false, message = "Order placed failed." });
                }
                return BadRequest(new { success = false, message = "Invalid Details" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

    }
}
