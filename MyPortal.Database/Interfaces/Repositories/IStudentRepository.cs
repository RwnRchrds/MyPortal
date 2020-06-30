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
        Task<IEnumerable<Student>> GetAll(StudentSearch searchParams);
        Task<IEnumerable<Student>> GetOnRoll(StudentSearch searchParams);
        Task<IEnumerable<Student>> GetLeavers(StudentSearch searchParams);
        Task<IEnumerable<Student>> GetFuture(StudentSearch searchParams);
        Task<IEnumerable<Student>> GetByClass(int classId);
        Task<IEnumerable<Student>> GetGiftedTalented();
    }
}
