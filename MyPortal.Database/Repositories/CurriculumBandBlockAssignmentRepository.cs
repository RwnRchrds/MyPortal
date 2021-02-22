using System.Data;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBandBlockAssignmentRepository : BaseReadWriteRepository<CurriculumBandBlockAssignment>, ICurriculumBandBlockAssignmentRepository
    {
        public CurriculumBandBlockAssignmentRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}