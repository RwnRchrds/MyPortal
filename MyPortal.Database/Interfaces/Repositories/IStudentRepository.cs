using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentRepository : IReadWriteRepository<Student>, IUpdateRepository<Student>
    {
        Task<Student> GetByUserId(Guid userId);
        Task<Student> GetByPersonId(Guid personId);
        Task<IEnumerable<Student>> GetAll(StudentSearchOptions searchParams);
        Task<IEnumerable<Student>> GetGiftedTalented();
        Task<IEnumerable<Student>> GetByContact(Guid contactId, bool reportableOnly);
        Task<IEnumerable<string>> GetUpns(int leaCode, int establishmentNo, int allocationYear);
    }
}
