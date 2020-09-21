using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Authentication
{
    public class TokenModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
