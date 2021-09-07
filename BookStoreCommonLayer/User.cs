using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommonLayer
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[A-Z]{1}[a-zA-Z//s]{2,}$", ErrorMessage = "Please enter a valid name ")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+([.][a-zA-Z0-9]+)?@[a-zA-Z0-9]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2})?$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()]){1}[a-zA-Z0-9]{5,}", ErrorMessage = "Please enter a valid password")]
        public string Password { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }

    /// <summary>
    /// model class for login
    /// </summary>
    public class Login
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+([.][a-zA-Z0-9]+)?@[a-zA-Z0-9]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2})?$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()]){1}[a-zA-Z0-9]{5,}", ErrorMessage = "Please enter a valid password")]
        public string Password { get; set; }
    }
    /// <summary>
    /// model class to reset password
    /// </summary>
    public class ResetPassword
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class UserResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
