using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Response.Contacts;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IParentEveningService
    {
        Task<IEnumerable<ParentEveningAppointmentTemplateModel>> GetAppointmentTemplatesByStaffMember(
            Guid parentEveningId, Guid staffMemberId);
    }
}