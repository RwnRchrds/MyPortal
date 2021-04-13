using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SchoolRepository : BaseReadWriteRepository<School>, ISchoolRepository
    {
        public SchoolRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "School")
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

            return await Transaction.Connection.QueryAsync<School, LocalAuthority, SchoolPhase, SchoolType, GovernanceType, IntakeType, Person, School>(sql.Sql,
                (school, lea, phase, type, gov, intake, head) =>
                {
                    school.LocalAuthority = lea;
                    school.SchoolPhase = phase;
                    school.Type = type;
                    school.GovernanceType = gov;
                    school.IntakeType = intake;
                    school.HeadTeacher = head;

                    return school;
                }, sql.NamedBindings, Transaction);
        }

        public async Task Update(School entity)
        {
            var school = await Context.Schools.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (school == null)
            {
                throw new EntityNotFoundException("School not found.");
            }

            school.Name = entity.Name;
            school.LocalAuthorityId = entity.LocalAuthorityId;
            school.EstablishmentNumber = entity.EstablishmentNumber;
            school.Urn = entity.Urn;
            school.Uprn = entity.Uprn;
            school.PhaseId = entity.PhaseId;
            school.TypeId = entity.TypeId;
            school.GovernanceTypeId = entity.GovernanceTypeId;
            school.IntakeTypeId = entity.IntakeTypeId;
            school.HeadTeacherId = entity.HeadTeacherId;
            school.TelephoneNo = entity.TelephoneNo;
            school.FaxNo = entity.FaxNo;
            school.EmailAddress = entity.EmailAddress;
            school.Website = entity.Website;
        }
    }
}
