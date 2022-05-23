using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Enums;
using MyPortal.Logic.Authentication;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin.Users;
using MyPortal.Logic.Models.Requests.Auth;
using MyPortal.Logic.Models.Response.Users;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Guid>> CreateUser(params CreateUserRequestModel[] createUserRequests);
        Task LinkPerson(Guid userId, Guid personId);
        Task UnlinkPerson(Guid userId);
        Task UpdateUser(params UpdateUserRequestModel[] updateUserRequests);
        Task DeleteUser(params Guid[] userIds);
        Task AddToRoles(Guid userId, params Guid[] roleIds);
        Task RemoveFromRoles(Guid userId, params Guid[] roleIds);
        Task SetPassword(Guid userId, string newPassword);
        Task<LoginResult> Login(LoginRequestModel login);
        Task<bool> UsernameExists(string username);
        Task SetUserEnabled(Guid userId, bool enabled);
        Task<UserModel> GetUserById(Guid userId);
        Task<UserModel> GetUserByPrincipal(ClaimsPrincipal principal);
        Task<IEnumerable<RoleModel>> GetUserRoles(Guid userId);
        Task<IEnumerable<UserModel>> GetUsers(string usernameSearch);
        Task<IEnumerable<PermissionModel>> GetPermissionsByUser(Guid userId);
        Task<IEnumerable<int>> GetPermissionValuesByUser(Guid userId);

        Task<bool> UserHasPermission(Guid userId, PermissionRequirement requirement,
            params PermissionValue[] permissionValues);
        Task<UserInfoResponseModel> GetUserInfo(Guid userId);
    }
}
