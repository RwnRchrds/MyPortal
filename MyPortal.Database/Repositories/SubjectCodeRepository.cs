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
    public class SubjectCodeRepository : BaseReadRepository<SubjectCode>, ISubjectCodeRepository
    {
        public SubjectCodeRepository(DbUser dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("SubjectCodeSets as SCS", "SCS.Id", $"{TblAlias}.SubjectCodeSetId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(SubjectCodeSet), "SCS");

            return query;
        }

        protected override async Task<IEnumerable<SubjectCode>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var subjectCodes = await DbUser.Transaction.Connection.QueryAsync<SubjectCode, SubjectCodeSet, SubjectCode>(
                sql.Sql,
                (code, set) =>
                {
                    code.SubjectCodeSet = set;

                    return code;
                }, sql.NamedBindings, DbUser.Transaction);

            return subjectCodes;
        }
    }
}