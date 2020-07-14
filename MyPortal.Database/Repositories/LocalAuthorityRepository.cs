using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class LocalAuthorityRepository : BaseReadRepository<LocalAuthority>, ILocalAuthorityRepository
    {
        public LocalAuthorityRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }

        public async Task<LocalAuthority> GetCurrent()
        {
            var query = SelectAllColumns();

            query.LeftJoin("School", "School.LocalAuthorityId", "LocalAuthority.Id");

            query.Where("School.Local", true);

            return (await ExecuteQuery(query)).First();
        }
    }
}