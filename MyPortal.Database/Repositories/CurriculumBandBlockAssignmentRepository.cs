using System.Data;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBandBlockAssignmentRepository : BaseReadWriteRepository<CurriculumBandBlockAssignment>
    {
        public CurriculumBandBlockAssignmentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }
    }
}