using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class PersonConditionRepository : BaseReadWriteRepository<PersonCondition>, IPersonConditionRepository
    {
        public PersonConditionRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(Person))},
{EntityHelper.GetPropertyNames(typeof(MedicalCondition))}";

            JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[PersonAttachment].[PersonId]")},
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[MedicalCondition]", "[MedicalCondition].[Id]", "[PersonAttachment].[ConditionId]")}";
        }

        protected override async Task<IEnumerable<PersonCondition>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<PersonCondition, Person, MedicalCondition, PersonCondition>(sql,
                (pcondition, person, condition) =>
                {
                    pcondition.Person = person;
                    pcondition.MedicalCondition = condition;

                    return pcondition;
                }, param);
        }
    }
}