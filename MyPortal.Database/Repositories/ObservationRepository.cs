using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Constants;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ObservationRepository : BaseReadWriteRepository<Observation>, IObservationRepository
    {
        public ObservationRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(StaffMember), "Observee");
            query.SelectAll(typeof(Person), "ObserveePerson");
            query.SelectAll(typeof(StaffMember), "Observer");
            query.SelectAll(typeof(Person), "ObserverPerson");
            query.SelectAll(typeof(ObservationOutcome));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.StaffMember as Observee", "Observee.Id", "Observation.ObserveeId");
            query.LeftJoin("dbo.Person as ObserveePerson", "ObserveePerson.Id", "Observee.PersonId");
            query.LeftJoin("dbo.StaffMember as Observer", "Observer.Id", "Observation.ObserverId");
            query.LeftJoin("dbo.Person as ObserverPerson", "ObserverPerson.Id", "Observer.PersonId");
            query.LeftJoin("dbo.ObservationOutcome", "ObservationOutcome.Id", "Observation.OutcomeId");
        }

        protected override async Task<IEnumerable<Observation>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Observation, StaffMember, Person, StaffMember, Person, ObservationOutcome, Observation>(sql.Sql,
                (observation, observee, pObservee, observer, pObserver, outcome) =>
                {
                    observation.Observee = observee;
                    observation.Observee.Person = pObservee;

                    observation.Observer = observer;
                    observation.Observer.Person = pObserver;

                    observation.Outcome = outcome;

                    return observation;
                }, sql.NamedBindings);
        }
    }
}