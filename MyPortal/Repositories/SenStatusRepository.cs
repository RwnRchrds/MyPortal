using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SenStatusRepository : ReadOnlyRepository<SenStatus>, ISenStatusRepository
    {
        public SenStatusRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}