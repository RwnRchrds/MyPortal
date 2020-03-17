using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Admin;
using MyPortal.Logic.Models.Details;

namespace MyPortal.Logic.Services
{
    public class ApplicationRoleService : BaseService, IApplicationRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationRoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateRole(RoleDetails details)
        {
            var role = new ApplicationRole
            {
                Id = details.Id,
                Name = details.Name,
                Description = details.Description
            };
            await _roleManager.CreateAsync(role);
        }

        public async Task<IEnumerable<RoleDetails>> Get(string searchParam = null)
        {
            var query = _roleManager.Roles;

            if (!string.IsNullOrWhiteSpace(searchParam))
            {
                query = query.Where(x => x.Name.StartsWith(searchParam));
            }

            var roles = await query.ToListAsync();

            return roles.Select(_businessMapper.Map<RoleDetails>);
        }
    }
}