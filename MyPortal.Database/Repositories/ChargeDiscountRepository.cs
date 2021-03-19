using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class ChargeDiscountRepository : BaseReadWriteRepository<ChargeDiscount>, IChargeDiscountRepository
    {
        public ChargeDiscountRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "CD")
        {
        }

        public async Task<IEnumerable<ChargeDiscount>> GetByDiscount(Guid discountId)
        {
            throw new NotImplementedException();
        }
    }
}
