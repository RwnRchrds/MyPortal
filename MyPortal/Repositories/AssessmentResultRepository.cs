using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class AssessmentResultRepository : ReadWriteRepository<AssessmentResult>, IAssessmentResultRepository
    {
        public AssessmentResultRepository(MyPortalDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<AssessmentResult>> GetResultsByStudent(int studentId, int resultSetId)
        {
            return await Context.AssessmentResults.Where(x => x.StudentId == studentId && x.ResultSetId == resultSetId).ToListAsync();
        }
    }
}