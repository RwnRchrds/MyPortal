﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class IncidentRepository : BaseReadWriteRepository<Incident>, IIncidentRepository
    {
        public IncidentRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("IncidentTypes as IT", "IT.Id", $"{TblAlias}.BehaviourTypeId");
            query.LeftJoin("Locations as L", "L.Id", $"{TblAlias}.LocationId");
            query.LeftJoin("AcademicYears as AY", "AY.Id", $"{TblAlias}.AcademicYearId");
            query.LeftJoin("Users as U", "U.Id", $"{TblAlias}.CreatedById");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(IncidentType), "IT");
            query.SelectAllColumns(typeof(Location), "L");
            query.SelectAllColumns(typeof(AcademicYear), "AY");
            query.SelectAllColumns(typeof(User), "U");

            return query;
        }

        protected override async Task<IEnumerable<Incident>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var incidents = await DbUser.Transaction.Connection.QueryAsync(sql.Sql,
                new[]
                {
                    typeof(Incident), typeof(IncidentType), typeof(Location), typeof(AcademicYear), typeof(User)
                },
                objects =>
                {
                    var incident = objects[0] as Incident;
                    var incidentType = objects[1] as IncidentType;
                    var location = objects[2] as Location;
                    var academicYear = objects[3] as AcademicYear;
                    var user = objects[4] as User;

                    if (incident != null)
                    {
                        incident.Type = incidentType;
                        incident.Location = location;
                        incident.AcademicYear = academicYear;
                        incident.CreatedBy = user;
                    }

                    return incident;
                }, sql.NamedBindings, DbUser.Transaction);

            return incidents;
        }

        // public async Task<IEnumerable<Incident>> GetByStudent(Guid studentId, Guid academicYearId)
        // {
        //     var query = GenerateQuery();
        //
        //     query.Where("Student.Id", studentId);
        //     query.Where("AcademicYear.Id", academicYearId);
        //
        //     return await ExecuteQuery(query);
        // }

        // public async Task<int> GetCountByStudent(Guid studentId, Guid academicYearId)
        // {
        //     var query = GenerateQuery().AsCount();
        //
        //     query.Where("Incident.StudentId", studentId);
        //     query.Where("Incident.AcademicYearId", academicYearId);
        //
        //     return await ExecuteQueryIntResult(query) ?? 0;
        // }

        // public async Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId)
        // {
        //     var query = new Query(TblName).AsSum("Incident.Points");
        //
        //     query.Where("Incident.StudentId", studentId);
        //     query.Where("Incident.AcademicYearId", academicYearId);
        //
        //     return await ExecuteQueryIntResult(query) ?? 0;
        // }

        public async Task Update(Incident entity)
        {
            var incident = await DbUser.Context.Incidents.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (incident == null)
            {
                throw new EntityNotFoundException("Incident not found.");
            }

            incident.Comments = entity.Comments;
            incident.BehaviourTypeId = entity.BehaviourTypeId;
            incident.LocationId = entity.LocationId;
        }
    }
}