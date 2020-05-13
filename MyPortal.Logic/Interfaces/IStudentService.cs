using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Requests.Student;

namespace MyPortal.Logic.Interfaces
{
    public interface IStudentService : IService
    {
        Task<IEnumerable<StudentModel>> Get(StudentSearchParams searchParams);

        Task<StudentModel> GetById(Guid studentId);

        Lookup GetSearchFilters();
    }
}
