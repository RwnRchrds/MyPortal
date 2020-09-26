using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Requests.Admin;
using MyPortal.Logic.Models.Requests.Auth;

namespace MyPortalWeb.Controllers.Api
{
    [Route("api/auth")]
    public class AuthenticationController : BaseApiController
    {
        private readonly ITokenService _tokenService;

        public AuthenticationController(IUserService userService, ITokenService tokenService) : base(userService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Login")]
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
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            return await ProcessAsync(async () =>
            {
                var user = await UserService.GetUserByToken(tokenModel.Token);

                var newTokens = await _tokenService.RefreshToken(user, tokenModel);

                return Ok(newTokens);
            });
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest userRequest)
        {
            return await ProcessAsync(async () =>
            {
                await UserService.CreateUser(userRequest);

                return Ok("User Created");
            });
        }
    }
}