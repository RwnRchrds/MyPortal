using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SenEventTypeRepository : ReadRepository<SenEventType>, ISenEventTypeRepository
    {
        public SenEventTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}