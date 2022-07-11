using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Curriculum.Homework;

namespace MyPortal.Logic.Interfaces.Services;

public interface IHomeworkService
{
    Task<IEnumerable<HomeworkItemModel>> GetHomework(HomeworkSearchOptions searchOptions);
    Task CreateHomework(CreateHomeworkRequestModel model);
    Task UpdateHomework(UpdateHomeworkRequestModel model);
    Task DeleteHomework(Guid homeworkId);
    Task<IEnumerable<HomeworkSubmissionModel>> GetSubmissionsByStudent(Guid studentId);
    Task<IEnumerable<HomeworkSubmissionModel>> GetSubmissionsByStudentGroup(Guid studentGroupId);
    Task CreateHomeworkSubmission(CreateHomeworkSubmissionRequestModel model);
    Task UpdateHomeworkSubmission(params UpdateHomeworkSubmissionRequestModel[] models);
    Task DeleteHomeworkSubmission(params Guid[] homeworkSubmissionIds);
}