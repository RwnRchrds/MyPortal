using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Exceptions;

namespace MyPortal.Processes
{
    public static class StaffProcesses
    {
        public static async Task CreateStaffMember(StaffMember staffMember, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(staffMember))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            context.Persons.Add(staffMember.Person);
            context.StaffMembers.Add(staffMember);

            await context.SaveChangesAsync();
        }

        public static async Task DeleteStaffMember(int staffMemberId, string userId, MyPortalDbContext context)
        {
            var staffInDb = context.StaffMembers.Single(x => x.Id == staffMemberId);

            if (staffInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Staff member not found");
            }

            if (staffInDb.Person.UserId == userId)
            {
                throw new ProcessException(ExceptionType.Forbidden, "Cannot delete yourself");
            }

            staffInDb.Deleted = true;

            await context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<StaffMemberDto>> GetAllStaffMembers(MyPortalDbContext context)
        {
            var staffMembers = await GetAllStaffMembersModel(context);

            return staffMembers.Select(Mapper.Map<StaffMember, StaffMemberDto>);
        }

        public static async Task<IEnumerable<GridStaffMemberDto>> GetAllStaffMembersDataGrid(MyPortalDbContext context)
        {
            var staffMembers = await GetAllStaffMembersModel(context);

            return staffMembers.Select(Mapper.Map<StaffMember, GridStaffMemberDto>);
        }

        public static async Task<IEnumerable<StaffMember>> GetAllStaffMembersModel(MyPortalDbContext context)
        {
            return await context.StaffMembers.Where(x => !x.Deleted).OrderBy(x => x.Person.LastName).ToListAsync();
        }

        public static string GetDisplayName(this StaffMember staffMember)
        {
            return staffMember == null
                ? null
                : $"{staffMember.Person.Title} {staffMember.Person.FirstName.Substring(0, 1)} {staffMember.Person.LastName}";
        }

        public static async Task<string> GetStaffDisplayNameFromUserId(string userId)
        {
            var context = new MyPortalDbContext();

            var staffMember = await GetStaffFromUserId(userId, context);

            return staffMember.GetDisplayName();
        }

        public static async Task<StaffMember> GetStaffFromUserId(string userId, MyPortalDbContext context)
        {
            var staff = await context.StaffMembers.SingleOrDefaultAsync(x => x.Person.UserId == userId);

            if (staff == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Staff member not found");
            }

            return staff;
        }

        public static string GetFullName(this StaffMember staffMember)
        {
            return $"{staffMember.Person.LastName}, {staffMember.Person.FirstName}";
        }

        public static async Task<StaffMember> GetAuthor(string userId, int authorId, MyPortalDbContext context)
        {
            var author = new StaffMember();

            if (authorId == 0)
            {

                author = await GetStaffFromUserId(userId, context);

                if (author == null)
                {
                    throw new ProcessException(ExceptionType.NotFound, "Staff member not found");
                }
            }

            if (authorId != 0) author = context.StaffMembers.SingleOrDefault(x => x.Id == authorId);

            if (author == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Staff member not found");
            }

            return author;
        }

        public static async Task<bool> StaffMemberHasWrittenLogs(int staffMemberId, MyPortalDbContext context)
        {
            var staffInDb = await context.StaffMembers.SingleOrDefaultAsync(x => x.Id == staffMemberId);

            if (staffInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Staff member not found");
            }

            return context.ProfileLogs.Any(x => x.AuthorId == staffMemberId);
        }

        public static async Task UpdateStaffMember(StaffMember staffMember, MyPortalDbContext context)
        {
            var staffInDb = context.StaffMembers.SingleOrDefault(x => x.Id == staffMember.Id);

            if (staffInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Staff member not found");
            }

            staffInDb.Person.FirstName = staffMember.Person.FirstName;
            staffInDb.Person.LastName = staffMember.Person.LastName;
            staffInDb.Person.Title = staffMember.Person.Title;
            staffInDb.Code = staffMember.Code;

            await context.SaveChangesAsync();
        }

        public static async Task<StaffMemberDto> GetStaffMemberById(int staffMemberId, MyPortalDbContext context)
        {

        }
    }
}