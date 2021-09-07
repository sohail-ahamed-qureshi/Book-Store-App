using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepositoryLayer.Interface
{
    public interface IUserRL
    {
        User Register(User userData);
        User Login(Login userData);
        User ForgotPassword(string userName);
        User ResetPassword(User existingUser, string password);
        User GetUserDetails(string email);
    }
}
