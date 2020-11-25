using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDiaryEventAttendeeRepository : IReadWriteRepository<DiaryEventAttendee>
    {
        Task<IEnumerable<DiaryEventAttendee>> GetByEvent(Guid eventId);
    }
}
