using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class StaffMemberService : BaseService, IStaffMemberService
    {
        public async Task<bool> IsLineManager(Guid staffMemberId, Guid lineManagerId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
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
        }

        public async Task<StaffMemberModel> GetById(Guid staffMemberId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var staffMember = await unitOfWork.StaffMembers.GetById(staffMemberId);

                if (staffMember == null)
                {
                    throw new NotFoundException("Staff member not found.");
                }

                return new StaffMemberModel(staffMember);
            }
        }

        public async Task<StaffMemberModel> GetByPersonId(Guid personId, bool throwIfNotFound = true)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var staffMember = await unitOfWork.StaffMembers.GetByPersonId(personId);

                if (staffMember == null && throwIfNotFound)
                {
                    throw new NotFoundException("Staff member not found.");
                }

                return new StaffMemberModel(staffMember);
            }
        }

        public async Task<StaffMemberModel> GetByUserId(Guid userId, bool throwIfNotFound = true)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var staffMember = await unitOfWork.StaffMembers.GetByUserId(userId);

                if (staffMember == null && throwIfNotFound)
                {
                    throw new NotFoundException("Staff member not found.");
                }

                return new StaffMemberModel(staffMember);
            }
        }
    }
}
