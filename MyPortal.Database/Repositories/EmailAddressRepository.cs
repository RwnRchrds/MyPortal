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
    public class EmailAddressRepository : BaseReadWriteRepository<EmailAddress>, IEmailAddressRepository
    {
        public EmailAddressRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(EmailAddressType))},
{EntityHelper.GetAllColumns(typeof(Person))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[EmailAddressType]", "[EmailAddressType].[Id]", "[EmailAddress].[TypeId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[EmailAddress].[PersonId]")}";
        }

        protected override async Task<IEnumerable<EmailAddress>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<EmailAddress, EmailAddressType, Person, EmailAddress>(sql,
                (address, type, person) =>
                {
                    address.Type = type;
                    address.Person = person;

                    return address;
                });
        }

        public async Task Update(EmailAddress entity)
        {
            var emailAddressInDb = await Context.EmailAddresses.FindAsync(entity.Id);

            emailAddressInDb.Address = entity.Address;
            emailAddressInDb.Main = entity.Main;
            emailAddressInDb.Primary = entity.Primary;
            emailAddressInDb.TypeId = entity.TypeId;
            emailAddressInDb.Notes = entity.Notes;
        }
    }
}