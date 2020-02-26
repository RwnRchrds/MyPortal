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
    public class ResultRepository : BaseReadWriteRepository<Result>, IResultRepository
    {
        public ResultRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(ResultSet))},
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Person))}
{EntityHelper.GetAllColumns(typeof(Aspect))},
{EntityHelper.GetAllColumns(typeof(Grade))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[ResultSet]", "[ResultSet].[Id]", "[Result].[ResultSetId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[Result].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[Student].[PersonId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Aspect]", "[Aspect].[Id]", "[Result].[AspectId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Grade]", "[Grade].[Id]", "[Result].[GradeId]")}";
        }

        protected override async Task<IEnumerable<Result>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Result, ResultSet, Student, Person, Aspect, Grade, Result>(sql,
                (result, set, student, person, aspect, grade) =>
                {
                    result.ResultSet = set;
                    result.Student = student;
                    result.Student.Person = person;
                    result.Aspect = aspect;
                    result.Grade = grade;

                    return result;
                }, param);
        }

        public async Task Update(Result entity)
        {
            var result = await Context.Results.FindAsync(entity.Id);

            result.GradeId = entity.GradeId;
            result.Mark = entity.Mark;
        }
    }
}