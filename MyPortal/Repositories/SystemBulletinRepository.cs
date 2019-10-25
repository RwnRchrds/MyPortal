using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SystemBulletinRepository : Repository<SystemBulletin>, ISystemBulletinRepository
    {
        public SystemBulletinRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}