using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Interface
{
    public interface IAdminBL
    {
        UserResponse Register(UserRequest userData);
        UserResponse LoginAdmin(Login userData);
    }
}
