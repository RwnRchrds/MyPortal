using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SenProvisionTypeRepository : ReadOnlyRepository<SenProvisionType>, ISenProvisionTypeRepository
    {
        public SenProvisionTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}