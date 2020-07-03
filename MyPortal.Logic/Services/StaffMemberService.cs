using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Services
{
    public class StaffMemberService : BaseService, IStaffMemberService
    {
        private readonly IStaffMemberRepository _staffMemberRepository;

        public StaffMemberService(IStaffMemberRepository staffMemberRepository) : base("Staff Member")
        {
            _staffMemberRepository = staffMemberRepository;
        }

        public override void Dispose()
        {
            
        }

        public async Task<bool> IsLineManager(Guid staffMemberId, Guid lineManagerId)
        {
            var staffMember = await _staffMemberRepository.GetById(staffMemberId);

            if (staffMember == null)
            {
                throw NotFound();
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
            var staffMember = await _staffMemberRepository.GetById(staffMemberId);

            if (staffMember == null)
            {
                throw NotFound();
            }

            return BusinessMapper.Map<StaffMemberModel>(staffMember);
        }

        public async Task<StaffMemberModel> GetByPersonId(Guid personId, bool throwIfNotFound = true)
        {
            var staffMember = await _staffMemberRepository.GetByPersonId(personId);

            if (staffMember == null && throwIfNotFound)
            {
                throw NotFound();
            }

            return BusinessMapper.Map<StaffMemberModel>(staffMember);
        }

        public async Task<StaffMemberModel> GetByUserId(Guid userId, bool throwIfNotFound = true)
        {
            var staffMember = await _staffMemberRepository.GetByUserId(userId);

            if (staffMember == null && throwIfNotFound)
            {
                throw NotFound();
            }

            return BusinessMapper.Map<StaffMemberModel>(staffMember);
        }
    }
}
