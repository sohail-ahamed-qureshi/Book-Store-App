using BookStoreBusinessLayer.Interface;
using BookStoreCommonLayer;
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
    public class BookStoreController : ControllerBase
    {
        private IUserBL userBL;
        public BookStoreController(IUserBL userBL)
        {
            this.userBL = userBL;
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult RegisterUser(User userData)
        {
            try
            {
                var user = userBL.Register(userData);
                if (user != null)
                {
                    return Ok(new { Success = false, Message = "Registration Successfull", data = user });
                }
                return Ok(new { Success = false, Message = "Registration Failed, User Already Exists" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult LoginUser(Login userData)
        {
            try
            {
                var user = userBL.Login(userData);
                if (user != null)
                {
                    return Ok(new { Success = false, Message = "Login Successfull", data = user });
                }
                return Ok(new { Success = false, Message = "Login Failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

    }
}
