using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CurriculumSessionRepository : Repository<CurriculumSession>, ICurriculumSessionRepository
    {
        public CurriculumSessionRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}