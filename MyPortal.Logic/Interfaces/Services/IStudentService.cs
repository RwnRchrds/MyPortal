using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
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
