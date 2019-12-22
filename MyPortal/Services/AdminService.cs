using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos.Identity;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.BusinessLogic.Models.Data;
using MyPortal.BusinessLogic.Services;
using MyPortal.Models.Identity;

namespace MyPortal.Services
{
    public class AdminService : IdentityService
    { 

        public AdminService() : base()
        {

        }
        
        public async Task AddUserToRole(UserRoleModel roleModel)
        {
            var userInDb = await UserManager.FindByIdAsync(roleModel.UserId);
            var roleInDb = await RoleManager.FindByNameAsync(roleModel.RoleName);

            if (userInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "User not found");
            }

            if (roleInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Role not found");
            }

            switch (roleModel.RoleName)
            {                
                case "Admin":
                case "Finance":
                case "SeniorStaff":
                case "Staff":

                    if (await UserManager.IsInRoleAsync(roleModel.UserId, "Student"))
                    {
                        throw new ServiceException(ExceptionType.BadRequest, "Users cannot be added to student and staff roles simultaneously");
                    }
                    break;

                case "Student":

                    if (await UserManager.IsInRoleAsync(roleModel.UserId, "Staff"))
                    {
                        throw new ServiceException(ExceptionType.BadRequest, "Users cannot be added to student and staff roles simultaneously");
                    }

                    break;

            }

            if (await UserManager.IsInRoleAsync(roleModel.UserId, roleModel.RoleName))
            {
                throw new ServiceException(ExceptionType.BadRequest,"User is already in role");
            }

            var result = await UserManager.AddToRoleAsync(roleModel.UserId, roleModel.RoleName);

            if (result.Succeeded)
            {
                return;
            }

            throw new ServiceException(ExceptionType.BadRequest,"An unknown error occurred");
        }

        public async Task AttachPersonToUser(UserProfile userProfile)
        {
            using (var peopleService = new PeopleService())
            {
                var person = await peopleService.GetPersonById(userProfile.PersonId);

                if (await UserManager.FindByIdAsync(userProfile.UserId) == null)
                {
                    throw new ServiceException(ExceptionType.NotFound,"User not found");
                }

                var userIsAttached = person.UserId != null;

                if (userIsAttached)
                {
                    throw new ServiceException(ExceptionType.BadRequest,"A person is already attached to this user");
                }

                person.UserId = userProfile.UserId;

                await peopleService.UpdatePerson(person);
            }
        }

        public async Task ChangePassword(ChangePasswordModel data)
        {
            if (data.Password != data.Confirm)
            {
                throw new ServiceException(ExceptionType.BadRequest,"Passwords do not match");
            }


            var userInDb = await UserManager.FindByIdAsync(data.UserId);

            if (userInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"User not found");
            }

            var removePassword = await UserManager.RemovePasswordAsync(data.UserId);

            if (removePassword.Succeeded)
            {
                var addNewPassword = await UserManager.AddPasswordAsync(data.UserId, data.Password);

                if (addNewPassword.Succeeded)
                {
                    return;
                }
            }

