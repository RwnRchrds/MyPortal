using System;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStaffMemberRepository : IReadWriteRepository<StaffMember>
    {
        Task<StaffMember> GetByPersonId(Guid personId);

        Task<StaffMember> GetByUserId(Guid userId);
    }
}
