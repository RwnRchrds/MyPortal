using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SubjectStaffMemberRoleRepository : ReadRepository<SubjectStaffMemberRole>, ISubjectStaffMemberRoleRepository
    {
        public SubjectStaffMemberRoleRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}