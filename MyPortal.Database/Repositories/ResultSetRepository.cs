using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ResultSetRepository : BaseReadWriteRepository<ResultSet>, IResultSetRepository
    {
        public ResultSetRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<ResultSet>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<ResultSet>(sql, param);
        }

        public async Task Update(ResultSet entity)
        {
            var rs = await Context.ResultSets.FindAsync(entity.Id);

            rs.Name = entity.Name;
            rs.Active = entity.Active;
        }
    }
}