using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Search;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentRepository : IReadWriteRepository<Student>
    {
        Task<Student> GetByUserId(Guid userId);
        Task<Student> GetByPersonId(Guid personId);
        Task<IEnumerable<Student>> GetAll(StudentSearchOptions searchParams);
        Task<IEnumerable<Student>> GetGiftedTalented();
    }
}
