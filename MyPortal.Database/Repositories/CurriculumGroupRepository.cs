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
        public CurriculumGroupRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "CurriculumGroup")
        {
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(CurriculumBlock), "Block");
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("CurriculumBlocks as Block", "Block.Id", "CurriculumGroup.BlockId");
        }

        protected override async Task<IEnumerable<CurriculumGroup>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<CurriculumGroup, CurriculumBlock, CurriculumGroup>(sql.Sql,
                (group, block) =>
                {
                    group.Block = block;

                    return group;
                }, sql.NamedBindings, Transaction);

        }

        public async Task<bool> CheckUniqueCode(Guid academicYearId, string code)
        {
            var query = GenerateQuery();

            query.LeftJoin("CurriculumBandBlockAssignment as Assignment", "Assignment.BlockId", "Block.Id");
            query.LeftJoin("CurriculumBand as Band", "Band.Id", "Assignment.BandId");

            query.Where("Band.AcademicYearId", academicYearId);
            query.Where("CurriculumGroup.Code", code);

            var result = await ExecuteQueryIntResult(query);

            return result == null || result == 0;
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