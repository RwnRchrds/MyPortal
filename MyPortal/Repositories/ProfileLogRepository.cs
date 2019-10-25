using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class ProfileLogRepository : Repository<ProfileLog>, IProfileLogRepository
    {
        public ProfileLogRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}