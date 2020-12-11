using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Authentication;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin;
using MyPortal.Logic.Models.Requests.Admin.Users;
using MyPortal.Logic.Models.Requests.Auth;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public override void Dispose()
        {
            _userManager.Dispose();
            _roleManager.Dispose();
        }

        public async Task CreateUser(params CreateUserModel[] createUserRequests)
        {
            foreach (var request in createUserRequests)
            {
                if (await UserExists(request.Username))
                {
                    throw new LogicException("Username is already in use.");
                }

                var user = new User
                {
                    UserName = request.Username.ToLower(),
                    UserType = request.UserType,
                    PersonId = request.PersonId,
                    Enabled = true,
                    CreatedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.ToString());
                }
            }
        }

        public async Task DeleteUser(params Guid[] userIds)
        {
            foreach (var userId in userIds)
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                await _userManager.DeleteAsync(user);
            }
        }

        public async Task SetPassword(Guid userId, string newPassword)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            await _userManager.RemovePasswordAsync(user);

            await _userManager.AddPasswordAsync(user, newPassword);
        }

        public async Task<LoginResult> Login(LoginModel login)
        {
            var result = new LoginResult();

            var user = await _userManager.Users.Include(u => u.Person)
                .FirstOrDefaultAsync(x => x.UserName == login.Username.ToLower());

            if (user == null)
            {
                result.Fail("Username/password incorrect.");

                return result;
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!signInResult.Succeeded)
            {
                result.Fail("Username/password incorrect.");
            }
            else if (!user.Enabled)
            {
                result.Fail("Your account has been disabled. Please try again later.");
            }
            else
            {
                result.Success(BusinessMapper.Map<UserModel>(user));
            }

            return result;
        }

        public async Task AddToRoles(Guid userId, params Guid[] roleIds)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            foreach (var roleId in roleIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());

                await _userManager.AddToRoleAsync(user, role.Name);
            }
        }

        public async Task RemoveFromRoles(Guid userId, params Guid[] roleIds)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            foreach (var roleId in roleIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());

                await _userManager.RemoveFromRoleAsync(user, role.Name);
            }
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        public async Task SetUserEnabled(Guid userId, bool enabled)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            user.Enabled = enabled;

            await _userManager.UpdateAsync(user);
        }

        public async Task<UserModel> GetUserById(Guid userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            return BusinessMapper.Map<UserModel>(user);
        }

        public async Task<UserModel> GetUserByPrincipal(ClaimsPrincipal principal)
        {
            var nameId = principal.Claims.FirstOrDefault(c => c.Type.Contains(JwtRegisteredClaimNames.NameId));

            if (nameId == null)
            {
                throw new SecurityTokenException("User ID could not be retrieved from token.");
            }

            var tokenValid = Guid.TryParse(nameId.Value, out var userId);

            if (!tokenValid)
            {
                throw new SecurityTokenException("User ID could not be retrieved from token.");
            }

            return await GetUserById(userId);
        }
    }
}
