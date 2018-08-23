using System.ComponentModel.DataAnnotations;

namespace TestApp.API.DTOs
{
    public class RegisterUser
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required, StringLength(20, MinimumLength = 4, ErrorMessage = "You must provide a username")]
        public string Username { get; set; }

        [Required, StringLength(20, MinimumLength = 8, ErrorMessage = "You must provide a password between 8 -20 characters")]
         public string Password { get; set; }
    }
}