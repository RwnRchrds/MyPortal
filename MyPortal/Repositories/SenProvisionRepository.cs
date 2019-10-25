using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SenProvisionRepository : Repository<SenProvision>, ISenProvisionRepository
    {
        public SenProvisionRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}