using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CurriculumLessonPlanTemplateRepository : Repository<CurriculumLessonPlanTemplate>, ICurriculumLessonPlanTemplateRepository
    {
        public CurriculumLessonPlanTemplateRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}