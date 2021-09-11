using BookStoreBusinessLayer.Interface;
using BookStoreCommonLayer;
using BookStoreRepositoryLayer.Interface;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BookStoreBusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private IUtility utility;
        private IUserRL userRL;
        //secret key for token
        private string secretKey;
        private readonly IEmailSender emailSender;
        MessageQueue messageQueue = null;
        public UserBL(IUtility utility,IUserRL userRL, IConfiguration configutration, IEmailSender emailSender)
        {
            this.utility = utility;
            this.userRL = userRL;
            secretKey = configutration.GetSection("Settings").GetSection("SecretKey").Value;
            this.emailSender = emailSender;
        }

       

        /// <summary>
        /// ability to check for data and encryption
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public User Register(User userData)
        {
            if (userData != null)
            {
                userData.Password = utility.EncodePassword(userData.Password);
                return userRL.Register(userData);
            }
            return null;
        }

        /// <summary>
        /// ability to generate jwt token with 10mins of expiry time.
        /// </summary>
        /// <param name="userEmail">userEmail</param>
        /// <param name="userId">user id</param>
        /// <returns>jwt token in string format</returns>
        public string GenerateToken(string userEmail, int userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescpritor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Email, userEmail),
                        new Claim("userId", userId.ToString(), ClaimValueTypes.Integer),
                        new Claim(ClaimTypes.Role , role),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescpritor);
            string jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

        public UserResponse Login(Login userData)
        {
            if (userData != null)
            {
                User existingUser = userRL.Login(userData);
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

        public bool ForgotPassword(string email)
        {
            if (email != null)
            {
                var existingUser = userRL.ForgotPassword(email);
                if (existingUser != null)
                {
                    SendMessageQueue(existingUser);
                    return true;
                }
            }
            return false;
        }

        public string ResetEmail(User user)
        {
            string token = GenerateToken(user.Email, user.UserId, user.Role);
            if (token == null)
                return null;
            return token;
        }

        /// <summary>
        /// passing the message to queue
        /// </summary>
        /// <param name="user"></param>
        public void SendMessageQueue(User user)
        {
            string token = ResetEmail(user);
            string path = @".\private$\ForgotPassword";
            try
            {
                if (MessageQueue.Exists(path))
                {
                    messageQueue = new MessageQueue(path);
                }
                else
                {
                    MessageQueue.Create(path);
                    messageQueue = new MessageQueue(path);
                }
                Message message1 = new Message();
                message1.Formatter = new BinaryMessageFormatter();
                messageQueue.ReceiveCompleted += Msmq_RecieveCompleted;
                messageQueue.Label = "url link";
                message1.Body = token;
                messageQueue.Send(message1);
                messageQueue.BeginReceive();
                messageQueue.Close();
                //return true;
            }
            catch
            {
                throw;
            }
        }

        private void Msmq_RecieveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = messageQueue.EndReceive(e.AsyncResult);
            message.Formatter = new BinaryMessageFormatter();
            string token = message.Body.ToString();
            string userEmail = ExtractData(token);
            var emailMessage = new Mail(new string[] { userEmail }, "Book Store - Reset Password", $"https://locolhost:44338/resetpassword/{token} ");
            emailSender.SendEmail(emailMessage);
        }

        private string ExtractData(string token)
        {
            var key = Encoding.ASCII.GetBytes(secretKey);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            SecurityToken securityToken;
            ClaimsPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                string userEmail = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var userId = Convert.ToInt32(principal.Claims.SingleOrDefault(c => c.Type == "userId").Value);
                return userEmail;
            }
            catch
            {
                principal = null;
            }
            return null;

        }

        public User ResetPassword(string email, ResetPassword resetPassword)
        {
            if (resetPassword.NewPassword.Equals(resetPassword.ConfirmPassword))
            {
                User existingUser = userRL.ForgotPassword(email);
                if(existingUser != null)
                {
                    resetPassword.NewPassword = utility.EncodePassword(resetPassword.NewPassword);
                    User user = userRL.ResetPassword(existingUser, resetPassword.NewPassword);
                    return user;
                }
            }
            return null;
        }
    }
}
