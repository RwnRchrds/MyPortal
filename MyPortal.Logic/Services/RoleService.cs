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
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Permissions;
using MyPortal.Logic.Models.Requests.Admin.Roles;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class RoleService : BaseService, IRoleService
    {
        private RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<TreeNode> GetPermissionsTree(Guid roleId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());

                var root = PermissionTree.Create(role.Permissions);

                root.SetEnabled(!role.System);

                return root;
            }
        }

        private async Task SetPermissions(Guid roleId, params int[] permValues)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
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
        }

        public async Task<IEnumerable<Guid>> Create(params CreateRoleRequestModel[] requests)
        {
            var newIds = new List<Guid>();

            foreach (var request in requests)
            {
                var role = new Role
                {
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
            }

            return newIds;
        }

        public async Task Update(params UpdateRoleRequestModel[] requests)
        {
            foreach (var request in requests)
            {
                var roleInDb = await _roleManager.FindByIdAsync(request.Id.ToString());

                if (roleInDb.System)
                {
                    throw new LogicException("Cannot modify system role.");
                }

                roleInDb.Name = request.Name;
                roleInDb.Description = request.Description;

                await _roleManager.UpdateAsync(roleInDb);

                if (request.PermissionValues != null)
                {
                    await SetPermissions(roleInDb.Id, request.PermissionValues);
                }
            }
        }

        public async Task Delete(params Guid[] roleIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var roleId in roleIds)
                {
                    await unitOfWork.UserRoles.DeleteAllByRole(roleId);

                    await unitOfWork.SaveChangesAsync();

                    var roleInDb = await _roleManager.FindByIdAsync(roleId.ToString());

                    if (roleInDb.System)
                    {
                        throw new LogicException("Cannot delete a system role.");
                    }

                    await _roleManager.DeleteAsync(roleInDb);
                }
            }
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

        public async Task<RoleModel> GetRoleById(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }

            return new RoleModel(role);
        }
    }
}