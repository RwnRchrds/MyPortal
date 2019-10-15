using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class PastoralProcesses
    {
        public static ProcessResponse<object> CreateRegGroup(PastoralRegGroup regGroup, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(regGroup))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            context.PastoralRegGroups.Add(regGroup);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Reg group created", null);
        }

        public static ProcessResponse<object> CreateYearGroup(PastoralYearGroup yearGroup, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(yearGroup))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            context.PastoralYearGroups.Add(yearGroup);
            return new ProcessResponse<object>(ResponseType.Ok, "Year group created", null);
        }

        public static ProcessResponse<object> DeleteRegGroup(int regGroupId, MyPortalDbContext context)
        {
            var regGroupInDb = context.PastoralRegGroups.SingleOrDefault(x => x.Id == regGroupId);

            if (regGroupInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Reg group not found", null);
            }

            context.PastoralRegGroups.Remove(regGroupInDb);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Reg group deleted", null);
        }

        public static ProcessResponse<object> DeleteYearGroup(int yearGroupId, MyPortalDbContext context)
        {
            var yearGroupInDb = context.PastoralYearGroups.SingleOrDefault(x => x.Id == yearGroupId);

            if (yearGroupInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Year group not found", null);
            }

            context.PastoralYearGroups.Remove(yearGroupInDb);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Year group deleted", null);
        }

        public static ProcessResponse<IEnumerable<PastoralRegGroupDto>> GetAllRegGroups(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<PastoralRegGroupDto>>(ResponseType.Ok, null,
                context.PastoralRegGroups.ToList().OrderBy(x => x.Name)
                    .Select(Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>));
        }

        public static ProcessResponse<IEnumerable<PastoralYearGroupDto>> GetAllYearGroups(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<PastoralYearGroupDto>>(ResponseType.Ok, null,
                context.PastoralYearGroups
                    .OrderBy(x => x.Id)
                    .ToList()
                    .Select(Mapper.Map<PastoralYearGroup, PastoralYearGroupDto>));
        }

        public static ProcessResponse<PastoralRegGroupDto> GetRegGroupById(int regGroupId, MyPortalDbContext context)
        {
            var regGroup = context.PastoralRegGroups.SingleOrDefault(x => x.Id == regGroupId);

            if (regGroup == null)
            {
                return new ProcessResponse<PastoralRegGroupDto>(ResponseType.NotFound, "Reg group not found", null);
            }

            return new ProcessResponse<PastoralRegGroupDto>(ResponseType.Ok, null,
                Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>(regGroup));
        }

        public static ProcessResponse<IEnumerable<PastoralRegGroupDto>> GetRegGroupsByYearGroup(int yearGroupId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<PastoralRegGroupDto>>(ResponseType.Ok, null,
                context.PastoralRegGroups
                    .Where(x => x.YearGroupId == yearGroupId)
                    .ToList()
                    .Select(Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>));
        }
        public static ProcessResponse<bool> RegGroupContainsStudents(int regGroupId, MyPortalDbContext context)
        {
            var regGroupInDb = context.PastoralRegGroups.SingleOrDefault(x => x.Id == regGroupId);

            if (regGroupInDb == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Reg group not found", false);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, regGroupInDb.Students.Any());
        }

        public static ProcessResponse<object> UpdateRegGroup(PastoralRegGroup regGroup, MyPortalDbContext context)
        {
            var regGroupInDb = context.PastoralRegGroups.SingleOrDefault(x => x.Id == regGroup.Id);

            if (regGroupInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Reg group not found", null);
            }

            regGroupInDb.Name = regGroup.Name;
            regGroupInDb.TutorId = regGroup.TutorId;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Reg group updated", null);
        }
        public static ProcessResponse<object> UpdateYearGroup(PastoralYearGroup yearGroup, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(yearGroup))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var yearGroupInDb = context.PastoralYearGroups.SingleOrDefault(x => x.Id == yearGroup.Id);

            if (yearGroupInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Year group not found", null);
            }

            yearGroupInDb.Name = yearGroup.Name;
            yearGroupInDb.HeadId = yearGroup.HeadId;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Year group updated", null);
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
    }
}