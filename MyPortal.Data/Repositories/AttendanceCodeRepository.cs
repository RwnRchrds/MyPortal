using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class AttendanceCodeRepository : ReadRepository<AttendanceCode>, IAttendanceCodeRepository
    {
        public AttendanceCodeRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<AttendanceCode> Get(string code)
        {
            return await Context.AttendanceCodes.SingleOrDefaultAsync(x => x.Code == code);
        }

        public async Task<IEnumerable<AttendanceCode>> GetUsable()
        {
            return await Context.AttendanceCodes.Where(x => !x.DoNotUse).ToListAsync();
        }
    }
}