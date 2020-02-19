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
    public class CommunicationTypeRepository : BaseReadRepository<CommunicationType>, ICommunicationTypeRepository
    {
        public CommunicationTypeRepository(IDbConnection connection) : base(connection)
        {
        }

        protected override async Task<IEnumerable<CommunicationType>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<CommunicationType>(sql, param);
        }
    }
}