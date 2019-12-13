using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ClassRepository : ReadWriteRepository<Class>, IClassRepository
    {
        public ClassRepository(MyPortalDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Class>> GetByAcademicYear(int academicYearId)
        {
            return await Context.Classes.Where(x => x.AcademicYearId == academicYearId).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetBySubject(int subjectId, int academicYearId)
        {
            return await Context.Classes
                .Where(x => x.AcademicYearId == academicYearId && x.SubjectId == subjectId).OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}