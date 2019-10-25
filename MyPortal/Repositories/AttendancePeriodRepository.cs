using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class AttendancePeriodRepository : Repository<AttendancePeriod>, IAttendancePeriodRepository
    {
        public AttendancePeriodRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}