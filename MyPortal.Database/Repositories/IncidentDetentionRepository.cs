using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class IncidentDetentionRepository : BaseReadWriteRepository<IncidentDetention>, IIncidentDetentionRepository
    {
        public IncidentDetentionRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Incident))},
{EntityHelper.GetAllColumns(typeof(IncidentType))},
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Person), "StudentPerson")},
{EntityHelper.GetAllColumns(typeof(Location))},
{EntityHelper.GetAllColumns(typeof(Detention))},
{EntityHelper.GetAllColumns(typeof(DiaryEvent))},
{EntityHelper.GetAllColumns(typeof(StaffMember))},
{EntityHelper.GetAllColumns(typeof(Person), "SupervisorPerson")}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Incident]", "[Incident].[Id]", "[IncidentDetention].[IncidentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[IncidentType]", "[IncidentType].[Id]", "[Incident].[IncidentTypeId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[Incident].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]", "StudentPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Location]", "[Location].[Id]", "[Incident].[LocationId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Detention]", "[Detention].[Id]", "[IncidentDetention].[DetentionId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[DiaryEvent]", "[DiaryEvent].[Id]", "[Detention].[EventId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[StaffMember].[Id]", "[Detention].[SupervisorId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[SupervisorPerson].[Id]", "[StaffMember].[PersonId]", "SupervisorPerson")}";
        }

        protected override async Task<IEnumerable<IncidentDetention>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync(sql,
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
                }, param);
        }

        public async Task Update(IncidentDetention entity)
        {
            var iDet = await Context.IncidentDetentions.FindAsync(entity.Id);

            iDet.DetentionId = entity.DetentionId;
        }
    }
}