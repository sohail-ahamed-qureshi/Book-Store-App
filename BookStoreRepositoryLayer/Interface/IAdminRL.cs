using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepositoryLayer.Interface
{
    public interface IAdminRL
    {
        User Register(User userData);
        User LoginAdmin(string email);
    }
}
