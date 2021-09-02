using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentGroupMembershipRepository : IReadWriteRepository<StudentGroupMembership>, IUpdateRepository<StudentGroupMembership>
    {
        
    }
}