using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class AssessmentAspectRepository : ReadWriteRepository<AssessmentAspect>, IAssessmentAspectRepository
    {
        public AssessmentAspectRepository(MyPortalDbContext context) : base (context)
        {

        }
    }
}