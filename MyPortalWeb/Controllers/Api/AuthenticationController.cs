using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Authentication;
using MyPortal.Logic.Interfaces;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : BaseApiController
    {
        public AuthenticationController(IUserService userService) : base(userService)
        {
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] LoginModel login)
        {
            return await ProcessAsync(async () =>
            {
                var loginResult = await UserService.Login(login);

                if (loginResult.Succeeded)
                {
                    var token = await UserService.GenerateToken(loginResult.User);
                    var refreshToken = await UserService.GenerateRefreshToken(loginResult.User.Id);

                    return Ok(new TokenModel {Token = token, RefreshToken = refreshToken});
                }

                return Unauthorized(loginResult.ErrorMessage);
            });
        }

        [HttpPost]
        [Authorize]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            return await ProcessAsync(async () =>
            {
                var tokenResult = await UserService.RefreshToken(User, refreshToken);

                return Ok(tokenResult);
            });
        }
    }
}