using System;
using System.Collections.Generic;
using System.Data;
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
    public class CurriculumGroupRepository : BaseReadWriteRepository<CurriculumGroup>, ICurriculumGroupRepository
    {
        public CurriculumGroupRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(CurriculumGroup entity)
        {
            var group = await Context.CurriculumGroups.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (group == null)
            {
                throw new EntityNotFoundException("Curriculum group not found.");
            }
            
            group.BlockId = entity.BlockId;
        }
    }
}