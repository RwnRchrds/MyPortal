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
    public class YearGroupRepository : BaseReadWriteRepository<YearGroup>, IYearGroupRepository
    {
        public YearGroupRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task Update(YearGroup entity)
        {
            var yearGroup = await Context.YearGroups.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (yearGroup == null)
            {
                throw new EntityNotFoundException("Year group not found.");
            }

            yearGroup.CurriculumYearGroupId = entity.CurriculumYearGroupId;
        }
    }
}