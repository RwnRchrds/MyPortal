using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Auth;

namespace MyPortal.Logic.Services
{
    public class TokenService : BaseService, ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public TokenService(IConfiguration config, ApplicationDbContext context)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["MyPortal:TokenKey"]));
            _rolePermissionRepository = new RolePermissionRepository(context);
            _refreshTokenRepository = new RefreshTokenRepository(context);
        }

        public override void Dispose()
        {
            _rolePermissionRepository.Dispose();
            _refreshTokenRepository.Dispose();
        }

        private async Task<string> GenerateAccessToken(UserModel userModel)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, userModel.Id.ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, userModel.Username),
                new Claim(ApplicationClaimTypes.UserType, userModel.UserType.ToString()),
                new Claim(ApplicationClaimTypes.DisplayName, userModel.GetDisplayName(userModel.UserType == UserTypes.Staff))
            };

            var rolePermissions = await _rolePermissionRepository.GetByUser(userModel.Id);

            claims.AddRange(rolePermissions.Select(rp =>
                new Claim(ApplicationClaimTypes.Permission, rp.PermissionId.ToString("N"))));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private async Task<string> GenerateRefreshToken(Guid userId)
        {
            var randomNumber = new byte[256];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                var token = Convert.ToBase64String(randomNumber);

                _refreshTokenRepository.Create(new RefreshToken
                {
                    UserId = userId,
                    Value = token,
                    ExpirationDate = DateTime.Now.AddDays(14)
                });

                await _refreshTokenRepository.SaveChanges();

                return token;
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
            var refreshToken = await GenerateRefreshToken(userModel.Id);

            return new TokenModel {Token = token, RefreshToken = refreshToken};
        }

        public async Task<TokenModel> RefreshToken(UserModel userModel, TokenModel tokenModel)
        {
            var userRefreshTokens = await _refreshTokenRepository.GetByUser(userModel.Id);

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

            var newRefreshToken = await GenerateRefreshToken(userModel.Id);

            var tokenResult = new TokenModel {Token = newToken, RefreshToken = newRefreshToken};

            await _refreshTokenRepository.Delete(selectedRefreshToken.Id);

            await _refreshTokenRepository.SaveChanges();

            return tokenResult;
        }
    }
}
