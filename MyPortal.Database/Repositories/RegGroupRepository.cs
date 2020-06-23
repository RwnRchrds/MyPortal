using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class RegGroupRepository : BaseReadWriteRepository<RegGroup>, IRegGroupRepository
    {
        public RegGroupRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(StaffMember));
            query.SelectAll(typeof(YearGroup));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.StaffMember", "StaffMember.Id", "RegGroup.TutorId");
            query.LeftJoin("dbo.YearGroup", "YearGroup.Id", "RegGroup.YearGroupId");
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