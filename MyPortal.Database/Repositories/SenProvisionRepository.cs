using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class SenProvisionRepository : BaseReadWriteRepository<SenProvision>, ISenProvisionRepository
    {
        public SenProvisionRepository(ApplicationDbContext context) : base(context, "SenProvision")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(SenProvisionType), "SenProvisionType");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Students as Student", "Student.Id", "SenProvision.StudentId");
            query.LeftJoin("SenProvisionTypes as SenProvisionType", "SenProvisionType.Id", "SenProvision.ProvisionTypeId");
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