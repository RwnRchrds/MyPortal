using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SenReviewRepository : BaseReadWriteRepository<SenReview>, ISenReviewRepository
    {
        public SenReviewRepository(DbUserWithContext dbUser) : base(dbUser)
        {
            
        }

        public async Task Update(SenReview entity)
        {
            var review = await DbUser.Context.SenReviews.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (review == null)
            {
                throw new EntityNotFoundException("SEN review not found.");
            }

            review.StudentId = entity.StudentId;
            review.ReviewTypeId = entity.ReviewTypeId;
            review.ReviewStatusId = entity.ReviewStatusId;
            review.SencoId = entity.SencoId;
            review.EventId = entity.EventId;
            review.OutcomeSenStatusId = entity.OutcomeSenStatusId;
            review.Comments = entity.Comments;
        }
    }
}