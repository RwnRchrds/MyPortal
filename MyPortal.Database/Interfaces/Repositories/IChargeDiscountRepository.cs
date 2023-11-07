using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IChargeDiscountRepository : IReadWriteRepository<ChargeDiscount>
    {
        Task<IEnumerable<ChargeDiscount>> GetByDiscount(Guid discountId);
        Task<IEnumerable<ChargeDiscount>> GetByStudent(Guid studentId);
    }
}