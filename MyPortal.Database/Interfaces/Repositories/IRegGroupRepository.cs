using System;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IRegGroupRepository : IReadWriteRepository<RegGroup>
    {
        Task<RegGroup> GetByStudent(Guid studentId);
        Task<StaffMember> GetTutor(Guid regGroupId);
    }
}
