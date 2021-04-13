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
    public class SubjectRepository : BaseReadWriteRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(Subject entity)
        {
            var subject = await Context.Subjects.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (subject == null)
            {
                throw new EntityNotFoundException("Subject not found.");
            }

            subject.Name = entity.Name;
            subject.SubjectCodeId = entity.SubjectCodeId;
        }
    }
}