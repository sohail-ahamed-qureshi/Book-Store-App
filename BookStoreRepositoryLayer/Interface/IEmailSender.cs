using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepositoryLayer.Interface
{
    public interface IEmailSender
    {
        void SendEmail(Mail mail);
        void SendSuccessEmail(SuccessMail mail);
    }
}
