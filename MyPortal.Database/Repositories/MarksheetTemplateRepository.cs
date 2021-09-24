using System.Data.Common;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class MarksheetTemplateRepository : BaseReadWriteRepository<MarksheetTemplate>, IMarksheetTemplateRepository
    {
        public MarksheetTemplateRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task Update(MarksheetTemplate entity)
        {
            var template = await Context.MarksheetTemplates.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (template == null)
            {
                throw new EntityNotFoundException("Markseet template not found.");
            }

            template.Name = entity.Name;
            template.Active = entity.Active;
        }
    }
}