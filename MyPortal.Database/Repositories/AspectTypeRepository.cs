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
    public class AspectTypeRepository : BaseReadRepository<AspectType>, IAspectTypeRepository
    {
        public AspectTypeRepository(IDbConnection connection) : base(connection)
        {
        }

        protected override async Task<IEnumerable<AspectType>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<AspectType>(sql, param);
        }
    }
}