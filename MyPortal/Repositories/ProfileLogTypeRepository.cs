using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class ProfileLogTypeRepository : ReadRepository<ProfileLogType>, IProfileLogTypeRepository
    {
        public ProfileLogTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}