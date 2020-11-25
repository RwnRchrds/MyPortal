using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDocumentRepository : IReadWriteRepository<Document>
    {
        Task<IEnumerable<Document>> GetByDirectory(Guid directoryId);
    }
}
