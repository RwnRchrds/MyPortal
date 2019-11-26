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
    public class CurriculumClassRepository : Repository<CurriculumClass>, ICurriculumClassRepository
    {
        public CurriculumClassRepository(MyPortalDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<CurriculumClass>> GetByAcademicYear(int academicYearId)
        {
            return await Context.CurriculumClasses.Where(x => x.AcademicYearId == academicYearId).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IEnumerable<CurriculumClass>> GetBySubject(int subjectId, int academicYearId)
        {
            return await Context.CurriculumClasses
                .Where(x => x.AcademicYearId == academicYearId && x.SubjectId == subjectId).OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}