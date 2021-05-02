using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class MedicalEventRepository : BaseReadWriteRepository<MedicalEvent>, IMedicalEventRepository
    {
        public MedicalEventRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        public async Task Update(MedicalEvent entity)
        {
            var medicalEvent = await Context.MedicalEvents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (medicalEvent == null)
            {
                throw new EntityNotFoundException("Medical event not found.");
            }

            medicalEvent.Date = entity.Date;
            medicalEvent.Note = entity.Note;
        }
    }
}