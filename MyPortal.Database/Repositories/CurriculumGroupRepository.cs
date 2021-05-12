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
    public class CurriculumGroupRepository : BaseReadWriteRepository<CurriculumGroup>, ICurriculumGroupRepository
    {
        public CurriculumGroupRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("CurriculumBlocks as CB", "CB.Id", $"{TblAlias}.BlockId");
            query.LeftJoin("StudentGroups as SG", "SG.Id", $"{TblAlias}.StudentGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(CurriculumBlock), "CB");
            query.SelectAllColumns(typeof(StudentGroup), "SG");

            return query;
        }

        protected override async Task<IEnumerable<CurriculumGroup>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var groups = await Transaction.Connection
                .QueryAsync<CurriculumGroup, CurriculumBlock, StudentGroup, CurriculumGroup>(sql.Sql,
                    (group, block, studentGroup) =>
                    {
                        group.Block = block;
                        group.StudentGroup = studentGroup;

                        return group;
                    }, sql.NamedBindings, Transaction);

            return groups;
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