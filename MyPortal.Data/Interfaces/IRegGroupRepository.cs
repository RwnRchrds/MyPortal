using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IRegGroupRepository : IReadWriteRepository<RegGroup>
    {
        Task<IEnumerable<RegGroup>> GetByYearGroup(int yearGroupId);
    }
}
