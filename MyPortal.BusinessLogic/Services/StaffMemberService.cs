using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.BusinessLogic.Extensions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class StaffMemberService : MyPortalService
    {
        public StaffMemberService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public StaffMemberService() : base()
        {

        }

        public async Task CreateStaffMember(StaffMemberDto staffMember)
        {
            ValidationService.ValidateModel(staffMember);

            UnitOfWork.StaffMembers.Add(Mapper.Map<StaffMember>(staffMember));

            await UnitOfWork.Complete();
        }

        public async Task DeleteStaffMember(int staffMemberId)
        {
            var staffInDb = await UnitOfWork.StaffMembers.GetById(staffMemberId);

            if (staffInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Staff member not found");
            }

            UnitOfWork.StaffMembers.Remove(staffInDb);

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<StaffMemberDto>> GetAllStaffMembers()
        {
            return (await UnitOfWork.StaffMembers.GetAll()).Select(Mapper.Map<StaffMemberDto>).ToList();
        }

        public async Task<IDictionary<int, string>> GetAllStaffMembersLookup()
        {
            return (await GetAllStaffMembers()).ToDictionary(x => x.Id, x => x.Person.GetDisplayName());
        }

        public async Task<StaffMemberDto> GetStaffMemberByUserId(string userId)
        {
            var staff = await UnitOfWork.StaffMembers.GetByUserId(userId);

            if (staff == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Staff member not found");
            }

            return Mapper.Map<StaffMemberDto>(staff);
        }

        public async Task<StaffMemberDto> TryGetStaffMemberByUserId(string userId)
        {
            var staff = await UnitOfWork.StaffMembers.GetByUserId(userId);

            return Mapper.Map<StaffMemberDto>(staff);
        }

        public async Task<StaffMemberDto> GetStaffMemberById(int staffMemberId)
        {
            var staff = await UnitOfWork.StaffMembers.GetById(staffMemberId);

            if (staff == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Staff member not found.");
            }

            return Mapper.Map<StaffMemberDto>(staff);
        }


        public async Task UpdateStaffMember(StaffMemberDto staffMember)
        {
            var staffInDb = await UnitOfWork.StaffMembers.GetById(staffMember.Id);

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