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
    public class YearGroupRepository : BaseStudentGroupRepository<YearGroup>, IYearGroupRepository
    {
        public YearGroupRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("StudentGroups as SG", "SG.Id", $"{TblAlias}.StudentGroupId");
            query.LeftJoin("CurriculumYearGroups as CYG", "CYG.Id", $"{TblAlias}.CurriculumYearGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentGroup), "SG");
            query.SelectAllColumns(typeof(CurriculumYearGroup), "CYG");

            return query;
        }

        protected override async Task<IEnumerable<YearGroup>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var yearGroups =
                await Transaction.Connection.QueryAsync<YearGroup, StudentGroup, CurriculumYearGroup, YearGroup>(
                    sql.Sql,
                    (yearGroup, studentGroup, curriculumYear) =>
                    {
                        yearGroup.StudentGroup = studentGroup;
                        yearGroup.CurriculumYearGroup = curriculumYear;

                        return yearGroup;
                    }, sql.NamedBindings, Transaction);

            return yearGroups;
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