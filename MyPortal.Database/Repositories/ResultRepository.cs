using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ResultRepository : BaseReadWriteRepository<Result>, IResultRepository
    {
        public ResultRepository(ApplicationDbContext context) : base(context, "Result")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ResultSet), "ResultSet");
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(Person), "Person");
            query.SelectAllColumns(typeof(Aspect), "Aspect");
            query.SelectAllColumns(typeof(Grade), "Grade");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("ResultSets as ResultSet", "ResultSet.Id", "Result.ResultSetId");
            query.LeftJoin("Students as Student", "Student.Id", "Result.StudentId");
            query.LeftJoin("People as Person", "Person.Id", "Student.PersonId");
            query.LeftJoin("Aspects as Aspect", "Aspect.Id", "Result.AspectId");
            query.LeftJoin("Grades as Grade", "Grade.Id", "Result.GradeId");
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