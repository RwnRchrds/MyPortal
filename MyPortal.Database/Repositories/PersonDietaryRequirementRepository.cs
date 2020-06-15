using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class PersonDietaryRequirementRepository : BaseReadWriteRepository<PersonDietaryRequirement>, IPersonDietaryRequirementRepository
    {
        public PersonDietaryRequirementRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(Person))},
{EntityHelper.GetPropertyNames(typeof(DietaryRequirement))}";

            (query => JoinRelated(query)) = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[PersonDietaryRequirement].[PersonId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[DietaryRequirement]", "[DietaryRequirement].[Id]", "[PersonDietaryRequirement].[DietaryRequirementId]")}";
        }

        protected override async Task<IEnumerable<PersonDietaryRequirement>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync<PersonDietaryRequirement, Person, DietaryRequirement, PersonDietaryRequirement>(sql,
                    (pdr, person, req) =>
                    {
                        pdr.Person = person;
                        pdr.DietaryRequirement = req;

                        return pdr;
                    }, param);
        }
    }
}