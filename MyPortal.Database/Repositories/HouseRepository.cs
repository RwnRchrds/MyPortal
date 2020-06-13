using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class HouseRepository : BaseReadWriteRepository<House>, IHouseRepository
    {
        public HouseRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(StaffMember))}";

            JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[StaffMember].[Id]", "[House].[HeadId]")}";
        }

        protected override async Task<IEnumerable<House>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<House, StaffMember, House>(sql, (house, head) =>
            {
                house.HeadOfHouse = head;

                return house;
            });
        }
    }
}