using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IParentEveningBreakRepository : IReadWriteRepository<ParentEveningBreak>,
        IUpdateRepository<ParentEveningBreak>
    {
        Task<IEnumerable<ParentEveningBreak>> GetBreaksByStaffMember(Guid parentEveningId,
            Guid staffMemberId);
    }
}