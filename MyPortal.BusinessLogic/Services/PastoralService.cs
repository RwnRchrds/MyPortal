using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class PastoralService : MyPortalService
    {
        public PastoralService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public PastoralService() : base()
        {

        }

        public async Task CreateRegGroup(RegGroup regGroup)
        {
            ValidationService.ValidateModel(regGroup);

            UnitOfWork.RegGroups.Add(regGroup);
            await UnitOfWork.Complete();
        }

        public async Task CreateYearGroup(YearGroup yearGroup)
        {
            ValidationService.ValidateModel(yearGroup);

            UnitOfWork.YearGroups.Add(yearGroup);
            await UnitOfWork.Complete();
        }

        public async Task DeleteRegGroup(int regGroupId)
        {
            var regGroupInDb = await GetRegGroupById(regGroupId);

            UnitOfWork.RegGroups.Remove(regGroupInDb);
            await UnitOfWork.Complete();
        }

        public async Task DeleteYearGroup(int yearGroupId)
        {
            var yearGroupInDb = await GetYearGroupById(yearGroupId);

            UnitOfWork.YearGroups.Remove(yearGroupInDb);
            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<RegGroup>> GetAllRegGroups()
        {
            var regGroups = await UnitOfWork.RegGroups.GetAll();

            return regGroups;
        }

        public async Task<IEnumerable<YearGroup>> GetAllYearGroups()
        {
            var yearGroups = await UnitOfWork.YearGroups.GetAll();

            return yearGroups;
        }

        public async Task<YearGroup> GetYearGroupById(int yearGroupId)
        {
            var yearGroup = await UnitOfWork.YearGroups.GetById(yearGroupId);

            if (yearGroup == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Year group not found");
            }

            return yearGroup;
        }

        public async Task<RegGroup> GetRegGroupById(int regGroupId)
        {
            var regGroup = await UnitOfWork.RegGroups.GetById(regGroupId);

            if (regGroup == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Reg group not found");
            }

            return regGroup;
        }

        public async Task<IEnumerable<RegGroup>> GetRegGroupsByYearGroup(int yearGroupId)
        {
            var yearGroups = await UnitOfWork.RegGroups.GetByYearGroup(yearGroupId);

            return yearGroups;
        }

        public async Task UpdateRegGroup(RegGroup regGroup)
        {
            var regGroupInDb = await GetRegGroupById(regGroup.Id);

            regGroupInDb.Name = regGroup.Name;
            regGroupInDb.TutorId = regGroup.TutorId;

            await UnitOfWork.Complete();
        }

        /// <summary>
        /// Update an existing year group.
        /// </summary>
        public async Task UpdateYearGroup(YearGroup yearGroup)
        {
            var yearGroupInDb = await UnitOfWork.YearGroups.GetById(yearGroup.Id);

            yearGroupInDb.Name = yearGroup.Name;
            yearGroupInDb.HeadId = yearGroup.HeadId;

            await UnitOfWork.Complete();
        }
        
        public async Task<IDictionary<int, string>> GetAllYearGroupsLookup()
        {
            var yearGroups = await GetAllYearGroups();

            return yearGroups.ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<IDictionary<int, string>> GetAllRegGroupsLookup()
        {
            var regGroups = await GetAllRegGroups();

            return regGroups.ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<IEnumerable<House>> GetAllHouses()
        {
            var houses = await UnitOfWork.Houses.GetAll();

            return houses;
        }

        public async Task<IDictionary<int, string>> GetAllHousesLookup()
        {
            var houses = await GetAllHouses();

            return houses.ToDictionary(x => x.Id, x => x.Name);
        }
    }
}