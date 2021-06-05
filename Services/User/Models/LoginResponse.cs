using System;

namespace Services.User.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public DateTime ValidTo { get; set; }

        public string[] ErrorMessages { get; set; }
    }
}
