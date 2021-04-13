using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
using MyPortal.Logic.Models.Requests.Admin.Roles;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly IIdentityServiceCollection _identityServices;

        public RoleService(IIdentityServiceCollection identityServices)
        {
            _identityServices = identityServices;
        }

        public async Task<TreeNode> GetPermissionsTree(Guid roleId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var role = await _identityServices.RoleManager.FindByIdAsync(roleId.ToString());

                var systemAreas = (await unitOfWork.SystemAreas.GetAll()).ToList();

                var permissions = (await unitOfWork.Permissions.GetAll()).ToList();

                var existingPermissions = new BitArray(role.Permissions);

                var root = TreeNode.CreateRoot("MyPortal");

                foreach (var systemArea in systemAreas.Where(a => a.ParentId == null))
                {
                    // Load System Areas
                    root.Children.Add(new TreeNode
                    {
                        Id = systemArea.Id.ToString("N"),
                        State = TreeNodeState.Default,
                        Text = systemArea.Description,

                        // Load Subareas
                        Children = systemAreas.Where(x => x.ParentId.HasValue && x.ParentId.Value == systemArea.Id).Select(sa => new TreeNode
                        {
                            Id = sa.Id.ToString("N"),
                            Text = sa.Description,
                            State = TreeNodeState.Default,

                            // Load Permissions
                            Children = permissions.Where(x => x.AreaId == sa.Id).Select(p => new TreeNode
                            {
                                Id = p.Value.ToString(),
                                Text = p.ShortDescription,
                                State = new TreeNodeState
                                {
                                    Opened = false,
                                    Disabled = false,
                                    Selected = existingPermissions[p.Value]
                                }
                            }).ToList()
                        }).ToList()
                    });
                }

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

        public async Task<IEnumerable<Guid>> Create(params CreateRoleModel[] requests)
        {
            var newIds = new List<Guid>();

            foreach (var request in requests)
            {
                var role = new Role
                {
                    Name = request.Name,
                    Description = request.Description
                };

                var result = await _identityServices.RoleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    var message = result.Errors.FirstOrDefault()?.Description;
                    throw new Exception(message);
                }

                if (request.Permissions != null && request.Permissions.Any())
                {
                    role = await _identityServices.RoleManager.FindByNameAsync(request.Name);

                    await SetPermissions(role.Id, request.Permissions);
                }

                newIds.Add(role.Id);
            }

            return newIds;
        }

        public async Task Update(params UpdateRoleModel[] requests)
        {
            foreach (var request in requests)
            {
                var roleInDb = await _identityServices.RoleManager.FindByIdAsync(request.Id.ToString());

                if (roleInDb.System)
                {
                    throw new LogicException("Cannot modify system role.");
                }

                roleInDb.Name = request.Name;
                roleInDb.Description = request.Description;

                await _identityServices.RoleManager.UpdateAsync(roleInDb);

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

                    var roleInDb = await _identityServices.RoleManager.FindByIdAsync(roleId.ToString());

                    if (roleInDb.System)
                    {
                        throw new LogicException("Cannot delete a system role.");
                    }

                    await _identityServices.RoleManager.DeleteAsync(roleInDb);
                }
            }
        }

        public async Task<IEnumerable<RoleModel>> GetRoles(string roleName)
        {
            var query = _identityServices.RoleManager.Roles;

            if (!string.IsNullOrWhiteSpace(roleName))
            {
                query = query.Where(r => r.Description.StartsWith(roleName));
            }

            var roles = await query.ToListAsync();

            return roles.OrderBy(r => r.Description).Select(BusinessMapper.Map<RoleModel>);
        }

        public async Task<RoleModel> GetRoleById(Guid roleId)
        {
            var role = await _identityServices.RoleManager.FindByIdAsync(roleId.ToString());

            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }

            return BusinessMapper.Map<RoleModel>(role);
        }
    }
}