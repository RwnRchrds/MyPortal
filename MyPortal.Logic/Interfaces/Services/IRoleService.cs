using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin.Roles;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Guid>> CreateRole(RoleRequestModel role);
        Task UpdateRole(Guid roleId, RoleRequestModel role);
        Task DeleteRole(Guid roleId);
        Task<TreeNode> GetPermissionsTree(Guid roleId);
        Task<IEnumerable<RoleModel>> GetRoles(string roleName);
        Task<RoleModel> GetRoleById(Guid roleId);
    }
}