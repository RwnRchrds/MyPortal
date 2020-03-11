using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CurriculumYearGroupRepository : BaseReadRepository<CurriculumYearGroup>, ICurriculumYearGroupRepository
    {
        protected override async Task<IEnumerable<CurriculumYearGroup>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<CurriculumYearGroup>(sql, param);
        }

        public CurriculumYearGroupRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }
    }
}