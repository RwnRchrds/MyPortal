using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CurriculumSubjectStaffMemberRepository : ReadWriteRepository<CurriculumSubjectStaffMember>, ICurriculumSubjectStaffMemberRepository
    {
        public CurriculumSubjectStaffMemberRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CurriculumSubjectStaffMember>> GetBySubject(int subjectId)
        {
            return await Context.CurriculumSubjectStaffMembers.Where(x => x.SubjectId == subjectId)
                .OrderBy(x => x.StaffMember.Person.LastName).ToListAsync();
        }
    }
}