﻿using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class BehaviourTargetRepository : BaseReadWriteRepository<BehaviourTarget>, IBehaviourTargetRepository
    {
        public BehaviourTargetRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(BehaviourTarget entity)
        {
            var target = await Context.BehaviourTargets.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (target == null)
            {
                throw new EntityNotFoundException("Target not found.");
            }

            target.Description = entity.Description;
            target.Active = entity.Active;
        }
    }
}