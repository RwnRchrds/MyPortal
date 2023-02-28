using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Enums;
using MyPortal.Logic.Authentication;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Data.Settings;

using MyPortal.Logic.Models.Requests.Auth;
using MyPortal.Logic.Models.Requests.Settings.Users;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IUserService : IService
    {
        Task<IEnumerable<Guid>> CreateUser(UserRequestModel user);
        Task LinkPerson(Guid userId, Guid personId);
        Task UnlinkPerson(Guid userId);
        Task UpdateUser(Guid userId, UserRequestModel user);
        Task DeleteUser(Guid userId);
        Task AddToRoles(Guid userId, params Guid[] roleIds);
        Task RemoveFromRoles(Guid userId, params Guid[] roleIds);
        Task SetPassword(Guid userId, string newPassword);
        Task ChangePassword(Guid userId, string oldPassword, string newPassword);
        Task<LoginResult> Login(LoginRequestModel login);
        Task<bool> UsernameExists(string username);
        Task SetUserEnabled(Guid userId, bool enabled);
        Task<UserModel> GetUserById(Guid userId);
        Task<UserModel> GetUserByPrincipal(ClaimsPrincipal principal);
        Task<IEnumerable<RoleModel>> GetUserRoles(Guid userId);
        Task<IEnumerable<UserModel>> GetUsers(string usernameSearch);
        Task<IEnumerable<int>> GetPermissionValuesByUser(Guid userId);

        Task<bool> UserHasPermission(Guid userId, PermissionRequirement requirement,
            params PermissionValue[] permissionValues);
        Task<UserInfoModel> GetUserInfo(Guid userId);
    }
}
