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
    public class ExclusionRepository : BaseReadWriteRepository<Exclusion>, IExclusionRepository
    {
        public ExclusionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task<int> GetCountByStudent(Guid studentId)
        {
            var query = GenerateQuery().AsCount();

            query.Where($"{TblAlias}.StudentId", studentId);

            return await ExecuteQueryIntResult(query) ?? 0;
        }

        public async Task<IEnumerable<Exclusion>> GetByStudent(Guid studentId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.StudentId", studentId);

            return await ExecuteQuery(query);
        }

        public async Task Update(Exclusion entity)
        {
            var exclusion = await Context.Exclusions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (exclusion == null)
            {
                throw new EntityNotFoundException("Exclusion not found.");
            }

            exclusion.ExclusionTypeId = entity.ExclusionTypeId;
            exclusion.ExclusionReasonId = entity.ExclusionReasonId;
            exclusion.StartDate = entity.StartDate;
            exclusion.EndDate = entity.EndDate;
            exclusion.Comments = entity.Comments;
        }
    }
}
