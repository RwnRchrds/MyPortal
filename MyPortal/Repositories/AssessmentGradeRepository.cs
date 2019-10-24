using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class AssessmentGradeRepository : Repository<AssessmentGrade>, IAssessmentGradeRepository
    {
        public AssessmentGradeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}