using BookStoreBusinessLayer.Interface;
using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        private IUtility utility;
        private IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL, IUtility utility)
        {
            this.utility = utility;
            this.adminRL = adminRL;
        }

        public UserResponse Register(UserRequest userData)
        {
            if (userData != null)
            {
                userData.Password = utility.EncodePassword(userData.Password);
                User newUser = new User
                {
                    FullName = userData.FullName,
                    Email = userData.Email,
                    Password = userData.Password,
                    Role = "Admin",
                    MobileNumber = userData.MobileNumber,
                    CreatedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now
                };

                User user = adminRL.Register(newUser);
                if (user != null)
                {
                    UserResponse userResponse = new UserResponse
                    {
                        UserId = user.UserId,
                        FullName = user.FullName,
                        Email = user.Email,
                        Role = user.Role
                    };
                    return userResponse;
                }
            }
            return null;
        }

        public UserResponse LoginAdmin(Login userData)
        {
            if (userData != null)
            {
                User existingUser = adminRL.LoginAdmin(userData.Email);
                if (existingUser != null)
                {
                    existingUser.Password = utility.DecodePassword(existingUser.Password);
                    if (existingUser.Password.Equals(userData.Password))
                    {
                        UserResponse userResponse = new UserResponse
                        {
                            FullName = existingUser.FullName,
                            Email = existingUser.Email,
                            UserId = existingUser.UserId,
                            Role = existingUser.Role
                        };
                        return userResponse;
                    }
                }
            }
            return null;
        }
    }
}
