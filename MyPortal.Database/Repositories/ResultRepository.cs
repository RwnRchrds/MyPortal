using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class ResultRepository : BaseReadWriteRepository<Result>, IResultRepository
    {
        public ResultRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(ResultSet))},
{EntityHelper.GetPropertyNames(typeof(Student))},
{EntityHelper.GetPropertyNames(typeof(Person))}
{EntityHelper.GetPropertyNames(typeof(Aspect))},
{EntityHelper.GetPropertyNames(typeof(Grade))}";

            JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[ResultSet]", "[ResultSet].[Id]", "[Result].[ResultSetId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[Result].[StudentId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[Student].[PersonId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Aspect]", "[Aspect].[Id]", "[Result].[AspectId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Grade]", "[Grade].[Id]", "[Result].[GradeId]")}";
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
    }
}