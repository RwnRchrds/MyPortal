using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class EmailAddressRepository : BaseReadWriteRepository<EmailAddress>, IEmailAddressRepository
    {
        public EmailAddressRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Agencies as A", "A.Id", $"{TblAlias}.AgencyId");
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");
            query.LeftJoin("EmailAddressTypes as EAT", "EAT.Id", $"{TblAlias}.TypeId");

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
                await DbUser.Transaction.Connection
                    .QueryAsync<EmailAddress, Agency, Person, EmailAddressType, EmailAddress>(
                        sql.Sql,
                        (email, agency, person, type) =>
                        {
                            email.Agency = agency;
                            email.Person = person;
                            email.Type = type;

                            return email;
                        }, sql.NamedBindings, DbUser.Transaction);

            return emailAddresses;
        }

        public async Task Update(EmailAddress entity)
        {
            var emailAddress = await DbUser.Context.EmailAddresses.FirstOrDefaultAsync(x => x.Id == entity.Id);

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