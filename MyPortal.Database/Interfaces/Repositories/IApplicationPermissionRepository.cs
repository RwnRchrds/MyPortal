using System.Threading.Tasks;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IApplicationPermissionRepository : IReadRepository<ApplicationPermission>
    {
        Task<ApplicationPermission> GetByClaimValue(int claimValue);
    }
}
