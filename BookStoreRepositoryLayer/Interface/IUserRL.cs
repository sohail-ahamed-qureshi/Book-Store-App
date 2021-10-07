using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.Interface
{
    public interface IUserRL
    {
        Task<User> GetDetails(string email);
        User Register(User userData);
        User Login(Login userData);
        User ForgotPassword(string userName);
        User ResetPassword(User existingUser, string password);
        Task<User> UpdateDetails(UserDetails reqData, int userId);
    }
}
