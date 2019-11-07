using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Exceptions;
using MyPortal.Extensions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Services
{
    public class StaffMemberService : MyPortalService
    {
        public StaffMemberService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task CreateStaffMember(StaffMember staffMember)
        {
            if (!ValidationService.ModelIsValid(staffMember))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.StaffMembers.Add(staffMember);

            await UnitOfWork.Complete();
        }

        public async Task DeleteStaffMember(int staffMemberId, string userId)
        {
            var staffInDb = await GetStaffMemberById(staffMemberId);

            if (staffInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Staff member not found");
            }

            if (staffInDb.Person.UserId == userId)
            {
                throw new ServiceException(ExceptionType.Forbidden, "Cannot delete yourself");
            }

            staffInDb.Deleted = true;

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<StaffMember>> GetAllStaffMembers()
        {
            return await UnitOfWork.StaffMembers.GetAllAsync();
        }


        public async Task<string> GetStaffDisplayNameFromUserId(string userId)
        {
            var context = new MyPortalDbContext();

            var staffMember = await GetStaffMemberByUserId(userId);

            return staffMember.GetDisplayName();
        }

        public async Task<StaffMember> GetStaffMemberByUserId(string userId)
        {
            var staff = await UnitOfWork.StaffMembers.GetByUserIdAsync(userId);

            if (staff == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Staff member not found");
            }

            return staff;
        }

        public async Task<StaffMember> TryGetStaffMemberByUserId(string userId)
        {
            var staff = await UnitOfWork.StaffMembers.GetByUserIdAsync(userId);

            return staff;
        }

        public async Task<StaffMember> GetStaffMemberById(int staffMemberId)
        {
            var staff = await UnitOfWork.StaffMembers.GetByIdAsync(staffMemberId);

            return staff;
        }


        public async Task UpdateStaffMember(StaffMember staffMember)
        {
            var staffInDb = await GetStaffMemberById(staffMember.Id);

            if (staffInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Staff member not found");
            }

            staffInDb.Person.FirstName = staffMember.Person.FirstName;
            staffInDb.Person.LastName = staffMember.Person.LastName;
            staffInDb.Person.Title = staffMember.Person.Title;
            staffInDb.Code = staffMember.Code;

            await UnitOfWork.Complete();
        }
    }
}