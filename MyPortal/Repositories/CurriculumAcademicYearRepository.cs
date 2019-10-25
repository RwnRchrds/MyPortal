using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CurriculumAcademicYearRepository : Repository<CurriculumAcademicYear>, ICurriculumAcademicYearRepository
    {
        public CurriculumAcademicYearRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}