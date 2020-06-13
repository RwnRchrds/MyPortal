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
    public class EmailAddressRepository : BaseReadWriteRepository<EmailAddress>, IEmailAddressRepository
    {
        public EmailAddressRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(EmailAddressType));
            query.SelectAll(typeof(Person));

            query = JoinRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("dbo.EmailAddressType", "EmailAddressType.Id", "EmailAddress.TypeId");
            query.LeftJoin("dbo.Person", "Person.Id", "EmailAddress.PersonId");

            return query;
        }

        protected override async Task<IEnumerable<EmailAddress>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<EmailAddress, EmailAddressType, Person, EmailAddress>(sql.Sql,
                (address, type, person) =>
                {
                    address.Type = type;
                    address.Person = person;

                    return address;
                }, sql.Bindings);
        }
    }
}