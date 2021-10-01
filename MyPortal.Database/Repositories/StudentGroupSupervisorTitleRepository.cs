using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StudentGroupSupervisorTitleRepository : BaseReadWriteRepository<StudentGroupSupervisorTitle>, IStudentGroupSupervisorTitleRepository
    {
        public StudentGroupSupervisorTitleRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(StudentGroupSupervisorTitle entity)
        {
            var title = await Context.StudentGroupSupervisorTitles.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (title == null)
            {
                throw new EntityNotFoundException("Supervisor title not found.");
            }

            title.Description = entity.Description;
            title.Active = entity.Active;
        }
    }
}