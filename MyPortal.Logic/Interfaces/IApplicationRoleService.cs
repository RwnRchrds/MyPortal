using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Admin;
using MyPortal.Logic.Models.Details;

namespace MyPortal.Logic.Interfaces
{
    public interface IApplicationRoleService
    {
        Task CreateRole(RoleDetails details);
        Task<IEnumerable<RoleDetails>> Get(string searchParam = null);
    }
}