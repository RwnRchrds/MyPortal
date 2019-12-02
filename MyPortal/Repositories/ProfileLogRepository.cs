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
    public class ProfileLogRepository : ReadWriteRepository<ProfileLog>, IProfileLogRepository
    {
        public ProfileLogRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ProfileLog>> GetByStudent(int studentId, int academicYearId)
        {
            return await Context.ProfileLogs.Where(x => !x.Deleted && x.StudentId == studentId && x.AcademicYearId == academicYearId).OrderByDescending(x => x.Date)
                .ToListAsync();
        }
    }
}