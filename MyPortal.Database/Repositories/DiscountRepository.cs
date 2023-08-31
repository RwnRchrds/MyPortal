using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DiscountRepository : BaseReadWriteRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(Discount entity)
        {
            var discount = await DbUser.Context.Discounts.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (discount == null)
            {
                throw new EntityNotFoundException("Discount not found.");
            }

            discount.Name = entity.Name;
            discount.Amount = entity.Amount;
            discount.Percentage = entity.Percentage;
            discount.BlockOtherDiscounts = entity.BlockOtherDiscounts;
        }
    }
}