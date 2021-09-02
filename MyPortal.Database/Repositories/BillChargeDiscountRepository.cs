using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class BillChargeDiscountRepository : BaseReadWriteRepository<BillChargeDiscount>, IBillChargeDiscountRepository
    {
        public BillChargeDiscountRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(BillChargeDiscount entity)
        {
            var bcd = await Context.BillChargeDiscounts.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (bcd == null)
            {
                throw new EntityNotFoundException("Bill Charge Discount not found.");
            }

            bcd.ChargeDiscountId = entity.ChargeDiscountId;
            bcd.GrossAmount = entity.GrossAmount;
        }
    }
};