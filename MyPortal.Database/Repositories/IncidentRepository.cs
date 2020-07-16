using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class IncidentRepository : BaseReadWriteRepository<Incident>, IIncidentRepository
    {
        public IncidentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(AcademicYear));
            query.SelectAll(typeof(IncidentType));
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person), "StudentPerson");
            query.SelectAll(typeof(BehaviourOutcome));
            query.SelectAll(typeof(BehaviourStatus));
            query.SelectAll(typeof(Location));
            query.SelectAll(typeof(ApplicationUser), "User");
            query.SelectAll(typeof(Person), "RecordedByPerson");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AcademicYear", "AcademicYear.Id", "Incident.AcademicYearId");
            query.LeftJoin("IncidentType", "IncidentType.Id", "Incident.BehaviourTypeId");
            query.LeftJoin("Student", "Student.Id", "Incident.StudentId");
            query.LeftJoin("Person as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("BehaviourOutcome", "BehaviourOutcome.Id", "Incident.OutcomeId");
            query.LeftJoin("BehaviourStatus", "BehaviourStatus.Id", "Incident.StatusId");
            query.LeftJoin("Location", "Location.Id", "Incident.LocationId");
            query.LeftJoin("AspNetUsers as User", "User.Id", "Incident.RecordedById");
            query.LeftJoin("Person as RecordedByPerson", "RecordedByPerson.UserId", "User.Id");
        }

        protected override async Task<IEnumerable<Incident>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync(sql.Sql,
                    new[]
                    {
                        typeof(Incident), typeof(AcademicYear), typeof(IncidentType), typeof(Student), typeof(Person),
                        typeof(BehaviourOutcome), typeof(BehaviourStatus), typeof(Location), typeof(ApplicationUser),
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
                        incident.RecordedBy = (ApplicationUser) objects[8];
                        incident.RecordedBy.Person = (Person) objects[9];

                        return incident;
                    }, sql.NamedBindings);
        }

        public async Task<IEnumerable<Incident>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var query = SelectAllColumns();

            query.Where("Student.Id", studentId);
            query.Where("AcademicYear.Id", academicYearId);

            return await ExecuteQuery(query);
        }

        public async Task<int> GetCountByStudent(Guid studentId, Guid academicYearId)
        {
            var query = SelectAllColumns().AsCount();

            query.Where("Student.Id", studentId);
            query.Where("AcademicYear.Id", academicYearId);

            return await ExecuteQueryIntResult(query) ?? 0;
        }

        public async Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId)
        {
            var query = SelectAllColumns().AsSum("Incident.Points");

            query.Where("Student.Id", studentId);
            query.Where("AcademicYear.Id", academicYearId);

            return await ExecuteQueryIntResult(query) ?? 0;
        }
    }
}