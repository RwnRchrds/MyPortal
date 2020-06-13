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
    public class ContactRepository : BaseReadWriteRepository<Contact>, IContactRepository
    {
        public ContactRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
     
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Person));

            query = JoinRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Person", "Person.Id", "Contact.PersonId");

            return query;
        }

        protected override async Task<IEnumerable<Contact>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Contact, Person, Contact>(sql.Sql, (contact, person) =>
            {
                contact.Person = person;

                return contact;
            }, sql.Bindings);
        }
    }
}