using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin.Roles;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly ISystemAreaRepository _systemAreaRepository;
        private readonly IRolePermissionsCache _rolePermissionsCache;

        public RoleService(RoleManager<Role> roleManager, IRolePermissionRepository rolePermissionRepository,
            IPermissionRepository permissionRepository, ISystemAreaRepository systemAreaRepository, IRolePermissionsCache rolePermissionsCache)
        {
            _roleManager = roleManager;
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
            _systemAreaRepository = systemAreaRepository;
            _rolePermissionsCache = rolePermissionsCache;
        }

        public async Task<IEnumerable<PermissionModel>> GetPermissions(Guid roleId)
        {
            var permissions = await _rolePermissionRepository.GetByRole(roleId);

            return permissions.Select(p => BusinessMapper.Map<PermissionModel>(p.Permission));
        }

        public async Task<TreeNode> GetPermissionsTree(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            var systemAreas = (await _systemAreaRepository.GetAll()).ToList();

            var permissions = (await _permissionRepository.GetAll()).ToList();

            var existingPermissions = (await _rolePermissionRepository.GetByRole(roleId)).ToList();

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
                            Id = p.Id.ToString("N"),
                            Text = p.ShortDescription,
                            State = new TreeNodeState
                            {
                                Opened = false,
                                Disabled = false,
                                Selected = existingPermissions.Any(ep => ep.PermissionId == p.Id)
                            }
                        }).ToList()
                    }).ToList()
                });
            }

            root.SetEnabled(!role.System);

            return root;
        }

        private async Task SetPermissions(Guid roleId, params Guid[] permIds)
        {
            // Add new permissions from list
            var existingPermissions = (await _rolePermissionRepository.GetByRole(roleId)).ToList();

            var permissionsToAdd = permIds.Where(x => existingPermissions.All(p => p.PermissionId != x)).ToList();

            var permissionsToRemove = existingPermissions.Where(p => permIds.All(x => x != p.PermissionId)).ToList();

            foreach (var permId in permissionsToAdd)
            {
                _rolePermissionRepository.Create(new RolePermission
                    {RoleId = roleId, PermissionId = permId});
            }

            // Remove permissions that no longer apply

            foreach (var perm in permissionsToRemove)
            {
                await _rolePermissionRepository.Delete(perm.RoleId, perm.PermissionId);
            }

            _rolePermissionsCache.Purge(roleId);

            await _rolePermissionRepository.SaveChanges();
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

                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    var message = result.Errors.FirstOrDefault()?.Description;
                    throw new Exception(message);
                }

                if (request.PermissionIds != null && request.PermissionIds.Any())
                {
                    role = await _roleManager.FindByNameAsync(request.Name);

                    await SetPermissions(role.Id, request.PermissionIds);
                }

                newIds.Add(role.Id);
            }

            return newIds;
        }

        public async Task Update(params UpdateRoleModel[] requests)
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

                if (request.PermissionIds.Any())
                {
                    await SetPermissions(roleInDb.Id, request.PermissionIds);
                }
            }
        }

        public async Task Delete(params Guid[] roleIds)
        {
            foreach (var roleId in roleIds)
            {
                var roleInDb = await _roleManager.FindByIdAsync(roleId.ToString());

                if (roleInDb.System)
                {
                    throw new LogicException("Cannot delete a system role.");
                }

                await _roleManager.DeleteAsync(roleInDb);
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

            return roles.Select(BusinessMapper.Map<RoleModel>);
        }

        public async Task<RoleModel> GetRoleById(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }

            return BusinessMapper.Map<RoleModel>(role);
        }

        public override void Dispose()
        {
            _rolePermissionRepository.Dispose();
            _permissionRepository.Dispose();
            _systemAreaRepository.Dispose();
        }
    }
}