using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class ObservationRepository : BaseReadWriteRepository<Observation>, IObservationRepository
    {
        public ObservationRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(StaffMember), "Observee")},
{EntityHelper.GetPropertyNames(typeof(Person), "ObserveePerson")},
{EntityHelper.GetPropertyNames(typeof(StaffMember), "Observer")},
{EntityHelper.GetPropertyNames(typeof(Person), "ObserverPerson")},
{EntityHelper.GetPropertyNames(typeof(ObservationOutcome))}";

            JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[Observee].[Id]", "[Observation].[ObserveeId]", "Observee")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[ObserveePerson].[Id]", "[Observee].[PersonId]", "ObserveePerson")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[Observer].[Id]", "[Observation].[ObserverId]", "Observer")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[ObserverPerson].[Id]", "[Observer].[PersonId]", "ObserverPerson")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[ObservationOutcome]", "[ObservationOutcome].[Id]", "[Observation].[OutcomeId]")}";
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
    }
}