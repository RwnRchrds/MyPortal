using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Authentication;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Admin;
using MyPortal.Logic.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private IConfiguration _config;

        public UserService(ApplicationDbContext context, IConfiguration config)
        {
            _config = config;
            _userRepository = new UserRepository(context);
            _rolePermissionRepository = new RolePermissionRepository(context);
            _refreshTokenRepository = new RefreshTokenRepository(context);
        }

        public override void Dispose()
        {
            _userRepository.Dispose();
            _rolePermissionRepository.Dispose();
        }

        public async Task CreateUser(params CreateUserRequest[] createUserRequests)
        {
            foreach (var createUserRequest in createUserRequests)
            {
                var passwordHash = PasswordManager.GenerateHash(createUserRequest.Password);

                var user = new User
                {
                    CreatedDate = DateTime.Now,
                    Username = createUserRequest.Username,
                    UserType = createUserRequest.UserType,
                    PasswordHash = passwordHash,
                    Enabled = true,
                    PersonId = createUserRequest.PersonId
                };

                _userRepository.Create(user);
            }

            await _userRepository.SaveChanges();
        }

        public async Task SetPassword(Guid userId, string newPassword)
        {
            var passwordHash = PasswordManager.GenerateHash(newPassword);

            var user = await _userRepository.GetByIdWithTracking(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            user.PasswordHash = passwordHash;

            await _userRepository.SaveChanges();
        }

        public async Task<LoginResult> Login(LoginModel login)
        {
            var result = new LoginResult();

            var user = await _userRepository.GetByUsername(login.Username);

            if (user != null)
            {
                if (!user.Enabled)
                {
                    result.Fail("Your account has been disabled.");
                }

                if (PasswordManager.CheckPassword(user.PasswordHash, login.Password))
                {
                    result.Success(BusinessMapper.Map<UserModel>(user));

                    return result;
                }
            }

            result.Fail("Username/password incorrect.");

            return result;
        }

        public async Task<string> GenerateToken(UserModel userModel)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString()),
                new Claim(ClaimTypes.Name, userModel.Username),
                new Claim(ApplicationClaimTypes.DisplayName, userModel.GetDisplayName(userModel.UserType == UserTypes.Staff)),
                new Claim(ApplicationClaimTypes.UserType, userModel.UserType.ToString())
            };

            var permissions = await _rolePermissionRepository.GetByUser(userModel.Id);

            foreach (var rolePermission in permissions)
            {
                if (claims.All(x => x.Value != rolePermission.PermissionId.ToString("N")))
                {
                    claims.Add(new Claim(ApplicationClaimTypes.Permission, rolePermission.PermissionId.ToString("N")));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("MyPortal:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

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

        public async Task<string> GenerateRefreshToken(Guid userId)
        {
            var randomNumber = new byte[2048];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                var token = Convert.ToBase64String(randomNumber);

                _refreshTokenRepository.Create(new RefreshToken
                {
                    UserId = userId,
                    Token = token,
                    ExpirationDate = DateTime.Now.AddDays(14)
                });

                await _refreshTokenRepository.SaveChanges();

                return token;
            }
        }

        private async Task<IEnumerable<RefreshToken>> GetRefreshTokens(Guid userId)
        {
            var tokens = await _refreshTokenRepository.GetByUser(userId);

            return tokens.ToList();
        }

        public async Task<TokenModel> RefreshToken(ClaimsPrincipal principal, string refreshToken)
        {
            var tokenValid = Guid.TryParse(principal.FindFirst(ClaimTypes.NameIdentifier).Value, out var userId);

            if (!tokenValid)
            {
                throw new SecurityTokenException("Invalid access token.");
            }

            var refreshTokens = await GetRefreshTokens(userId);

            var selectedRefreshToken = refreshTokens.FirstOrDefault(x => x.Token == refreshToken);

            if (selectedRefreshToken == null)
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }

            if (selectedRefreshToken.ExpirationDate < DateTime.Now)
            {
                throw new SecurityTokenExpiredException("Refresh token has expired.");
            }

            var user = await _userRepository.GetById(userId);

            var userModel = BusinessMapper.Map<UserModel>(user);

            var newToken = await GenerateToken(userModel);

            var newRefreshToken = await GenerateRefreshToken(userId);

            var tokenResult = new TokenModel {Token = newToken, RefreshToken = newRefreshToken};

            await _refreshTokenRepository.Delete(selectedRefreshToken.Id);

            await _refreshTokenRepository.SaveChanges();

            return tokenResult;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userRepository.UserExists(username);
        }

        public async Task<bool> EnableDisableUser(Guid userId)
        {
            var user = await _userRepository.GetByIdWithTracking(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            user.Enabled = !user.Enabled;

            await _userRepository.SaveChanges();

            return user.Enabled;
        }

        public async Task<UserModel> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetById(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            return BusinessMapper.Map<UserModel>(user);
        }

        public async Task<UserModel> GetUserByPrincipal(ClaimsPrincipal principal)
        {
            var claimValue = principal.FindFirst(ClaimTypes.NameIdentifier).Value;

            var findId = Guid.TryParse(claimValue, out var userId);

            if (findId)
            {
                var user = await GetUserById(userId);

                return BusinessMapper.Map<UserModel>(user);
            }

            throw new NotFoundException("User ID claim not found.");
        }
    }
}
