using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Auth;

namespace MyPortal.Logic.Services
{
    public class TokenService : BaseService, ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService()
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Instance.TokenKey));
        }

        private async Task<string> GenerateAccessToken(UserModel userModel)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, userModel.Id.Value.ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, userModel.UserName),
                    new Claim(ApplicationClaimTypes.UserType, userModel.UserType.ToString()),
                    new Claim(ApplicationClaimTypes.DisplayName, userModel.GetDisplayName(NameFormat.FullNameAbbreviated))
                };

                var roles = await unitOfWork.UserRoles.GetByUser(userModel.Id.Value);

                claims.AddRange(roles.Select(r =>
                    new Claim(ClaimTypes.Role, r.RoleId.ToString("N"))));

                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddMinutes(15),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
        }

        private async Task<string> GenerateRefreshToken(Guid userId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                await unitOfWork.RefreshTokens.DeleteExpired(userId);

                var randomNumber = new byte[256];

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);

                    var token = Convert.ToBase64String(randomNumber);

                    unitOfWork.RefreshTokens.Create(new RefreshToken
                    {
                        UserId = userId,
                        Value = token,
                        ExpirationDate = DateTime.Now.AddDays(14)
                    });

                    await unitOfWork.SaveChangesAsync();

                    return token;
                }
            }
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            
            SecurityToken securityToken;
            
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid access token.");
            }

            return principal;
        }

        public async Task<TokenModel> GenerateToken(UserModel userModel)
        {
            var token = await GenerateAccessToken(userModel);
            var refreshToken = await GenerateRefreshToken(userModel.Id.Value);

            return new TokenModel {Token = token, RefreshToken = refreshToken};
        }

        public async Task<TokenModel> RefreshToken(UserModel userModel, TokenModel tokenModel)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var userRefreshTokens = await unitOfWork.RefreshTokens.GetByUser(userModel.Id.Value);

                var selectedRefreshToken = userRefreshTokens.FirstOrDefault(x => x.Value == tokenModel.RefreshToken);

                if (selectedRefreshToken == null)
                {
                    throw new SecurityTokenException("Invalid refresh token.");
                }

                if (selectedRefreshToken.ExpirationDate < DateTime.Now)
                {
                    throw new SecurityTokenExpiredException("Refresh token has expired.");
                }

                var newToken = await GenerateAccessToken(userModel);

                var newRefreshToken = await GenerateRefreshToken(userModel.Id.Value);

                var tokenResult = new TokenModel { Token = newToken, RefreshToken = newRefreshToken };

                await unitOfWork.RefreshTokens.Delete(selectedRefreshToken.Id);

                await unitOfWork.SaveChangesAsync();

                return tokenResult;
            }
        }

        public async Task<bool> RevokeToken(UserModel userModel, TokenModel tokenModel)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var userRefreshTokens = await unitOfWork.RefreshTokens.GetByUser(userModel.Id.Value);

                var selectedRefreshToken = userRefreshTokens.FirstOrDefault(x => x.Value == tokenModel.RefreshToken);

                if (selectedRefreshToken == null)
                {
                    return false;
                }

                await unitOfWork.RefreshTokens.Delete(selectedRefreshToken.Id);
                await unitOfWork.SaveChangesAsync();

                return true;
            }
        }
    }
}
