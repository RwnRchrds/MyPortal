using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using MyPortal.Database.Models;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Authentication
{
    public class LoginResult
    {
        public bool Succeeded { get; private set; }
        public ClaimsPrincipal ClaimsPrincipal { get; private set; }
        public string ErrorMessage { get; private set; }

        public LoginResult()
        {
            Succeeded = false;
        }

        public void Fail(string errorMessage)
        {
            Succeeded = false;
            ErrorMessage = errorMessage;
        }

        public void Success(ClaimsPrincipal principal)
        {
            Succeeded = true;
            ClaimsPrincipal = principal;
        }
    }
}
