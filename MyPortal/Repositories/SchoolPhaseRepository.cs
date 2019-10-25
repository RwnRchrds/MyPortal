using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SchoolPhaseRepository : ReadOnlyRepository<SchoolPhase>, ISchoolPhaseRepository
    {
        public SchoolPhaseRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}