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
        public SenReviewRepository(ApplicationDbContext context) : base(context, "SenReview")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(Person), "Person");
            query.SelectAllColumns(typeof(SenReviewType), "SenReviewType");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Students as Student", "Student.Id", "SenReview.StudentId");
            query.LeftJoin("People as Person", "Person.Id", "Student.PersonId");
            query.LeftJoin("SenReviewTypes as SenReviewType", "SenReviewType.Id", "SenReview.ReviewTypeId");
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