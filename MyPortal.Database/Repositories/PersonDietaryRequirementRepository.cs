using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class PersonDietaryRequirementRepository : BaseReadWriteRepository<PersonDietaryRequirement>,
        IPersonDietaryRequirementRepository
    {
        public PersonDietaryRequirementRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");
            query.LeftJoin("DietaryRequirements as DR", "DR.Id", $"{TblAlias}.DietaryRequirementId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "P");
            query.SelectAllColumns(typeof(DietaryRequirement), "DR");

            return query;
        }

        protected override async Task<IEnumerable<PersonDietaryRequirement>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var pdrs = await DbUser.Transaction.Connection
                .QueryAsync<PersonDietaryRequirement, Person, DietaryRequirement, PersonDietaryRequirement>(sql.Sql,
                    (pdr, person, dietaryReq) =>
                    {
                        pdr.Person = person;
                        pdr.DietaryRequirement = dietaryReq;

                        return pdr;
                    }, sql.NamedBindings, DbUser.Transaction);

            return pdrs;
        }
    }
}