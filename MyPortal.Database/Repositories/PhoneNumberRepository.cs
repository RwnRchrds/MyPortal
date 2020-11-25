using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class PhoneNumberRepository : BaseReadWriteRepository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(ApplicationDbContext context) : base(context, "PhoneNumber")
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

            return await Connection.QueryAsync<PhoneNumber, PhoneNumberType, Person, PhoneNumber>(sql.Sql,
                (telNo, type, person) =>
                {
                    telNo.Type = type;
                    telNo.Person = person;

                    return telNo;
                }, sql.NamedBindings);
        }
    }
}