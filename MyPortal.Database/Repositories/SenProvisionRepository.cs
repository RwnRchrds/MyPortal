using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SenProvisionRepository : BaseReadWriteRepository<SenProvision>, ISenProvisionRepository
    {
        public SenProvisionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task Update(SenProvision entity)
        {
            var senProvision = await Context.SenProvisions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (senProvision == null)
            {
                throw new EntityNotFoundException("SEN provision not found.");
            }

            senProvision.ProvisionTypeId = entity.ProvisionTypeId;
            senProvision.StartDate = entity.StartDate;
            senProvision.EndDate = entity.EndDate;
            senProvision.Note = entity.Note;
        }
    }
}