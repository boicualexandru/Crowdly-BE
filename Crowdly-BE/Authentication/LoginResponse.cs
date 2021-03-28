using System;

namespace Crowdly_BE.Authentication
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
