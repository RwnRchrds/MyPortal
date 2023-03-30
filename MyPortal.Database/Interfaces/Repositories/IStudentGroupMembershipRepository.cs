using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentGroupMembershipRepository : IReadWriteRepository<StudentGroupMembership>, IUpdateRepository<StudentGroupMembership>
    {
        Task<IEnumerable<StudentGroupMembership>> GetMembershipsByGroup(Guid studentGroupId, DateTime dateFrom, DateTime dateTo);
    }
}