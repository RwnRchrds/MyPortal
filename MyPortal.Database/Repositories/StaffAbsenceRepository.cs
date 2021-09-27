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
    public class StaffAbsenceRepository : BaseReadWriteRepository<StaffAbsence>, IStaffAbsenceRepository
    {
        public StaffAbsenceRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(StaffAbsence entity)
        {
            var absence = await Context.StaffAbsences.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (absence == null)
            {
                throw new EntityNotFoundException("Absence not found.");
            }

            absence.AbsenceTypeId = entity.AbsenceTypeId;
            absence.IllnessTypeId = entity.IllnessTypeId;
            absence.StartDate = entity.StartDate;
            absence.EndDate = entity.EndDate;
            absence.Confidential = entity.Confidential;
            absence.Notes = entity.Notes;
        }
    }
}