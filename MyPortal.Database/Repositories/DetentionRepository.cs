using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DetentionRepository : BaseReadWriteRepository<Detention>, IDetentionRepository
    {
        private readonly string RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(DetentionType))},
{EntityHelper.GetAllColumns(typeof(DiaryEvent))},
{EntityHelper.GetAllColumns(typeof(StaffMember))},
{EntityHelper.GetAllColumns(typeof(Person))}";

        private readonly string JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[DetentionType]", "[DetentionType].[Id]", "[Detention].[DetentionTypeId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[DiaryEvent]", "[DiaryEvent].[Id]", "[Detention].[EventId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[StaffMember].[Id]", "[Detention].[SupervisorId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[StaffMember].[PersonId]")}";

        public DetentionRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Detention>> GetAll()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";

            return await ExecuteQuery(sql);
        }

        public async Task<Detention> GetById(Guid id)
        {
            var sql =
                $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated} WHERE [Detention].[Id] = @DetentionId";

            return (await ExecuteQuery(sql, new {DetentionId = id})).Single();
        }

        public async Task Update(Detention entity)
        {
            var detentionInDb = await Context.Detentions.FindAsync(entity.Id);

            detentionInDb.SupervisorId = entity.SupervisorId;
        }

        protected override async Task<IEnumerable<Detention>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Detention, DetentionType, DiaryEvent, StaffMember, Person, Detention>(sql,
                (detention, type, diaryEvent, supervisor, person) =>
                {
                    detention.Type = type;
                    detention.Event = diaryEvent;
                    detention.Supervisor = supervisor;

                    detention.Supervisor.Person = person;

                    return detention;
                }, param);
        }
    }
}
