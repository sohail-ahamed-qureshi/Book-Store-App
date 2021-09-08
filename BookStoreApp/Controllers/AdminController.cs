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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly IAdminBL adminBL;
        public AdminController( IAdminBL adminBL, IUserBL userBL)
        {
            this.adminBL = adminBL;
            this.userBL = userBL;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RegisterAdmin(UserRequest user)
        {
            try
            {
                var userResponse = adminBL.Register(user);
                if (userResponse != null)
                {
                    return Ok(new { Success = false, Message = "Registration Successfull", data = userResponse });
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
                var user = adminBL.LoginAdmin(userData);
                if (user != null)
                {
                    string token = userBL.GenerateToken(user.Email, user.UserId, user.Role);
                    return Ok(new { Success = false, Message = "Login Successfull", data = user, Token = token });
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
