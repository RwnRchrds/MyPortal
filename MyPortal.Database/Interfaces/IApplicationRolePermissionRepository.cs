using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Interfaces
{
    public interface IApplicationRolePermissionRepository : IReadWriteRepository<ApplicationRolePermission>
    {
        Task<IEnumerable<ApplicationRolePermission>> GetByRole(Guid roleId);    
        Task<IEnumerable<string>> GetClaimValuesByRole(Guid roleId);
    }
}