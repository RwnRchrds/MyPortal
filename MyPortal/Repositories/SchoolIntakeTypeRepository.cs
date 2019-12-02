using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SchoolIntakeTypeRepository : ReadRepository<SchoolIntakeType>, ISchoolIntakeTypeRepository
    {
        public SchoolIntakeTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}