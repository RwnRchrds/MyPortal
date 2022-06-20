using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories;

public class ChargeBillingPeriodRepository : BaseReadWriteRepository<ChargeBillingPeriod>, IChargeBillingPeriodRepository
{
    public ChargeBillingPeriodRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
    {
    }

    public async Task Update(ChargeBillingPeriod entity)
    {
        var billingPeriod = await Context.ChargeBillingPeriods.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (billingPeriod == null)
        {
            throw new EntityNotFoundException("Charge billing period not found.");
        }

        billingPeriod.StartDate = entity.StartDate;
        billingPeriod.EndDate = entity.EndDate;
        billingPeriod.Name = entity.Name;
    }
}