using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Auth;

namespace MyPortal.Logic.Interfaces
{
    public interface ITokenService : IService
    {
        Task<TokenModel> GenerateToken(UserModel userModel);
        Task<TokenModel> RefreshToken(UserModel userModel, TokenModel tokenModel);
        Task<bool> RevokeToken(UserModel userModel, TokenModel tokenModel);
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
