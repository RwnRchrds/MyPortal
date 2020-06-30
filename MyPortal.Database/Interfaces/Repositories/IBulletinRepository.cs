using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IBulletinRepository : IReadWriteRepository<Bulletin>
    {
        Task<IEnumerable<Bulletin>> GetApproved();

        Task<IEnumerable<Bulletin>> GetStudent();

        Task<IEnumerable<Bulletin>> GetOwn(Guid authorId);
    }
}
