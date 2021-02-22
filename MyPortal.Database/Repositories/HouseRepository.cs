using System.Collections.Generic;
using System.Data;
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
    public class HouseRepository : BaseReadWriteRepository<House>, IHouseRepository
    {
        public HouseRepository(ApplicationDbContext context) : base(context, "House")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StaffMember), "StaffMember");
            query.SelectAllColumns(typeof(Person), "Person");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("StaffMembers as StaffMember", "StaffMember.Id", "House.HeadId");
            query.LeftJoin("People as Person", "Person.Id", "StaffMember.PersonId");
        }

        protected override async Task<IEnumerable<House>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<House, StaffMember, House>(sql.Sql, (house, head) =>
            {
                house.HeadOfHouse = head;

                return house;
            }, sql.NamedBindings);
        }
    }
}