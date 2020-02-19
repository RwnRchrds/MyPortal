using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class AttendanceCodeRepository : BaseReadRepository<AttendanceCode>, IAttendanceCodeRepository
    {
        public AttendanceCodeRepository(IDbConnection connection) : base(connection)
        {
        RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(AttendanceCodeMeaning))}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AttendanceCodeMeaning]", "[AttendanceCodeMeaning].[Id]", "[AttendanceCode].[MeaningId]")}";
        }

        protected override async Task<IEnumerable<AttendanceCode>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<AttendanceCode, AttendanceCodeMeaning, AttendanceCode>(sql,
                (code, meaning) =>
                {
                    code.CodeMeaning = meaning;

                    return code;
                }, param);
        }
    }
}