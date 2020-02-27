using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IStudentRepository : IReadWriteRepository<Student>
    {
        Task<Student> GetByUserId(string userId);
        Task<IEnumerable<Student>> GetAll(Student student);
        Task<IEnumerable<Student>> GetOnRoll(Student student);
        Task<IEnumerable<Student>> GetLeavers(Student student);
        Task<IEnumerable<Student>> GetFuture(Student student);
        Task<IEnumerable<Student>> GetByClass(int classId);
        Task<IEnumerable<Student>> GetGiftedTalented();
    }
}
