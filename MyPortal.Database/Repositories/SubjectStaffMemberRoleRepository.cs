using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SubjectStaffMemberRoleRepository : BaseReadWriteRepository<SubjectStaffMemberRole>, ISubjectStaffMemberRoleRepository
    {
        public SubjectStaffMemberRoleRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(SubjectStaffMemberRole entity)
        {
            var role = await Context.SubjectStaffMemberRoles.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (role == null)
            {
                throw new EntityNotFoundException("Subject role not found.");
            }

            if (role.System)
            {
                throw ExceptionHelper.UpdateSystemEntityException;
            }

            role.Description = entity.Description;
            role.Active = entity.Active;
        }
    }
}