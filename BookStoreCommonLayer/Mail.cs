using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreCommonLayer
{
    public class Mail
    {
        //email address of recipient
        public List<MailboxAddress> To { get; set; }
        //subject of email
        public string Subject { get; set; }
        //content of email
        public string Content { get; set; }
        //public string Token { get; set; }

        public Mail(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;

        }
    }

    public class SuccessMail
    {
        //email address of recipient
        public List<MailboxAddress> To { get; set; }
        //subject of email
        public string Subject { get; set; }
        //content of email
        public OrderResponse Content { get; set; }
        //public string Token { get; set; }

        public SuccessMail(IEnumerable<string> to, string subject, OrderResponse content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;

        }
    }
}
