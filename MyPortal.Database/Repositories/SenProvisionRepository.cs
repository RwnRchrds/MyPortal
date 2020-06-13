using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SenProvisionRepository : BaseReadWriteRepository<SenProvision>, ISenProvisionRepository
    {
        public SenProvisionRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(Student))},
{EntityHelper.GetPropertyNames(typeof(SenProvisionType))}";

            JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[SenProvision].[StudentId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[SenProvisionType]", "[SenProvisionType].[Id]", "[SenProvision].[ProvisionTypeId]")}";
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
    }
}