using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
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
            query.SelectAll(typeof(Location));
            query.SelectAll(typeof(ApplicationUser));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.AcademicYear", "AcademicYear.Id", "Incident.AcademicYearId");
            query.LeftJoin("dbo.IncidentType", "IncidentType.Id", "Incident.BehaviourTypeId");
            query.LeftJoin("dbo.Student", "Student.Id", "Incident.StudentId");
            query.LeftJoin("dbo.Location", "Location.Id", "Incident.LocationId");
            query.LeftJoin("dbo.AspNetUsers as User", "User.Id", "Incident.RecordedById");
        }

        protected override async Task<IEnumerable<Incident>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<Incident, AcademicYear, IncidentType, Student, Location, ApplicationUser, Incident>(sql.Sql,
                    (incident, year, type, student, location, user) =>
                    {
                        incident.AcademicYear = year;
                        incident.Type = type;
                        incident.Student = student;
                        incident.Location = location;
                        incident.RecordedBy = user;

                        return incident;
                    }, sql.NamedBindings);
        }
    }
}