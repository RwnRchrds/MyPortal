using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Interfaces
{
    public interface IApplicationRoleService : IService
    {
        Task CreateRole(RoleModel model);
        Task<IEnumerable<RoleModel>> Get(string searchParam = null);
    }
}