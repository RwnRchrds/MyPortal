using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Identity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Exceptions;

namespace MyPortal.Logic.Services
{
    public class ApplicationRolePermissionService : BaseService, IApplicationRolePermissionService
    {
        private readonly IApplicationRolePermissionRepository _rolePermissionRepository;
        private readonly IApplicationPermissionRepository _permissionRepository;

        public ApplicationRolePermissionService(IApplicationRolePermissionRepository rolePermissionRepository, IApplicationPermissionRepository permissionRepository) : base("Role permission")
        {
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
        }
        
        public async Task<IEnumerable<string>> GetPermissionClaimValues(Guid roleId)
        {
            var claimValues = await _rolePermissionRepository.GetClaimValuesByRole(roleId);

            return claimValues;
        }

        public async Task SetPermissions(Guid roleId, IList<Guid> permIds)
        {
            // Add new permissions from list
            var existingPermissions = (await _rolePermissionRepository.GetByRole(roleId)).ToList();
            
            foreach (var permId in permIds)
            {
                if (existingPermissions.All(x => x.PermissionId != permId))
                {
                    _rolePermissionRepository.Create(new ApplicationRolePermission{RoleId = roleId, PermissionId = permId});
                }
            }

            // Remove permissions that no longer apply
            var permsToRemove = existingPermissions.Where(x => !permIds.Contains(x.PermissionId));

            foreach (var perm in permsToRemove)
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