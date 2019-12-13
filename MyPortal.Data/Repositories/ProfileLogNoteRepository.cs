using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ProfileLogNoteRepository : ReadWriteRepository<ProfileLogNote>, IProfileLogNoteRepository
    {
        public ProfileLogNoteRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ProfileLogNote>> GetByStudent(int studentId, int academicYearId)
        {
            return await Context.ProfileLogNotes.Where(x => !x.Deleted && x.StudentId == studentId && x.AcademicYearId == academicYearId).OrderByDescending(x => x.Date)
                .ToListAsync();
        }
    }
}