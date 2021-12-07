using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IParentEveningAppointmentRepository : IReadWriteRepository<ParentEveningAppointment>, IUpdateRepository<ParentEveningAppointment>
    {
        Task<IEnumerable<ParentEveningAppointment>> GetAppointmentsByStaffMember(Guid staffMemberId, DateTime fromDate,
            DateTime toDate);

        Task<IEnumerable<ParentEveningAppointment>> GetAppointmentsByStaffMember(Guid parentEveningId,
            Guid staffMemberId);

        Task<IEnumerable<ParentEveningAppointment>> GetAppointmentsByContact(Guid contactId, DateTime fromDate,
            DateTime toDate);

        Task<IEnumerable<ParentEveningAppointment>> GetAppointmentsByContact(Guid parentEveningId,
            Guid contactId);
    }
}