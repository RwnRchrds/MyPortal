using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SenReviewRepository : BaseReadWriteRepository<SenReview>, ISenReviewRepository
    {
        public SenReviewRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task Update(SenReview entity)
        {
            var review = await Context.SenReviews.FirstOrDefaultAsync(x => x.Id == entity.Id);

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