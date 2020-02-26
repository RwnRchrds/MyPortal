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
    public class EnrolmentRepository : BaseReadWriteRepository<Enrolment>, IEnrolmentRepository
    {
        public EnrolmentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Class))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[Enrolment].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Class]", "[Class].[Id]", "[Enrolment].[ClassId]")}";
        }

        protected override async Task<IEnumerable<Enrolment>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Enrolment, Student, Class, Enrolment>(sql,
                (enrolment, student, currClass) =>
                {
                    enrolment.Student = student;
                    enrolment.Class = currClass;

                    return enrolment;
                }, param);
        }

        public async Task Update(Enrolment entity)
        {
            var enrolmentInDb = await Context.Enrolments.FindAsync(entity.Id);

            enrolmentInDb.ClassId = entity.ClassId;
        }
    }
}