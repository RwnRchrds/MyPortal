using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IFileRepository : IReadWriteRepository<File>
    {
        Task<File> GetByDocumentId(Guid documentId);
    }
}
