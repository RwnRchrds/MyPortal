using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = MyPortal.Database.Models.Entity.Task;

namespace MyPortal.Database.Repositories
{
    public class HomeworkSubmissionRepository : BaseReadWriteRepository<HomeworkSubmission>, IHomeworkSubmissionRepository
    {
        public HomeworkSubmissionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async System.Threading.Tasks.Task Update(HomeworkSubmission entity)
        {
            var homeworkSubmission = await Context.HomeworkSubmissions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (homeworkSubmission == null)
            {
                throw new EntityNotFoundException("Homework submission not found.");
            }

            homeworkSubmission.DocumentId = entity.DocumentId;
            homeworkSubmission.PointsAchieved = entity.PointsAchieved;
        }
    }
}