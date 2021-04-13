using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class PersonConditionRepository : BaseReadWriteRepository<PersonCondition>, IPersonConditionRepository
    {
        public PersonConditionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "PersonCondition")
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

            return await Transaction.Connection.QueryAsync<PersonCondition, Person, MedicalCondition, PersonCondition>(sql.Sql,
                (pcondition, person, condition) =>
                {
                    pcondition.Person = person;
                    pcondition.MedicalCondition = condition;

                    return pcondition;
                }, sql.NamedBindings, Transaction);
        }

        public async Task Update(PersonCondition entity)
        {
            var personCondition = await Context.PersonConditions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (personCondition == null)
            {
                throw new EntityNotFoundException("Person condition not found.");
            }

            personCondition.MedicationTaken = entity.MedicationTaken;
            personCondition.Medication = entity.Medication;
        }
    }
}