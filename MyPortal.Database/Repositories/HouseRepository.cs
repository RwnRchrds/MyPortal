using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class HouseRepository : BaseReadWriteRepository<House>, IHouseRepository
    {
        public HouseRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(StaffMember))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[StaffMember].[Id]", "[House].[HeadId]")}";
        }

        protected override async Task<IEnumerable<House>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<House, StaffMember, House>(sql, (house, head) =>
            {
                house.HeadOfHouse = head;

                return house;
            });
        }

        public async Task Update(House entity)
        {
            var house = await Context.Houses.FindAsync(entity.Id);

            house.Name = entity.Name;
            house.ColourCode = entity.ColourCode;
            house.HeadId = entity.HeadId;
        }
    }
}