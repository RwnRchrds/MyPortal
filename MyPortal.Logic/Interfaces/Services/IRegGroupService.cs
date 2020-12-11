using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IRegGroupService : IService
    {
        Task<IEnumerable<RegGroupModel>> GetRegGroups();
    }
}
