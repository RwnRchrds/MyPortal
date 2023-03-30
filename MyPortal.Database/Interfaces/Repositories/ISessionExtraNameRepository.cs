using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories;

public interface ISessionExtraNameRepository : IReadWriteRepository<SessionExtraName>
{
    Task<IEnumerable<SessionExtraName>> GetExtraNamesBySession(Guid sessionId, Guid attendanceWeekId);
}