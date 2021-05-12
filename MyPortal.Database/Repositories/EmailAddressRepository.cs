using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class EmailAddressRepository : BaseReadWriteRepository<EmailAddress>, IEmailAddressRepository
    {
        public EmailAddressRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Agencies", "A", "AgencyId");
            JoinEntity(query, "People", "P", "PersonId");
            JoinEntity(query, "EmailAddressTypes", "EAT", "TypeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Agency), "A");
            query.SelectAllColumns(typeof(Person), "P");
            query.SelectAllColumns(typeof(EmailAddressType), "EAT");

            return query;
        }

        protected override async Task<IEnumerable<EmailAddress>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var emailAddresses =
                await Transaction.Connection.QueryAsync<EmailAddress, Agency, Person, EmailAddressType, EmailAddress>(
                    sql.Sql,
                    (email, agency, person, type) =>
                    {
                        email.Agency = agency;
                        email.Person = person;
                        email.Type = type;

                        return email;
                    }, sql.NamedBindings, Transaction);

            return emailAddresses;
        }

        public async Task Update(EmailAddress entity)
        {
            var emailAddress = await Context.EmailAddresses.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (emailAddress == null)
            {
                throw new EntityNotFoundException("Email address not found.");
            }

            emailAddress.Address = entity.Address;
            emailAddress.Main = entity.Main;
            emailAddress.Notes = entity.Notes;
            emailAddress.TypeId = entity.TypeId;
        }
    }
}