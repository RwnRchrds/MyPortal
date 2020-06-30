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
    public class ResultRepository : BaseReadWriteRepository<Result>, IResultRepository
    {
        public ResultRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(ResultSet));
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person));
            query.SelectAll(typeof(Aspect));
            query.SelectAll(typeof(Grade));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.ResultSet", "ResultSet.Id", "Result.ResultSetId");
            query.LeftJoin("dbo.Student", "Student.Id", "Result.StudentId");
            query.LeftJoin("dbo.Person", "Person.Id", "Student.PersonId");
            query.LeftJoin("dbo.Aspect", "Aspect.Id", "Result.AspectId");
            query.LeftJoin("dbo.Grade", "Grade.Id", "Result.GradeId");
        }

        protected override async Task<IEnumerable<Result>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Result, ResultSet, Student, Person, Aspect, Grade, Result>(sql.Sql,
                (result, set, student, person, aspect, grade) =>
                {
                    result.ResultSet = set;
                    result.Student = student;
                    result.Student.Person = person;
                    result.Aspect = aspect;
                    result.Grade = grade;

                    return result;
                }, sql.NamedBindings);
        }
    }
}