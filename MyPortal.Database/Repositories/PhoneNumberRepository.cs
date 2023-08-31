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
    public class PhoneNumberRepository : BaseReadWriteRepository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("PhoneNumberTypes as PNT", "PNT.Id", $"{TblAlias}.TypeId");
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");
            query.LeftJoin("Agencies as A", "A.Id", $"{TblAlias}.AgencyId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(PhoneNumberType), "PNT");
            query.SelectAllColumns(typeof(Person), "P");
            query.SelectAllColumns(typeof(Agency), "A");

            return query;
        }

        protected override async Task<IEnumerable<PhoneNumber>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var phoneNumbers =
                await DbUser.Transaction.Connection
                    .QueryAsync<PhoneNumber, PhoneNumberType, Person, Agency, PhoneNumber>(
                        sql.Sql,
                        (number, type, person, agency) =>
                        {
                            number.Type = type;
                            number.Person = person;
                            number.Agency = agency;

                            return number;
                        }, sql.NamedBindings, DbUser.Transaction);

            return phoneNumbers;
        }

        public async Task Update(PhoneNumber entity)
        {
            var phoneNumber = await DbUser.Context.PhoneNumbers.FirstOrDefaultAsync(x => x.Id == entity.Id);

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