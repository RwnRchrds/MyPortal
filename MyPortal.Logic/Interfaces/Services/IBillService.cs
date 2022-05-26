using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IBillService
    {
        Task<IEnumerable<BillModel>> GenerateChargeBills(Guid chargeBillingPeriodId);
    }
}
