using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IBulletinRepository : IReadWriteRepository<Bulletin>
    {
        Task<IEnumerable<Bulletin>> GetApproved();

        Task<IEnumerable<Bulletin>> GetStudent();

        Task<IEnumerable<Bulletin>> GetOwn(int authorId);
    }
}
