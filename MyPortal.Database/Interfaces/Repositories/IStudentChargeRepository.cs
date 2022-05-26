using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentChargeRepository : IReadWriteRepository<StudentCharge>, IUpdateRepository<StudentCharge>
    {
        Task<IEnumerable<StudentCharge>> GetOutstandingByBillingPeriod(Guid chargeBillingPeriodId);
    }
}
