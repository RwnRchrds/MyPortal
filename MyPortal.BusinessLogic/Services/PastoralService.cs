using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
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

        public async Task CreateRegGroup(RegGroupDto regGroup)
        {
            ValidationService.ValidateModel(regGroup);

            UnitOfWork.RegGroups.Add(Mapping.Map<RegGroup>(regGroup));
            await UnitOfWork.Complete();
        }

        public async Task CreateYearGroup(YearGroupDto yearGroup)
        {
            ValidationService.ValidateModel(yearGroup);

            UnitOfWork.YearGroups.Add(Mapping.Map<YearGroup>(yearGroup));
            await UnitOfWork.Complete();
        }

        public async Task DeleteRegGroup(int regGroupId)
        {
            var regGroupInDb = await UnitOfWork.RegGroups.GetById(regGroupId);

            if (regGroupInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Reg group not found.");
            }

            UnitOfWork.RegGroups.Remove(regGroupInDb);
            await UnitOfWork.Complete();
        }

        public async Task DeleteYearGroup(int yearGroupId)
        {
            var yearGroupInDb = await UnitOfWork.YearGroups.GetById(yearGroupId);

            if (yearGroupInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Year group not found");
            }

            UnitOfWork.YearGroups.Remove(yearGroupInDb);
            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<RegGroupDto>> GetAllRegGroups()
        {
            return (await UnitOfWork.RegGroups.GetAll()).Select(Mapping.Map<RegGroupDto>);
        }

        public async Task<IEnumerable<YearGroupDto>> GetAllYearGroups()
        {
            return (await UnitOfWork.YearGroups.GetAll()).Select(Mapping.Map<YearGroupDto>);
        }

        public async Task<YearGroupDto> GetYearGroupById(int yearGroupId)
        {
            var yearGroup = await UnitOfWork.YearGroups.GetById(yearGroupId);

            if (yearGroup == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Year group not found");
            }

            return Mapping.Map<YearGroupDto>(yearGroup);
        }

        public async Task<RegGroupDto> GetRegGroupById(int regGroupId)
        {
            var regGroup = await UnitOfWork.RegGroups.GetById(regGroupId);

            if (regGroup == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Reg group not found");
            }

            return Mapping.Map<RegGroupDto>(regGroup);
        }

        public async Task<IEnumerable<RegGroupDto>> GetRegGroupsByYearGroup(int yearGroupId)
        {
            return (await UnitOfWork.RegGroups.GetByYearGroup(yearGroupId)).Select(Mapping.Map<RegGroupDto>);
        }

        public async Task UpdateRegGroup(RegGroupDto regGroup)
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

            if (yearGroupInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Year group not found.");
            }

            yearGroupInDb.Name = yearGroup.Name;
            yearGroupInDb.HeadId = yearGroup.HeadId;

            await UnitOfWork.Complete();
        }
        
        public async Task<IDictionary<int, string>> GetAllYearGroupsLookup()
        {
            return (await GetAllYearGroups()).ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<IDictionary<int, string>> GetAllRegGroupsLookup()
        {
            return (await GetAllRegGroups()).ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<IEnumerable<HouseDto>> GetAllHouses()
        {
            return (await UnitOfWork.Houses.GetAll()).Select(Mapping.Map<HouseDto>);
        }

        public async Task<IDictionary<int, string>> GetAllHousesLookup()
        {
            return (await GetAllHouses()).ToDictionary(x => x.Id, x => x.Name);
        }
    }
}