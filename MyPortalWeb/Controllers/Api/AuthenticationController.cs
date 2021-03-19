using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Requests.Auth;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/auth")]
    public class AuthenticationController : BaseApiController
    {
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(TokenModel), 200)]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            return await ProcessAsync(async () =>
            {
                var loginResult = await Services.Users.Login(login);

                if (loginResult.Succeeded)
                {
                    var tokenModel = await Services.Tokens.GenerateToken(loginResult.User);

                    return Ok(tokenModel);
                }

                return Unauthorized(loginResult.ErrorMessage);
            });
        }

        [HttpGet]
        [Authorize]
        [Route("permissions")]
        [ProducesResponseType(typeof(IEnumerable<Guid>), 200)]
        public async Task<IActionResult> GetEffectivePermissions()
        {
            return await ProcessAsync(async () =>
            {
                var user = await GetLoggedInUser();

                var effectivePermissions = await Services.Users.GetEffectivePermissions(user.Id);

                return Ok(effectivePermissions);
            });
        }

        [HttpPost]
        [Route("tokens/refresh")]
        [ProducesResponseType(typeof(TokenModel), 200)]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            return await ProcessAsync(async () =>
            {
                var principal = Services.Tokens.GetPrincipalFromToken(tokenModel.Token);

                var user = await Services.Users.GetUserByPrincipal(principal);

                var newTokenModel = await Services.Tokens.RefreshToken(user, tokenModel);

                return Ok(newTokenModel);
            });
        }

        [HttpPost]
        [Route("tokens/revoke")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> RevokeToken([FromBody] TokenModel tokenModel)
        {
            return await ProcessAsync(async () =>
            {
                var principal = Services.Tokens.GetPrincipalFromToken(tokenModel.Token);

                var user = await Services.Users.GetUserByPrincipal(principal);

                var result = await Services.Tokens.RevokeToken(user, tokenModel);

                return Ok(result);
            });
        }

        public AuthenticationController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {
        }
    }
}