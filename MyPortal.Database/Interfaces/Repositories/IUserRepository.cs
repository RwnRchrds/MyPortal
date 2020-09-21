using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IUserRepository : IReadWriteRepository<User>
    {
        Task<bool> UserExists(string username);
        Task<User> GetByUsername(string username);
    }
}