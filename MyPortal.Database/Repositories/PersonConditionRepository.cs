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
    public class PersonConditionRepository : BaseReadWriteRepository<PersonCondition>, IPersonConditionRepository
    {
        public PersonConditionRepository(ApplicationDbContext context) : base(context, "PersonCondition")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "Person");
            query.SelectAllColumns(typeof(MedicalCondition), "MedicalCondition");

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