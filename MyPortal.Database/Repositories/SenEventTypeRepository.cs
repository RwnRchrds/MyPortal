using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SenEventTypeRepository : BaseReadRepository<SenEventType>, ISenEventTypeRepository
    {
        public SenEventTypeRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }

        protected override async Task<IEnumerable<SenEventType>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<SenEventType>(sql, param);
        }
    }
}