using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ILocalAuthorityRepository : IReadRepository<LocalAuthority>
    {
        Task<LocalAuthority> GetCurrent();
    }
}
