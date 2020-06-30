using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class YearGroupRepository : BaseReadWriteRepository<YearGroup>, IYearGroupRepository
    {
        public YearGroupRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(StaffMember));
            query.SelectAll(typeof(Person));
            query.SelectAll(typeof(CurriculumYearGroup));
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.StaffMember", "StaffMember.Id", "YearGroup.HeadId");
            query.LeftJoin("dbo.Person", "Person.Id", "StaffMember.PersonId");
            query.LeftJoin("dbo.CurriculumYearGroup", "CurriculumYearGroup.Id", "YearGroup.Id");
        }

        protected override async Task<IEnumerable<YearGroup>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<YearGroup, StaffMember, Person, CurriculumYearGroup, YearGroup>(sql.Sql, (yearGroup, head, person, curriculumGroup) =>
            {
                yearGroup.HeadOfYear = head;
                yearGroup.HeadOfYear.Person = person;

                yearGroup.CurriculumYearGroup = curriculumGroup;

                return yearGroup;
            }, sql.NamedBindings);
        }
    }
}