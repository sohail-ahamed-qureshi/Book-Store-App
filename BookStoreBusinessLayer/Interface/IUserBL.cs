using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Interface
{
    public interface IUserBL
    {
        User Register(User userData);
    }
}
