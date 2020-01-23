using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IStudentRepository : IReadWriteRepository<Student, int>
    {
        Task<Student> GetByUserId(string userId);
        Task<IEnumerable<Student>> GetOnRoll();
        Task<IEnumerable<Student>> GetLeavers();
        Task<IEnumerable<Student>> GetFuture();
        Task<IEnumerable<Student>> GetByRegGroup(int regGroupId);
        Task<IEnumerable<Student>> GetByYearGroup(int yearGroupId);
        Task<IEnumerable<Student>> GetByClass(int classId);
        Task<IEnumerable<Student>> GetGiftedTalented();
        Task<IEnumerable<Student>> GetByHouse(int houseId);
    }
}
