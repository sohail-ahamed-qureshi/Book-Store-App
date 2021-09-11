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
    [Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme ,Roles = Role.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private IWishlistBL wishlistBL;
        public WishListController(IWishlistBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        private int GetUserIDFromToken()
        {
            return Convert.ToInt32(User.FindFirst(user => user.Type == "userId").Value);
        }

        [HttpGet]
        public IActionResult GetAllWishlistItems()
        {
            try
            {
                int userId = GetUserIDFromToken();
                if (userId != 0)
                {
                    var wishlistItems = wishlistBL.GetAllItemsInWishList(userId);
                    if(wishlistItems != null)
                    {
                        return Ok(new { Success = true, Message = $"you have {wishlistItems.Count} items in wishlist", data = wishlistItems });
                    }
                }
                return BadRequest(new { Success = false, Message = "Invalid Details" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }


    }
}
