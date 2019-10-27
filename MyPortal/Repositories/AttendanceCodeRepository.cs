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
    public class AttendanceCodeRepository : ReadOnlyRepository<AttendanceCode>, IAttendanceCodeRepository
    {
        public AttendanceCodeRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<AttendanceCode> GetCode(string code)
        {
            return await Context.AttendanceCodes.SingleOrDefaultAsync(x => x.Code == code);
        }
    }
}