using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IGiftedTalentedRepository : IReadWriteRepository<GiftedTalented>, IUpdateRepository<GiftedTalented>
    {
        Task<IEnumerable<GiftedTalented>> GetByStudent(Guid studentId);
    }
}