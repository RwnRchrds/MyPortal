using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class DetentionTypeRepository : BaseReadWriteRepository<DetentionType>, IDetentionTypeRepository
    {
        public DetentionTypeRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<DetentionType>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<DetentionType>(sql, param);
        }
    }
}