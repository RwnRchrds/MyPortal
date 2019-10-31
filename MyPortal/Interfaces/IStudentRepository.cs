using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> GetByUserIdAsync(string userId);

        Task<IEnumerable<Student>> GetOnRoll();

        Task<IEnumerable<Student>> GetLeavers();

        Task<IEnumerable<Student>> GetFuture();

        Task<IEnumerable<Student>> GetStudentsByRegGroup(int regGroupId);

        Task<IEnumerable<Student>> GetStudentsByYearGroup(int yearGroupId);
    }
}
