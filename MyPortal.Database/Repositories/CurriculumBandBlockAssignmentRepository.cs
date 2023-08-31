using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBandBlockAssignmentRepository : BaseReadWriteRepository<CurriculumBandBlockAssignment>,
        ICurriculumBandBlockAssignmentRepository
    {
        public CurriculumBandBlockAssignmentRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("CurriculumBlocks as CB", "CB.Id", $"{TblAlias}.BlockId");
            query.LeftJoin("CurriculumBands as B", "B.Id", $"{TblAlias}.BandId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(CurriculumBlock), "CB");
            query.SelectAllColumns(typeof(CurriculumBand), "B");

            return query;
        }

        protected override async Task<IEnumerable<CurriculumBandBlockAssignment>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var assignments = await DbUser.Transaction.Connection
                .QueryAsync<CurriculumBandBlockAssignment, CurriculumBand, CurriculumBlock,
                    CurriculumBandBlockAssignment>(sql.Sql,
                    (assignment, band, block) =>
                    {
                        assignment.Band = band;
                        assignment.Block = block;

                        return assignment;
                    }, sql.NamedBindings, DbUser.Transaction);

            return assignments;
        }

        public async Task Update(CurriculumBandBlockAssignment entity)
        {
            var blockAssignment = await DbUser.Context.CurriculumBandBlocks.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (blockAssignment == null)
            {
                throw new EntityNotFoundException("Block assignment not found.");
            }

            blockAssignment.BandId = entity.BandId;
        }
    }
}