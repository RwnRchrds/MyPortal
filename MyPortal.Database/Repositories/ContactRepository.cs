using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ContactRepository : BaseReadWriteRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
     
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "P");

            return query;
        }

        protected override async Task<IEnumerable<Contact>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var contacts = await Transaction.Connection.QueryAsync<Contact, Person, Contact>(sql.Sql,
                (contact, person) =>
                {
                    contact.Person = person;

                    return contact;
                }, sql.NamedBindings, Transaction);

            return contacts;
        }

        public async Task Update(Contact entity)
        {
            var contact = await Context.Contacts.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (contact == null)
            {
                throw new EntityNotFoundException("Contact not found.");
            }

            contact.PlaceOfWork = entity.PlaceOfWork;
            contact.JobTitle = entity.JobTitle;
            contact.NiNumber = entity.NiNumber;
            contact.ParentalBallot = entity.ParentalBallot;
        }
    }
}