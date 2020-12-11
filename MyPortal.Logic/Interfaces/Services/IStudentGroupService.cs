using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IStudentGroupService : IService
    {
        Task<IEnumerable<StudentModel>> GetStudents(Guid groupId);
    }
}
