using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos.GridDtos;
using MyPortal.Dtos.Identity;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;
using MyPortal.ViewModels;

namespace MyPortal.Processes
{
    public static class AdminProcesses
    {
        public static async Task AddUserToRole(UserRoleModel roleModel, UserManager<ApplicationUser, string> userManager, IdentityContext identity)
        {
            var userInDb = identity.Users.SingleOrDefault(u => u.Id == roleModel.UserId);
            var roleInDb = identity.Roles.SingleOrDefault(r => r.Name == roleModel.RoleName);

            if (userInDb == null)
            {
                throw new NotFoundException("User not found");
            }

            if (roleInDb == null)
            {
                throw new NotFoundException("Role not found");
            }

            switch (roleModel.RoleName)
            {                
                case "Admin":
                case "Finance":
                case "SeniorStaff":
                case "Staff":

                    if (await userManager.IsInRoleAsync(roleModel.UserId, "Student"))
                    {
                        throw new BadRequestException("Users cannot be added to student and staff roles simultaneously");
                    }
                    break;

                case "Student":

                    if (await userManager.IsInRoleAsync(roleModel.UserId, "Staff"))
                    {
                        throw new BadRequestException("Users cannot be added to student and staff roles simultaneously");
                    }

                    break;

            }

            if (await userManager.IsInRoleAsync(roleModel.UserId, roleModel.RoleName))
            {
                throw new BadRequestException("User is already in role");
            }

            var result = await userManager.AddToRoleAsync(roleModel.UserId, roleModel.RoleName);

            if (result.Succeeded)
            {
                return;
            }

            throw new BadRequestException("An unknown error occurred");
        }

        public static async Task AttachPersonToUser(UserProfile userProfile,
            UserManager<ApplicationUser, string> userManager, IdentityContext identity, MyPortalDbContext context)
        {
            var userInDb = identity.Users.FirstOrDefault(u => u.Id == userProfile.UserId);
            var roleInDb = identity.Roles.FirstOrDefault(r => r.Name == userProfile.RoleName);

            var userIsAttached = context.Students.Any(x => x.Person.UserId == userProfile.UserId) ||
                                 context.StaffMembers.Any(x => x.Person.UserId == userProfile.UserId);                       

            if (userInDb == null)
            {
                throw new NotFoundException("User not found");
            }

            if (roleInDb == null)
            {
                throw new NotFoundException("Role not found");
            }

            if (userIsAttached)
            {
                throw new BadRequestException("A person is already attached to this user");
            }

            switch (userProfile.RoleName)
            {
                case "Staff":
                {
                    var roles = await userManager.GetRolesAsync(userProfile.UserId);
                    await userManager.RemoveFromRolesAsync(userProfile.UserId, roles.ToArray());
                    await userManager.AddToRoleAsync(userProfile.UserId, "Staff");
                    var personInDb = context.StaffMembers.Single(x => x.Id == userProfile.PersonId);
                    if (personInDb.Person.UserId != null)
                    {
                        throw new BadRequestException("This person is attached to another user");
                    }

                    personInDb.Person.UserId = userInDb.Id;
                    context.SaveChanges();
                    break;
                }
                case "Student":
                {
                    var roles = await userManager.GetRolesAsync(userProfile.UserId);
                    await userManager.RemoveFromRolesAsync(userProfile.UserId, roles.ToArray());
                    await userManager.AddToRoleAsync(userProfile.UserId, "Student");
                    var personInDb = context.Students.Single(x => x.Id == userProfile.PersonId);
                    if (personInDb.Person.UserId != null)
                    {
                        throw new BadRequestException("This person is attached to another user");
                    }

                    personInDb.Person.UserId = userInDb.Id;
                    context.SaveChanges();
                    break;
                }
            }
        }

