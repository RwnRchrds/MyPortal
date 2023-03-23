using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Settings;

using MyPortal.Logic.Models.Permissions;
using MyPortal.Logic.Models.Requests.Settings.Roles;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class RoleService : BaseUserService, IRoleService
    {
        private RoleManager<Role> _roleManager;

        public RoleService(ISessionUser user, RoleManager<Role> roleManager) : base(user)
        {
            _roleManager = roleManager;
        }

        public async Task<TreeNode> GetPermissionsTree(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            var root = PermissionTree.Create(role.Permissions);

            root.SetEnabled(!role.System);

            return root;
        }

        private async Task SetPermissions(Guid roleId, params int[] permValues)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var role = await unitOfWork.Roles.GetById(roleId);

            var permArray = PermissionHelper.CreatePermissionArray();

            foreach (var permValue in permValues)
            {
                permArray.Set(permValue, true);
            }

            role.Permissions = permArray.ToBytes();

            await unitOfWork.Roles.Update(role);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Guid>> CreateRole(RoleRequestModel request)
        {
            Validate(request);
            
            var newIds = new List<Guid>();

            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                var message = result.Errors.FirstOrDefault()?.Description;
                throw new Exception(message);
            }

            if (request.Permissions != null && request.Permissions.Any())
            {
                role = await _roleManager.FindByNameAsync(request.Name);

                await SetPermissions(role.Id, request.Permissions);
            }

            newIds.Add(role.Id);

            return newIds;
        }

        public async Task UpdateRole(Guid roleId, RoleRequestModel request)
        {
            Validate(request);
            
            var roleInDb = await _roleManager.FindByIdAsync(roleId.ToString());

            if (roleInDb.System)
            {
                throw new LogicException("Cannot modify system role.");
            }

            roleInDb.Name = request.Name;
            roleInDb.Description = request.Description;

            await _roleManager.UpdateAsync(roleInDb);

            if (request.Permissions != null)
            {
                await SetPermissions(roleInDb.Id, request.Permissions);
            }

            await CacheHelper.RoleCache.Purge(roleId);
        }

        public async Task DeleteRole(Guid roleId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            await unitOfWork.UserRoles.DeleteAllByRole(roleId);

            await unitOfWork.SaveChangesAsync();

            var roleInDb = await _roleManager.FindByIdAsync(roleId.ToString());

            if (roleInDb.System)
            {
                throw new LogicException("Cannot delete a system role.");
            }

            await _roleManager.DeleteAsync(roleInDb);

            await CacheHelper.RoleCache.Purge(roleId);
        }

        public async Task<IEnumerable<RoleModel>> GetRoles(string roleName)
        {
            var query = _roleManager.Roles;

            if (!string.IsNullOrWhiteSpace(roleName))
            {
                query = query.Where(r => r.Description.StartsWith(roleName));
            }

            var roles = await query.ToListAsync();

            return roles.OrderBy(r => r.Description).Select(r => new RoleModel(r));
        }

        public async Task<RoleModel> GetRoleById(Guid roleId, bool useCache)
        {
            Role role;
            
            if (useCache)
            {
                // Eliminates latency between the web app and database during permission checks
                role = await CacheHelper.RoleCache.GetOrCreate(roleId,
                    async () => await _roleManager.FindByIdAsync(roleId.ToString()), TimeSpan.FromHours(1));
            }
            else
            {
                role = await _roleManager.FindByIdAsync(roleId.ToString());
            }

            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }

            return new RoleModel(role);
        }
    }
}