using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CurriculumSubjectRepository : Repository<CurriculumSubject>, ICurriculumSubjectRepository
    {
        public CurriculumSubjectRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}