using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Student;
using MyPortal.Logic.Models.Response.Students;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentModel>> GetStudents(StudentSearchOptions searchModel);
        Task<IEnumerable<StudentSummaryModel>> SearchStudents(StudentSearchOptions searchOptions);

        Task<StudentStatsResponseModel> GetStatsById(Guid studentId, Guid academicYearId);

        Task<StudentModel> GetStudentById(Guid studentId);

        Task<StudentModel> GetStudentByUserId(Guid userId, bool throwIfNotFound = true);

        Task<StudentModel> GetStudentByPersonId(Guid personId, bool throwIfNotFound = true);

        Task CreateStudent(params CreateStudentRequestModel[] students);

        Task UpdateStudent(params UpdateStudentRequestModel[] models);

        Dictionary<string, int> GetStudentStatusOptions();

        Task<string> GenerateUpn();
    }
}
