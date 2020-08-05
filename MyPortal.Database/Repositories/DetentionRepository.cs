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
        public DetentionRepository(ApplicationDbContext context) : base(context, "Detention")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(DetentionType), "DetentionType");
            query.SelectAllColumns(typeof(DiaryEvent), "DiaryEvent");
            query.SelectAllColumns(typeof(StaffMember), "StaffMember");
            query.SelectAllColumns(typeof(Person), "Person");
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("DetentionTypes as DetentionType", "DetentionType.Id", "Detention.DetentionTypeId");
            query.LeftJoin("DiaryEvents as DiaryEvent", "DiaryEvent.Id", "Detention.EventId");
            query.LeftJoin("StaffMembers as StaffMember", "StaffMember.Id", "Detention.SupervisorId");
            query.LeftJoin("People as Person", "Person.Id", "StaffMember.PersonId");
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
            var query = GenerateQuery();

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
            var query = GenerateQuery();

            query.LeftJoin("IncidentDetention", "IncidentDetention.DetentionId", "Detention.Id");
            query.LeftJoin("Incident", "Incident.Id", "IncidentDetention.IncidentId");

            query.Where("Incident.StudentId", studentId);

            query.Where("Incident.AcademicYearId", academicYearId);

            return await ExecuteQuery(query);
        }

        public async Task<Detention> GetByIncident(Guid incidentId)
        {
            var query = GenerateQuery();
            
            query.LeftJoin("IncidentDetention", "IncidentDetention.DetentionId", "Detention.Id");
            query.LeftJoin("Incident", "Incident.Id", "IncidentDetention.IncidentId");

            query.Where("Incident.Id", incidentId);

            return await ExecuteQueryFirstOrDefault(query);
        }

        public async Task<IEnumerable<Detention>> GetAll(DetentionSearchOptions searchOptions)
        {
            var query = GenerateQuery();

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
