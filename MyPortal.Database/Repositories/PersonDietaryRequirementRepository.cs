using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class PersonDietaryRequirementRepository : BaseReadWriteRepository<PersonDietaryRequirement>, IPersonDietaryRequirementRepository
    {
        public PersonDietaryRequirementRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Person))},
{EntityHelper.GetAllColumns(typeof(DietaryRequirement))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[PersonDietaryRequirement].[PersonId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[DietaryRequirement]", "[DietaryRequirement].[Id]", "[PersonDietaryRequirement].[DietaryRequirementId]")}";
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

        public async Task Update(PersonDietaryRequirement entity)
        {
            var pdr = await Context.PersonDietaryRequirements.FindAsync(entity.Id);

            pdr.PersonId = entity.PersonId;
            pdr.DietaryRequirementId = entity.DietaryRequirementId;
        }
    }
}