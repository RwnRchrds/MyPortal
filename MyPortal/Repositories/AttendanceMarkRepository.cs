using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class AttendanceMarkRepository : Repository<AttendanceMark>, IAttendanceMarkRepository
    {
        public AttendanceMarkRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}