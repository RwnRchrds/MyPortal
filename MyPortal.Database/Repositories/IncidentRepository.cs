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
        public IncidentRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "Incident")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AcademicYear), "AcademicYear");
            query.SelectAllColumns(typeof(IncidentType), "IncidentType");
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(Person), "StudentPerson");
            query.SelectAllColumns(typeof(BehaviourOutcome), "BehaviourOutcome");
            query.SelectAllColumns(typeof(BehaviourStatus), "BehaviourStatus");
            query.SelectAllColumns(typeof(Location), "Location");
            query.SelectAllColumns(typeof(User), "User");
            query.SelectAllColumns(typeof(Person), "RecordedByPerson");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AcademicYears as AcademicYear", "AcademicYear.Id", "Incident.AcademicYearId");
            query.LeftJoin("IncidentTypes as IncidentType", "IncidentType.Id", "Incident.BehaviourTypeId");
            query.LeftJoin("Students as Student", "Student.Id", "Incident.StudentId");
            query.LeftJoin("People as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("BehaviourOutcomes as BehaviourOutcome", "BehaviourOutcome.Id", "Incident.OutcomeId");
            query.LeftJoin("BehaviourStatus", "BehaviourStatus.Id", "Incident.StatusId");
            query.LeftJoin("Locations as Location", "Location.Id", "Incident.LocationId");
            query.LeftJoin("Users as User", "User.Id", "Incident.RecordedById");
            query.LeftJoin("People as RecordedByPerson", "RecordedByPerson.UserId", "User.Id");
        }

        protected override async Task<IEnumerable<Incident>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection
                .QueryAsync(sql.Sql,
                    new[]
                    {
                        typeof(Incident), typeof(AcademicYear), typeof(IncidentType), typeof(Student), typeof(Person),
                        typeof(BehaviourOutcome), typeof(BehaviourStatus), typeof(Location), typeof(User),
                        typeof(Person)
                    },
                    objects =>
                    {
                        var incident = (Incident) objects[0];

                        incident.AcademicYear = (AcademicYear) objects[1];
                        incident.Type = (IncidentType) objects[2];
                        incident.Student = (Student) objects[3];
                        incident.Student.Person = (Person) objects[4];
                        incident.Outcome = (BehaviourOutcome) objects[5];
                        incident.Status = (BehaviourStatus) objects[6];
                        incident.Location = (Location) objects[7];
                        incident.RecordedBy = (User) objects[8];
                        incident.RecordedBy.Person = (Person) objects[9];

                        return incident;
                    }, sql.NamedBindings, Transaction);
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