using System.Data;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBandBlockAssignmentRepository : BaseReadWriteRepository<CurriculumBandBlockAssignment>, ICurriculumBandBlockAssignmentRepository
    {
        public CurriculumBandBlockAssignmentRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}