        public static async Task ChangePassword(ChangePasswordModel data,
            UserManager<ApplicationUser, string> userManager)
        {
            if (data.Password != data.Confirm)
            {
                throw new BadRequestException("Passwords do not match");
            }


            var userInDb = await userManager.FindByIdAsync(data.UserId);

            if (userInDb == null)
            {
                throw new NotFoundException("User not found");
            }

            var removePassword = await userManager.RemovePasswordAsync(data.UserId);

            if (removePassword.Succeeded)
            {
                var addNewPassword = await userManager.AddPasswordAsync(data.UserId, data.Password);

                if (addNewPassword.Succeeded)
                {
                    return;
                }
            }

            throw new BadRequestException("An unknown error occurred");
        }

        public static async Task CreateRole(ApplicationRole role, RoleManager<ApplicationRole, string> roleManager)
        {
            role.System = false;
            role.Id = UtilityProcesses.GenerateId();

            if (!ValidationProcesses.ModelIsValid(role))
            {
                throw new BadRequestException("Invalid data");
            }

            var result = await roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return;
            }

            throw new BadRequestException("An unknown error occurred");
        }

        public static async Task CreateUser(NewUserViewModel model, UserManager<ApplicationUser, string> userManager)
        {
            model.Id = UtilityProcesses.GenerateId();

            if (model.Username.IsNullOrWhiteSpace() || model.Password.IsNullOrWhiteSpace())
            {
                throw new BadRequestException("Invalid data");
            }

            var user = new ApplicationUser
            {
                Id = model.Id,
                UserName = model.Username
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return;
            }

            throw new BadRequestException("An unknown error occurred");
        }

        public static async Task DeleteRole(string roleId, RoleManager<ApplicationRole, string> roleManager)
        {
            var roleInDb = await roleManager.FindByIdAsync(roleId);

            if (roleInDb == null)
            {
                throw new NotFoundException("Role not found");
            }

            await roleManager.DeleteAsync(roleInDb);
        }

        public static async Task DeleteUser(string userId,
                                    UserManager<ApplicationUser, string> userManager)
        {
            var userInDb = await userManager.FindByIdAsync(userId);

            if (userInDb == null)
            {
                throw new NotFoundException("User not found");
            }

            var userRoles = await userManager.GetRolesAsync(userId);

            foreach (var role in userRoles)
            {
                await userManager.RemoveFromRoleAsync(userId, role);
            }

            var result = await userManager.DeleteAsync(userInDb);

            if (result.Succeeded)
            {
                return;
            }

            throw new BadRequestException("An unknown error occurred");
        }

        public static async Task DetachPerson(ApplicationUser user,
            UserManager<ApplicationUser, string> userManager, MyPortalDbContext context)
        {
            var userIsAttached = context.Students.Any(x => x.Person.UserId == user.Id) ||
                                 context.StaffMembers.Any(x => x.Person.UserId == user.Id);

            if (!userIsAttached)
            {
                throw new BadRequestException("User is not attached");
            }

            var personInDb = context.Persons.Single(x => x.UserId == user.Id);
            personInDb.UserId = null;
            context.SaveChanges();

            var roles = await userManager.GetRolesAsync(user.Id);
            var result = await userManager.RemoveFromRolesAsync(user.Id, roles.ToArray());

            if (result.Succeeded)
            {
                return;
            }

            throw new BadRequestException("An unknown error occurred");
        }

        public static async Task<IEnumerable<ApplicationRoleDto>> GetAllRoles(IdentityContext identity)
        {
            var roles = await GetAllRolesModel(identity);

            return roles.Select(Mapper.Map<ApplicationRole, ApplicationRoleDto>);
        }

        public static async Task<IEnumerable<ApplicationRole>> GetAllRolesModel(IdentityContext identity)
        {
            var roles = await identity.Roles.Where(x => !x.System).OrderBy(x => x.Name).ToListAsync();

            return roles;
        }

        public static async Task<IEnumerable<ApplicationUserDto>> GetAllUsers(IdentityContext identity)
        {
            var users = await GetAllUsersModel(identity);

            var list = users.Select(Mapper.Map<ApplicationUser, ApplicationUserDto>);

            return list;
        }

        public static async Task<IEnumerable<GridApplicationUserDto>> GetAllUsersDataGrid(IdentityContext identity)
        {
            var users = await GetAllUsersModel(identity);

            var list = users.Select(Mapper.Map<ApplicationUser, GridApplicationUserDto>);

            return list;
        }

