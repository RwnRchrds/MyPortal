using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseModel>> GetHouses();
    }
}
