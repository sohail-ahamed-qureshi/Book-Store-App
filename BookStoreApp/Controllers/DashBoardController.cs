using BookStoreBusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public DashBoardController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllBooks()
        {

            try
            {
                var allbooks =bookBL.GetAllBooks();
                if(allbooks.Count > 0)
                {
                    return Ok(new { success = true, message = $"you have {allbooks.Count} Books", data = allbooks });
                }
                return Ok(new { success = true, message = "No books to display" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            
        }

        
    }
}
