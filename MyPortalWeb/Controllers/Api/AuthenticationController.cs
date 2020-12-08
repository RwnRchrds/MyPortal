using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Requests.Admin;
using MyPortal.Logic.Models.Requests.Auth;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/auth")]
    public class AuthenticationController : BaseApiController
    {
        private readonly ITokenService _tokenService;

        public AuthenticationController(IUserService userService, IAcademicYearService academicYearService,
            ITokenService tokenService) : base(userService, academicYearService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        [Produces(typeof(TokenModel))]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            return await ProcessAsync(async () =>
            {
                var loginResult = await UserService.Login(login);

                if (loginResult.Succeeded)
                {
                    var tokenModel = await _tokenService.GenerateToken(loginResult.User);

                    return Ok(tokenModel);
                }

                return Unauthorized(loginResult.ErrorMessage);
            });
        }

        [HttpPost]
        [Route("tokens/refresh")]
        [Produces(typeof(TokenModel))]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            return await ProcessAsync(async () =>
            {
                var principal = _tokenService.GetPrincipalFromToken(tokenModel.Token);

                var user = await UserService.GetUserByPrincipal(principal);

                var newTokenModel = await _tokenService.RefreshToken(user, tokenModel);

                return Ok(newTokenModel);
            });
        }

        [HttpPost]
        [Route("tokens/revoke")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> RevokeToken([FromBody] TokenModel tokenModel)
        {
            return await ProcessAsync(async () =>
            {
                var principal = _tokenService.GetPrincipalFromToken(tokenModel.Token);

                var user = await UserService.GetUserByPrincipal(principal);

                var result = await _tokenService.RevokeToken(user, tokenModel);

                return Ok(result);
            });
        }

        public override void Dispose()
        {
            _tokenService.Dispose();

            base.Dispose();
        }
    }
}