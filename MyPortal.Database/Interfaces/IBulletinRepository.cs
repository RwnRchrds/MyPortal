using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IBulletinRepository : IReadWriteRepository<Bulletin>
    {
        Task<IEnumerable<Bulletin>> GetApproved();

        Task<IEnumerable<Bulletin>> GetStudent();

        Task<IEnumerable<Bulletin>> GetOwn(Guid authorId);
    }
}
