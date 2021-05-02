using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class RoleRepository : BaseReadWriteRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        public async Task Update(Role entity)
        {
            var role = await Context.Roles.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (role == null)
            {
                throw new EntityNotFoundException("Role not found.");
            }

            role.Description = entity.Description;
            role.Permissions = entity.Permissions;
        }
    }
}
