using BookStoreBusinessLayer.Interface;
using BookStoreCommonLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    [EnableCors()]
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
        public ActionResult RegisterUser([FromBody] User userData)
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
        public ActionResult LoginUser([FromBody] Login userData)
        {
            try
            {
                var user = userBL.Login(userData);
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

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword([FromBody] string email)
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

        [Authorize(Roles = Role.User)]
        [HttpPut("resetpassword/{token}")]
        public async Task<ActionResult> ResetPassword([FromRoute] string token, [FromBody] ResetPassword reset)
        {
            if (reset != null && token != null)
            {
                //extracting userId from token
                string email = await GetEmailFromToken();
                if (email != null)
                {
                    User updatedUser = userBL.ResetPassword(email, reset);
                    if (updatedUser.UserId != 0)
                    {
                        return Ok(new { Success = true, Message = $"Reset Password Successfully at {updatedUser.UpdatedDateTime}", Data = updatedUser });
                    }
                }
            }
            return NotFound(new { Success = false, Messgae = "Invalid User Details" });
        }

        private async Task<string> GetEmailFromToken()
        {
            string email = "";
            await Task.Run(() =>
           //getting user details from token
           email = User.FindFirst(user => user.Type == ClaimTypes.Email).Value
            );
            return email;

        }

        [Authorize]
        [HttpGet("GetDetails")]
        public async Task<IActionResult> GetDetails()
        {
            try
            {
                string email = await GetEmailFromToken();
                if (email != null)
                {
                    var existingUser = await userBL.GetDetails(email);
                    if (existingUser != null)
                    {
                        return Ok(new { Success = true, Message = $"Is an Existing User", data = existingUser });
                    }
                }
                return NotFound(new { Success = false, Message ="Invalid user, please try login/SignUp"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }

        }

    }
}
