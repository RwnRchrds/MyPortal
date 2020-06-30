using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ILocalAuthorityRepository : IReadRepository<LocalAuthority>
    {
        Task<LocalAuthority> GetCurrent();
    }
}
