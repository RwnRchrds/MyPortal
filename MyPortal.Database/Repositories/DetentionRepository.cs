using System;
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
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DetentionRepository : BaseReadWriteRepository<Detention>, IDetentionRepository
    {
        public DetentionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("DetentionTypes as DT", "DT.Id", $"{TblAlias}.DetentionTypeId");
            query.LeftJoin("DiaryEvent as DE", "DE.Id", $"{TblAlias}.EventId");
            query.LeftJoin("StaffMembers as S", "S.Id", $"{TblAlias}.SupervisorId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(DetentionType), "DT");
            query.SelectAllColumns(typeof(DiaryEvent), "DE");
            query.SelectAllColumns(typeof(StaffMember), "S");

            return query;
        }

        protected override async Task<IEnumerable<Detention>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var detentions =
                await Transaction.Connection.QueryAsync<Detention, DetentionType, DiaryEvent, StaffMember, Detention>(
                    sql.Sql,
                    (detention, type, diaryEvent, supervisor) =>
                    {
                        detention.Type = type;
                        detention.Event = diaryEvent;
                        detention.Supervisor = supervisor;

                        return detention;
                    }, sql.NamedBindings, Transaction);

            return detentions;
        }

        public async Task<IEnumerable<Detention>> GetByStudent(Guid studentId, DateTime dateFrom, DateTime dateTo)
        {
            var query = GenerateQuery();
            
            query.LeftJoin("IncidentDetention", "IncidentDetention.DetentionId", $"{TblAlias}.Id");
            query.LeftJoin("Incident", "Incident.Id", "IncidentDetention.IncidentId");

            query.Where("Incident.StudentId", studentId);

            query.Where(q =>
                q.WhereDate("DE.StartTime", ">=", dateFrom)
                    .WhereDate("DE.EndTime", "<=", dateTo));

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Detention>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var query = GenerateQuery();

            query.LeftJoin("IncidentDetention", "IncidentDetention.DetentionId", $"{TblAlias}.Id");
            query.LeftJoin("Incident", "Incident.Id", "IncidentDetention.IncidentId");

            query.Where("Incident.StudentId", studentId);

            query.Where("Incident.AcademicYearId", academicYearId);

            return await ExecuteQuery(query);
        }

        public async Task<Detention> GetByIncident(Guid incidentId)
        {
            var query = GenerateQuery();
            
            query.LeftJoin("IncidentDetention", "IncidentDetention.DetentionId", $"{TblAlias}.Id");
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
