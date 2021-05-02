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
    public class SenEventRepository : BaseReadWriteRepository<SenEvent>, ISenEventRepository
    {
        public SenEventRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        public async Task Update(SenEvent entity)
        {
            var senEvent = await Context.SenEvents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (senEvent == null)
            {
                throw new EntityNotFoundException("SEN event not found.");
            }

            senEvent.Date = entity.Date;
            senEvent.Note = entity.Note;
            senEvent.EventTypeId = entity.EventTypeId;
        }
    }
}