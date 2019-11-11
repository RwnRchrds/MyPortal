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
    public class CurriculumEnrolmentRepository : Repository<CurriculumEnrolment>, ICurriculumEnrolmentRepository
    {
        public CurriculumEnrolmentRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CurriculumEnrolment>> GetByClass(int classId)
        {
            return await Context.CurriculumEnrolments.Where(x => x.ClassId == classId).ToListAsync();
        }

        public async Task<IEnumerable<CurriculumEnrolment>> GetByStudent(int studentId)
        {
            return await Context.CurriculumEnrolments.Where(x => x.StudentId == studentId).ToListAsync();
        }
    }
}