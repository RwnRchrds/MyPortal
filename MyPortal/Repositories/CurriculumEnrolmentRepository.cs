using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CurriculumEnrolmentRepository : Repository<CurriculumEnrolment>, ICurriculumEnrolmentRepository
    {
        public CurriculumEnrolmentRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}