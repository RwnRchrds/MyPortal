using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class IncidentDetentionRepository : BaseReadWriteRepository<IncidentDetention>, IIncidentDetentionRepository
    {
        public IncidentDetentionRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Incident));
            query.SelectAll(typeof(IncidentType));
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person), "StudentPerson");
            query.SelectAll(typeof(Location));
            query.SelectAll(typeof(Detention));
            query.SelectAll(typeof(DiaryEvent));
            query.SelectAll(typeof(StaffMember));
            query.SelectAll(typeof(Person), "SupervisorPerson");
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Incident", "Incident.Id", "IncidentDetention.IncidentId");
            query.LeftJoin("IncidentType", "IncidentType.Id", "Incident.IncidentTypeId");
            query.LeftJoin("Student", "Student.Id", "Incident.StudentId");
            query.LeftJoin("Person as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("Location", "Location.Id", "Incident.LocationId");
            query.LeftJoin("Detention", "Detention.Id", "IncidentDetention.DetentionId");
            query.LeftJoin("DiaryEvent", "DiaryEvent.Id", "Detention.EventId");
            query.LeftJoin("StaffMember", "StaffMember.Id", "Detention.SupervisorId");
            query.LeftJoin("Person as SupervisorPerson", "SupervisorPerson.Id", "StaffMember.PersonId");
        }

        protected override async Task<IEnumerable<IncidentDetention>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync(sql.Sql,
                new[]
                {
                    typeof(IncidentDetention), typeof(Incident), typeof(IncidentType), typeof(Student), typeof(Person),
                    typeof(Location), typeof(Detention), typeof(DiaryEvent), typeof(StaffMember), typeof(Person)
                },
                objects =>
                {
                    var incidentDetention = (IncidentDetention) objects[0];

                    incidentDetention.Incident = (Incident) objects[1];
                    incidentDetention.Incident.Type = (IncidentType) objects[2];
                    incidentDetention.Incident.Student = (Student) objects[3];
                    incidentDetention.Incident.Student.Person = (Person) objects[4];
                    incidentDetention.Incident.Location = (Location) objects[5];
                    incidentDetention.Detention = (Detention) objects[6];
                    incidentDetention.Detention.Event = (DiaryEvent) objects[7];
                    incidentDetention.Detention.Supervisor = (StaffMember) objects[8];
                    incidentDetention.Detention.Supervisor.Person = (Person) objects[9];

                    return incidentDetention;
                }, sql.NamedBindings);
        }

        public async Task<IncidentDetention> Get(Guid detentionId, Guid studentId)
        {
            var query = GenerateQuery();

            query.Where("Detention.Id", detentionId);
            query.Where("Student.Id", studentId);

            return await ExecuteQueryFirstOrDefault(query);
        }
    }
}