using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SubjectStaffMemberRoleRepository : BaseReadWriteRepository<SubjectStaffMemberRole>, ISubjectStaffMemberRoleRepository
    {
        public SubjectStaffMemberRoleRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
        }

        protected override async Task<IEnumerable<SubjectStaffMemberRole>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<SubjectStaffMemberRole>(sql, param);
        }

        public async Task Update(SubjectStaffMemberRole entity)
        {
            var role = await Context.SubjectStaffMemberRoles.FindAsync(entity.Id);

            role.Description = entity.Description;
        }
    }
}