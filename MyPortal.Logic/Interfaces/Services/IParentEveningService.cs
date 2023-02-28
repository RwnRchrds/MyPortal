using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.StaffMembers;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IParentEveningService : IService
    {
        Task<IEnumerable<ParentEveningAppointmentPlaceholderModel>> GetAppointmentTemplatesByStaffMember(
            Guid parentEveningId, Guid staffMemberId);
    }
}