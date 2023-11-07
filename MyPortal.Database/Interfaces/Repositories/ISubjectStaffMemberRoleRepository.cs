using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ISubjectStaffMemberRoleRepository : IReadWriteRepository<SubjectStaffMemberRole>,
        IUpdateRepository<SubjectStaffMemberRole>
    {
    }
}