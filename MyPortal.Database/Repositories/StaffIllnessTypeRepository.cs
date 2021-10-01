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
    public class StaffIllnessTypeRepository : BaseReadWriteRepository<StaffIllnessType>, IStaffIllnessTypeRepository
    {
        public StaffIllnessTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(StaffIllnessType entity)
        {
            var illnessType = await Context.StaffIllnessTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (illnessType == null)
            {
                throw new EntityNotFoundException("Illness type not found.");
            }

            illnessType.Description = entity.Description;
            illnessType.Active = entity.Active;
        }
    }
}