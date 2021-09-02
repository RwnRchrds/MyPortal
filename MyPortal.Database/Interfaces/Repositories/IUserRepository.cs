using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IUserRepository : IReadWriteRepository<User>, IUpdateRepository<User>
    {
        Task<bool> UserExists(string username);
        Task<User> GetByUsername(string username);
    }
}