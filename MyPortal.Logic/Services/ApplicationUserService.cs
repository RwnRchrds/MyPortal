using System;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Admin;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class ApplicationUserService : BaseService, IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPersonRepository _personRepository;
        private readonly IAcademicYearRepository _academicYearRepository;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, IAcademicYearRepository academicYearRepository, IPersonRepository personRepository) : base("User")
        {
            _userManager = userManager;
            _academicYearRepository = academicYearRepository;
            _personRepository = personRepository;
        }

        public async Task CreateUser(CreateUser creator)
        {
            if (creator.UserType != UserTypes.Staff && creator.UserType != UserTypes.Student &&
                creator.UserType != UserTypes.Parent)
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
                Enabled = true,
            };

            var result = await _userManager.CreateAsync(user, creator.Password);

            if (!result.Succeeded)
            {
                throw BadRequest(result.Errors.FirstOrDefault()?.Description);
            }
        }

        public async Task<PasswordVerificationResult> CheckPassword(Guid userId, string password)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            return hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        }

        public async Task SetPassword(Guid userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            await _userManager.RemovePasswordAsync(user);

            await _userManager.AddPasswordAsync(user, newPassword);
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

        public async Task<AcademicYearModel> GetSelectedAcademicYear(Guid userId, bool throwIfNotFound = true)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var selected = user.SelectedAcademicYearId;

            if (selected == null && throwIfNotFound)
            {
                throw NotFound("No academic year has been selected.");
            }

            var acadYear = await _academicYearRepository.GetById(selected.Value);

            return BusinessMapper.Map<AcademicYearModel>(acadYear);
        }

        public async Task<UserModel> GetUserByPrincipal(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);

            return BusinessMapper.Map<UserModel>(user);
        }

        public async Task<UserModel> GetUserById(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            return BusinessMapper.Map<UserModel>(user);
        }

        public async Task<string> GetDisplayName(Guid userId)
        {
            var personInDb = await _personRepository.GetByUserId(userId);

            var person = BusinessMapper.Map<PersonModel>(personInDb);

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (person == null)
            {
                return user.UserName;
            }

            if (user.UserType == UserTypes.Staff)
            {
                return person.GetDisplayName(true);
            }

            return person.GetDisplayName();
        }

        public override void Dispose()
        {
            _userManager.Dispose();
            _academicYearRepository.Dispose();
            _personRepository.Dispose();
        }
    }
}