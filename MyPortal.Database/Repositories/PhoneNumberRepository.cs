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
    public class PhoneNumberRepository : BaseReadWriteRepository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "PhoneNumber")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(PhoneNumberType), "PhoneNumberType");
            query.SelectAllColumns(typeof(Person), "Person");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("PhoneNumberTypes as PhoneNumberType", "PhoneNumberType.Id", "PhoneNumber.TypeId");
            query.LeftJoin("People as Person", "Person.Id", "PhoneNumber.PersonId");
        }

        protected override async Task<IEnumerable<PhoneNumber>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<PhoneNumber, PhoneNumberType, Person, PhoneNumber>(sql.Sql,
                (telNo, type, person) =>
                {
                    telNo.Type = type;
                    telNo.Person = person;

                    return telNo;
                }, sql.NamedBindings, Transaction);
        }

        public async Task Update(PhoneNumber entity)
        {
            var phoneNumber = await Context.PhoneNumbers.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (phoneNumber == null)
            {
                throw new EntityNotFoundException("Phone number not found.");
            }

            phoneNumber.TypeId = entity.TypeId;
            phoneNumber.Number = entity.Number;
            phoneNumber.Main = entity.Main;
        }
    }
}