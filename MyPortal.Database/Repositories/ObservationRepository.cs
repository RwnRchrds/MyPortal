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
    public class ObservationRepository : BaseReadWriteRepository<Observation>, IObservationRepository
    {
        public ObservationRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "StaffMembers", "OE", "ObserveeId");
            JoinEntity(query, "StaffMembers", "OR", "ObserverId");
            JoinEntity(query, "ObservationOutcomes", "OO", "OutcomeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StaffMember), "OE");
            query.SelectAllColumns(typeof(StaffMember), "OR");
            query.SelectAllColumns(typeof(ObservationOutcome), "OO");

            return query;
        }

        protected override async Task<IEnumerable<Observation>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var observations =
                await Transaction.Connection
                    .QueryAsync<Observation, StaffMember, StaffMember, ObservationOutcome, Observation>(sql.Sql,
                        (observation, observee, observer, outcome) =>
                        {
                            observation.Observee = observee;
                            observation.Observer = observer;
                            observation.Outcome = outcome;

                            return observation;
                        }, sql.NamedBindings, Transaction);

            return observations;
        }

        public async Task Update(Observation entity)
        {
            var observation = await Context.Observations.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (observation == null)
            {
                throw new EntityNotFoundException("Observation not found.");
            }

            observation.ObserverId = entity.ObserverId;
            observation.OutcomeId = entity.OutcomeId;
            observation.Notes = entity.Notes;
        }
    }
}