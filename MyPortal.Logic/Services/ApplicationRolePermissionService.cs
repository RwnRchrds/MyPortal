using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Identity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Exceptions;

namespace MyPortal.Logic.Services
{
    public class ApplicationRolePermissionService : BaseService, IApplicationRolePermissionService
    {
        private readonly IApplicationRolePermissionRepository _rolePermissionRepository;
        private readonly IApplicationPermissionRepository _permissionRepository;
        private readonly ISystemAreaRepository _systemAreaRepository;

        public ApplicationRolePermissionService(IApplicationRolePermissionRepository rolePermissionRepository, IApplicationPermissionRepository permissionRepository, ISystemAreaRepository systemAreaRepository) : base("Role permission")
        {
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
            _systemAreaRepository = systemAreaRepository;
        }
        
        public async Task<IEnumerable<string>> GetPermissionClaimValues(Guid roleId)
        {
            var claimValues = await _rolePermissionRepository.GetClaimValuesByRole(roleId);

            return claimValues;
        }

        public async Task<IEnumerable<TreeNode>> GetPermissionsTree(Guid roleId)
        {
            var nodes = new List<TreeNode>();

            var systemAreas = (await _systemAreaRepository.GetAll()).ToList();

            var permissions = (await _permissionRepository.GetAll()).ToList();

            var exisingPermissions = (await _rolePermissionRepository.GetByRole(roleId)).ToList();

            nodes.Add(TreeNode.CreateRoot("MyPortal"));

            foreach (var systemArea in systemAreas)
            {
                nodes.Add(new TreeNode
                {
                    Id = systemArea.Id.ToString(),
                    Parent = systemArea.ParentId == null ? "#" : systemArea.ParentId.ToString(),
                    State = TreeNodeState.Default(),
                    Container = true,
                    Text = systemArea.Description
                });
            }

            foreach (var permission in permissions)
            {
                nodes.Add(new TreeNode
                {
                    Id = permission.Id.ToString(),
                    Parent = permission.AreaId.ToString(),
                    State = new TreeNodeState
                    {
                        Disabled = false,
                        Opened = false,
                        Selected = exisingPermissions.Any(x => x.Id == permission.Id)
                    },
                    Container = false,
                    Text = permission.ShortDescription,
                    Icon = "fas fa-check-circle text-success"
                });
            }

            return nodes;
        }

        public async Task SetPermissions(Guid roleId, IEnumerable<Guid> permIds)
        {
            // Add new permissions from list
            var existingPermissions = (await _rolePermissionRepository.GetByRole(roleId)).ToList();

            var permissionsToAdd = permIds.Where(x => existingPermissions.All(p => p.Id != x)).ToList();

            var permissionsToRemove = existingPermissions.Where(p => permIds.All(x => x != p.Id)).ToList();

            foreach (var permId in permissionsToAdd)
            {
                _rolePermissionRepository.Create(new ApplicationRolePermission
                    {RoleId = roleId, PermissionId = permId});
            }

            // Remove permissions that no longer apply

            foreach (var perm in permissionsToRemove)
            {
                await _rolePermissionRepository.Delete(perm.Id);
            }

            await _rolePermissionRepository.SaveChanges();
        }

        public override void Dispose()
        {
            _rolePermissionRepository.Dispose();
            _permissionRepository.Dispose();
        }
    }
}