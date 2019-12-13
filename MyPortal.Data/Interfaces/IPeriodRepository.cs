using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IPeriodRepository : IReadWriteRepository<Period>
    {
        Task<IEnumerable<Period>> GetByDayOfWeek(DayOfWeek weekDay);
        Task<IEnumerable<Period>> GetByClass(int classId);
        Task<IEnumerable<Period>> GetRegPeriods();
    }
}
