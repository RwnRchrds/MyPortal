using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Logic.Authentication;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin.Users;
using MyPortal.Logic.Models.Requests.Auth;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IUserService : IService
    {
        Task CreateUser(params CreateUserModel[] createUserRequests);
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
