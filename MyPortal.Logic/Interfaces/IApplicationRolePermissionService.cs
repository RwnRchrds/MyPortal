using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPortal.Logic.Interfaces
{
    public interface IApplicationRolePermissionService
    {
        Task<IEnumerable<string>> GetPermissionClaimValues(Guid roleId);
    }
}