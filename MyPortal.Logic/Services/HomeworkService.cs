using System;
using MyPortal.Database.Constants;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Requests.Curriculum.Homework;

namespace MyPortal.Logic.Services;

public class HomeworkService : BaseService, IHomeworkService
{
    public async System.Threading.Tasks.Task CreateHomework(CreateHomeworkRequestModel model)
    {
        var now = DateTime.Now;
        
        using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
        {
            var homeworkItem = new HomeworkItem
            {
                Title = model.Title,
                Description = model.Description,
                SubmitOnline = model.SubmitOnline,
                MaxPoints = model.MaxPoints,
                Directory = new Directory
                {
                    Name = "homework-root"
                }
            };

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
    }
}