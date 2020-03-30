using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class GradeSetRepository : BaseReadWriteRepository<GradeSet>, IGradeSetRepository
    {
        public GradeSetRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<GradeSet>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<GradeSet>(sql, param);
        }
    }
}