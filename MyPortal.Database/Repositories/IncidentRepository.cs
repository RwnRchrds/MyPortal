using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class IncidentRepository : BaseReadWriteRepository<Incident>, IIncidentRepository
    {
        public IncidentRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(AcademicYear))},
{EntityHelper.GetAllColumns(typeof(IncidentType))},
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Location))},
{EntityHelper.GetUserColumns("User")}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AcademicYear]", "[AcademicYear].[Id]", "[Incident].[AcademicYearId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[IncidentType]", "[IncidentType].[Id]", "[Incident].[BehaviourTypeId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[Incident].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Location]", "[Location].[Id]", "[Incident].[LocationId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[Incident].[RecordedById]")}";
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

        public async Task Update(Incident entity)
        {
            var incident = await Context.Incidents.FindAsync(entity.Id);

            incident.BehaviourTypeId = entity.BehaviourTypeId;
            incident.LocationId = entity.LocationId;
            incident.Comments = entity.Comments;
            incident.Points = entity.Points;
            incident.Resolved = entity.Resolved;
            incident.Deleted = entity.Deleted;
        }
    }
}