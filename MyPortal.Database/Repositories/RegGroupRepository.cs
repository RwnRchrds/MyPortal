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

        public async Task<RegGroup> GetByStudent(Guid studentId)
        {
            var query = GenerateQuery();

            query.LeftJoin("StudentGroups AS SG", "SG.Id", $"{TblAlias}.StudentGroupId");
            query.LeftJoin("StudentGroupMemberships AS SGM", "SGM.StudentGroupId", "SG.Id");

            query.Where("SG.StudentGroupTypeId", StudentGroupTypes.RegGroup);
            query.Where("SGM.StudentId", studentId);

            return await ExecuteQueryFirstOrDefault(query);
        }

        public async Task<StaffMember> GetTutor(Guid regGroupId)
        {
            var query = GenerateQuery();
            
            query.LeftJoin("StudentGroups AS SG", "SG.Id", $"{TblAlias}.StudentGroupId");
            query.LeftJoin("StudentGroupSupervisors AS SGS", "SGS.StudentGroupId", $"{TblAlias}.Id");

            query.Where($"{TblAlias}.Id", regGroupId);
            query.Where("SGS.SupervisorTitleId", StudentGroupSupervisorTitles.RegTutor);

            return await ExecuteQueryFirstOrDefault<StaffMember>(query);
        }
    }
}