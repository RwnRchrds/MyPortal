using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StaffAbsenceTypeRepository : BaseReadWriteRepository<StaffAbsenceType>, IStaffAbsenceTypeRepository
    {
        public StaffAbsenceTypeRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(StaffAbsenceType entity)
        {
            var absenceType = await DbUser.Context.StaffAbsenceTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (absenceType == null)
            {
                throw new EntityNotFoundException("Absence type not found.");
            }

            absenceType.Description = entity.Description;
            absenceType.Active = entity.Active;
        }
    }
}