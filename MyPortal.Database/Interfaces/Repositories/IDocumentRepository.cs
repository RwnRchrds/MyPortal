using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDocumentRepository : IReadWriteRepository<Document>, IUpdateRepository<Document>
    {
        Task<IEnumerable<Document>> GetByDirectory(Guid directoryId);
        Task UpdateWithAttachment(Document entity);
    }
}
