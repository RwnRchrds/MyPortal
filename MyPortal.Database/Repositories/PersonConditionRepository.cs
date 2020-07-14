using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class PersonConditionRepository : BaseReadWriteRepository<PersonCondition>, IPersonConditionRepository
    {
        public PersonConditionRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Person));
            query.SelectAll(typeof(MedicalCondition));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Person", "Person.Id", "PersonCondition.PersonId");
            query.LeftJoin("MedicalCondition", "MedicalCondition.Id", "PersonCondition.ConditionId");
        }

        protected override async Task<IEnumerable<PersonCondition>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<PersonCondition, Person, MedicalCondition, PersonCondition>(sql.Sql,
                (pcondition, person, condition) =>
                {
                    pcondition.Person = person;
                    pcondition.MedicalCondition = condition;

                    return pcondition;
                }, sql.NamedBindings);
        }
    }
}