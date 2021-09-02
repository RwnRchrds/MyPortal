using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AttendanceWeekPatternRepository : BaseReadWriteRepository<AttendanceWeekPattern>, IAttendanceWeekPatternRepository
    {
        public AttendanceWeekPatternRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(AttendanceWeekPattern entity)
        {
            var pattern = await Context.AttendanceWeekPatterns.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (pattern == null)
            {
                throw new EntityNotFoundException("Pattern");
            }

            pattern.Description = entity.Description;
        }
    }
}