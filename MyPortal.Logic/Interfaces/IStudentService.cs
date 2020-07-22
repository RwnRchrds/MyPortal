using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Database.Search;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Student;

namespace MyPortal.Logic.Interfaces
{
    public interface IStudentService : IService
    {
        Task<IEnumerable<StudentModel>> Get(StudentSearchOptions searchModel);

        Task<StudentModel> GetById(Guid studentId);

        Task<StudentModel> GetByUserId(Guid userId, bool throwIfNotFound = true);

        Task<StudentModel> GetByPersonId(Guid personId, bool throwIfNotFound = true);

        SelectList GetStudentStatusOptions(StudentStatus defaultStatus = StudentStatus.OnRoll);
    }
}
