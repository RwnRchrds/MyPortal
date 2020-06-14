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

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(StaffMember));
            query.SelectAll(typeof(Person));

            query = JoinRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("dbo.StaffMember", "StaffMember.Id", "House.HeadId");
            query.LeftJoin("dbo.Person", "Person.Id", "StaffMember.PersonId");

            return query;
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