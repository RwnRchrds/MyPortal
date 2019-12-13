using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IStaffMemberRepository : IReadWriteRepository<StaffMember>
    {
        Task<StaffMember> GetByUserId(string userId);

        Task<StaffMember> GetByIdWithRelated(int staffId,
            params Expression<Func<StaffMember, object>>[] includeProperties);
    }
}
