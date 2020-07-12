using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin;

namespace MyPortal.Logic.Interfaces
{
    public interface IApplicationUserService : IService
    {
        Task CreateUser(CreateUser creator);
        Task ResetPassword(PasswordReset model);
        Task<bool> EnableDisableUser(Guid userId);
        Task<Guid?> GetSelectedAcademicYearId(Guid userId);
        Task<AcademicYearModel> GetSelectedAcademicYear(Guid userId, bool throwIfNotFound = true);
        Task<UserModel> GetUserByPrincipal(ClaimsPrincipal principal);
        Task<string> GetDisplayName(Guid userId);
        Task<UserModel> GetUserById(Guid userId);
    }
}
