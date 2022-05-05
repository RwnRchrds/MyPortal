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
        Task<IEnumerable<StudentModel>> Get(StudentSearchOptions searchModel);
        Task<IEnumerable<StudentSummaryModel>> Search(StudentSearchOptions searchOptions);

        Task<StudentStatsModel> GetStatsById(Guid studentId, Guid academicYearId);

        Task<StudentModel> GetById(Guid studentId);

        Task<StudentModel> GetByUserId(Guid userId, bool throwIfNotFound = true);

        Task<StudentModel> GetByPersonId(Guid personId, bool throwIfNotFound = true);

        Task CreateStudent(params CreateStudentModel[] students);

        Task UpdateStudent(params UpdateStudentModel[] models);

        Dictionary<string, int> GetStudentStatusOptions();

        Task<string> GenerateUpn();
    }
}
