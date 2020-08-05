using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class CurriculumGroupRepository : BaseReadWriteRepository<CurriculumGroup>, ICurriculumGroupRepository
    {
        public CurriculumGroupRepository(ApplicationDbContext context) : base(context, "CurriculumGroup")
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

            return await Connection.QueryAsync<CurriculumGroup, CurriculumBlock, CurriculumGroup>(sql.Sql,
                (group, block) =>
                {
                    group.Block = block;

                    return group;
                }, sql.NamedBindings);

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
    }
}