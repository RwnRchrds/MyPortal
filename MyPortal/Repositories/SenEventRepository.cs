using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SenEventRepository : ReadWriteRepository<SenEvent>, ISenEventRepository
    {
        public SenEventRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}