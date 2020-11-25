using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDirectoryRepository : IReadWriteRepository<Directory>
    {
        Task<IEnumerable<Directory>> GetSubdirectories(Guid directoryId, bool includeStaffOnly);
    }
}
