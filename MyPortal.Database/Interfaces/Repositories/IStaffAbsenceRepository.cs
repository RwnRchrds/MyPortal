using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStaffAbsenceRepository : IReadWriteRepository<StaffAbsence>, IUpdateRepository<StaffAbsence>
    {
    }
}