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
        private readonly string RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(AttendanceCodeMeaning))}";

        private readonly string JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AttendanceCodeMeaning]", "[AttendanceCodeMeaning].[Id]", "[AttendanceCode].[MeaningId]")}";
        
        public AttendanceCodeRepository(IDbConnection connection) : base(connection)
        {
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

        public async Task<IEnumerable<AttendanceCode>> GetAll()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";

            return await ExecuteQuery(sql);
        }

        public async Task<AttendanceCode> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";
            
            SqlHelper.Where(ref sql, "[AttendanceCode].[Id] = @AttendanceCodeId");

            return (await ExecuteQuery(sql, new {AttendanceCodeId = id})).Single();
        }
    }
}