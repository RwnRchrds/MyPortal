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
    public class PersonDietaryRequirementRepository : BaseReadWriteRepository<PersonDietaryRequirement>, IPersonDietaryRequirementRepository
    {
        public PersonDietaryRequirementRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Person));
            query.SelectAll(typeof(DietaryRequirement));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Person", "Person.Id", "PersonDietaryRequirement.PersonId");
            query.LeftJoin("dbo.DietaryRequirement", "DietaryRequirement.Id",
                "PersonDietaryRequirement.DietaryRequirementId");
        }

        protected override async Task<IEnumerable<PersonDietaryRequirement>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<PersonDietaryRequirement, Person, DietaryRequirement, PersonDietaryRequirement>(sql.Sql,
                    (pdr, person, req) =>
                    {
                        pdr.Person = person;
                        pdr.DietaryRequirement = req;

                        return pdr;
                    }, sql.Bindings);
        }
    }
}