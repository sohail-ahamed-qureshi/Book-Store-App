using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepositoryLayer.Services
{
    public class EmailSender : IEmailSender
    {
        private EmailConfiguration emailConfig;
        public EmailSender(EmailConfiguration emailConfiguration)
        {
            emailConfig = emailConfiguration;
        }
        public void SendEmail(Mail mail)
        {
            var emailMessage = CreateMailMessage(mail);

            Send(emailMessage);
        }

        /// <summary>
        /// configuring sender's settings with smtp sever.
        /// </summary>
        /// <param name="emailMessage"></param>
        private void Send(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(emailConfig.SmtpServer, emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailConfig.Username, emailConfig.Password);

                    client.Send(emailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
        /// <summary>
        /// initializing the recipient's e-mail 
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        private MimeMessage CreateMailMessage(Mail mail)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailConfig.From));
            emailMessage.To.AddRange(mail.To);
            emailMessage.Subject = mail.Subject;
            // .Text - content in text format, .Html - content to be sent in html format 
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = string.Format($"<div style='text-align: center'>" +
            $"<h3 style='text-align:center'>Book Store</h3>" +
            $"<p>Click the below link to reset password</p>" +
            $"<a href='{mail.Content}' style ='color:red'>Reset Password</a>" +
            $"</div>")
            };

            return emailMessage;
        }

        public void SendSuccessEmail(SuccessMail mail)
        {
            var emailMessage = OrderSuccessfullMessage(mail);

            Send(emailMessage);
        }


        private MimeMessage OrderSuccessfullMessage(SuccessMail mail)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailConfig.From));
            emailMessage.To.AddRange(mail.To);
            emailMessage.Subject = mail.Subject;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = string.Format($"<div style='text-align: center'>" +
                $"<h3 style='text-align:center'>Book Store App</h3>" +
                $"<p>Dear {mail.Content.FullName},</p><br>" +
                $"<p>Thank tou for Shopping from Book Store.</p>" +
                $"<p> Your Order Number is: #{mail.Content.OrderId}. Order summary is given as below:</p><br>" +
                $"</div>"+

                $"<div style='text-align: left'>" +
                $"<h2>Order Summary</h2>"+
                $"<p>To,</p>" +
                $"<p>{mail.Content.FullName}</p>" +
                $"<p>{mail.Content.Address}</p>" +
                $"</div>" +

                $"<div style='text-align: center'>" +
                    $"<table style='text-align: center; font-family: arial, sans-serif;border-collapse: collapse;border:1px solid black ; width: 100%' >" +
                        $"<tr style='border:1px solid black ' >" +
                            $"<th style='padding: 8px; border:1px solid black  ' >Order Id</th>" +
                            $"<th style='padding: 8px;border:1px solid black ' >Order Date</th>" +
                            $"<th style='padding : 8px;border:1px solid black'>Book Name</th>" +
                            $"<th style='padding: 8px;border:1px solid black ' >Quantity</th>" +
                            $"<th style='padding : 8px;border:1px solid black' >Price</th>" +
                        $"</tr>" +
                        $"<tr  style='border:1px solid black ' >" +
                            $"<td style='padding: 8px;border:1px solid black ' >#{mail.Content.OrderId}</td>" +
                            $"<td style='padding: 8px;border:1px solid black ' >{mail.Content.OrderDate}</td>" +
                            $"<td style='padding: 8px;border:1px solid black ' >{mail.Content.BookName}</td>" +
                            $"<td style='padding: 8px;border:1px solid black ' >{mail.Content.Quantity}</td>" +
                            $"<td style='padding: 8px;border:1px solid black ' >{mail.Content.Price}</td>" +
                        $"</tr>" +
                         $"<tr  style='border:1px solid black ' >" +
                            $"<td style='padding: 8px;border:1px solid black ' ></td>" +
                            $"<td style='padding: 8px;border:1px solid black ' ></td>" +
                            $"<td style='padding: 8px;border:1px solid black ' ></td>" +
                            $"<td style='font-weight: bold; padding: 8px;border:1px solid black ' >Total Price in Rs</td>" +
                            $"<td style='font-weight : bold;padding: 8px;border:1px solid black' >{mail.Content.TotalPrice}</td>" +
                        $"</tr>" +
                    $"</table>" +
                $"</div>")
            };
            return emailMessage;
        }
    }
}
