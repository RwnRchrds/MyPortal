using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class RegGroupRepository : BaseReadWriteRepository<RegGroup>, IRegGroupRepository
    {
        public RegGroupRepository(ApplicationDbContext context) : base(context, "RegGroup")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StaffMember), "StaffMember");
            query.SelectAllColumns(typeof(YearGroup), "YearGroup");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("StaffMembers as StaffMember", "StaffMember.Id", "RegGroup.TutorId");
            query.LeftJoin("YearGroups as YearGroup", "YearGroup.Id", "RegGroup.YearGroupId");
        }

        protected override async Task<IEnumerable<RegGroup>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<RegGroup, StaffMember, YearGroup, RegGroup>(sql.Sql, (reg, tutor, year) =>
            {
                reg.Tutor = tutor;
                reg.YearGroup = year;

                return reg;
            }, sql.NamedBindings);
        }
    }
}