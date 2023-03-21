using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Curriculum;

using MyPortal.Logic.Models.Requests.Curriculum.Homework;
using Task = MyPortal.Database.Models.Entity.Task;

namespace MyPortal.Logic.Services;

public class HomeworkService : BaseUserService, IHomeworkService
{
    public HomeworkService(ICurrentUser user) : base(user)
    {
    }

    public async System.Threading.Tasks.Task CreateHomework(HomeworkRequestModel model)
    {
        Validate(model);
        
        await using var unitOfWork = await User.GetConnection();

        var homeworkItem = new HomeworkItem
        {
            Id = Guid.NewGuid(),
            Title = model.Title,
            Description = model.Description,
            SubmitOnline = model.SubmitOnline,
            MaxPoints = model.MaxPoints,
            Directory = new Directory
            {
                Name = "homework-root"
            }
        };
            
        var now = DateTime.Now;

        foreach (var studentId in model.StudentIds)
        {
            var student = await unitOfWork.Students.GetById(studentId);

            if (student == null)
            {
                throw new NotFoundException("Student not found.");
            }

            var submission = new HomeworkSubmission
            {
                StudentId = studentId,
                Task = new Task
                {
                    DueDate = model.DueDate,
                    TypeId = TaskTypes.Homework,
                    AssignedToId = student.PersonId,
                    AssignedById = model.AssignedById,
                    System = true,
                    CreatedDate = now
                }
            };
                
            homeworkItem.Submissions.Add(submission);
        }
            
        unitOfWork.HomeworkItems.Create(homeworkItem);
        await unitOfWork.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task UpdateHomework(Guid homeworkId, HomeworkRequestModel model)
    {
        Validate(model);
        
        await using var unitOfWork = await User.GetConnection();
        
        var homework = await unitOfWork.HomeworkItems.GetById(homeworkId);

        homework.Title = model.Title;
        homework.Description = model.Description;
        homework.SubmitOnline = model.SubmitOnline;
        homework.MaxPoints = model.MaxPoints;

        await unitOfWork.HomeworkItems.Update(homework);

        await unitOfWork.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteHomework(Guid homeworkId)
    {
        await using var unitOfWork = await User.GetConnection();
        
        await unitOfWork.HomeworkItems.Delete(homeworkId);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<HomeworkSubmissionModel>> GetSubmissionsByStudent(Guid studentId)
    {
        await using var unitOfWork = await User.GetConnection();
        
        var submissions = await unitOfWork.HomeworkSubmissions.GetHomeworkSubmissionsByStudent(studentId);

        return submissions.Select(s => new HomeworkSubmissionModel(s));
    }

    public async Task<IEnumerable<HomeworkSubmissionModel>> GetSubmissionsByStudentGroup(Guid studentGroupId)
    {
        await using var unitOfWork = await User.GetConnection();
        
        var submissions = await unitOfWork.HomeworkSubmissions.GetHomeworkSubmissionsByStudentGroup(studentGroupId);

        return submissions.Select(s => new HomeworkSubmissionModel(s));
    }

    public async Task<IEnumerable<HomeworkItemModel>> GetHomework(HomeworkSearchOptions searchOptions)
    {
        await using var unitOfWork = await User.GetConnection();
        
        var homeworkItems =
            (await unitOfWork.HomeworkItems.GetHomework(searchOptions)).Select(hi => new HomeworkItemModel(hi));

        return homeworkItems;
    }

    public async System.Threading.Tasks.Task CreateHomeworkSubmission(HomeworkSubmissionRequestModel model)
    {
        Validate(model);
        
        await using var unitOfWork = await User.GetConnection();
        
        var homework = await unitOfWork.HomeworkItems.GetById(model.HomeworkId);
        var student = await unitOfWork.Students.GetById(model.StudentId);

        if (homework == null)
        {
            throw new NotFoundException("Homework item not found.");
        }

        if (student == null)
        {
            throw new NotFoundException("Student not found.");
        }
            
        var now = DateTime.Now;

        var homeworkSubmission = new HomeworkSubmission
        {
            Id = Guid.NewGuid(),
            HomeworkId = model.HomeworkId,
            StudentId = model.StudentId,
            Task = new Task
            {
                Id = Guid.NewGuid(),
                DueDate = model.DueDate,
                AssignedToId = student.PersonId,
                AssignedById = model.AssignedById,
                System = true,
                TypeId = TaskTypes.Homework,
                CreatedDate = now,
            }
        };

        unitOfWork.HomeworkSubmissions.Create(homeworkSubmission);

        await unitOfWork.SaveChangesAsync();
    }

    // TODO: Review this service method
    public async System.Threading.Tasks.Task UpdateHomeworkSubmission(Guid homeworkSubmissionId, HomeworkSubmissionRequestModel model)
    {
        Validate(model);
        
        await using var unitOfWork = await User.GetConnection();
        
        var homeworkSubmission = await unitOfWork.HomeworkSubmissions.GetById(homeworkSubmissionId);

        if (homeworkSubmission == null)
        {
            throw new NotFoundException("Homework submission not found.");
        }

        homeworkSubmission.PointsAchieved = model.PointsAchieved;
        homeworkSubmission.Comments = model.Comments;
        homeworkSubmission.Task.DueDate = model.DueDate;

        if (model.Completed)
        {
            homeworkSubmission.Task.Completed = true;
            homeworkSubmission.Task.CompletedDate = DateTime.Now;
        }

        await unitOfWork.HomeworkSubmissions.Update(homeworkSubmission);

        await unitOfWork.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteHomeworkSubmission(Guid homeworkSubmissionId)
    {
        await using var unitOfWork = await User.GetConnection();
        
        await unitOfWork.HomeworkSubmissions.Delete(homeworkSubmissionId);

        await unitOfWork.SaveChangesAsync();
    }
}