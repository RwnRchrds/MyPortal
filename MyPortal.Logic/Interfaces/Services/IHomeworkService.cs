using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Curriculum.Homework;

namespace MyPortal.Logic.Interfaces.Services;

public interface IHomeworkService
{
    Task CreateHomework(CreateHomeworkRequestModel model);
    Task UpdateHomework(UpdateHomeworkRequestModel model);
    Task DeleteHomework(Guid homeworkId);
    Task CreateHomeworkSubmission(CreateHomeworkSubmissionRequestModel model);
    Task UpdateHomeworkSubmission(params UpdateHomeworkSubmissionRequestModel[] models);
    Task DeleteHomeworkSubmission(params Guid[] homeworkSubmissionIds);
}