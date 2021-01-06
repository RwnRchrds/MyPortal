using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IRolePermissionRepository : IDisposable
    {
        Task<IEnumerable<RolePermission>> GetByRole(Guid roleId);
        Task<IEnumerable<RolePermission>> GetByUser(Guid userId);
        void Create(RolePermission rolePermission);
        Task Delete(Guid roleId, Guid permissionId);
        Task DeleteAllPermissions(Guid roleId);
        Task SaveChanges();
    }
}