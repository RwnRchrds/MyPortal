using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StaffIllnessTypeRepository : BaseReadWriteRepository<StaffIllnessType>, IStaffIllnessTypeRepository
    {
        public StaffIllnessTypeRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(StaffIllnessType entity)
        {
            var illnessType = await DbUser.Context.StaffIllnessTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (illnessType == null)
            {
                throw new EntityNotFoundException("Illness type not found.");
            }

            illnessType.Description = entity.Description;
            illnessType.Active = entity.Active;
        }
    }
}