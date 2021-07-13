using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Constants;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class RegGroupRepository : BaseReadWriteRepository<RegGroup>, IRegGroupRepository
    {
        public RegGroupRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "StudentGroups", "SG", "StudentGroupId");
            JoinEntity(query, "YearGroups", "YG", "YearGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentGroup), "SG");
            query.SelectAllColumns(typeof(YearGroup), "YG");

            return query;
        }

        protected override async Task<IEnumerable<RegGroup>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var regGroups = await Transaction.Connection.QueryAsync<RegGroup, StudentGroup, YearGroup, RegGroup>(
                sql.Sql,
                (reg, studentGroup, year) =>
                {
                    reg.StudentGroup = studentGroup;
                    reg.YearGroup = year;

                    return reg;
                }, sql.NamedBindings, Transaction);

            return regGroups;
        }

        public async Task<RegGroup> GetByStudent(Guid studentId)
        {
            var query = GenerateQuery();
            
            query.LeftJoin("StudentGroupMemberships AS SGM", "SGM.StudentGroupId", "SG.Id");

            query.Where("SG.StudentGroupTypeId", StudentGroupTypes.RegGroup);
            query.Where("SGM.StudentId", studentId);

            return await ExecuteQueryFirstOrDefault(query);
        }

        public async Task<StaffMember> GetTutor(Guid regGroupId)
        {
            var query = GenerateQuery();
            
            query.LeftJoin("StudentGroupSupervisors AS SGS", "SGS.StudentGroupId", $"{TblAlias}.Id");

            query.Where($"{TblAlias}.Id", regGroupId);
            query.Where("SGS.SupervisorTitleId", StudentGroupSupervisorTitles.RegTutor);

            return await ExecuteQueryFirstOrDefault<StaffMember>(query);
        }
    }
}