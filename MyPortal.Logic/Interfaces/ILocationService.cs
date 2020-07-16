using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces
{
    public interface ILocationService : IService
    {
        Task<Lookup> GetLocations();
    }
}