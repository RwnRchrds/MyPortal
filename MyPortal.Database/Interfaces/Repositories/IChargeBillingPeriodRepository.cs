using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories;

public interface IChargeBillingPeriodRepository : IReadWriteRepository<ChargeBillingPeriod>, IUpdateRepository<ChargeBillingPeriod>
{
    
}