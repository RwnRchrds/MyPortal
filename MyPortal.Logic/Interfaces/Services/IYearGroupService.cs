using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IYearGroupService : IService
    {
        Task<IEnumerable<YearGroupModel>> GetYearGroups();
    }
}
