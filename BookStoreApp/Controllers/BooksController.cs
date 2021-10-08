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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Role.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookBL booksBL;
        public BooksController(IBookBL booksBL)
        {
            this.booksBL = booksBL;
        }


        private async Task<int> GetUserIDFromToken()
        {
            int userId = 0;
            await Task.Run(() =>
               userId = Convert.ToInt32(User.FindFirst(user => user.Type == "userId").Value)
            );
            return userId;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BooksRequest reqData)
        {
            try
            {
                int userId = await GetUserIDFromToken();
                if (userId != 0)
                {
                    var bookAdded = await booksBL.AddBook(reqData);
                    if (bookAdded != null)
                    {
                        return Ok(new { Success = true, Message = $"Book Added Successfully", data = bookAdded });
                    }
                    return BadRequest(new { Success = false, Message = "Book Add failed" });
                }
                return BadRequest(new { Success = false, Message = "Invalid Details" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] BooksResponse reqData)
        {
            try
            {
                int userId = await GetUserIDFromToken();
                if (userId != 0)
                {
                    var bookUpdated = await booksBL.UpdateBook(reqData);
                    if (bookUpdated != null)
                    {
                        return Ok(new { Success = true, Message = $"Book Updated Successfully", data = bookUpdated });
                    }
                    return BadRequest(new { Success = false, Message = "Book Update failed" });
                }
                return BadRequest(new { Success = false, Message = "Invalid Details" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int bookId)
        {
            try
            {
                int userId = await GetUserIDFromToken();
                if (userId != 0)
                {
                    var bookdeleted = await booksBL.DeleteBook(bookId);
                    if (bookdeleted != null)
                    {
                        return Ok(new { Success = true, Message = $"Book Deleted Successfully", data = bookdeleted });
                    }
                    return BadRequest(new { Success = false, Message = "Book Delete failed" });
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
