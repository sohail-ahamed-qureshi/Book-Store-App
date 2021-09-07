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
    public class UserController : ControllerBase
    {
        private IUserBL userBL;
        public UserController(IUserBL userBL)
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
                    string token = userBL.GenerateToken(user.Email, user.UserId);
                    return Ok(new { Success = false, Message = "Login Successfull", data = user, Token = token });
                }
                return Ok(new { Success = false, Message = "Login Failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(string email)
        {
            try
            {
                bool user = userBL.ForgotPassword(email);
                if (user)
                {
                    return Ok(new { Success = true, Message = $"Password Reset Link has been sent to Registered Email: {email}" });
                }
                return NotFound("Invalid Email");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

    }
}
