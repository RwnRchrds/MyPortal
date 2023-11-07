using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDiaryEventAttendeeRepository : IReadWriteRepository<DiaryEventAttendee>,
        IUpdateRepository<DiaryEventAttendee>
    {
        Task<IEnumerable<DiaryEventAttendee>> GetByEvent(Guid eventId);
        Task<DiaryEventAttendee> GetAttendee(Guid eventId, Guid personId);
    }
}