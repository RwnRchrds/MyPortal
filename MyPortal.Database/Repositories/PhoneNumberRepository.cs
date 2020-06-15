using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class PhoneNumberRepository : BaseReadWriteRepository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(PhoneNumberType))},
{EntityHelper.GetPropertyNames(typeof(Person))}";

            (query => JoinRelated(query)) = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[PhoneNumberType]", "[PhoneNumberType].[Id]", "[PhoneNumber].[TypeId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[PhoneNumber].[PersonId]")}";
        }

        protected override async Task<IEnumerable<PhoneNumber>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<PhoneNumber, PhoneNumberType, Person, PhoneNumber>(sql,
                (telNo, type, person) =>
                {
                    telNo.Type = type;
                    telNo.Person = person;

                    return telNo;
                });
        }
    }
}