﻿using System.Collections.Generic;
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
    public class ObservationRepository : BaseReadWriteRepository<Observation>, IObservationRepository
    {
        public ObservationRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("StaffMembers as OE", "OE.Id", $"{TblAlias}.ObserveeId");
            query.LeftJoin("StaffMembers as OR", "OR.Id", $"{TblAlias}.ObserverId");
            query.LeftJoin("ObservationOutcomes as OO", "OO.Id", $"{TblAlias}.OutcomeId");

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
                await DbUser.Transaction.Connection
                    .QueryAsync<Observation, StaffMember, StaffMember, ObservationOutcome, Observation>(sql.Sql,
                        (observation, observee, observer, outcome) =>
                        {
                            observation.Observee = observee;
                            observation.Observer = observer;
                            observation.Outcome = outcome;

                            return observation;
                        }, sql.NamedBindings, DbUser.Transaction);

            return observations;
        }

        public async Task Update(Observation entity)
        {
            var observation = await DbUser.Context.Observations.FirstOrDefaultAsync(x => x.Id == entity.Id);

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