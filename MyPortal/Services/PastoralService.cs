using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Services
{
    public class PastoralService : MyPortalService
    {
        public PastoralService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public PastoralService() : base()
        {

        }

        public async Task CreateRegGroup(PastoralRegGroup regGroup)
        {
            if (!ValidationService.ModelIsValid(regGroup))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.PastoralRegGroups.Add(regGroup);
            await UnitOfWork.Complete();
        }

        public async Task CreateYearGroup(PastoralYearGroup yearGroup)
        {
            if (!ValidationService.ModelIsValid(yearGroup))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.PastoralYearGroups.Add(yearGroup);
            await UnitOfWork.Complete();
        }

        public async Task DeleteRegGroup(int regGroupId)
        {
            var regGroupInDb = await GetRegGroupById(regGroupId);

            UnitOfWork.PastoralRegGroups.Remove(regGroupInDb);
            await UnitOfWork.Complete();
        }

        public async Task DeleteYearGroup(int yearGroupId)
        {
            var yearGroupInDb = await GetYearGroupById(yearGroupId);

            UnitOfWork.PastoralYearGroups.Remove(yearGroupInDb);
            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<PastoralRegGroup>> GetAllRegGroups()
        {
            var regGroups = await UnitOfWork.PastoralRegGroups.GetAll();

            return regGroups;
        }

        public async Task<IEnumerable<PastoralYearGroup>> GetAllYearGroups()
        {
            var yearGroups = await UnitOfWork.PastoralYearGroups.GetAll();

            return yearGroups;
        }

        public async Task<PastoralYearGroup> GetYearGroupById(int yearGroupId)
        {
            var yearGroup = await UnitOfWork.PastoralYearGroups.GetById(yearGroupId);

            if (yearGroup == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Year group not found");
            }

            return yearGroup;
        }

        public async Task<PastoralRegGroup> GetRegGroupById(int regGroupId)
        {
            var regGroup = await UnitOfWork.PastoralRegGroups.GetById(regGroupId);

            if (regGroup == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Reg group not found");
            }

            return regGroup;
        }

        public async Task<IEnumerable<PastoralRegGroup>> GetRegGroupsByYearGroup(int yearGroupId)
        {
            var yearGroups = await UnitOfWork.PastoralRegGroups.GetByYearGroup(yearGroupId);

            return yearGroups;
        }

        public async Task UpdateRegGroup(PastoralRegGroup regGroup)
        {
            var regGroupInDb = await GetRegGroupById(regGroup.Id);

            regGroupInDb.Name = regGroup.Name;
            regGroupInDb.TutorId = regGroup.TutorId;

            await UnitOfWork.Complete();
        }

        /// <summary>
        /// Update an existing year group.
        /// </summary>
        public async Task UpdateYearGroup(PastoralYearGroup yearGroup)
        {
            var yearGroupInDb = await UnitOfWork.PastoralYearGroups.GetById(yearGroup.Id);

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

        public async Task<IEnumerable<PastoralHouse>> GetAllHouses()
        {
            var houses = await UnitOfWork.PastoralHouses.GetAll();

            return houses;
        }

        public async Task<IDictionary<int, string>> GetAllHousesLookup()
        {
            var houses = await GetAllHouses();

            return houses.ToDictionary(x => x.Id, x => x.Name);
        }
    }
}