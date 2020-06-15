using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Repositories
{
    public class IncidentRepository : BaseReadWriteRepository<Incident>, IIncidentRepository
    {
        public IncidentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(AcademicYear))},
{EntityHelper.GetPropertyNames(typeof(IncidentType))},
{EntityHelper.GetPropertyNames(typeof(Student))},
{EntityHelper.GetPropertyNames(typeof(Location))},
{EntityHelper.GetUserProperties("User")}";

            (query => JoinRelated(query)) = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[AcademicYear]", "[AcademicYear].[Id]", "[Incident].[AcademicYearId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[IncidentType]", "[IncidentType].[Id]", "[Incident].[BehaviourTypeId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[Incident].[StudentId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Location]", "[Location].[Id]", "[Incident].[LocationId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[Incident].[RecordedById]")}";
        }

        protected override async Task<IEnumerable<Incident>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync<Incident, AcademicYear, IncidentType, Student, Location, ApplicationUser, Incident>(sql,
                    (incident, year, type, student, location, user) =>
                    {
                        incident.AcademicYear = year;
                        incident.Type = type;
                        incident.Student = student;
                        incident.Location = location;
                        incident.RecordedBy = user;

                        return incident;
                    }, param);
        }
    }
}