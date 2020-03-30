using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Admin;
using MyPortal.Logic.Models.Details;

namespace MyPortal.Logic.Interfaces
{
    public interface IApplicationUserService
    {
        Task CreateUser(CreateUser creator);
        Task ResetPassword(PasswordReset model);
        Task<bool> EnableDisableUser(Guid userId);
        Task<Guid?> GetSelectedAcademicYearId(Guid userId);
        Task<AcademicYearDetails> GetSelectedAcademicYear(Guid userId);
        Task<UserDetails> GetUserByPrincipal(ClaimsPrincipal principal);
    }
}
