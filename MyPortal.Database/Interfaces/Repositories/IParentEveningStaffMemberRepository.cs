using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IParentEveningStaffMemberRepository : IReadWriteRepository<ParentEveningStaffMember>, IUpdateRepository<ParentEveningStaffMember>
    {
        Task<IEnumerable<ParentEveningStaffMember>> GetLinkedParentEveningsByStaffMember(
            Guid staffMemberId);

        Task<ParentEveningStaffMember> GetInstanceByStaffMember(Guid parentEveningId, Guid staffMemberId);
    }
}