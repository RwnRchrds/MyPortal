using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBandBlockAssignmentRepository : BaseReadWriteRepository<CurriculumBandBlockAssignment>, ICurriculumBandBlockAssignmentRepository
    {
        public CurriculumBandBlockAssignmentRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task Update(CurriculumBandBlockAssignment entity)
        {
            var blockAssignment = await Context.CurriculumBandBlocks.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (blockAssignment == null)
            {
                throw new EntityNotFoundException("Block assignment not found.");
            }

            blockAssignment.BandId = entity.BandId;
        }
    }
}