using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentChargeDiscountRepository : IReadWriteRepository<StudentChargeDiscount>
    {
        Task<IEnumerable<StudentChargeDiscount>> GetByStudent(Guid studentId);
    }
}