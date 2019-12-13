using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SubjectStaffMemberRepository : ReadWriteRepository<SubjectStaffMember>, ISubjectStaffMemberRepository
    {
        public SubjectStaffMemberRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<SubjectStaffMember>> GetBySubject(int subjectId)
        {
            return await Context.SubjectStaffMembers.Where(x => x.SubjectId == subjectId)
                .OrderBy(x => x.StaffMember.Person.LastName).ToListAsync();
        }
    }
}