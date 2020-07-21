using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Search;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DetentionRepository : BaseReadWriteRepository<Detention>, IDetentionRepository
    {
        public DetentionRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(DetentionType));
            query.SelectAll(typeof(DiaryEvent));
            query.SelectAll(typeof(StaffMember));
            query.SelectAll(typeof(Person));
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("DetentionType", "DetentionType.Id", "Detention.DetentionTypeId");
            query.LeftJoin("DiaryEvent", "DiaryEvent.Id", "Detention.EventId");
            query.LeftJoin("StaffMember", "StaffMember.Id", "Detention.SupervisorId");
            query.LeftJoin("Person", "Person.Id", "StaffMember.PersonId");
        }

        protected override async Task<IEnumerable<Detention>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Detention, DetentionType, DiaryEvent, StaffMember, Person, Detention>(sql.Sql,
                (detention, type, diaryEvent, supervisor, person) =>
                {
                    detention.Type = type;
                    detention.Event = diaryEvent;
                    detention.Supervisor = supervisor;

                    detention.Supervisor.Person = person;

                    return detention;
                }, sql.NamedBindings);
        }

        public async Task<IEnumerable<Detention>> GetByStudent(Guid studentId, Tuple<DateTime, DateTime> dateRange)
        {
            var query = SelectAllColumns();

            query.LeftJoin("IncidentDetention", "IncidentDetention.DetentionId", "Detention.Id");
            query.LeftJoin("Incident", "Incident.Id", "IncidentDetention.IncidentId");

            query.Where("Incident.StudentId", studentId);

            if (dateRange != null)
            {
                query.Where(q =>
                    q.WhereDate("DiaryEvent.StartTime", ">=", dateRange.Item1)
                        .WhereDate("DiaryEvent.EndTime", "<=", dateRange.Item2));
            }

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Detention>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var query = SelectAllColumns();

            query.LeftJoin("IncidentDetention", "IncidentDetention.DetentionId", "Detention.Id");
            query.LeftJoin("Incident", "Incident.Id", "IncidentDetention.IncidentId");

            query.Where("Incident.StudentId", studentId);

            query.Where("Incident.AcademicYearId", academicYearId);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Detention>> GetAll(DetentionSearchOptions searchOptions)
        {
            var query = SelectAllColumns();

            if (searchOptions.DetentionType != null && searchOptions.DetentionType != Guid.Empty)
            {
                query.Where("Detention.DetentionTypeId", searchOptions.DetentionType);
            }

            if (searchOptions.FirstDate != null && searchOptions.LastDate != null)
            {
                query.Where("DiaryEvent.StartTime", ">=", searchOptions.FirstDate.Value.Date);
                query.Where("DiaryEvent.EndTime", "<", searchOptions.LastDate.Value.Date.AddDays(1));
            }

            return await ExecuteQuery(query);
        }
    }
}
