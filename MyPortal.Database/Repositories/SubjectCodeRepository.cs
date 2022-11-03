using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class SubjectCodeRepository : BaseReadRepository<SubjectCode>, ISubjectCodeRepository
    {
        public SubjectCodeRepository(DbTransaction transaction) : base(transaction)
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

            var subjectCodes = await Transaction.Connection.QueryAsync<SubjectCode, SubjectCodeSet, SubjectCode>(
                sql.Sql,
                (code, set) =>
                {
                    code.SubjectCodeSet = set;

                    return code;
                }, sql.NamedBindings, Transaction);

            return subjectCodes;
        }
    }
}