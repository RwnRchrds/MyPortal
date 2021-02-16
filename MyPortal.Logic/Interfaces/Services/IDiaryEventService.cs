using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IDiaryEventService : IService
    {
        Task<IEnumerable<DiaryEventTypeModel>> GetEventTypes(bool includeReserved = false);
    }
}