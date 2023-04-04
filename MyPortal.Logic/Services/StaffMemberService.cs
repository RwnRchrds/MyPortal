using System;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.StaffMembers;
using MyPortal.Logic.Models.Requests.StaffMember;

namespace MyPortal.Logic.Services
{
    public class StaffMemberService : BaseUserService, IStaffMemberService
    {
        public StaffMemberService(ISessionUser user) : base(user)
        {
        }

        public async Task<bool> IsLineManager(Guid staffMemberId, Guid lineManagerId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var staffMember = await unitOfWork.StaffMembers.GetById(staffMemberId);

            if (staffMember == null)
            {
                throw new NotFoundException("Staff member not found.");
            }

            if (staffMember.LineManagerId == null)
            {
                return false;
            }

            if (staffMember.LineManagerId == lineManagerId)
            {
                return true;
            }

            return await IsLineManager(staffMember.LineManagerId.Value, lineManagerId);
        }

        public async Task<StaffMemberModel> GetById(Guid staffMemberId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var staffMember = await unitOfWork.StaffMembers.GetById(staffMemberId);

            if (staffMember == null)
            {
                throw new NotFoundException("Staff member not found.");
            }

            return new StaffMemberModel(staffMember);
        }

        public async Task<StaffMemberModel> GetByPersonId(Guid personId, bool throwIfNotFound = true)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var staffMember = await unitOfWork.StaffMembers.GetByPersonId(personId);

            if (staffMember == null && throwIfNotFound)
            {
                throw new NotFoundException("Staff member not found.");
            }

            return new StaffMemberModel(staffMember);
        }

        public async Task<StaffMemberModel> GetByUserId(Guid userId, bool throwIfNotFound = true)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var staffMember = await unitOfWork.StaffMembers.GetByUserId(userId);

            if (staffMember == null && throwIfNotFound)
            {
                throw new NotFoundException("Staff member not found.");
            }

            return new StaffMemberModel(staffMember);
        }

        public async System.Threading.Tasks.Task CreateStaffMember(StaffMemberRequestModel model)
        {
            Validate(model);

            await using var unitOfWork = await User.GetConnection();

            var staffMember = new StaffMember
            {
                Id = Guid.NewGuid(),
                LineManagerId = model.LineManagerId,
                Code = model.Code,
                BankName = model.BankName,
                BankAccount = model.BankAccount,
                BankSortCode = model.BankSortCode,
                NiNumber = model.NiNumber,
                Qualifications = model.Qualifications,
                TeachingStaff = model.TeachingStaff,        
                Person = PersonHelper.CreatePersonFromModel(model)
            };

            unitOfWork.StaffMembers.Create(staffMember);

            await unitOfWork.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateStaffMember(Guid staffMemberId, StaffMemberRequestModel model)
        {
            Validate(model);

            await using var unitOfWork = await User.GetConnection();

            var staffMemberInDb = await unitOfWork.StaffMembers.GetById(staffMemberId);

            if (staffMemberInDb == null)
            {
                throw new NotFoundException("Staff member not found.");
            }

            staffMemberInDb.LineManagerId = model.LineManagerId;
            staffMemberInDb.PersonId = model.PersonId;
            staffMemberInDb.Code = model.Code;
            staffMemberInDb.BankName = model.BankName;
            staffMemberInDb.BankAccount = model.BankAccount;
            staffMemberInDb.BankSortCode = model.BankSortCode;
            staffMemberInDb.NiNumber = model.NiNumber;
            staffMemberInDb.Qualifications = model.Qualifications;
            staffMemberInDb.TeachingStaff = model.TeachingStaff;

            PersonHelper.UpdatePersonFromModel(staffMemberInDb.Person, model);

            await unitOfWork.People.Update(staffMemberInDb.Person);
            await unitOfWork.StaffMembers.Update(staffMemberInDb);

            await unitOfWork.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteStaffMember(Guid staffMemberId)
        {
            await using var unitOfWork = await User.GetConnection();

            var staffMemberInDb = await unitOfWork.StaffMembers.GetById(staffMemberId);

            if (staffMemberInDb == null)
            {
                throw new NotFoundException("Staff member not found.");
            }

            await unitOfWork.StaffMembers.Delete(staffMemberId);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