            throw new ServiceException(ExceptionType.BadRequest,"An unknown error occurred");
        }

        public async Task CreateRole(ApplicationRole role)
        {
            using (var utilityService = new UtilityService())
            {
                role.System = false;
                role.Id = utilityService.GenerateId();

               ValidationService.ValidateModel(role);

                var result = await RoleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return;
                }

                throw new ServiceException(ExceptionType.BadRequest, "An unknown error occurred");
            }
        }

        public async Task<string> CreateUser(NewUserModel model)
        {
            using (var utilityService = new UtilityService())
            {
                model.Id = utilityService.GenerateId();

                if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                {
                    throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
                }

                var user = new ApplicationUser
                {
                    Id = model.Id,
                    UserName = model.Username,
                    UserType = model.UserType
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return model.Id;
                }

                if (result.Errors.Any())
                {
                    throw new ServiceException(ExceptionType.BadRequest, result.Errors.FirstOrDefault());
                }

                throw new ServiceException(ExceptionType.BadRequest, "An unknown error occurred");
            }
        }

        public async Task DeleteRole(string roleId)
        {
            var roleInDb = await RoleManager.FindByIdAsync(roleId);

            if (roleInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Role not found");
            }

            await RoleManager.DeleteAsync(roleInDb);
        }

        public async Task DeleteUser(string userId)
        {
            var userInDb = await UserManager.FindByIdAsync(userId);

            if (userInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"User not found");
            }

            var userRoles = await UserManager.GetRolesAsync(userId);

            foreach (var role in userRoles)
            {
                await UserManager.RemoveFromRoleAsync(userId, role);
            }

            var result = await UserManager.DeleteAsync(userInDb);

            if (result.Succeeded)
            {
                return;
            }

            throw new ServiceException(ExceptionType.BadRequest,"An unknown error occurred");
        }

        public async Task DetachPerson(ApplicationUser user)
        {
            var userIsAttached = await UnitOfWork.Students.Any(x => x.Person.UserId == user.Id) ||
                                 await UnitOfWork.StaffMembers.Any(x => x.Person.UserId == user.Id);

            if (!userIsAttached)
            {
                throw new ServiceException(ExceptionType.BadRequest,"User is not attached");
            }

            var personInDb = await UnitOfWork.People.GetByUserId(user.Id);
            personInDb.UserId = null;
            await UnitOfWork.Complete();

            var roles = await UserManager.GetRolesAsync(user.Id);
            var result = await UserManager.RemoveFromRolesAsync(user.Id, roles.ToArray());

            if (result.Succeeded)
            {
                return;
            }

            throw new ServiceException(ExceptionType.BadRequest,"An unknown error occurred");
        }
        
        public async Task<IEnumerable<ApplicationRole>> GetUserDefinedRoles()
        {
            var roles = await Identity.Roles.Where(x => !x.System).OrderBy(x => x.Name).ToListAsync();

            return roles;
        }

        public async Task<IEnumerable<ApplicationRole>> GetAllRoles()
        {
            var roles = await Identity.Roles.OrderBy(x => x.Name).ToListAsync();

            return roles;
        }
        
        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            var users = await Identity.Users.OrderBy(x => x.UserName).ToListAsync();

            return users;
        }

        public async Task<IEnumerable<PermissionIndicator>> GetPermissionsByRole(string roleId)
        {
            var mappingService = new MappingService();
            var permissions = await Identity.Permissions.ToListAsync();

            var role = await RoleManager.FindByIdAsync(roleId);

            var permList = permissions.Select(permission => new PermissionIndicator
            {
                Permission = mappingService.Map<PermissionDto>(permission),
                HasPermission = role.RolePermissions.Any(x => x.PermissionId == permission.Id)
            }).OrderBy(x => x.Permission.Area).ThenBy(x => x.Permission.Name).ToList();

            return permList;
        }

        public async Task<ApplicationRole> GetRoleById(string roleId)
        {
            var roleInDb = await RoleManager.FindByIdAsync(roleId);

            if (roleInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Role not found");
            }

            return roleInDb;
        }

        public async Task<IEnumerable<ApplicationRole>> GetRolesByUser(string userId)
        {
            var roles = await RoleManager.Roles.Where(x => x.Users.Any(u => u.UserId == userId)).OrderBy(x => x.Name)
                .ToListAsync();

            return roles;
        }

        public async Task RemoveFromRole(string userId, string roleName)
        {
            var userInDb = await Identity.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if (userInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"User not found");
            }

            var userRoles = await UserManager.GetRolesAsync(userId);

            var roleToRemove =
                userRoles.FirstOrDefault(role => role.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));

            if (roleToRemove == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"User is not in role");
            }

            var result = await UserManager.RemoveFromRoleAsync(userId, roleToRemove);

            if (result.Succeeded)
            {
                return;
            }

            throw new ServiceException(ExceptionType.BadRequest,"An unknown error occurred");
        }

        public async Task ToggleRolePermission(RolePermission rolePermission)
        {
            if (await RoleManager.FindByIdAsync(rolePermission.RoleId) == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Role not found");
            }

            if (!await Identity.Permissions.AnyAsync(x => x.Id == rolePermission.PermissionId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Permission not found");
            }

            var rolePermissionInDb = await Identity.RolePermissions.SingleOrDefaultAsync(x =>
                x.PermissionId == rolePermission.PermissionId && x.RoleId == rolePermission.RoleId);

            if (rolePermissionInDb != null)
            {
                Identity.RolePermissions.Remove(rolePermissionInDb);
                await Identity.SaveChangesAsync();
            }

            else
            {
                Identity.RolePermissions.Add(rolePermission);
                await Identity.SaveChangesAsync();
            }
        }

        public async Task UpdateRole(ApplicationRole role)
        {
            var roleInDb = await RoleManager.FindByIdAsync(role.Id);

            roleInDb.Description = role.Description;
            roleInDb.Name = role.Name;

            await Identity.SaveChangesAsync();
        }

        public async Task ChangeUserType(string userId, UserType userType)
        {
            var userInDb = await UserManager.FindByIdAsync(userId);

            userInDb.UserType = userType;

            await UserManager.UpdateAsync(userInDb);
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "User not found");
            }

            return user;
        }
    }
}