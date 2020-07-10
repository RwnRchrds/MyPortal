using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDiaryEventRepository : IReadWriteRepository<DiaryEvent>
    {
        Task<IEnumerable<DiaryEvent>> GetByDate(DateTime date);
        Task<IEnumerable<DiaryEvent>> GetByDateRange(DateTime start, DateTime end);
    }
}
