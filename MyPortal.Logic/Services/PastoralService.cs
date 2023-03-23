using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Curriculum;


namespace MyPortal.Logic.Services;

public class PastoralService : BaseUserService, IPastoralService
{
    public PastoralService(ISessionUser user) : base(user)
    {
    }

    public async Task<IEnumerable<RegGroupModel>> GetRegGroups()
    {
        await using var unitOfWork = await User.GetConnection();
        
        var regGroups = await unitOfWork.RegGroups.GetAll();

        var result = regGroups.Select(r => new RegGroupModel(r)).ToList();

        return result.OrderBy(r => r.StudentGroup.Description);
    }
    
    public async Task<IEnumerable<YearGroupModel>> GetYearGroups()
    {
        await using var unitOfWork = await User.GetConnection();
        
        var yearGroups = await unitOfWork.YearGroups.GetAll();

        var models = yearGroups.Select(y => new YearGroupModel(y)).ToList();

        return models.OrderBy(m => m.StudentGroup.Description);
    }
    
    public async Task<IEnumerable<HouseModel>> GetHouses()
    {
        await using var unitOfWork = await User.GetConnection();
        
        var houses = await unitOfWork.Houses.GetAll();
                
        var houseModels = houses.Select(h => new HouseModel(h)).ToList();

        return houseModels.OrderBy(h => h.StudentGroup.Description).ToList();
    }
}