        public static async Task<IEnumerable<ApplicationUser>> GetAllUsersModel(IdentityContext identity)
        {
            var users = await identity.Users.OrderBy(x => x.UserName).ToListAsync();

            return users;
        }

        public static async Task<IEnumerable<PermissionIndicator>> GetPermissionsByRole(string roleId,
            RoleManager<ApplicationRole, string> roleManager, IdentityContext identity)
        {
            var permissions = await identity.Permissions.ToListAsync();

            var role = await roleManager.FindByIdAsync(roleId);

            var permList = permissions.Select(permission => new PermissionIndicator
            {
                Permission = Mapper.Map<Permission, PermissionDto>(permission),
                HasPermission = role.RolePermissions.Any(x => x.PermissionId == permission.Id)
            }).OrderBy(x => x.Permission.Area).ThenBy(x => x.Permission.Name).ToList();

            return permList;
        }

        public static async Task<ApplicationRoleDto> GetRoleById(string roleId,
            RoleManager<ApplicationRole, string> roleManager)
        {
            var roleInDb = await GetRoleByIdModel(roleId, roleManager);

            return Mapper.Map<ApplicationRole, ApplicationRoleDto>(roleInDb);
        }

        public static async Task<ApplicationRole> GetRoleByIdModel(string roleId,
            RoleManager<ApplicationRole, string> roleManager)
        {
            var roleInDb = await roleManager.FindByIdAsync(roleId);

            if (roleInDb == null)
            {
                throw new NotFoundException("Role not found");
            }

            return roleInDb;
        }

        public static async Task<IEnumerable<ApplicationRoleDto>> GetRolesByUser(string userId, RoleManager<ApplicationRole, string> roleManager)
        {
            var result = await GetRolesByUserModel(userId, roleManager);

            var roles = result.Select(Mapper.Map<ApplicationRole, ApplicationRoleDto>);

            return roles;
        }

        public static async Task<IEnumerable<ApplicationRole>> GetRolesByUserModel(string userId, RoleManager<ApplicationRole, string> roleManager)
        {
            var roles = await roleManager.Roles.Where(x => x.Users.Any(u => u.UserId == userId)).OrderBy(x => x.Name)
                .ToListAsync();

            return roles;
        }

        public static async Task RemoveFromRole(string userId, string roleName,
                                                    UserManager<ApplicationUser, string> userManager, IdentityContext identity)
        {
            var userInDb = identity.Users.FirstOrDefault(user => user.Id == userId);

            if (userInDb == null)
            {
                throw new NotFoundException("User not found");
            }

            var userRoles = await userManager.GetRolesAsync(userId);

            var roleToRemove =
                userRoles.FirstOrDefault(role => role.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));

            if (roleToRemove == null)
            {
                throw new NotFoundException("User is not in role");
            }

            var result = await userManager.RemoveFromRoleAsync(userId, roleToRemove);

            if (result.Succeeded)
            {
                return;
            }

            throw new BadRequestException("An unknown error occurred");
        }

        public static async Task ToggleRolePermission(RolePermission rolePermission,
            RoleManager<ApplicationRole, string> roleManager, IdentityContext identity)
        {
            if (await roleManager.FindByIdAsync(rolePermission.RoleId) == null)
            {
                throw new NotFoundException("Role not found");
            }

            if (!identity.Permissions.Any(x => x.Id == rolePermission.PermissionId))
            {
                throw new NotFoundException("Permission not found");
            }

            var rolePermissionInDb = await identity.RolePermissions.SingleOrDefaultAsync(x =>
                x.PermissionId == rolePermission.PermissionId && x.RoleId == rolePermission.RoleId);

            if (rolePermissionInDb != null)
            {
                identity.RolePermissions.Remove(rolePermissionInDb);
                await identity.SaveChangesAsync();
            }

            else
            {
                identity.RolePermissions.Add(rolePermission);
                await identity.SaveChangesAsync();
            }
        }

        public static async Task UpdateRole(ApplicationRole role, RoleManager<ApplicationRole, string> roleManager)
        {
            await roleManager.UpdateAsync(role);
        }
    }
}