using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}