using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Models;
using MyPortal.Logic.Authentication;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin;
using MyPortal.Logic.Models.Requests.Auth;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Interfaces
{
    public interface IUserService : IService
    {
        Task CreateUser(params CreateUserRequest[] createUserRequests);
        Task DeleteUser(params Guid[] userIds);
        Task AddToRoles(Guid userId, params Guid[] roleIds);
        Task RemoveFromRoles(Guid userId, params Guid[] roleIds);
        Task SetPassword(Guid userId, string newPassword);
        Task<LoginResult> Login(LoginModel login);
        Task<bool> UserExists(string username);
        Task SetUserEnabled(Guid userId, bool enabled);
        Task<UserModel> GetUserById(Guid userId);
        Task<UserModel> GetUserByPrincipal(ClaimsPrincipal principal);
    }
}
