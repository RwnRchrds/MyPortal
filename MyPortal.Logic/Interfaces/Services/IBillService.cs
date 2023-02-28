using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.Finance;


namespace MyPortal.Logic.Interfaces.Services
{
    public interface IBillService : IService
    {
        Task<IEnumerable<BillModel>> GenerateChargeBills(Guid chargeBillingPeriodId);
    }
}
