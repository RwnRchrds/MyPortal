using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class AssessmentAspectTypeRepository : ReadOnlyRepository<AssessmentAspectType>, IAssessmentAspectTypeRepository
    {
        public AssessmentAspectTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}