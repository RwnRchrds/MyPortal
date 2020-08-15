using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces
{
    public interface IStudentGroupService : IService
    {
        Task<IEnumerable<StudentModel>> GetStudents(Guid groupId);
    }
}
