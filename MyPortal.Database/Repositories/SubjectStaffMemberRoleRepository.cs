using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SubjectStaffMemberRoleRepository : BaseReadWriteRepository<SubjectStaffMemberRole>,
        ISubjectStaffMemberRoleRepository
    {
        public SubjectStaffMemberRoleRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(SubjectStaffMemberRole entity)
        {
            var role = await DbUser.Context.SubjectStaffMemberRoles.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (role == null)
            {
                throw new EntityNotFoundException("Subject role not found.");
            }

            role.Description = entity.Description;
            role.Active = entity.Active;
        }
    }
}