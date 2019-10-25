using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CurriculumClassRepository : Repository<CurriculumClass>, ICurriculumClassRepository
    {
        public CurriculumClassRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}