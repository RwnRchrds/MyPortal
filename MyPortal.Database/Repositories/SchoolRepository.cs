﻿using System.Collections.Generic;
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
            JoinEntity(query, "Agencies", "A", "AgencyId");
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
            query.SelectAllColumns(typeof(Agency), "A");
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
                .QueryAsync(sql.Sql,
                    new [] { typeof(School), typeof(Agency), typeof(SchoolPhase), typeof(SchoolType), typeof(GovernanceType), typeof(IntakeType), typeof(Person), typeof(LocalAuthority)}, (objects) =>
                    {
                        var school = (School)objects[0];
                        school.Agency = (Agency)objects[1];
                        school.SchoolPhase = (SchoolPhase)objects[2];
                        school.Type = (SchoolType)objects[3];
                        school.GovernanceType = (GovernanceType)objects[4];
                        school.IntakeType = (IntakeType)objects[5];
                        school.HeadTeacher = (Person)objects[6];
                        school.LocalAuthority = (LocalAuthority)objects[7];

                        return school;
                    }, sql.NamedBindings, Transaction);

            return schools;
        }

        public async Task<string> GetLocalSchoolName()
        {
            var query = GenerateEmptyQuery();

            JoinRelated(query);

            query.Select("A.Name");

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
            
            school.LocalAuthorityId = entity.LocalAuthorityId;
            school.EstablishmentNumber = entity.EstablishmentNumber;
            school.Urn = entity.Urn;
            school.Uprn = entity.Uprn;
            school.PhaseId = entity.PhaseId;
            school.TypeId = entity.TypeId;
            school.GovernanceTypeId = entity.GovernanceTypeId;
            school.IntakeTypeId = entity.IntakeTypeId;
            school.HeadTeacherId = entity.HeadTeacherId;
        }
    }
}
