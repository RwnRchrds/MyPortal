using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Admin;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Interfaces
{
    public interface IApplicationUserService
    {
        Task CreateUser(CreateUser creator);
        Task ResetPassword(PasswordReset model);
        Task<bool> EnableDisableUser(Guid userId);
        Task<Guid?> GetSelectedAcademicYearId(Guid userId);
        Task<AcademicYearModel> GetSelectedAcademicYear(Guid userId);
        Task<UserModel> GetUserByPrincipal(ClaimsPrincipal principal);
    }
}
