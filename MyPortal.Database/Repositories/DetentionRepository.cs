using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DetentionRepository : BaseReadWriteRepository<Detention>, IDetentionRepository
    {
        public DetentionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "Detention")
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

            return await Transaction.Connection.QueryAsync<Detention, DetentionType, DiaryEvent, StaffMember, Person, Detention>(sql.Sql,
                (detention, type, diaryEvent, supervisor, person) =>
                {
                    detention.Type = type;
                    detention.Event = diaryEvent;
                    detention.Supervisor = supervisor;

                    detention.Supervisor.Person = person;

                    return detention;
                }, sql.NamedBindings, Transaction);
        }

        public async Task<IEnumerable<Detention>> GetByStudent(Guid studentId, DateTime dateFrom, DateTime dateTo)
        {
            var query = GenerateQuery();

            query.LeftJoin("IncidentDetention", "IncidentDetention.DetentionId", "Detention.Id");
            query.LeftJoin("Incident", "Incident.Id", "IncidentDetention.IncidentId");

            query.Where("Incident.StudentId", studentId);

            query.Where(q =>
                q.WhereDate("DiaryEvent.StartTime", ">=", dateFrom)
                    .WhereDate("DiaryEvent.EndTime", "<=", dateTo));

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


        public async Task Update(Detention entity)
        {
            var detention = await Context.Detentions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (detention == null)
            {
                throw new EntityNotFoundException("Detention not found.");
            }

            detention.SupervisorId = entity.SupervisorId;
            detention.DetentionTypeId = entity.DetentionTypeId;
        }
    }
}
