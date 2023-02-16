using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDirectoryRepository : IReadWriteRepository<Directory>, IUpdateRepository<Directory>
    {
        Task<IEnumerable<Directory>> GetSubdirectories(Guid directoryId, bool includeRestricted);
        Task DeleteWithChildren(Guid directoryId);
    }
}
