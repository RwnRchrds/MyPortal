using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class PersonDietaryRequirementRepository : BaseReadWriteRepository<PersonDietaryRequirement>, IPersonDietaryRequirementRepository
    {
        public PersonDietaryRequirementRepository(ApplicationDbContext context) : base(context, "PDR")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "P");
            query.SelectAllColumns(typeof(DietaryRequirement), "D");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("People as P", "P.Id", "PDR.PersonId");
            query.LeftJoin("DietaryRequirements as D", "D.Id", "PDR.DietaryRequirementId");
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
                    }, sql.NamedBindings);
        }
    }
}