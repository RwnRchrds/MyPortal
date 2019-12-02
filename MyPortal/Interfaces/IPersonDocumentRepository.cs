using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IPersonDocumentRepository : IReadWriteRepository<PersonDocument>
    {
        Task<IEnumerable<PersonDocument>> GetByPerson(int personId);
    }
}
