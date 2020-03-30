using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventAttendeeRepository : BaseReadWriteRepository<DiaryEventAttendee>, IDiaryEventAttendeeRepository
    {
        public DiaryEventAttendeeRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context, "Attendee")
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(DiaryEvent), "Event")},
{EntityHelper.GetAllColumns(typeof(Person))},
{EntityHelper.GetAllColumns(typeof(DiaryEventAttendeeResponse), "Response")}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[DiaryEvent]", "[Event].[Id]", "[Attendee].[EventId]", "Event")},
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[Attendee].[PersonId]")},
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[DiaryEventAttendeeResponse]", "[Response].[Id]", "[Attendee].[ResponseId]", "Response")}";
        }

        protected override async Task<IEnumerable<DiaryEventAttendee>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync<DiaryEventAttendee, DiaryEvent, Person, DiaryEventAttendeeResponse, DiaryEventAttendee>(sql,
                    (attendee, diaryEvent, person, response) =>
                    {
                        attendee.Event = diaryEvent;
                        attendee.Person = person;
                        attendee.Response = response;

                        return attendee;
                    }, param);
        }
    }
}