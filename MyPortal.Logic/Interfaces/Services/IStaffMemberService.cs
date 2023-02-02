using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.StaffMembers;


namespace MyPortal.Logic.Interfaces.Services
{
    public interface IStaffMemberService
    {
        Task<bool> IsLineManager(Guid staffMemberId, Guid lineManagerId);

        Task<StaffMemberModel> GetById(Guid staffMemberId);

        Task<StaffMemberModel> GetByPersonId(Guid personId, bool throwIfNotFound = true);

        Task<StaffMemberModel> GetByUserId(Guid userId, bool throwIfNotFound = true);
    }
}
