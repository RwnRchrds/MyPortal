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
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class SchoolRepository : BaseReadWriteRepository<School>, ISchoolRepository
    {
        public SchoolRepository(ApplicationDbContext context) : base(context, "School")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(LocalAuthority), "LocalAuthority");
            query.SelectAllColumns(typeof(SchoolPhase), "SchoolPhase");
            query.SelectAllColumns(typeof(SchoolType), "SchoolType");
            query.SelectAllColumns(typeof(GovernanceType), "GovernanceType");
            query.SelectAllColumns(typeof(IntakeType), "IntakeType");
            query.SelectAllColumns(typeof(Person), "Person");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("LocalAuthorities as LocalAuthority", "LocalAuthority.Id", "School.LocalAuthorityId");
            query.LeftJoin("SchoolPhases as SchoolPhase", "SchoolPhase.Id", "School.PhaseId");
            query.LeftJoin("SchoolTypes as SchoolType", "SchoolType.Id", "School.TypeId");
            query.LeftJoin("GovernanceTypes as GovernanceType", "GovernanceType.Id", "School.GovernanceTypeId");
            query.LeftJoin("IntakeTypes as IntakeType", "IntakeType.Id", "School.IntakeTypeId");
            query.LeftJoin("People as Person", "Person.Id", "School.HeadTeacherId");
        }

        public async Task<string> GetLocalSchoolName()
        {
            var query = new Query(TblName).Select("School.Name");

            query.Where("School.Local", true);

            return await ExecuteQueryStringResult(query);
        }

        public async Task<School> GetLocal()
        {
            var query = GenerateQuery();

            query.Where("School.Local", true);

            return (await ExecuteQuery(query)).First();
        }

        protected override async Task<IEnumerable<School>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<School, LocalAuthority, SchoolPhase, SchoolType, GovernanceType, IntakeType, Person, School>(sql.Sql,
                (school, lea, phase, type, gov, intake, head) =>
                {
                    school.LocalAuthority = lea;
                    school.SchoolPhase = phase;
                    school.Type = type;
                    school.GovernanceType = gov;
                    school.IntakeType = intake;
                    school.HeadTeacher = head;

                    return school;
                }, sql.NamedBindings);
        }
    }
}
