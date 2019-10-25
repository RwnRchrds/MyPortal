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
    public class AssessmentResultSetRepository : Repository<AssessmentResultSet>, IAssessmentResultSetRepository
    {
        public AssessmentResultSetRepository(MyPortalDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<AssessmentResultSet>> GetResultSetsByStudent(int studentId)
        {
            return await Context.AssessmentResultSets.Where(x => x.Results.Any(r => r.StudentId == studentId))
                .ToListAsync();
        }

        public async Task<AssessmentResultSet> GetCurrent()
        {
            return await Context.AssessmentResultSets.SingleOrDefaultAsync(x => x.IsCurrent);
        }
    }
}