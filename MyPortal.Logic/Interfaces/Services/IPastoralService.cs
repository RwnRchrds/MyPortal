using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.Curriculum;


namespace MyPortal.Logic.Interfaces.Services;

public interface IPastoralService
{
    Task<IEnumerable<RegGroupModel>> GetRegGroups();
    Task<IEnumerable<YearGroupModel>> GetYearGroups();
    Task<IEnumerable<HouseModel>> GetHouses();
}