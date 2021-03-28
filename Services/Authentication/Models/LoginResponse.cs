using System;

namespace Services.Authentication.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public DateTime ValidTo { get; set; }

        public string[] ErrorMessages { get; set; }
    }
}
