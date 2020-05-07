using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Interfaces
{
    public interface IApplicationPermissionRepository : IReadRepository<ApplicationPermission>
    {
        Task<ApplicationPermission> GetByClaimValue(int claimValue);
    }
}
