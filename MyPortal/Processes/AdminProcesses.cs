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
using MyPortal.Models.Misc;
using MyPortal.ViewModels;

namespace MyPortal.Processes
{
    public static class AdminProcesses
    {
        public static async Task<ProcessResponse<object>> AddUserToRole(UserRoleModel roleModel, UserManager<ApplicationUser, string> userManager, IdentityContext identity)
        {
            var userInDb = identity.Users.SingleOrDefault(u => u.Id == roleModel.UserId);
            var roleInDb = identity.Roles.SingleOrDefault(r => r.Name == roleModel.RoleName);

            if (userInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "User not found", null);
            }

            if (roleInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Role not found", null);
            }

            switch (roleModel.RoleName)
            {                
                case "Admin":
                case "Finance":
                case "SeniorStaff":
                case "Staff":

                    if (await userManager.IsInRoleAsync(roleModel.UserId, "Student"))
                    {
                        return new ProcessResponse<object>(ResponseType.BadRequest, "Students cannot be added to staff roles", null);
                    }
                    break;

                case "Student":

                    if (await userManager.IsInRoleAsync(roleModel.UserId, "Staff"))
                    {
                        return new ProcessResponse<object>(ResponseType.BadRequest, "Staff members cannot be added to student roles", null);
                    }

                    break;

            }

