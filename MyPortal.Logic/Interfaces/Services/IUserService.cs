using System;
using System.Collections.Generic;
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
        Task LinkPerson(Guid userId, Guid personId);
        Task UnlinkPerson(Guid userId);
        Task UpdateUser(params UpdateUserModel[] updateUserRequests);
        Task DeleteUser(params Guid[] userIds);
        Task AddToRoles(Guid userId, params Guid[] roleIds);
        Task RemoveFromRoles(Guid userId, params Guid[] roleIds);
        Task SetPassword(Guid userId, string newPassword);
        Task<LoginResult> Login(LoginModel login);
        Task<bool> UsernameExists(string username);
        Task SetUserEnabled(Guid userId, bool enabled);
        Task<UserModel> GetUserById(Guid userId);
        Task<UserModel> GetUserByPrincipal(ClaimsPrincipal principal);
        Task<IEnumerable<RoleModel>> GetUserRoles(Guid userId);
        Task<IEnumerable<UserModel>> GetUsers(string usernameSearch);
        Task<IEnumerable<PermissionModel>> GetPermissions(Guid userId);
        Task<IEnumerable<Guid>> GetEffectivePermissions(Guid userId);
    }
}
