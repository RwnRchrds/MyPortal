using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AttendanceCodeRepository : BaseReadRepository<AttendanceCode>, IAttendanceCodeRepository
    {
        public AttendanceCodeRepository(IDbConnection connection) : base(connection)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(AttendanceCodeMeaning));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.AttendanceCodeMeaning", "AttendanceCodeMeaning.Id", "AttendanceCode.MeaningId");
        }

        protected override async Task<IEnumerable<AttendanceCode>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<AttendanceCode, AttendanceCodeMeaning, AttendanceCode>(sql.Sql,
                (code, meaning) =>
                {
                    code.CodeMeaning = meaning;

                    return code;
                }, sql.NamedBindings);
        }

        public async Task<AttendanceCode> GetByCode(string code)
        {
            var query = SelectAllColumns();

            query.Where("AttendanceCode.Code", "=", code);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }
    }
}