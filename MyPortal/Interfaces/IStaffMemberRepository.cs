using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IStaffMemberRepository : IReadWriteRepository<StaffMember>
    {
        Task<StaffMember> GetByUserId(string userId);

        Task<StaffMember> GetByIdWithRelated(int staffId,
            params Expression<Func<StaffMember, object>>[] includeProperties);
    }
}
