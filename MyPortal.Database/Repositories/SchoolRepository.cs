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
        public SchoolRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "SchoolPhases", "SP", "PhaseId");
            JoinEntity(query, "SchoolTypes", "ST", "TypeId");
            JoinEntity(query, "GovernanceTypes", "GT", "GovernanceTypeId");
            JoinEntity(query, "IntakeTypes", "IT", "IntakeTypeId");
            JoinEntity(query, "People", "HT", "HeadTeacherId");
            JoinEntity(query, "LocalAuthorities", "LA", "LocalAuthorityId");
            
            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(SchoolPhase), "SP");
            query.SelectAllColumns(typeof(SchoolType), "ST");
            query.SelectAllColumns(typeof(GovernanceType), "GT");
            query.SelectAllColumns(typeof(IntakeType), "IT");
            query.SelectAllColumns(typeof(Person), "HT");
            query.SelectAllColumns(typeof(LocalAuthority), "LA");

            return query;
        }

        protected override async Task<IEnumerable<School>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var schools = await Transaction.Connection
                .QueryAsync<School, SchoolPhase, SchoolType, GovernanceType, IntakeType, Person, LocalAuthority,
                    School>(sql.Sql,
                    (school, phase, type, govType, intakeType, headTeacher, la) =>
                    {
                        school.SchoolPhase = phase;
                        school.Type = type;
                        school.GovernanceType = govType;
                        school.IntakeType = intakeType;
                        school.HeadTeacher = headTeacher;
                        school.LocalAuthority = la;

                        return school;
                    }, sql.NamedBindings, Transaction);

            return schools;
        }

        public async Task<string> GetLocalSchoolName()
        {
            var query = new Query(TblName).Select($"{TblAlias}.Name");

            query.Where($"{TblAlias}.Local", true);

            return await ExecuteQueryStringResult(query);
        }

        public async Task<School> GetLocal()
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.Local", true);

            return (await ExecuteQuery(query)).First();
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
