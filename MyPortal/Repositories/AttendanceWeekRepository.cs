using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class AttendanceWeekRepository : Repository<AttendanceWeek>, IAttendanceWeekRepository
    {
        public AttendanceWeekRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}