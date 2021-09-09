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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        private int GetUserIDFromToken()
        {
            return Convert.ToInt32(User.FindFirst(user => user.Type == "userId").Value);
        }

        [HttpGet]
        public IActionResult GetItemsFromCart()
        {
            try
            {
                int id = GetUserIDFromToken();
                if (id != 0)
                {
                    var cartitems = cartBL.GetAllItemsInCart(id);
                    if( cartitems != null)
                    {
                        return Ok(new { success = true, message = $"you have {cartitems.Count} items in cart", data = cartitems });
                    }
                    return Ok(new { success = true, message = $"your cart is Empty" });
                }
                return BadRequest(new { success = false, message = "Invalid Details" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddItemToCart([FromBody] CartRequest reqData)
        {
            try
            {
                int id = GetUserIDFromToken();
                if (id != 0)
                {
                    reqData.UserId = id;
                    bool isAdded = cartBL.AddItemToCart(reqData);
                    if (isAdded)
                    {
                        return Ok(new { success = true, message = $"Item added to card" });
                    }
                }
                return BadRequest(new { success = false, message = "Invalid Details" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("{bookId}")]
        public IActionResult RemoveItemFromCart([FromRoute] int bookId)
        {

            try
            {
                int id = GetUserIDFromToken();
                if (id != 0 && bookId != 0)
                {
                    CartRequest reqData = new CartRequest
                    {
                        BookId = bookId,
                        UserId = id
                    };
                    bool isRemoved = cartBL.RemoveItemFromCart(reqData);
                    if (isRemoved)
                    {
                        return Ok(new { success = true, message = $"Item removed from cart" });
                    }
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
