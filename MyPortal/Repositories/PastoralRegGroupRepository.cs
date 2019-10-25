using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class PastoralRegGroupRepository : Repository<PastoralRegGroup>, IPastoralRegGroupRepository
    {
        public PastoralRegGroupRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}