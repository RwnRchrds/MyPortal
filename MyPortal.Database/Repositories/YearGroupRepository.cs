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
        public YearGroupRepository(ApplicationDbContext context) : base(context, "YearGroup")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StaffMember), "StaffMember");
            query.SelectAllColumns(typeof(Person), "Person");
            query.SelectAllColumns(typeof(CurriculumYearGroup), "CYG");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("StaffMembers as StaffMember", "StaffMember.Id", "YearGroup.HeadId");
            query.LeftJoin("People as Person", "Person.Id", "StaffMember.PersonId");
            query.LeftJoin("CurriculumYearGroup as CYG", "CYG.Id", "YearGroup.Id");
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