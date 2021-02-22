using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class StaffMemberService : BaseService, IStaffMemberService
    {
        public StaffMemberService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> IsLineManager(Guid staffMemberId, Guid lineManagerId)
        {
            var staffMember = await UnitOfWork.StaffMembers.GetById(staffMemberId);

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
            var staffMember = await UnitOfWork.StaffMembers.GetById(staffMemberId);

            if (staffMember == null)
            {
                throw new NotFoundException("Staff member not found.");
            }

            return BusinessMapper.Map<StaffMemberModel>(staffMember);
        }

        public async Task<StaffMemberModel> GetByPersonId(Guid personId, bool throwIfNotFound = true)
        {
            var staffMember = await UnitOfWork.StaffMembers.GetByPersonId(personId);

            if (staffMember == null && throwIfNotFound)
            {
                throw new NotFoundException("Staff member not found.");
            }

            return BusinessMapper.Map<StaffMemberModel>(staffMember);
        }

        public async Task<StaffMemberModel> GetByUserId(Guid userId, bool throwIfNotFound = true)
        {
            var staffMember = await UnitOfWork.StaffMembers.GetByUserId(userId);

            if (staffMember == null && throwIfNotFound)
            {
                throw new NotFoundException("Staff member not found.");
            }

            return BusinessMapper.Map<StaffMemberModel>(staffMember);
        }
    }
}
