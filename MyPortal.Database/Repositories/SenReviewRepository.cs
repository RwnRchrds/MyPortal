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
    public class SenReviewRepository : BaseReadWriteRepository<SenReview>, ISenReviewRepository
    {
        public SenReviewRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person));
            query.SelectAll(typeof(SenReviewType));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Student", "Student.Id", "SenReview.StudentId");
            query.LeftJoin("dbo.Person", "Person.Id", "Student.PersonId");
            query.LeftJoin("dbo.SenReviewType", "SenReviewType.Id", "SenReview.ReviewTypeId");
        }

        protected override async Task<IEnumerable<SenReview>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<SenReview, Student, Person, SenReviewType, SenReview>(sql.Sql,
                (review, student, person, reviewType) =>
                {
                    review.Student = student;
                    review.Student.Person = person;
                    review.ReviewType = reviewType;

                    return review;
                }, sql.NamedBindings);
        }
    }
}