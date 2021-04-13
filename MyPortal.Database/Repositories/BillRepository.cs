using System.Data;
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
    public class BillRepository : BaseReadWriteRepository<Bill>, IBillRepository
    {
        public BillRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "Bill")
        {
            
        }

        public async Task Update(Bill entity)
        {
            var bill = await Context.Bills.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (bill == null)
            {
                throw new EntityNotFoundException("Bill not found.");
            }

            bill.DueDate = entity.DueDate;
            bill.Dispatched = entity.Dispatched;
        }
    }
}