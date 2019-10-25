using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SchoolTypeRepository : ReadOnlyRepository<SchoolType>, ISchoolTypeRepository
    {
        public SchoolTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}