            if (await userManager.IsInRoleAsync(roleModel.UserId, roleModel.RoleName))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "User is already in role", null);
            }

            var result = await userManager.AddToRoleAsync(roleModel.UserId, roleModel.RoleName);

            if (result.Succeeded)
            {
                return new ProcessResponse<object>(ResponseType.Ok, "User added to role", null);
            }

            return new ProcessResponse<object>(ResponseType.BadRequest, "An error occurred", null);
        }

        public static async Task<ProcessResponse<object>> AttachPersonToUser(UserProfile userProfile,
            UserManager<ApplicationUser, string> userManager, IdentityContext identity, MyPortalDbContext context)
        {
            var userInDb = identity.Users.FirstOrDefault(u => u.Id == userProfile.UserId);
            var roleInDb = identity.Roles.FirstOrDefault(r => r.Name == userProfile.RoleName);

            var userIsAttached = context.Students.Any(x => x.Person.UserId == userProfile.UserId) ||
                                 context.StaffMembers.Any(x => x.Person.UserId == userProfile.UserId);                       

            if (userInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "User not found", null);
            }

            if (roleInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Role not found", null);
            }

            if (userIsAttached)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "A person is already attached to this user", null);
            }

            if (userProfile.RoleName == "Staff")
            {
                var roles = await userManager.GetRolesAsync(userProfile.UserId);
                await userManager.RemoveFromRolesAsync(userProfile.UserId, roles.ToArray());
                await userManager.AddToRoleAsync(userProfile.UserId, "Staff");
                var personInDb = context.StaffMembers.Single(x => x.Id == userProfile.PersonId);
                if (personInDb.Person.UserId != null)
                {
                    return new ProcessResponse<object>(ResponseType.BadRequest, "This person is attached to another user", null);
                }

                personInDb.Person.UserId = userInDb.Id;
                context.SaveChanges();
                return new ProcessResponse<object>(ResponseType.Ok, "Person attached", null);
            }

            if (userProfile.RoleName == "Student")
            {
                var roles = await userManager.GetRolesAsync(userProfile.UserId);
                await userManager.RemoveFromRolesAsync(userProfile.UserId, roles.ToArray());
                await userManager.AddToRoleAsync(userProfile.UserId, "Student");
                var personInDb = context.Students.Single(x => x.Id == userProfile.PersonId);
                if (personInDb.Person.UserId != null)
                {
                    return new ProcessResponse<object>(ResponseType.BadRequest, "This person is attached to another user", null);
                }

                personInDb.Person.UserId = userInDb.Id;
                context.SaveChanges();
                return new ProcessResponse<object>(ResponseType.Ok, "Person attached", null);
            }

            return new ProcessResponse<object>(ResponseType.BadRequest, "An error occurred", null);
        }

        public static async Task<ProcessResponse<object>> ChangePassword(ChangePasswordModel data,
            UserManager<ApplicationUser, string> userManager, IdentityContext identity)
        {
            if (data.Password != data.Confirm)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Passwords do not match", null);
            }


            var userInDb = await userManager.FindByIdAsync(data.UserId);

            if (userInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "User not found", null);
            }

            var removePassword = await userManager.RemovePasswordAsync(data.UserId);

            if (removePassword.Succeeded)
            {
                var addNewPassword = await userManager.AddPasswordAsync(data.UserId, data.Password);

                if (addNewPassword.Succeeded)
                {
                    return new ProcessResponse<object>(ResponseType.Ok, "Password changed", null);
                }
            }

            return new ProcessResponse<object>(ResponseType.BadRequest, "An unknown error occurred", null);
        }

        public static async Task<ProcessResponse<object>> DeleteUser(string userId,
            UserManager<ApplicationUser, string> userManager, IdentityContext identity)
        {
            var userInDb = await userManager.FindByIdAsync(userId);

            if (userInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "User not found", null);
            }

            var userRoles = await userManager.GetRolesAsync(userId);

            foreach (var role in userRoles)
            {
                await userManager.RemoveFromRoleAsync(userId, role);
            }

            var result = await userManager.DeleteAsync(userInDb);

            if (result.Succeeded)
            {
                return new ProcessResponse<object>(ResponseType.Ok, "User deleted", null);
            }

            return new ProcessResponse<object>(ResponseType.BadRequest, "An unknown error occurred", null);
        }

        public static async Task<ProcessResponse<object>> DetachPerson(ApplicationUser user,
            UserManager<ApplicationUser, string> userManager, IdentityContext identity, MyPortalDbContext context)
        {
            var userIsAttached = context.Students.Any(x => x.Person.UserId == user.Id) ||
                                 context.StaffMembers.Any(x => x.Person.UserId == user.Id);

            if (!userIsAttached)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "User is not attached to a person", null);
            }

            if (await userManager.IsInRoleAsync(user.Id, "Staff"))
            {
                var personInDb = context.StaffMembers.Single(x => x.Person.UserId == user.Id);
                personInDb.Person.UserId = null;
                context.SaveChanges();

                var roles = await userManager.GetRolesAsync(user.Id);
                var result = await userManager.RemoveFromRolesAsync(user.Id, roles.ToArray());

                if (result.Succeeded)
                {
                    return new ProcessResponse<object>(ResponseType.Ok,
                        $"User {user.UserName} detached from staff member {PeopleProcesses.GetStaffDisplayName(personInDb)}",
                        null);
                }
            }

            if (await userManager.IsInRoleAsync(user.Id, "Student"))
            {
                var personInDb = context.Students.Single(x => x.Person.UserId == user.Id);
                personInDb.Person.UserId = null;
                context.SaveChanges();

                var roles = await userManager.GetRolesAsync(user.Id);
                var result = await userManager.RemoveFromRolesAsync(user.Id, roles.ToArray());

                if (result.Succeeded)
                {
                    return new ProcessResponse<object>(ResponseType.Ok,
                        $"User {user.UserName} detached from student {PeopleProcesses.GetStudentDisplayName(personInDb)}",
                        null);
                }
            }

            return new ProcessResponse<object>(ResponseType.BadRequest, "An unknown error occurred", null);
        }

        public static async Task<ProcessResponse<object>> CreateUser(NewUserViewModel model, UserManager<ApplicationUser, string> userManager, IdentityContext identity)
        {
            model.Id = Guid.NewGuid().ToString();

            if (model.Username.IsNullOrWhiteSpace() || model.Password.IsNullOrWhiteSpace())
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var user = new ApplicationUser
            {
                Id = model.Id,
                UserName = model.Username
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return new ProcessResponse<object>(ResponseType.Ok, "User created", null);
            }

            return new ProcessResponse<object>(ResponseType.BadRequest, "An unknown error occurred", null);
        }

        public static async Task<ProcessResponse<IEnumerable<ApplicationUser>>> GetAllUsers_Model(IdentityContext identity)
        {
            var users = await identity.Users.OrderBy(x => x.UserName).ToListAsync();

            return new ProcessResponse<IEnumerable<ApplicationUser>>(ResponseType.Ok, null, users);
        }

        public static async Task<ProcessResponse<IEnumerable<ApplicationUserDto>>> GetAllUsers(IdentityContext identity)
        {
            var users = await GetAllUsers_Model(identity);

            var list = users.ResponseObject.Select(Mapper.Map<ApplicationUser, ApplicationUserDto>);

            return new ProcessResponse<IEnumerable<ApplicationUserDto>>(ResponseType.Ok, null, list);
        }

        public static async Task<ProcessResponse<IEnumerable<GridApplicationUserDto>>> GetAllUsers_DataGrid(IdentityContext identity)
        {
            var users = await GetAllUsers_Model(identity);

            var list = users.ResponseObject.Select(Mapper.Map<ApplicationUser, GridApplicationUserDto>);

            return new ProcessResponse<IEnumerable<GridApplicationUserDto>>(ResponseType.Ok, null, list);
        }

        public static async Task<ProcessResponse<object>> RemoveFromRole(string userId, string roleName,
            UserManager<ApplicationUser, string> userManager, IdentityContext identity, MyPortalDbContext context)
        {
            var userInDb = identity.Users.FirstOrDefault(user => user.Id == userId);

            if (userInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "User not found", null);
            }

            var userRoles = await userManager.GetRolesAsync(userId);

            var roleToRemove =
                userRoles.FirstOrDefault(role => role.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));

            if (roleToRemove == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "User is not in role", null);
            }

            var result = await userManager.RemoveFromRoleAsync(userId, roleToRemove);

            if (result.Succeeded)
            {
                return new ProcessResponse<object>(ResponseType.Ok, "User removed from role", null);
            }

            return new ProcessResponse<object>(ResponseType.BadRequest, "An unknown error occurred", null);
        }

        public static async Task<ProcessResponse<object>> CreateRole(ApplicationRole role, RoleManager<ApplicationRole, string> roleManager, IdentityContext identity)
        {
            role.System = false;
            role.Id = Guid.NewGuid().ToString();

            if (!ValidationProcesses.ModelIsValid(role))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var result = await roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return new ProcessResponse<object>(ResponseType.Ok, "Role created", null);
            }

            return new ProcessResponse<object>(ResponseType.BadRequest, "An unknown error occurred", null);
        }

        public static async Task<ProcessResponse<object>> UpdateRole(ApplicationRole role, RoleManager<ApplicationRole, string> roleManager, IdentityContext identity)
        {
            var roleInDb = await roleManager.FindByIdAsync(role.Id);

            if (roleInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Role not found", null);
            }

            if (roleInDb.System)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Cannot edit default system roles", null);
            }

            roleInDb.Name = role.Name;
            await identity.SaveChangesAsync();

            return new ProcessResponse<object>(ResponseType.Ok, "Role updated", null);
        }

        public static async Task<ProcessResponse<object>> DeleteRole(string roleId, RoleManager<ApplicationRole, string> roleManager, IdentityContext identity)
        {
            var roleInDb = await roleManager.FindByIdAsync(roleId);

            if (roleInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Role not found", null);
            }

            var result = await roleManager.DeleteAsync(roleInDb);

            if (result.Succeeded)
            {
                return new ProcessResponse<object>(ResponseType.Ok, "Role deleted", null);
            }

            return new ProcessResponse<object>(ResponseType.BadRequest, "An unknown error occurred", null);
        }

        public static async Task<ProcessResponse<ApplicationRole>> GetRoleById_Model(string roleId,
            RoleManager<ApplicationRole, string> roleManager, IdentityContext identity)
        {
            var roleInDb = await roleManager.FindByIdAsync(roleId);

            if (roleInDb == null)
            {
                return new ProcessResponse<ApplicationRole>(ResponseType.NotFound, "Role not found", null);
            }

            return new ProcessResponse<ApplicationRole>(ResponseType.Ok, null,
                roleInDb);
        }

        public static async Task<ProcessResponse<ApplicationRoleDto>> GetRoleById(string roleId,
            RoleManager<ApplicationRole, string> roleManager, IdentityContext identity)
        {
            var roleInDb = await roleManager.FindByIdAsync(roleId);

            if (roleInDb == null)
            {
                return new ProcessResponse<ApplicationRoleDto>(ResponseType.NotFound, "Role not found", null);
            }

            return new ProcessResponse<ApplicationRoleDto>(ResponseType.Ok, null,
                Mapper.Map<ApplicationRole, ApplicationRoleDto>(roleInDb));
        }

        public static async Task<ProcessResponse<IEnumerable<ApplicationRole>>> GetAllRoles_Model(IdentityContext identity)
        {
            var roles = await identity.Roles.Where(x => x.System).OrderBy(x => x.Name).ToListAsync();

            return new ProcessResponse<IEnumerable<ApplicationRole>>(ResponseType.Ok, null, roles);
        }

        public static async Task<ProcessResponse<IEnumerable<ApplicationRoleDto>>> GetAllRoles(IdentityContext identity)
        {
            var process = await GetAllRoles_Model(identity);

            var roles = process.ResponseObject.Select(Mapper.Map<ApplicationRole, ApplicationRoleDto>);

            return new ProcessResponse<IEnumerable<ApplicationRoleDto>>(ResponseType.Ok, null, roles);
        }

        public static async Task<ProcessResponse<IEnumerable<ApplicationRole>>> GetRolesByUser_Model(string userId, RoleManager<ApplicationRole, string> roleManager)
        {
            var roles = await roleManager.Roles.Where(x => x.Users.Any(u => u.UserId == userId)).OrderBy(x => x.Name)
                .ToListAsync();

            return new ProcessResponse<IEnumerable<ApplicationRole>>(ResponseType.Ok, null, roles);
        }

        public static async Task<ProcessResponse<IEnumerable<ApplicationRoleDto>>> GetRolesByUser(string userId, RoleManager<ApplicationRole, string> roleManager)
        {
            var result = await GetRolesByUser_Model(userId, roleManager);

            var roles = result.ResponseObject.Select(Mapper.Map<ApplicationRole, ApplicationRoleDto>);

            return new ProcessResponse<IEnumerable<ApplicationRoleDto>>(ResponseType.Ok, null, roles);
        }

        public static async Task<ProcessResponse<object>> ToggleRolePermission(RolePermission rolePermission,
            RoleManager<ApplicationRole, string> roleManager, IdentityContext identity)
        {
            if (await roleManager.FindByIdAsync(rolePermission.RoleId) == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Role not found", null);
            }

            if (!identity.Permissions.Any(x => x.Id == rolePermission.PermissionId))
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Permission not found", null);
            }

            var rolePermissionInDb = await identity.RolePermissions.SingleOrDefaultAsync(x =>
                x.PermissionId == rolePermission.PermissionId && x.RoleId == rolePermission.RoleId);

            if (rolePermissionInDb != null)
            {
                identity.RolePermissions.Remove(rolePermissionInDb);
                await identity.SaveChangesAsync();

                return new ProcessResponse<object>(ResponseType.Ok, "Permission removed from role", null);
            }

            identity.RolePermissions.Add(rolePermission);
            await identity.SaveChangesAsync();

            return new ProcessResponse<object>(ResponseType.Ok, "Permission added to role", null);
        }

        public static async Task<ProcessResponse<IEnumerable<PermissionIndicator>>> GetPermissionsByRole(string roleId,
            RoleManager<ApplicationRole, string> roleManager, IdentityContext identity)
        {
            var permissions = await identity.Permissions.ToListAsync();

            var role = await roleManager.FindByIdAsync(roleId);

            var permList = new List<PermissionIndicator>();

            foreach (var permission in permissions)
            {
                permList.Add(new PermissionIndicator
                    {
                        Permission = Mapper.Map<Permission, PermissionDto>(permission),
                        HasPermission = role.RolePermissions.Any(x => x.PermissionId == permission.Id)
                    });
            }

            return new ProcessResponse<IEnumerable<PermissionIndicator>>(ResponseType.Ok, null,
                permList.OrderBy(x => x.Permission.Area).ThenBy(x => x.Permission.Name).ToList());
        }
    }
}