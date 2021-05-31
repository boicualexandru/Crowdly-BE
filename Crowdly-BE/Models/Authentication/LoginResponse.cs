using System;

namespace Crowdly_BE.Models.Authentication
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
