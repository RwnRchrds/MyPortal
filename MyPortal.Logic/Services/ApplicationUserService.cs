using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Admin;
using MyPortal.Logic.Models.Details;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class ApplicationUserService : BaseService, IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAcademicYearRepository _academicYearRepository;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, IAcademicYearRepository academicYearRepository)
        {
            _userManager = userManager;
            _academicYearRepository = academicYearRepository;
        }

        public async Task CreateUser(CreateUser creator)
        {
            if (creator.UserType != UserTypeDictionary.Staff && creator.UserType != UserTypeDictionary.Student &&
                creator.UserType != UserTypeDictionary.Parent)
            {
                throw new ArgumentException("User type is not valid.");
            }
            
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = creator.Username,
                Email = creator.Email,
                PhoneNumber = creator.PhoneNumber,
                UserType = creator.UserType,
                Enabled = true
            };
            
            await _userManager.CreateAsync(user, creator.Password);
        }

        public async Task ResetPassword(PasswordReset model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());

            await _userManager.RemovePasswordAsync(user);

            await _userManager.AddPasswordAsync(user, model.NewPassword);
        }

        public async Task<bool> EnableDisableUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            user.Enabled = !user.Enabled;

            await _userManager.UpdateAsync(user);

            return user.Enabled;
        }

        public async Task<Guid?> GetSelectedAcademicYearId(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            return user.SelectedAcademicYearId;
        }

        public async Task<AcademicYearDetails> GetSelectedAcademicYear(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var selected = user.SelectedAcademicYearId;

            if (selected != null)
            {
                var acadYear = await _academicYearRepository.GetById((Guid) selected);

                return _businessMapper.Map<AcademicYearDetails>(acadYear);
            }

            return null;
        }

        public async Task<UserDetails> GetUserByPrincipal(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);

            return _businessMapper.Map<UserDetails>(user);
        }
    }
}