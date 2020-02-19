using System;
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
    public class ContactRepository : BaseReadWriteRepository<Contact>, IContactRepository
    {
        public ContactRepository(IDbConnection connection) : base(connection)
        {
       RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Person))}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[Contact].[PersonId]")}";
        }

        protected override async Task<IEnumerable<Contact>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Contact, Person, Contact>(sql, (contact, person) =>
            {
                contact.Person = person;

                return contact;
            }, param);
        }

        public async Task Update(Contact entity)
        {
            throw new NotImplementedException();
        }
    }
}