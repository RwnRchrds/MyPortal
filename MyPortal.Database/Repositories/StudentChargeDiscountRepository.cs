using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class StudentChargeDiscountRepository : BaseReadWriteRepository<StudentChargeDiscount>,
        IStudentChargeDiscountRepository
    {
        public StudentChargeDiscountRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task<IEnumerable<StudentChargeDiscount>> GetByStudent(Guid studentId)
        {
            throw new NotImplementedException();
        }
    }
}