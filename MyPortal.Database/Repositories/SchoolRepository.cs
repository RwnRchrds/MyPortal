using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Repositories
{
    public class SchoolRepository : BaseReadWriteRepository<School>, ISchoolRepository
    {
        public SchoolRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(LocalAuthority))},
{EntityHelper.GetPropertyNames(typeof(Phase))},
{EntityHelper.GetPropertyNames(typeof(SchoolType))},
{EntityHelper.GetPropertyNames(typeof(GovernanceType))},
{EntityHelper.GetPropertyNames(typeof(IntakeType))},
{EntityHelper.GetPropertyNames(typeof(Person))}";

        JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[LocalAuthority]", "[LocalAuthority].[Id]", "[School].[LocalAuthorityId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Phase]", "[Phase].[Id]", "[School].[PhaseId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[SchoolType]", "[SchoolType].[Id]", "[School].[TypeId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[GovernanceType]", "[GovernanceType].[Id]", "[School].[GovernanceTypeId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[IntakeType]", "[IntakeType].[Id]", "[School].[IntakeTypeId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[School].[HeadTeacherId]")}";
        }

        public async Task<string> GetLocalSchoolName()
        {
            var sql = $"SELECT [School].[Name] FROM {TblName}";
            
            QueryHelper.Where(ref sql, "[School].[Local] = 1");

            return await ExecuteStringQuery(sql);
        }

        public async Task<School> GetLocal()
        {
            var sql = SelectAllColumns();
            
            QueryHelper.Where(ref sql, "[School].[Local] = 1");

            return (await ExecuteQuery(sql)).First();
        }

        protected override async Task<IEnumerable<School>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<School, LocalAuthority, Phase, SchoolType, GovernanceType, IntakeType, Person, School>(sql,
                (school, lea, phase, type, gov, intake, head) =>
                {
                    school.LocalAuthority = lea;
                    school.Phase = phase;
                    school.Type = type;
                    school.GovernanceType = gov;
                    school.IntakeType = intake;
                    school.HeadTeacher = head;

                    return school;
                }, param);
        }
    }
}
