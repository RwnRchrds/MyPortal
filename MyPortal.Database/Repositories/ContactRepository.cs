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
        public ContactRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "Contact")
        {
     
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "Person");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("People as Person", "Person.Id", "Contact.PersonId");
        }

        protected override async Task<IEnumerable<Contact>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<Contact, Person, Contact>(sql.Sql, (contact, person) =>
            {
                contact.Person = person;

                return contact;
            }, sql.NamedBindings, Transaction);
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