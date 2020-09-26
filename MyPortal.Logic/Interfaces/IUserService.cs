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
        Task SetPassword(Guid userId, string newPassword);
        Task<LoginResult> Login(LoginModel login);
        Task<bool> UserExists(string username);
        Task<bool> EnableDisableUser(Guid userId);
        Task<UserModel> GetUserById(Guid userId);
        Task<UserModel> GetUserByPrincipal(ClaimsPrincipal principal);
        Task<UserModel> GetUserByToken(string token);
    }
}
