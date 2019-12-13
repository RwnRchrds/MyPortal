using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IPersonAttachmentRepository : IReadWriteRepository<PersonAttachment>
    {
        Task<IEnumerable<PersonAttachment>> GetByPerson(int personId);
    }
}
