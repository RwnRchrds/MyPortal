using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class PastoralProcesses
    {
        public static async Task CreateRegGroup(PastoralRegGroup regGroup, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(regGroup))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            context.PastoralRegGroups.Add(regGroup);
            await context.SaveChangesAsync();
        }

        public static async Task CreateYearGroup(PastoralYearGroup yearGroup, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(yearGroup))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            context.PastoralYearGroups.Add(yearGroup);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteRegGroup(int regGroupId, MyPortalDbContext context)
        {
            var regGroupInDb = context.PastoralRegGroups.SingleOrDefault(x => x.Id == regGroupId);

            if (regGroupInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Reg group not found");
            }

            context.PastoralRegGroups.Remove(regGroupInDb);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteYearGroup(int yearGroupId, MyPortalDbContext context)
        {
            var yearGroupInDb = context.PastoralYearGroups.SingleOrDefault(x => x.Id == yearGroupId);

            if (yearGroupInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Year group not found");
            }

            context.PastoralYearGroups.Remove(yearGroupInDb);
            await context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<PastoralRegGroupDto>> GetAllRegGroups(MyPortalDbContext context)
        {
            var regGroups = await context.PastoralRegGroups.OrderBy(x => x.Name).ToListAsync();

            return regGroups.Select(Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>);
        }

        public static async Task<IEnumerable<PastoralYearGroupDto>> GetAllYearGroups(MyPortalDbContext context)
        {
            var yearGroups = await context.PastoralYearGroups.OrderBy(x => x.Id).ToListAsync();

            return yearGroups.Select(Mapper.Map<PastoralYearGroup, PastoralYearGroupDto>);
        }

        public static async Task<PastoralRegGroupDto> GetRegGroupById(int regGroupId, MyPortalDbContext context)
        {
            var regGroup = await context.PastoralRegGroups.SingleOrDefaultAsync(x => x.Id == regGroupId);

            if (regGroup == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Reg group not found");
            }

            return Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>(regGroup);
        }

        public static async Task<IEnumerable<PastoralRegGroupDto>> GetRegGroupsByYearGroup(int yearGroupId,
            MyPortalDbContext context)
        {
            var yearGroups = await context.PastoralRegGroups.Where(x => x.YearGroupId == yearGroupId).ToListAsync();

            return yearGroups.Select(Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>);
        }

        public static async Task<bool> RegGroupContainsStudents(int regGroupId, MyPortalDbContext context)
        {
            return await context.Students.AnyAsync(x => x.RegGroupId == regGroupId);
        }

        public static async Task UpdateRegGroup(PastoralRegGroup regGroup, MyPortalDbContext context)
        {
            var regGroupInDb = await context.PastoralRegGroups.SingleOrDefaultAsync(x => x.Id == regGroup.Id);

            if (regGroupInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Reg group not found");
            }

            regGroupInDb.Name = regGroup.Name;
            regGroupInDb.TutorId = regGroup.TutorId;

            await context.SaveChangesAsync();
        }

        public static async Task UpdateYearGroup(PastoralYearGroup yearGroup, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(yearGroup))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            var yearGroupInDb = await context.PastoralYearGroups.SingleOrDefaultAsync(x => x.Id == yearGroup.Id);

            if (yearGroupInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Year group not found");
            }

            yearGroupInDb.Name = yearGroup.Name;
            yearGroupInDb.HeadId = yearGroup.HeadId;

            await context.SaveChangesAsync();
        }
        
        public static async Task<IDictionary<int, string>> GetAllYearGroupsLookup(MyPortalDbContext context)
        {
            var yearGroups = await context.PastoralYearGroups.OrderBy(x => x.Name)
                .ToDictionaryAsync(x => x.Id, x => x.Name);

            return yearGroups;
        }

        public static async Task<IDictionary<int, string>> GetAllRegGroupsLookup(MyPortalDbContext context)
        {
            var regGroups = await context.PastoralRegGroups.OrderBy(x => x.Name)
                .ToDictionaryAsync(x => x.Id, x => x.Name);

            return regGroups;
        }

        public static async Task<IDictionary<int, string>> GetAllHousesLookup(MyPortalDbContext context)
        {
            var houses = await context.PastoralHouses.OrderBy(x => x.Name).ToDictionaryAsync(x => x.Id, x => x.Name);

            return houses;
        }
    }
}