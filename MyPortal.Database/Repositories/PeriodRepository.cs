using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class PeriodRepository : BaseReadWriteRepository<Period>, IPeriodRepository
    {
        public PeriodRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<Period>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Period>(sql, param);
        }
    }
}