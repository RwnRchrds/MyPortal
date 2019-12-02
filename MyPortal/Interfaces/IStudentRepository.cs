using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IStudentRepository : IReadWriteRepository<Student>
    {
        Task<Student> GetByIdWithRelated(int studentId, params Expression<Func<Student, object>>[] includeProperties);

        Task<Student> GetByUserId(string userId);

        Task<IEnumerable<Student>> GetOnRoll();

        Task<IEnumerable<Student>> GetLeavers();

        Task<IEnumerable<Student>> GetFuture();

        Task<IEnumerable<Student>> GetByRegGroup(int regGroupId);

        Task<IEnumerable<Student>> GetByYearGroup(int yearGroupId);
    }
}
