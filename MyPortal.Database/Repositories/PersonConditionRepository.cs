using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class PersonConditionRepository : BaseReadWriteRepository<PersonCondition>, IPersonConditionRepository
    {
        public PersonConditionRepository(DbUserWithContext dbUser) : base(dbUser)
        {
           
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");
            query.LeftJoin("MedicalConditions as MC", "MC.Id", $"{TblAlias}.ConditionId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "P");
            query.SelectAllColumns(typeof(MedicalCondition), "MC");

            return query;
        }

        protected override async Task<IEnumerable<PersonCondition>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var personConditions =
                await DbUser.Transaction.Connection.QueryAsync<PersonCondition, Person, MedicalCondition, PersonCondition>(
                    sql.Sql,
                    (pc, person, condition) =>
                    {
                        pc.Person = person;
                        pc.MedicalCondition = condition;

                        return pc;
                    }, sql.NamedBindings, DbUser.Transaction);

            return personConditions;
        }

        public async Task Update(PersonCondition entity)
        {
            var personCondition = await DbUser.Context.PersonConditions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (personCondition == null)
            {
                throw new EntityNotFoundException("Person condition not found.");
            }

            personCondition.MedicationTaken = entity.MedicationTaken;
            personCondition.Medication = entity.Medication;
        }
    }
}