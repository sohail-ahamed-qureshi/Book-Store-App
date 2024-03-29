﻿using BookStoreBusinessLayer.Interface;
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
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        private int GetUserIDFromToken()
        {
            return Convert.ToInt32(User.FindFirst(user => user.Type == "userId").Value);
        }

        [HttpGet]
        public IActionResult GetAddresses()
        {
            try
            {
                int userId = GetUserIDFromToken();
                if(userId != 0)
                {
                   var addressList = addressBL.GetAddresses(userId);
                    if(addressList != null)
                    {
                        return Ok(new { Success = true, Message = $"You have {addressList.Count()} addresses", data = addressList });
                    }
                    return BadRequest(new { Success = false, Message = "Address list is empty" });
                }
                return BadRequest(new {Success = false, Message = "Invalid Details" });
            }
            catch(Exception ex)
            {
                return BadRequest(new {Success = false, Message = ex.Message });
            }
        }


        [HttpGet("{typeOf}")]
        public IActionResult GetAddresses([FromRoute] string typeOf )
        {
            try
            {
                int userId = GetUserIDFromToken();
                if (userId != 0 && typeOf != "")
                {
                    var address = addressBL.GetAddress(userId, typeOf);
                    if (address != null)
                    {
                        return Ok(new { Success = true, Message = $"address", data = address });
                    }
                    return BadRequest(new { Success = false, Message = "Address is Not present" });
                }
                return BadRequest(new { Success = false, Message = "Invalid Details" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }


        [HttpPost]
        public IActionResult AddAddresses([FromBody]AddressRequest reqData)
        {
            try
            {
                int userId = GetUserIDFromToken();
                if (userId != 0)
                {
                    var address = addressBL.AddAddress(reqData, userId);
                    if (address != null)
                    {
                        return Ok(new { Success = true, Message = $"Address added Successfully", data = address });
                    }
                    return BadRequest(new { Success = false, Message = "Address add failed" });
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
