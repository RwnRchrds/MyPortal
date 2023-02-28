using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Data.Students;

using MyPortal.Logic.Models.Requests.Student;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IStudentService : IService
    {
        Task<IEnumerable<StudentModel>> GetStudents(StudentSearchOptions searchModel);
        Task<IEnumerable<StudentModel>> GetStudentsByContact(Guid contactId, bool reportableOnly);
        Task<IEnumerable<StudentSummaryModel>> SearchStudents(StudentSearchOptions searchOptions);

        Task<StudentStatsModel> GetStatsById(Guid studentId, Guid academicYearId);

        Task<StudentModel> GetStudentById(Guid studentId);

        Task<StudentModel> GetStudentByUserId(Guid userId, bool throwIfNotFound = true);

        Task<StudentModel> GetStudentByPersonId(Guid personId, bool throwIfNotFound = true);

        Task CreateStudent(StudentRequestModel student);

        Task UpdateStudent(Guid studentId, StudentRequestModel model);

        Dictionary<string, int> GetStudentStatusOptions();

        Task<string> GenerateUpn();
    }
}
