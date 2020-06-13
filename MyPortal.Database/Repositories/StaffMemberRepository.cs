using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class StaffMemberRepository : BaseReadWriteRepository<StaffMember>, IStaffMemberRepository
    {
        public StaffMemberRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(Person))}";

            JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[StaffMember].[PersonId]")}";
        }

        protected override async Task<IEnumerable<StaffMember>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<StaffMember, Person, StaffMember>(sql, (staff, person) =>
            {
                staff.Person = person;

                return staff;
            }, param);
        }
    }
}