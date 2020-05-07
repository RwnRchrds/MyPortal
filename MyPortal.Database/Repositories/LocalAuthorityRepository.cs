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
    public class LocalAuthorityRepository : BaseReadRepository<LocalAuthority>, ILocalAuthorityRepository
    {
        public LocalAuthorityRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }

        protected override async Task<IEnumerable<LocalAuthority>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<LocalAuthority>(sql, param);
        }

        public async Task<LocalAuthority> GetCurrent()
        {
            var sql = SelectAllColumns();

            SqlHelper.Join(JoinType.LeftJoin, "[dbo].[School]", "[School].[LocalAuthorityId]", "[LocalAuthority].[Id]");

            SqlHelper.Where(ref sql, "[School].[Local] = 1");

            SqlHelper.Where(ref sql, "[LocalAuthority].[Id] = [School].[LocalAuthorityId]");

            return (await ExecuteQuery(sql)).First();
        }
    }
}