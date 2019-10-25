using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class PastoralYearGroupRepository : Repository<PastoralYearGroup>, IPastoralYearGroupRepository
    {
        public PastoralYearGroupRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}