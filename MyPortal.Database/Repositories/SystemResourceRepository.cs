using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SystemResourceRepository : BaseReadRepository<SystemResource>, ISystemResourceRepository
    {
        public SystemResourceRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(SystemArea))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[SystemArea]", "[SystemArea].[Id]", "[SystemResource].[AreaId]")}";
        }

        protected override async Task<IEnumerable<SystemResource>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<SystemResource, SystemArea, SystemResource>(sql, (resource, area) =>
            {
                resource.Area = area;

                return resource;
            }, param);
        }
    }
}