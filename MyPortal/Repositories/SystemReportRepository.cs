using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SystemReportRepository : ReadRepository<SystemReport>, ISystemReportRepository
    {
        public SystemReportRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}