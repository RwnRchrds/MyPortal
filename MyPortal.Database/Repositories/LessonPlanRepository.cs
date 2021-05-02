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
    public class LessonPlanRepository : BaseReadWriteRepository<LessonPlan>, ILessonPlanRepository
    {
        public LessonPlanRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        public async Task Update(LessonPlan entity)
        {
            var lessonPlan = await Context.LessonPlans.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (lessonPlan == null)
            {
                throw new EntityNotFoundException("Lesson plan not found.");
            }

            lessonPlan.StudyTopicId = entity.StudyTopicId;
            lessonPlan.Title = entity.Title;
            lessonPlan.LearningObjectives = entity.LearningObjectives;
            lessonPlan.PlanContent = entity.PlanContent;
            lessonPlan.Homework = entity.Homework;
        }
    }
}