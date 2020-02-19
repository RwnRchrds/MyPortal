using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventTypeRepository : BaseReadRepository<DiaryEventType>, IDiaryEventTypeRepository
    {
        public DiaryEventTypeRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }

        protected override async Task<IEnumerable<DiaryEventType>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<DiaryEventType>(sql, param);
        }
    }
}