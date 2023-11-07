using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class RegGroupRepository : BaseStudentGroupRepository<RegGroup>, IRegGroupRepository
    {
        public RegGroupRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("StudentGroups as SG", "SG.Id", $"{TblAlias}.StudentGroupId");
            query.LeftJoin("YearGroups as YG", "YG.Id", $"{TblAlias}.YearGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentGroup), "SG");
            query.SelectAllColumns(typeof(YearGroup), "YG");

            return query;
        }

        protected override async Task<IEnumerable<RegGroup>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var regGroups = await DbUser.Transaction.Connection.QueryAsync<RegGroup, StudentGroup, YearGroup, RegGroup>(
                sql.Sql,
                (reg, studentGroup, year) =>
                {
                    reg.StudentGroup = studentGroup;
                    reg.YearGroup = year;

                    return reg;
                }, sql.NamedBindings, DbUser.Transaction);

            return regGroups;
        }
    }
}