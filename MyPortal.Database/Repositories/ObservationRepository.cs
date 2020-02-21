﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ObservationRepository : BaseReadWriteRepository<Observation>, IObservationRepository
    {
        public ObservationRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(StaffMember), "Observee")},
{EntityHelper.GetAllColumns(typeof(Person), "ObserveePerson")},
{EntityHelper.GetAllColumns(typeof(StaffMember), "Observer")},
{EntityHelper.GetAllColumns(typeof(Person), "ObserverPerson")},
{EntityHelper.GetAllColumns(typeof(ObservationOutcome))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[Observee].[Id]", "[Observation].[ObserveeId]", "Observee")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[ObserveePerson].[Id]", "[Observee].[PersonId]", "ObserveePerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[Observer].[Id]", "[Observation].[ObserverId]", "Observer")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[ObserverPerson].[Id]", "[Observer].[PersonId]", "ObserverPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[ObservationOutcome]", "[ObservationOutcome].[Id]", "[Observation].[OutcomeId]")}";
        }

        protected override async Task<IEnumerable<Observation>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Observation, StaffMember, Person, StaffMember, Person, ObservationOutcome, Observation>(sql,
                (observation, observee, pObservee, observer, pObserver, outcome) =>
                {
                    observation.Observee = observee;
                    observation.Observee.Person = pObservee;

                    observation.Observer = observer;
                    observation.Observer.Person = pObserver;

                    observation.Outcome = outcome;

                    return observation;
                }, param);
        }

        public async Task Update(Observation entity)
        {
            var observation = await Context.Observations.FindAsync(entity.Id);

            observation.ObserverId = entity.ObserverId;
            observation.OutcomeId = entity.OutcomeId;
        }
    }
}