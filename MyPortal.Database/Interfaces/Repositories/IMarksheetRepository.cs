using System;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Assessment;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IMarksheetRepository : IReadWriteRepository<Marksheet>
    {
        Task<MarksheetMetadata> GetMarksheetMetadata(Guid marksheetId);
    }
}