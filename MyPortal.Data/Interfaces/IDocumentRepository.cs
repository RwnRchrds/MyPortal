using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IDocumentRepository : IReadWriteRepository<Document>
    {
        Task<IEnumerable<Document>> GetGeneral();

        Task<IEnumerable<Document>> GetApproved();
    }
}
