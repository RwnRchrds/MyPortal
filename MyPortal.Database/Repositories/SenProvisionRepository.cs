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
    public class SenProvisionRepository : BaseReadWriteRepository<SenProvision>, ISenProvisionRepository
    {
        public SenProvisionRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(SenProvisionType))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[SenProvision].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[SenProvisionType]", "[SenProvisionType].[Id]", "[SenProvision].[ProvisionTypeId]")}";
        }

        protected override async Task<IEnumerable<SenProvision>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<SenProvision, Student, SenProvisionType, SenProvision>(sql,
                (provision, student, type) =>
                {
                    provision.Student = student;
                    provision.Type = type;

                    return provision;
                }, param);
        }

        public async Task Update(SenProvision entity)
        {
            var provision = await Context.SenProvisions.FindAsync(entity.Id);

            provision.ProvisionTypeId = entity.ProvisionTypeId;
            provision.StartDate = entity.StartDate;
            provision.EndDate = entity.EndDate;
            provision.Note = entity.Note;
        }
    }
}