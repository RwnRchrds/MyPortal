using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces
{
    public interface IApplicationRoleService : IService
    {
        Task CreateRole(RoleModel model);
        Task<IEnumerable<RoleModel>> Get(string searchParam = null, bool includeSystemRoles = false);
    }
}