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
    public class StaffAbsenceTypeRepository : BaseReadWriteRepository<StaffAbsenceType>, IStaffAbsenceTypeRepository
    {
        public StaffAbsenceTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(StaffAbsenceType entity)
        {
            var absenceType = await Context.StaffAbsenceTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (absenceType == null)
            {
                throw new EntityNotFoundException("Absence type not found.");
            }

            absenceType.Description = entity.Description;
            absenceType.Active = entity.Active;
        }
    }
}