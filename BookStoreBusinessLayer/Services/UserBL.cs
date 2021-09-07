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

        /// <summary>
        /// ability to encry password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string EncodePassword(string password)
        {
            byte[] encData = new byte[password.Length];
            encData = Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData);
            return encodedData;
        }

        /// <summary>
        /// ability to decrypt password into human readable format
        /// </summary>
        /// <param name="encPassword"></param>
        /// <returns>decrypted password</returns>
        private string DecodePassword(string encPassword)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder decoder = encoder.GetDecoder();
            byte[] todecodeByte = Convert.FromBase64String(encPassword);
            int charCount = decoder.GetCharCount(todecodeByte, 0, todecodeByte.Length);
            char[] decodeChar = new char[charCount];
            decoder.GetChars(todecodeByte, 0, todecodeByte.Length, decodeChar, 0);
            string password = new String(decodeChar);
            return password;
        }

        /// <summary>
        /// ability to check for data and encryption
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public User Register(User userData)
        {
            if(userData != null)
            {
                userData.Password =  EncodePassword(userData.Password);
                return userRL.Register(userData);
            }
            return null;
        }

        public UserResponse Login(Login userData)
        {
            if(userData != null)
            {
                User existingUser = userRL.Login(userData);
                if(existingUser != null)
                {
                    existingUser.Password = DecodePassword(existingUser.Password);
                    if (existingUser.Password.Equals(userData.Password))
                    {
                        UserResponse userResponse = new UserResponse
                        {
                            FullName = existingUser.FullName,
                            Email = existingUser.Email,
                            UserId = existingUser.UserId
                        };
                        return userResponse; 
                    }
                }
            }
            return null;
        }
    }
}
