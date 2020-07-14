using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    public class SchoolRepository : BaseReadWriteRepository<School>, ISchoolRepository
    {
        public SchoolRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(LocalAuthority));
            query.SelectAll(typeof(Phase));
            query.SelectAll(typeof(SchoolType));
            query.SelectAll(typeof(GovernanceType));
            query.SelectAll(typeof(IntakeType));
            query.SelectAll(typeof(Person));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("LocalAuthority", "LocalAuthority.Id", "School.LocalAuthorityId");
            query.LeftJoin("Phase", "Phase.Id", "School.PhaseId");
            query.LeftJoin("SchoolType", "SchoolType.Id", "School.TypeId");
            query.LeftJoin("GovernanceType", "GovernanceType.Id", "School.GovernanceTypeId");
            query.LeftJoin("IntakeType", "IntakeType.Id", "School.IntakeTypeId");
            query.LeftJoin("Person", "Person.Id", "School.HeadTeacherId");
        }

        public async Task<string> GetLocalSchoolName()
        {
            var query = new Query(TblName).Select("School.Name");

            query.Where("School.Local", true);

            return await ExecuteQueryStringResult(query);
        }

        public async Task<School> GetLocal()
        {
            var query = SelectAllColumns();

            query.Where("School.Local", true);

            return (await ExecuteQuery(query)).First();
        }

        protected override async Task<IEnumerable<School>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<School, LocalAuthority, Phase, SchoolType, GovernanceType, IntakeType, Person, School>(sql.Sql,
                (school, lea, phase, type, gov, intake, head) =>
                {
                    school.LocalAuthority = lea;
                    school.Phase = phase;
                    school.Type = type;
                    school.GovernanceType = gov;
                    school.IntakeType = intake;
                    school.HeadTeacher = head;

                    return school;
                }, sql.NamedBindings);
        }
    }
}
