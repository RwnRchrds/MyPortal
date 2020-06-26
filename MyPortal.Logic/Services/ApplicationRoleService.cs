using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Services
{
    public class ApplicationRoleService : BaseService, IApplicationRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationRoleService(RoleManager<ApplicationRole> roleManager) : base("Role")
        {
            _roleManager = roleManager;
        }

        public async Task CreateRole(RoleModel model)
        {
            var role = new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                System = false
            };
            await _roleManager.CreateAsync(role);
        }

        public async Task UpdateRole(RoleModel model)
        {
            var role = new ApplicationRole
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                System = false
            };

            await _roleManager.UpdateAsync(role);
        }

        public async Task DeleteRole(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            await _roleManager.DeleteAsync(role);
        }

        public async Task<IEnumerable<RoleModel>> Get(string searchParam = null, bool includeSystemRoles = false)
        {
            var query = _roleManager.Roles.Where(x => x.System == includeSystemRoles);

            if (!string.IsNullOrWhiteSpace(searchParam))
            {
                query = query.Where(x => x.Name.StartsWith(searchParam));
            }

            var roles = await query.ToListAsync();

            return roles.Select(BusinessMapper.Map<RoleModel>);
        }

        public override void Dispose()
        {
            _roleManager.Dispose();
        }
    }
}