using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentDiscountRepository : IReadWriteRepository<StudentDiscount>
    {
        Task<IEnumerable<StudentDiscount>> GetByStudent(Guid studentId);
    }
}
