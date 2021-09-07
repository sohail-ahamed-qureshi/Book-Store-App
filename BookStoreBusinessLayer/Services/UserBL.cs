using BookStoreBusinessLayer.Interface;
using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public User Register(User userData)
        {
            if(userData != null)
            {
                return userRL.Register(userData);
            }
            return null;
        }
    }
}
