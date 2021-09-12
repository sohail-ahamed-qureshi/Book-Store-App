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


        [HttpPost]
        public IActionResult AddItemtoWishlist(int bookId)
        {
            try
            {
                int userId = GetUserIDFromToken();
                if (userId != 0)
                {
                    WishlistRequest reqData = new WishlistRequest
                    {
                        BookId = bookId,
                        UserId = userId
                    };
                    var wishlistItem = wishlistBL.AddItemToWishlist(reqData);      
                    if (wishlistItem != null)
                    {
                        return Ok(new { Success = true, Message = $"Item added to wishlist", data = wishlistItem });
                    }
                    return BadRequest(new { Success = false, Message = "Item add failed" });
                }
                return BadRequest(new { Success = false, Message = "Invalid Details" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("{bookId}")]
        public IActionResult RemoveItemFromWishlist([FromRoute]  int bookId )
        {
            try
            {
                int userId = GetUserIDFromToken();
                if (userId != 0)
                {
                    WishlistRequest reqData = new WishlistRequest
                    {
                        BookId = bookId,
                        UserId = userId
                    };
                    var wishlistItem = wishlistBL.RemoveItemFromWishlist(reqData);
                    if(wishlistItem != null)
                    {
                        return Ok(new { Success = true, Message = $"Item removed from wishlist", data = wishlistItem });
                    }
                    return BadRequest(new { Success = false, Message = "Item remove failed" });
                }
                return BadRequest(new { Success = false, Message = "Invalid Details" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }



    }
}
