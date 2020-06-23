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
    public class SenProvisionRepository : BaseReadWriteRepository<SenProvision>, ISenProvisionRepository
    {
        public SenProvisionRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(SenProvisionType));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Student", "Student.Id", "SenProvision.StudentId");
            query.LeftJoin("dbo.SenProvisionType", "SenProvisionType.Id", "SenProvision.ProvisionTypeId");
        }

        protected override async Task<IEnumerable<SenProvision>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<SenProvision, Student, SenProvisionType, SenProvision>(sql.Sql,
                (provision, student, type) =>
                {
                    provision.Student = student;
                    provision.Type = type;

                    return provision;
                }, sql.NamedBindings);
        }
    }
}