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
    public class ProfileLogRepository : Repository<ProfileLog>, IProfileLogRepository
    {
        public ProfileLogRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<ProfileLog>> GetLogsByStudent(int studentId)
        {
            return await Context.ProfileLogs.Where(x => x.StudentId == studentId).OrderByDescending(x => x.Date)
                .ToListAsync();
        }
    }
}