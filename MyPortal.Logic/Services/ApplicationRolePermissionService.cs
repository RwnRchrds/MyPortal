using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Identity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Services
{
    public class ApplicationRolePermissionService : BaseService, IApplicationRolePermissionService
    {
        private readonly IApplicationRolePermissionRepository _repository;

        public ApplicationRolePermissionService(IApplicationRolePermissionRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<string>> GetPermissionClaimValues(Guid roleId)
        {
            var claimValues = await _repository.GetClaimValuesByRole(roleId);

            return claimValues;
        }

        public async Task SetPermissions(Guid roleId, IList<Guid> permIds)
        {
            // Add new permissions from list
            var existingPermissions = (await _repository.GetPermissionsByRole(roleId)).ToList();
            
            foreach (var permId in permIds)
            {
                if (existingPermissions.All(x => x.PermissionId != permId))
                {
                    _repository.Create(new ApplicationRolePermission{RoleId = roleId, PermissionId = permId});
                }
            }

            // Remove permissions that no longer apply
            var permsToRemove = existingPermissions.Where(x => !permIds.Contains(x.PermissionId));

            foreach (var perm in permsToRemove)
            {
                _repository.Delete(perm);
            }

            await _repository.SaveChanges();
        }
    }
}