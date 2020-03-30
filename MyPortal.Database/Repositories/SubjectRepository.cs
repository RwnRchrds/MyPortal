using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SubjectRepository : BaseReadWriteRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
        }

        protected override async Task<IEnumerable<Subject>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Subject>(sql, param);
        }
    }
}