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
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class IncidentRepository : BaseReadWriteRepository<Incident>, IIncidentRepository
    {
        public IncidentRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task<IEnumerable<Incident>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var query = GenerateQuery();

            query.Where("Student.Id", studentId);
            query.Where("AcademicYear.Id", academicYearId);

            return await ExecuteQuery(query);
        }

        public async Task<int> GetCountByStudent(Guid studentId, Guid academicYearId)
        {
            var query = GenerateQuery().AsCount();

            query.Where("Incident.StudentId", studentId);
            query.Where("Incident.AcademicYearId", academicYearId);

            return await ExecuteQueryIntResult(query) ?? 0;
        }

        public async Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId)
        {
            var query = new Query(TblName).AsSum("Incident.Points");

            query.Where("Incident.StudentId", studentId);
            query.Where("Incident.AcademicYearId", academicYearId);

            return await ExecuteQueryIntResult(query) ?? 0;
        }

        public async Task Update(Incident entity)
        {
            var incident = await Context.Incidents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (incident == null)
            {
                throw new EntityNotFoundException("Incident not found.");
            }

            incident.Comments = entity.Comments;
            incident.BehaviourTypeId = entity.BehaviourTypeId;
            incident.LocationId = entity.LocationId;
            incident.OutcomeId = entity.OutcomeId;
            incident.StatusId = entity.StatusId;
            incident.Comments = entity.Comments;
            incident.Points = entity.Points;
        }
    }
}