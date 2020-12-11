using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IRoleService : IService
    {
        Task<TreeNode> GetPermissionsTree(Guid roleId);
        Task SetPermissions(Guid roleId, params Guid[] permIds);
        Task<IEnumerable<RoleModel>> GetRoles();
    }
}