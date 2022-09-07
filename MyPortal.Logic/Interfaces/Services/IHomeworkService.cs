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
    Task CreateHomework(HomeworkRequestModel homework);
    Task UpdateHomework(Guid homeworkId, HomeworkRequestModel homework);
    Task DeleteHomework(Guid homeworkId);
    Task<IEnumerable<HomeworkSubmissionModel>> GetSubmissionsByStudent(Guid studentId);
    Task<IEnumerable<HomeworkSubmissionModel>> GetSubmissionsByStudentGroup(Guid studentGroupId);
    Task CreateHomeworkSubmission(HomeworkSubmissionRequestModel homeworkSubmission);
    Task UpdateHomeworkSubmission(Guid homeworkSubmissionId, HomeworkSubmissionRequestModel homeworkSubmission);
    Task DeleteHomeworkSubmission(Guid homeworkSubmissionId);
}