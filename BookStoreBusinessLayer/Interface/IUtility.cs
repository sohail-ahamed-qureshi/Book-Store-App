using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusinessLayer.Interface
{
    public interface IUtility
    {
        string EncodePassword(string password);
        string DecodePassword(string encPassword);
    }
}
