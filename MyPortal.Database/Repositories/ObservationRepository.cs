using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Constants;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ObservationRepository : BaseReadWriteRepository<Observation>, IObservationRepository
    {
        public ObservationRepository(ApplicationDbContext context) : base(context, "Observation")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StaffMember), "Observee");
            query.SelectAllColumns(typeof(Person), "ObserveePerson");
            query.SelectAllColumns(typeof(StaffMember), "Observer");
            query.SelectAllColumns(typeof(Person), "ObserverPerson");
            query.SelectAllColumns(typeof(ObservationOutcome), "ObservationOutcome");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("StaffMembers as Observee", "Observee.Id", "Observation.ObserveeId");
            query.LeftJoin("People as ObserveePerson", "ObserveePerson.Id", "Observee.PersonId");
            query.LeftJoin("StaffMembers as Observer", "Observer.Id", "Observation.ObserverId");
            query.LeftJoin("People as ObserverPerson", "ObserverPerson.Id", "Observer.PersonId");
            query.LeftJoin("ObservationOutcomes as ObservationOutcome", "ObservationOutcome.Id", "Observation.OutcomeId");
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