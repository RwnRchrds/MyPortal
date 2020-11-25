using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class RolePermissionService : BaseService, IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly ISystemAreaRepository _systemAreaRepository;

        public RolePermissionService(ApplicationDbContext context)
        {
            var connection = context.Database.GetDbConnection();

            _rolePermissionRepository = new RolePermissionRepository(context);
            _permissionRepository = new PermissionRepository(connection);
            _systemAreaRepository = new SystemAreaRepository(connection);
        }

        public async Task<TreeNode> GetPermissionsTree(Guid roleId)
        {
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

            return root;
        }

        public async Task SetPermissions(Guid roleId, IEnumerable<Guid> permIds)
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

            await _rolePermissionRepository.SaveChanges();
        }

        public override void Dispose()
        {
            _rolePermissionRepository.Dispose();
            _permissionRepository.Dispose();
            _systemAreaRepository.Dispose();
        }
    }
}