using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDocumentRepository : IReadWriteRepository<Document>
    {
        Task<IEnumerable<Document>> GetByDirectory(Guid directoryId);
    }
}
