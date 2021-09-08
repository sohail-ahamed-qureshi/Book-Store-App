﻿using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Interface
{
    public interface IUserBL
    {
        User Register(User userData);
        UserResponse Login(Login userData);
        string GenerateToken(string userEmail, int userId,  string role);
        bool ForgotPassword(string email);
        User ResetPassword(string userId, ResetPassword resetPassword);
    }
}
