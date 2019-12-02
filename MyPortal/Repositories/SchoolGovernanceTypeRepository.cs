using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SchoolGovernanceTypeRepository : ReadRepository<SchoolGovernanceType>, ISchoolGovernanceTypeRepository
    {
        public SchoolGovernanceTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}