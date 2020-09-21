using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IRolePermissionRepository : IReadWriteRepository<RolePermission>
    {
        Task<IEnumerable<RolePermission>> GetByRole(Guid roleId);
        Task<IEnumerable<RolePermission>> GetByUser(Guid userId);
    }
}