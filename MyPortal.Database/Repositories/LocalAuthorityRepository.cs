using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class LocalAuthorityRepository : BaseReadRepository<LocalAuthority>, ILocalAuthorityRepository
    {
        public LocalAuthorityRepository(IDbConnection connection) : base(connection, "LA")
        {
        }

        public async Task<LocalAuthority> GetCurrent()
        {
            var query = GenerateQuery();

            query.LeftJoin("Schools as School", "School.LocalAuthorityId", "LA.Id");

            query.Where("School.Local", true);

            return (await ExecuteQuery(query)).First();
        }
    }
}