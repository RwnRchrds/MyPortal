using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Models.Admin;

namespace MyPortal.Logic.Services
{
    public class ApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
    }
}