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
        public YearGroupRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "YearGroup")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StaffMember), "StaffMember");
            query.SelectAllColumns(typeof(Person), "Person");
            query.SelectAllColumns(typeof(CurriculumYearGroup), "CYG");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("StaffMembers as StaffMember", "StaffMember.Id", "YearGroup.HeadId");
            query.LeftJoin("People as Person", "Person.Id", "StaffMember.PersonId");
            query.LeftJoin("CurriculumYearGroups as CYG", "CYG.Id", "YearGroup.Id");
        }

        protected override async Task<IEnumerable<YearGroup>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<YearGroup, CurriculumYearGroup, YearGroup>(sql.Sql, (yearGroup, curriculumGroup) =>
            {
                yearGroup.CurriculumYearGroup = curriculumGroup;

                return yearGroup;
            }, sql.NamedBindings, Transaction);
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