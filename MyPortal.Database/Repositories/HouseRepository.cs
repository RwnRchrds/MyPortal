using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class HouseRepository : BaseReadWriteRepository<House>, IHouseRepository
    {
        public HouseRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(StaffMember));
            query.SelectAll(typeof(Person));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.StaffMember", "StaffMember.Id", "House.HeadId");
            query.LeftJoin("dbo.Person", "Person.Id", "StaffMember.PersonId");
        }

        protected override async Task<IEnumerable<House>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<House, StaffMember, House>(sql.Sql, (house, head) =>
            {
                house.HeadOfHouse = head;

                return house;
            }, sql.Bindings);
        }
    }
}