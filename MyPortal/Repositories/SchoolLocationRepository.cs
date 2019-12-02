using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SchoolLocationRepository : ReadRepository<SchoolLocation>, ISchoolLocationRepository
    {
        public SchoolLocationRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}