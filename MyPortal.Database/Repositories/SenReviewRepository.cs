using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SenReviewRepository : BaseReadWriteRepository<SenReview>, ISenReviewRepository
    {
        public SenReviewRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(Student))},
{EntityHelper.GetPropertyNames(typeof(Person))},
{EntityHelper.GetPropertyNames(typeof(SenReviewType))}";

            JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[SenReview].[StudentId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[Student].[PersonId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[SenReviewType]", "[SenReviewType].[Id]", "[SenReview].[ReviewTypeId]")}";
        }

        protected override async Task<IEnumerable<SenReview>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<SenReview, Student, Person, SenReviewType, SenReview>(sql,
                (review, student, person, reviewType) =>
                {
                    review.Student = student;
                    review.Student.Person = person;
                    review.ReviewType = reviewType;

                    return review;
                }, param);
        }
    }
}